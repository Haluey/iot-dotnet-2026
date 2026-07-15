using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MahApps.Metro.Controls.Dialogs;
using MySqlConnector;
using System.Collections.ObjectModel;
using System.Data;
using WpfMvvm02.Helpers;
using WpfMvvm02.Models;

namespace WpfMvvm02.ViewModels {
    public partial class BookViewModel : ObservableObject {

        private readonly DatabaseHelper _helper;

        private readonly IDialogCoordinator _coordinator;

        public ObservableCollection<Division> Divisions { get; set; }

        // null 상태(X) -> 객체 생성된 상태로
        public ObservableCollection<Book> Books { get; set; } = new ObservableCollection<Book>();

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(DeleteCommand))]     // 변경알림
        private Book selectedBook;

        public BookViewModel(IDialogCoordinator coordinator) {
            _coordinator = coordinator;
            _helper = new DatabaseHelper();

            LoadComboFromDb();
            LoadDataFromDb();

            SelectedBook = CreateEmptyBook();
        }

        private void LoadComboFromDb() {
            try {
                Divisions = new ObservableCollection<Division>();

                string query = "SELECT div_code, div_name FROM division";
                var result = _helper.Select(query).AsEnumerable().ToList();     // 데이터 넘어옴

                foreach (DataRow row in result) {
                    Division division = new Division {
                        DivCode = row["div_code"].ToString(),
                        DivName = row["div_name"].ToString()
                    };

                    Divisions.Add(division);
                }
            }
            catch (Exception ex) {
                _coordinator.ShowMessageAsync(this, "조회오류", $"DB조회 오류 발생 : {ex.Message}");
            }
        }

        private void LoadDataFromDb() {
            try {
                Books.Clear();  // 책 리스트를 전부 초기화
                string query = @"SELECT b.book_idx, b.author, 
	                                    b.div_code, d.div_name,
	                                    b.book_name, b.release_dt,
	                                    b.isbn, b.price
                                   FROM books b
                                  INNER JOIN division d
                                     ON b.div_code = d.div_code
                                  ORDER BY b.book_idx ASC";

                var result = _helper.Select(query).AsEnumerable().ToList();

                foreach (DataRow row in result) {
                    Book book = new Book {
                        BookIdx = Convert.ToInt32(row["book_idx"]),
                        Author = row["author"].ToString(),
                        DivCode = row["div_code"].ToString(),
                        DivName = row["div_name"].ToString(),
                        BookName = row["book_name"].ToString(),
                        ReleaseDt = Convert.ToDateTime(row["release_dt"]),
                        Isbn = row["isbn"].ToString(),
                        Price = Convert.ToDecimal(row["price"])
                    };

                    Books.Add(book);
                }
            }
            catch (Exception ex) {
                _coordinator.ShowMessageAsync(this, "조회오류", $"DB조회 오류 발생 : {ex.Message}");
            }
        }

        private void UpdateBook() { // @""이거 일때는 {}이거를 {{}}이렇게 작성해야 함
            string query = """
                            UPDATE books
                               SET author=@author
                                 , div_code=@div_code
                                 , book_name=@book_name
                                 , release_dt=@release_dt
                                 , isbn=@isbn
                                 , price=@price
                             WHERE book_idx=@book_idx
                            """;

            _helper.Execute(query,
                new MySqlParameter("@author", SelectedBook.Author),
                new MySqlParameter("@div_code", SelectedBook.DivCode),
                new MySqlParameter("@book_name", SelectedBook.BookName),
                new MySqlParameter("@release_dt", SelectedBook.ReleaseDt.ToString("yyyy-MM-dd")),
                new MySqlParameter("@isbn", SelectedBook.Isbn),
                new MySqlParameter("@price", SelectedBook.Price),
                new MySqlParameter("@book_idx", SelectedBook.BookIdx)
                );
        }

        private Book CreateEmptyBook() {
            return new Book {
                BookIdx = 0,
                Author = string.Empty,
                DivCode = string.Empty,
                DivName = string.Empty,
                BookName = string.Empty,
                Isbn = string.Empty,
                ReleaseDt = DateTime.Today,
                Price = 0
            };
        }

        private void InsertBook() {
            string query = @"
                            INSERT INTO books
		                          ( author
	                              , div_code
	                              , book_name
	                              , release_dt
	                              , isbn
	                              , price)
                            VALUES
                                 ( @author
                                 , @div_code
                                 , @book_name
                                 , @release_dt
                                 , @isbn
                                 , @price)
                            ";

            _helper.Execute(query,
                new MySqlParameter("@author", SelectedBook.Author),
                new MySqlParameter("@div_code", SelectedBook.DivCode),
                new MySqlParameter("@book_name", SelectedBook.BookName),
                new MySqlParameter("@release_dt", SelectedBook.ReleaseDt.ToString("yyyy-MM-dd")),
                new MySqlParameter("@isbn", SelectedBook.Isbn),
                new MySqlParameter("@price", SelectedBook.Price)
                );
        }

        public bool CanDelete() {
            return SelectedBook is { BookIdx: > 0 };
        }

        // 입력검증
        private bool ValidationBook() {
            var result = true;
            var message = string.Empty;

            if (string.IsNullOrWhiteSpace(SelectedBook.DivCode)) {
                message += "책장르를 선택하세요.\n";
                result = false;

                //_coordinator.ShowMessageAsync(this, "입력확인", "책장르를 선택하세요.");
                //return false;
            }

            if (string.IsNullOrWhiteSpace(SelectedBook.BookName)) {
                message += "책제목을 입력하세요.\n";
                result = false;

                //_coordinator.ShowMessageAsync(this, "입력확인", "책제목을 입력하세요.");
                //return false;
            }

            if (string.IsNullOrWhiteSpace(SelectedBook.Author)) {
                message += "저자를 입력하세요\n";
                result = false;

                //_coordinator.ShowMessageAsync(this, "입력확인", "저자를 입력하세요.");
                //return false;
            }

            if (SelectedBook.Price <= 0) {
                message += "가격은 0원 이상이어야 합니다.\n";
                result = false;

                //_coordinator.ShowMessageAsync(this, "입력확인", "가격은 0원 이상이어야 합니다.");
                //return false;
            }

            if (!result) {
                _coordinator.ShowMessageAsync(this, "입력확인", message);
            }

            return result;
        }

        #region 'Command 명령 영역'

        [RelayCommand]
        public void Reset() {
            SelectedBook = CreateEmptyBook();
        }

        [RelayCommand]
        public async Task SaveAsync() {

            if (!ValidationBook()) return;

            try {
                if (SelectedBook.BookIdx == 0) {    // 신규
                    InsertBook();
                }
                else {  // 수정
                    UpdateBook();
                }

                await _coordinator.ShowMessageAsync(this, "저장 완료", "도서 정보가 저장되었습니다.");

                LoadDataFromDb();
                SelectedBook = CreateEmptyBook();
            }
            catch (Exception ex) {
                await _coordinator.ShowMessageAsync(this, "저장오류", $"도서 저장 중 오류 발생 : {ex.Message}");
            }
        }

        [RelayCommand(CanExecute = nameof(CanDelete))]
        public async Task DeleteAsync() {
            // 삭제버튼 활성화 토글이 되면 아래 로직은 필요없음
            //if (SelectedBook.BookIdx == 0) {
            //    await _coordinator.ShowMessageAsync(this, "삭제확인", "삭제할 도서를 선택하세요.");
            //    return;
            //}

            var result = await _coordinator.ShowMessageAsync(this, "삭제확인",
                                            $"'{SelectedBook.BookName}' 도서를 삭제하시겠습니까?",
                                            MessageDialogStyle.AffirmativeAndNegative,
                                            new MetroDialogSettings {
                                                AffirmativeButtonText = "삭제",   // OK 대신
                                                NegativeButtonText = "취소"   // Cancel 대신
                                            });

            if (result != MessageDialogResult.Affirmative) return;

            // 삭제로직 처리
            try {
                string query = @"DELETE FROM books WHERE book_idx = @book_idx";

                _helper.Execute(query, new MySqlParameter("@book_idx", SelectedBook.BookIdx));

                await _coordinator.ShowMessageAsync(this, "삭제완료", "도서정보가 삭제되었습니다.");

                LoadDataFromDb();
                SelectedBook = CreateEmptyBook();
            }
            catch (Exception ex) {
                await _coordinator.ShowMessageAsync(this, "삭제오류", $"도서 삭제 중 오류 발생 : {ex.Message}");
            }
        }

        #endregion

    }

}
