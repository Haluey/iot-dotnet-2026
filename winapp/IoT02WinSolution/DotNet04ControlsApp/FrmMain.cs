using System.Drawing;

namespace DotNet04ControlsApp
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        // 폼 로드 이벤트핸들러
        private void FrmMain_Load(object sender, EventArgs e)
        {
            var Fonts = FontFamily.Families;    // OS에 설치된 폰트리스트
            foreach (var font in Fonts)
            {
                CboFonts.Items.Add(font.Name);
            }

            TxtResult.Text = "현재 글씨체 / Fonts";
            PrgStatus.Value = 0;    // 0으로 초기화
        }

        private void CboFonts_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeFontStyle();
        }

        private void ChkBold_CheckedChanged(object sender, EventArgs e)
        {
            ChangeFontStyle();
        }

        private void ChkItalic_CheckedChanged(object sender, EventArgs e)
        {
            ChangeFontStyle();
        }

        // 폰트 글자체, 굵게, 이탤릭
        private void ChangeFontStyle()
        {
            if (CboFonts.SelectedIndex <= 0)
            {
                return;     // 콤보박스 아무것도 선택 안됨 
            }

            FontStyle style = FontStyle.Regular;    // 처음에는 기본 글자

            if (ChkBold.Checked)
            {
                style |= FontStyle.Bold;    // 볼드체 변경
            }

            if (ChkItalic.Checked)
            {
                style |= FontStyle.Italic;
            }

            TxtResult.Font = new Font(CboFonts.SelectedItem as string, 10, style);
        }

        // 모달 버튼클릭 이벤트핸들러
        // 모달창이 닫히기 전엔 부모창을 제어할 수 없음
        private void BtnModal_Click(object sender, EventArgs e)
        {
            FrmSub frmSub = new FrmSub();
            frmSub.Text = "모달창";    // 런타임 시 속성값 변경
            frmSub.BackColor = Color.OrangeRed;
            frmSub.StartPosition = FormStartPosition.CenterParent;  // 런타임 시 변경
            frmSub.ShowDialog();    // 모달창으로 오픈
        }

        // 모달리스 버튼클릭 이벤트핸들러
        // 모달리스창과 관계없이 부모창을 제어할 수 있음
        private void BtnModaless_Click(object sender, EventArgs e)
        {
            FrmSub frmSub = new FrmSub();
            frmSub.Text = "모달리스창";
            frmSub.BackColor = Color.GreenYellow;
            //frmSub.StartPosition = FormStartPosition.CenterParent;  // 적용안됨. 모달창과 달리 부모관계가 정확하지 않기 때문에 
            // 모달리스 창은 위치를 직접 계산해야됨
            frmSub.StartPosition = FormStartPosition.Manual;
            frmSub.Location = new Point(
                this.Location.X + (this.Width - frmSub.Width) / 2,
                this.Location.Y + (this.Height - frmSub.Height) / 2
            );
            frmSub.Show(this);  // this -> FrmMain
        }

        private void BtnMsgbox_Click(object sender, EventArgs e)
        {
            // 기본적인 메시지박스 사용
            // 파라미터 : 메시지, 타이틀, 버튼종류, 아이콘 종류
            MessageBox.Show(TxtResult.Text, "메시지 박스",
                MessageBoxButtons.CancelTryContinue,
                MessageBoxIcon.Error
            );
        }

        // 다이얼로그창 띄우기
        private void BtnDialog_Click(object sender, EventArgs e)
        {
            //DlgOpenFile.ShowDialog(this); // 일반 오픈(잘 사용하지 않음)
            if (DlgOpenFile.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show($"선택한 파일은 {DlgOpenFile.FileName} 입니다.");
            }
        }

        // 트랙바 스크롤변경 이벤트핸들러
        private void TrkStatus_Scroll(object sender, EventArgs e)
        {
            PrgStatus.Value = TrkStatus.Value;
        }

        // 트리뷰 내용을 리스트 뷰에 표현하는 메서드
        private void TreeToList()
        {
            LvwDummy.Items.Clear();
            foreach (TreeNode node in TvwDummy.Nodes)
            {
                TreeToList(node);
            }
        }

        private void TreeToList(TreeNode node)
        {
            LvwDummy.Items.Add(
                new ListViewItem(
                    new String[] { node.Text, node.FullPath.Count(f => f == '\\').ToString() }, 1
                )
            );

            foreach (TreeNode subNode in node.Nodes)
            {
                TreeToList(subNode);    // 재귀호출
            }
        }

        private void BtnAddRoot_Click(object sender, EventArgs e)
        {
            var random = new Random();
            // comboBox는 item이지만, treeView는 node
            TvwDummy.Nodes.Add(random.Next().ToString());
            TreeToList();
        }

        private void BtnAddNode_Click(object sender, EventArgs e)
        {
            if (TvwDummy.SelectedNode == null)
            {
                MessageBox.Show("노드를 선택하세요", "경고", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;     // 메서드 탈출
            }

            var random = new Random();
            TreeNode childNode = new TreeNode(random.Next().ToString());
            childNode.ImageIndex = 1;
            TvwDummy.SelectedNode.Nodes.Add(childNode);
            TvwDummy.ExpandAll();   // 하위노드 전부 확장. 추가된 노드 클릭 안해도 보임

            TreeToList();
        }

        private void BtnLoadImg_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "이미지 열기";
            dlg.Filter = "Image Files(*.bmp;*.png;*.jpg)|*.bmp;*.png;*.jpg";

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                PicImage.Image = Bitmap.FromFile(dlg.FileName);
                PicImage.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        // 픽쳐박스 컨트롤클릭 이벤트핸들러
        private void PicImage_Click(object sender, EventArgs e)
        {
            if (PicImage.SizeMode == PictureBoxSizeMode.CenterImage)
            {
                PicImage.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else
            {
                PicImage.SizeMode = PictureBoxSizeMode.CenterImage;
            }
        }
    }
}
