using MaterialSkin.Controls;

namespace DotNet06DbBooksApp
{
    public partial class FrmBook : MaterialForm
    {
        DatabaseHelper dbHelper;
        public FrmBook()
        {
            InitializeComponent();
        }

        private void FrmBook_Load(object sender, EventArgs e)
        {
            dbHelper = new DatabaseHelper();    // 객체생성
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            string query = "SELECT book_idx, author, div_code, book_name, release_dt, isbn, price" +
                        "   FROM books";
            DgvBooks.DataSource = dbHelper.Select(query);
        }
    }
}
