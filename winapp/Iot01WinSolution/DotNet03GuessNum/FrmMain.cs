namespace DotNet03GuessNum
{
    public partial class FrmMain : Form
    {
        private int findNumber = 0; // 맞힐 숫자
        private int chance = 0; // 기회

        public FrmMain()
        {
            InitializeComponent();
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            findNumber = rand.Next(1, 31);  // 1 ~ 30 사이 랜덤수
            chance = 10;    // 10번 기회
            LblDisplay.Text = "맞힐 숫자를 입력하세요.";
        }
    }
}
