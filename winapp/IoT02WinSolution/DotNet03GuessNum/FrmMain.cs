namespace DotNet03GuessNum
{
    public partial class FrmMain : Form
    {
        private int findNumber = 0; // 변수만 소문자로 시작
        private int chance = 0;

        // 생성자는 되도록 손대지 않음
        public FrmMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// BtnStart 버튼클릭 이벤트핸들러 메서드
        /// </summary>
        /// <param name="sender">이벤트를 보내준 객체</param>
        /// <param name="e">버튼 자체에 필요한 매개변수 속성</param>
        private void BtnStart_Click(object sender, EventArgs e)
        {
            var rand = new Random();
            findNumber = rand.Next(1, 30 + 1);  // 1 ~ 30까지의 수 중에서 랜덤
            chance = 10;    // 맞출기회 10번
            LblDisplay.Text = "맞힐 숫자를 입력하세요.";

            MessageBox.Show("게임을 시작하지", "게임시작", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 텍스트 박스에 입력한 숫자가 정답인지 체크하는 기능
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCheck_Click(object sender, EventArgs e)
        {
            int inputNum = int.Parse(TxtNum.Text);  // 정수형으로 파싱해서 타입 바꾸기

            if (inputNum == findNumber)
            {
                LblDisplay.Text = "맞히셨습니다!";
                MessageBox.Show("축하드립니다!", "게임종료", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                chance--;
                string strVal;
                if (inputNum > findNumber)
                {
                    strVal = "큼";
                }
                else
                {
                    strVal = "작음";
                }

                LblDisplay.Text = $"기회 {chance}번. 찾는 수보다 {strVal}";
            }

            if (chance <= 0)
            {
                LblDisplay.Text = "실패했습니다!";
                MessageBox.Show("실패했습니다!", "게임종료", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
