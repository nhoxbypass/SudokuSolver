using SudokuSolver.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuSolver
{
    public partial class Form1 : Form
    {
        //Khai báo biến
        Sudoku sdk;

        private const int Rows = 9, Cols = 9;
        private int[,] _Arr;
        private int[,] tmpArr;
        private int _Mode;
        private bool _isReady;
        public int timer_Min, timer_Sec;
        public int numbOfButton;

        private Graphics grs;
        private Pen pen;
        private SolidBrush sb;
        private Color main_BG_Color, main_Fore_Color, HL_BG_Color, HL_Fore_Color;
        private Image img;
       
        public int Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }
        public int[,] Arr {
            get { return _Arr; }
            set { _Arr = value; }
        }
        public bool isReady
        {
            get { return _isReady;}
            set {_isReady = value;}
        }
        
        public Form1()
        {
            InitializeComponent();

            //Instance của lớp Sudoku
            sdk = new Sudoku();

            //Khởi tạo đôi tượng đồ họa
            grs = panel1.CreateGraphics();
            main_BG_Color = Color.PaleTurquoise;
            HL_BG_Color = Color.LightSkyBlue;
            main_Fore_Color = Color.Navy;
            HL_Fore_Color = Color.Firebrick;
            sb = new SolidBrush(HL_BG_Color);
            pen = new Pen(Color.Black);
            img = Image.FromFile(@"G:\Coding\C#\Proj\SudokuSolver\SudokuSolver\Resources\logo.png");
            pictureBox1.Image = img;

           
            
            //Mode = 0 is unchosen, 1 is Solver, 2,3,4 is play Easy/Normal/Hard
            _Mode = 0;

            //Khởi tạo hướng dẫn sử dụng
            lbl_GameGuide.Text = "Chương trình gồm hai chế độ:\n\n"
                + " - Chơi game:\n"
            + " Điền vào những ô trống những\n"
            + "con số thích hợp, theo quy luật\n"
            + "đơn giản sau:\n"
 + "+ Các ô ở mỗi hàng (ngang) phải\n"
            + "có đủ các số từ 1 - 9, không cần\n"
            + "theo thứ tự.\n"
 + "+ Các ô ở mỗi cột (dọc) phải có\n"
            + "đủ các số từ 1 -9, không cần\n"
            + "theo thứ tự.\n"
 + "+ Mỗi miền 3x3, được viền đậm,\n"
            + "phải có đủ các số từ 1 đến 9.\n"

 + "+ Game bắt đầu với lưới Sudoku,\n"
            + "trong đó một số ô đã cho sẵn các\n"
            + "con số đúng. Bạn phải suy luận\n"
            + "để tìm ra những con số\n"
            + "trong các ô trống còn lại.\n\n\n"
 + " - Phần mềm giải Sudoku:\n"
            + "+ Chọn chế độ Solver và nhập\n"
            + "vào lưới Sudoku đề bài\n"
            + "+ Có thể xóa toàn bộ lưới Sudoku\n"
            + "đã nhập bằng nút Clear\n"
            + "+ Sau đó ấn Solve.\n"
 + "+ Phần mềm sẽ tự giải và đưa ra\n"
            + "đáp án các ô còn lại!\n\n\n";


            //Additonal
            numbOfButton = 0; //Số của nút bấm khi nhập lưới sudoku
            _isReady = false;
            timer2.Start();  
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            sdk.DrawBoard(grs, pen);
        }

        //Load ưng dụng và 1 sô chưc năng khác
        #region LoadApp
        //Câp nhật thêm đề sudoku mới tại đây
        private void SelectInputSudoku()
        {
            Random rnd = new Random();
            int randSelect = rnd.Next(1, 5);
            if(_Mode == 2)
            {
                switch(randSelect)
                {
                    case 1:
                        _Arr = new int[9, 9]
                        {
                            {0,0,0,0,0,0,0,0,0},{0,0,0,0,6,0,0,0,0},{0,0,0,0,0,0,0,0,0}, {0,0,0,9,0,0,0,0,0}, {0,7,0,0,0,0,0,4,0}, {0,0,0,0,0,1,0,0,0}, {0,0,0,0,0,0,0,0,0}, {0,0,0,0,3,0,0,0,0}, {0,0,0,0,0,0,0,0,0}
                        };
                        break;
                    case 2:
                         _Arr = new int[9, 9]
                        {
                            {0,6,0,1,0,4,0,0,0},{0,1,8,5,0,0,0,7,9},{0,0,0,0,2,9,0,1,0},{7,0,5,0,0,0,0,8,2},{0,0,4,0,0,0,3,0,0},{3,8,0,0,0,0,5,0,1},{0,5,0,4,7,0,0,0,0},{6,7,0,0,0,5,9,2,0},{0,0,0,9,0,6,0,5,0}
                        };
                        break;
                    case 3:
                        _Arr = new int[9, 9]
                        {
                            {2,0,0,9,5,0,6,0,1},{0,5,0,7,0,0,0,2,0},{0,0,7,0,0,0,5,0,9},{3,8,0,4,1,0,0,0,0},{6,0,0,8,0,9,0,0,5},{0,0,0,0,2,6,0,4,8},{1,0,5,0,0,0,7,0,0},{0,3,0,0,0,7,0,8,0},{7,0,8,0,6,3,0,0,2}
                        };
                        break;
                    case 4:
                        _Arr = new int[9, 9]
                        {
                            {7,0,1,0,0,0,0,0,0},{6,0,0,4,8,1,0,0,0},{0,8,4,0,7,2,0,0,5},{0,7,0,1,0,3,9,0,4},{3,0,0,8,0,4,0,0,6},{4,0,9,5,0,7,0,8,0},{2,0,0,9,4,0,8,3,0},{0,0,0,7,3,8,0,0,2},{0,0,0,0,0,0,6,0,9}
                        };
                        break;
                    case 5:
                        _Arr = new int[9, 9]
                        {
                            {0,9,1,0,0,0,0,0,0},{0,3,0,0,0,0,0,7,9},{0,0,2,0,6,1,4,0,8},{0,0,7,1,0,6,0,0,0},{0,0,9,0,0,0,3,0,0},{0,0,0,3,0,4,2,0,0},{2,0,3,4,5,0,6,0,0},{1,7,0,0,0,0,0,2,0},{0,0,0,0,0,0,7,4,0}
                        };
                        break;
                }

            }
            else if (_Mode == 3)
            {
                switch (randSelect)
                {
                    case 1:
                        _Arr = new int[9, 9]
                        {
                           {0,7,0,3,0,0,4,5,6},{3,0,0,0,0,0,0,0,0},{0,0,5,0,0,7,0,0,0},{0,5,0,0,1,0,0,4,3},{2,0,0,5,7,3,0,0,1},{1,6,0,0,2,0,0,8,0},{0,0,0,6,0,0,3,0,0},{0,0,0,0,0,0,0,0,5},{5,8,6,0,0,2,0,9,0}
                        };
                        break;

                    case 2:
                        _Arr = new int[9, 9]
                        { 
                        {0,0,0,7,0,0,9,0,0},{3,0,0,0,9,0,0,0,4},{0,0,0,0,2,6,0,7,0},{8,0,2,0,0,0,4,0,0},{0,6,0,5,0,8,0,9,0},{0,0,7,0,0,0,6,0,1},{0,2,0,3,5,0,0,0,0},{9,0,0,0,6,0,0,0,5},{0,0,4,0,0,2,0,0,0}
                        };
                        break;

                    case 3:
                        _Arr = new int[9, 9]
                        {
                           {0,0,5,0,0,3,0,6,0},{0,8,0,5,1,0,0,0,3},{0,7,0,0,0,0,0,0,5},{5,0,0,0,0,9,0,0,0},{3,6,0,7,5,8,0,2,4},{0,0,0,3,0,0,0,0,6},{8,0,0,0,0,0,0,7,0},{9,0,0,0,3,6,0,4,0},{0,2,0,8,0,0,5,0,0}
                        };
                        break;

                    case 4:
                        _Arr = new int[9, 9]
                        {
                           {0,8,0,5,0,0,0,4,0},{5,2,0,0,0,7,1,3,0},{0,0,0,0,0,0,0,0,5},{0,4,0,0,8,0,9,7,0},{0,0,9,0,7,0,8,0,0},{0,1,8,0,3,0,0,6,0},{6,0,0,0,0,0,0,0,0},{0,5,1,4,0,0,0,2,7},{0,9,0,0,0,2,0,8,0}
                        };
                        break;

                    case 5:
                        _Arr = new int[9, 9]
                        {
                           {8,0,0,1,0,4,2,0,0},{0,0,6,5,0,7,4,0,0},{0,0,0,0,3,0,0,6,0},{0,2,8,0,0,0,0,0,0},{6,0,4,0,2,0,8,0,7},{0,0,0,0,0,0,3,9,0},{0,6,0,0,1,0,0,0,0},{0,0,5,6,0,3,7,0,0},{0,0,2,7,0,8,0,0,3}
                        };
                        break;
                }
            }
            else if (_Mode == 4)
            {

            }
        }
        public void LoadData()
        {
            tmpArr = new int[9, 9]
                { {3,5,1,7,9,8,2,4,6},{2,9,7,6,1,4,8,3,5},{8,6,4,5,2,3,1,7,9},{6,4,5,1,3,2,9,8,7},{0,7,8,4,6,5,3,2,1},{1,0,2,9,8,7,6,5,4},{5,2,0,8,4,1,7,9,3},{4,1,3,0,7,9,5,6,8},{7,8,9,3,5,6,0,1,2}
                };
            
            SelectInputSudoku();
        }

        //In dữ liệu ra lưới Sudoku
        public void ShowData()
        {
            int i = 0, j = 0;
            button1.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button2.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button3.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button4.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button5.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button6.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button7.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button8.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button9.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;

            i = 1; j = 0;
            button10.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button11.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button12.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button13.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button14.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button15.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button16.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button17.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button18.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;

            i = 2; j = 0;
            button19.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button20.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button21.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button22.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button23.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button24.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button25.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button26.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button27.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;

            i = 3; j = 0;
            button28.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button29.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button30.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button31.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button32.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button33.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button34.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button35.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button36.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;

            i = 4; j = 0;
            button37.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button38.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button39.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button40.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button41.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button42.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button43.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button44.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button45.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;

            i = 5; j = 0;
            button46.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button47.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button48.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button49.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button50.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button51.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button52.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button53.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button54.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;

            i = 6; j = 0;
            button55.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button56.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button57.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button58.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button59.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button60.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button61.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button62.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button63.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;

            i = 7; j = 0;
            button64.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button65.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button66.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button67.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button68.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button69.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button70.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button71.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button72.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;

            i = 8; j = 0;
            button73.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button74.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button75.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button76.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button77.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button78.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button79.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button80.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
            button81.Text = _Arr[i, j] != 0 ? _Arr[i, j].ToString() : ""; j++;
        }

        //Highlight các ô đề bài
        public void Highlighter()
        {
            int NumberofButton = 0;
            for(int i = 0; i < 9; i++)
            {
                for(int j = 0; j<9; j++)
                {
                    if(_Arr[i,j] != 0)
                    {
                        //Tính STT của nút theo dòng và cột, loại bỏ các nút = 0
                        NumberofButton = i * 9 + j + 1;
                        switch(NumberofButton)
                        {
                            case 1:
                                button1.BackColor = HL_BG_Color;
                                button1.ForeColor =  HL_Fore_Color;
                                break;
                            case 2:
                                button2.BackColor = HL_BG_Color;
                                button2.ForeColor =  HL_Fore_Color;
                                break;
                            case 3:
                                button3.BackColor = HL_BG_Color;
                                button3.ForeColor =  HL_Fore_Color;
                                break;
                            case 4:
                                button4.BackColor = HL_BG_Color;
                                button4.ForeColor =  HL_Fore_Color;
                                break;
                            case 5:
                                button5.BackColor = HL_BG_Color;
                                button5.ForeColor =  HL_Fore_Color;
                                break;
                            case 6:
                                button6.BackColor = HL_BG_Color;
                                button6.ForeColor =  HL_Fore_Color;
                                break;
                            case 7:
                                button7.BackColor = HL_BG_Color;
                                button7.ForeColor =  HL_Fore_Color;
                                break;
                            case 8:
                                button8.BackColor = HL_BG_Color;
                                button8.ForeColor =  HL_Fore_Color;
                                break;
                            case 9:
                                button9.BackColor = HL_BG_Color;
                                button9.ForeColor =  HL_Fore_Color;
                                break;

                            case 10:
                                button10.BackColor = HL_BG_Color;
                                button10.ForeColor =  HL_Fore_Color;
                                break;
                            case 11:
                                button11.BackColor = HL_BG_Color;
                                button11.ForeColor =  HL_Fore_Color;
                                break;
                            case 12:
                                button12.BackColor = HL_BG_Color;
                                button12.ForeColor =  HL_Fore_Color;
                                break;
                            case 13:
                                button13.BackColor = HL_BG_Color;
                                button13.ForeColor =  HL_Fore_Color;
                                break;
                            case 14:
                                button14.BackColor = HL_BG_Color;
                                button14.ForeColor =  HL_Fore_Color;
                                break;
                            case 15:
                                button15.BackColor = HL_BG_Color;
                                button15.ForeColor =  HL_Fore_Color;
                                break;
                            case 16:
                                button16.BackColor = HL_BG_Color;
                                button16.ForeColor =  HL_Fore_Color;
                                break;
                            case 17:
                                button17.BackColor = HL_BG_Color;
                                button17.ForeColor =  HL_Fore_Color;
                                break;
                            case 18:
                                button18.BackColor = HL_BG_Color;
                                button18.ForeColor =  HL_Fore_Color;
                                break;

                            case 19:
                                button19.BackColor = HL_BG_Color;
                                button19.ForeColor =  HL_Fore_Color;
                                break;
                            case 20:
                                button20.BackColor = HL_BG_Color;
                                button20.ForeColor =  HL_Fore_Color;
                                break;
                            case 21:
                                button21.BackColor = HL_BG_Color;
                                button21.ForeColor =  HL_Fore_Color;
                                break;
                            case 22:
                                button22.BackColor = HL_BG_Color;
                                button22.ForeColor =  HL_Fore_Color;
                                break;
                            case 23:
                                button23.BackColor = HL_BG_Color;
                                button23.ForeColor =  HL_Fore_Color;
                                break;
                            case 24:
                                button24.BackColor = HL_BG_Color;
                                button24.ForeColor =  HL_Fore_Color;
                                break;
                            case 25:
                                button25.BackColor = HL_BG_Color;
                                button25.ForeColor =  HL_Fore_Color;
                                break;
                            case 26:
                                button26.BackColor = HL_BG_Color;
                                button26.ForeColor =  HL_Fore_Color;
                                break;
                            case 27:
                                button27.BackColor = HL_BG_Color;
                                button27.ForeColor =  HL_Fore_Color;
                                break;

                            case 28:
                                button28.BackColor = HL_BG_Color;
                                button28.ForeColor =  HL_Fore_Color;
                                break;
                            case 29:
                                button29.BackColor = HL_BG_Color;
                                button29.ForeColor =  HL_Fore_Color;
                                break;
                            case 30:
                                button30.BackColor = HL_BG_Color;
                                button30.ForeColor =  HL_Fore_Color;
                                break;
                            case 31:
                                button31.BackColor = HL_BG_Color;
                                button31.ForeColor =  HL_Fore_Color;
                                break;
                            case 32:
                                button32.BackColor = HL_BG_Color;
                                button32.ForeColor =  HL_Fore_Color;
                                break;
                            case 33:
                                button33.BackColor = HL_BG_Color;
                                button33.ForeColor =  HL_Fore_Color;
                                break;
                            case 34:
                                button34.BackColor = HL_BG_Color;
                                button34.ForeColor =  HL_Fore_Color;
                                break;
                            case 35:
                                button35.BackColor = HL_BG_Color;
                                button35.ForeColor =  HL_Fore_Color;
                                break;
                            case 36:
                                button36.BackColor = HL_BG_Color;
                                button36.ForeColor =  HL_Fore_Color;
                                break;

                            case 37:
                                button37.BackColor = HL_BG_Color;
                                button37.ForeColor =  HL_Fore_Color;
                                break;
                            case 38:
                                button38.BackColor = HL_BG_Color;
                                button38.ForeColor =  HL_Fore_Color;
                                break;
                            case 39:
                                button39.BackColor = HL_BG_Color;
                                button39.ForeColor =  HL_Fore_Color;
                                break;
                            case 40:
                                button40.BackColor = HL_BG_Color;
                                button40.ForeColor =  HL_Fore_Color;
                                break;
                            case 41:
                                button41.BackColor = HL_BG_Color;
                                button41.ForeColor =  HL_Fore_Color;
                                break;
                            case 42:
                                button42.BackColor = HL_BG_Color;
                                button42.ForeColor =  HL_Fore_Color;
                                break;
                            case 43:
                                button43.BackColor = HL_BG_Color;
                                button43.ForeColor =  HL_Fore_Color;
                                break;
                            case 44:
                                button44.BackColor = HL_BG_Color;
                                button44.ForeColor =  HL_Fore_Color;
                                break;
                            case 45:
                                button45.BackColor = HL_BG_Color;
                                button45.ForeColor =  HL_Fore_Color;
                                break;

                            case 46:
                                button46.BackColor = HL_BG_Color;
                                button46.ForeColor =  HL_Fore_Color;
                                break;
                            case 47:
                                button47.BackColor = HL_BG_Color;
                                button47.ForeColor =  HL_Fore_Color;
                                break;
                            case 48:
                                button48.BackColor = HL_BG_Color;
                                button48.ForeColor =  HL_Fore_Color;
                                break;
                            case 49:
                                button49.BackColor = HL_BG_Color;
                                button49.ForeColor =  HL_Fore_Color;
                                break;
                            case 50:
                                button50.BackColor = HL_BG_Color;
                                button50.ForeColor =  HL_Fore_Color;
                                break;
                            case 51:
                                button51.BackColor = HL_BG_Color;
                                button51.ForeColor =  HL_Fore_Color;
                                break;
                            case 52:
                                button52.BackColor = HL_BG_Color;
                                button52.ForeColor =  HL_Fore_Color;
                                break;
                            case 53:
                                button53.BackColor = HL_BG_Color;
                                button53.ForeColor =  HL_Fore_Color;
                                break;
                            case 54:
                                button54.BackColor = HL_BG_Color;
                                button54.ForeColor =  HL_Fore_Color;
                                break;

                            case 55:
                                button55.BackColor = HL_BG_Color;
                                button55.ForeColor =  HL_Fore_Color;
                                break;
                            case 56:
                                button56.BackColor = HL_BG_Color;
                                button56.ForeColor =  HL_Fore_Color;
                                break;
                            case 57:
                                button57.BackColor = HL_BG_Color;
                                button57.ForeColor =  HL_Fore_Color;
                                break;
                            case 58:
                                button58.BackColor = HL_BG_Color;
                                button58.ForeColor =  HL_Fore_Color;
                                break;
                            case 59:
                                button59.BackColor = HL_BG_Color;
                                button59.ForeColor =  HL_Fore_Color;
                                break;
                            case 60:
                                button60.BackColor = HL_BG_Color;
                                button60.ForeColor =  HL_Fore_Color;
                                break;
                            case 61:
                                button61.BackColor = HL_BG_Color;
                                button61.ForeColor =  HL_Fore_Color;
                                break;
                            case 62:
                                button62.BackColor = HL_BG_Color;
                                button62.ForeColor =  HL_Fore_Color;
                                break;
                            case 63:
                                button63.BackColor = HL_BG_Color;
                                button63.ForeColor =  HL_Fore_Color;
                                break;

                            case 64:
                                button64.BackColor = HL_BG_Color;
                                button64.ForeColor =  HL_Fore_Color;
                                break;
                            case 65:
                                button65.BackColor = HL_BG_Color;
                                button65.ForeColor =  HL_Fore_Color;
                                break;
                            case 66:
                                button66.BackColor = HL_BG_Color;
                                button66.ForeColor =  HL_Fore_Color;
                                break;
                            case 67:
                                button67.BackColor = HL_BG_Color;
                                button67.ForeColor =  HL_Fore_Color;
                                break;
                            case 68:
                                button68.BackColor = HL_BG_Color;
                                button68.ForeColor =  HL_Fore_Color;
                                break;
                            case 69:
                                button69.BackColor = HL_BG_Color;
                                button69.ForeColor =  HL_Fore_Color;
                                break;
                            case 70:
                                button70.BackColor = HL_BG_Color;
                                button70.ForeColor =  HL_Fore_Color;
                                break;
                            case 71:
                                button71.BackColor = HL_BG_Color;
                                button71.ForeColor =  HL_Fore_Color;
                                break;
                            case 72:
                                button72.BackColor = HL_BG_Color;
                                button72.ForeColor =  HL_Fore_Color;
                                break;

                            case 73:
                                button73.BackColor = HL_BG_Color;
                                button73.ForeColor =  HL_Fore_Color;
                                break;
                            case 74:
                                button74.BackColor = HL_BG_Color;
                                button74.ForeColor =  HL_Fore_Color;
                                break;
                            case 75:
                                button75.BackColor = HL_BG_Color;
                                button75.ForeColor =  HL_Fore_Color;
                                break;
                            case 76:
                                button76.BackColor = HL_BG_Color;
                                button76.ForeColor =  HL_Fore_Color;
                                break;
                            case 77:
                                button77.BackColor = HL_BG_Color;
                                button77.ForeColor =  HL_Fore_Color;
                                break;
                            case 78:
                                button78.BackColor = HL_BG_Color;
                                button78.ForeColor =  HL_Fore_Color;
                                break;
                            case 79:
                                button79.BackColor = HL_BG_Color;
                                button79.ForeColor =  HL_Fore_Color;
                                break;
                            case 80:
                                button80.BackColor = HL_BG_Color;
                                button80.ForeColor =  HL_Fore_Color;
                                break;
                            case 81:
                                button81.BackColor = HL_BG_Color;
                                button81.ForeColor =  HL_Fore_Color;
                                break;




                        }
                    }
                }
            }
        }

        //Reset giao diện sudoku
        public void ResetBoard()
        {
            button1.BackColor = main_BG_Color;
            button2.BackColor = main_BG_Color;
            button3.BackColor = main_BG_Color;
            button4.BackColor = main_BG_Color;
            button5.BackColor = main_BG_Color;
            button6.BackColor = main_BG_Color;
            button7.BackColor = main_BG_Color;
            button8.BackColor = main_BG_Color;
            button9.BackColor = main_BG_Color;

            button10.BackColor = main_BG_Color;
            button11.BackColor = main_BG_Color;
            button12.BackColor = main_BG_Color;
            button13.BackColor = main_BG_Color;
            button14.BackColor = main_BG_Color;
            button15.BackColor = main_BG_Color;
            button16.BackColor = main_BG_Color;
            button17.BackColor = main_BG_Color;
            button18.BackColor = main_BG_Color;

            button19.BackColor = main_BG_Color;
            button20.BackColor = main_BG_Color;
            button21.BackColor = main_BG_Color;
            button22.BackColor = main_BG_Color;
            button23.BackColor = main_BG_Color;
            button24.BackColor = main_BG_Color;
            button25.BackColor = main_BG_Color;
            button26.BackColor = main_BG_Color;
            button27.BackColor = main_BG_Color;

            button28.BackColor = main_BG_Color;
            button29.BackColor = main_BG_Color;
            button30.BackColor = main_BG_Color;
            button31.BackColor = main_BG_Color;
            button32.BackColor = main_BG_Color;
            button33.BackColor = main_BG_Color;
            button34.BackColor = main_BG_Color;
            button35.BackColor = main_BG_Color;
            button36.BackColor = main_BG_Color;

            button37.BackColor = main_BG_Color;
            button38.BackColor = main_BG_Color;
            button39.BackColor = main_BG_Color;
            button40.BackColor = main_BG_Color;
            button41.BackColor = main_BG_Color;
            button42.BackColor = main_BG_Color;
            button43.BackColor = main_BG_Color;
            button44.BackColor = main_BG_Color;
            button45.BackColor = main_BG_Color;

            button46.BackColor = main_BG_Color;
            button47.BackColor = main_BG_Color;
            button48.BackColor = main_BG_Color;
            button49.BackColor = main_BG_Color;
            button50.BackColor = main_BG_Color;
            button51.BackColor = main_BG_Color;
            button52.BackColor = main_BG_Color;
            button53.BackColor = main_BG_Color;
            button54.BackColor = main_BG_Color;

            button55.BackColor = main_BG_Color;
            button56.BackColor = main_BG_Color;
            button57.BackColor = main_BG_Color;
            button58.BackColor = main_BG_Color;
            button59.BackColor = main_BG_Color;
            button60.BackColor = main_BG_Color;
            button61.BackColor = main_BG_Color;
            button62.BackColor = main_BG_Color;
            button63.BackColor = main_BG_Color;


            button64.BackColor = main_BG_Color;
            button65.BackColor = main_BG_Color;
            button66.BackColor = main_BG_Color;
            button67.BackColor = main_BG_Color;
            button68.BackColor = main_BG_Color;
            button69.BackColor = main_BG_Color;
            button70.BackColor = main_BG_Color;
            button71.BackColor = main_BG_Color;
            button72.BackColor = main_BG_Color;

            button73.BackColor = main_BG_Color;
            button74.BackColor = main_BG_Color;
            button75.BackColor = main_BG_Color;
            button76.BackColor = main_BG_Color;
            button77.BackColor = main_BG_Color;
            button78.BackColor = main_BG_Color;
            button79.BackColor = main_BG_Color;
            button80.BackColor = main_BG_Color;
            button81.BackColor = main_BG_Color;


            button1.ForeColor = main_Fore_Color;
            button2.ForeColor = main_Fore_Color;
            button3.ForeColor = main_Fore_Color;
            button4.ForeColor = main_Fore_Color;
            button5.ForeColor = main_Fore_Color;
            button6.ForeColor = main_Fore_Color;
            button7.ForeColor = main_Fore_Color;
            button8.ForeColor = main_Fore_Color;
            button9.ForeColor = main_Fore_Color;

            button10.ForeColor = main_Fore_Color;
            button11.ForeColor = main_Fore_Color;
            button12.ForeColor = main_Fore_Color;
            button13.ForeColor = main_Fore_Color;
            button14.ForeColor = main_Fore_Color;
            button15.ForeColor = main_Fore_Color;
            button16.ForeColor = main_Fore_Color;
            button17.ForeColor = main_Fore_Color;
            button18.ForeColor = main_Fore_Color;

            button19.ForeColor = main_Fore_Color;
            button20.ForeColor = main_Fore_Color;
            button21.ForeColor = main_Fore_Color;
            button22.ForeColor = main_Fore_Color;
            button23.ForeColor = main_Fore_Color;
            button24.ForeColor = main_Fore_Color;
            button25.ForeColor = main_Fore_Color;
            button26.ForeColor = main_Fore_Color;
            button27.ForeColor = main_Fore_Color;

            button28.ForeColor = main_Fore_Color;
            button29.ForeColor = main_Fore_Color;
            button30.ForeColor = main_Fore_Color;
            button31.ForeColor = main_Fore_Color;
            button32.ForeColor = main_Fore_Color;
            button33.ForeColor = main_Fore_Color;
            button34.ForeColor = main_Fore_Color;
            button35.ForeColor = main_Fore_Color;
            button36.ForeColor = main_Fore_Color;

            button37.ForeColor = main_Fore_Color;
            button38.ForeColor = main_Fore_Color;
            button39.ForeColor = main_Fore_Color;
            button40.ForeColor = main_Fore_Color;
            button41.ForeColor = main_Fore_Color;
            button42.ForeColor = main_Fore_Color;
            button43.ForeColor = main_Fore_Color;
            button44.ForeColor = main_Fore_Color;
            button45.ForeColor = main_Fore_Color;

            button46.ForeColor = main_Fore_Color;
            button47.ForeColor = main_Fore_Color;
            button48.ForeColor = main_Fore_Color;
            button49.ForeColor = main_Fore_Color;
            button50.ForeColor = main_Fore_Color;
            button51.ForeColor = main_Fore_Color;
            button52.ForeColor = main_Fore_Color;
            button53.ForeColor = main_Fore_Color;
            button54.ForeColor = main_Fore_Color;

            button55.ForeColor = main_Fore_Color;
            button56.ForeColor = main_Fore_Color;
            button57.ForeColor = main_Fore_Color;
            button58.ForeColor = main_Fore_Color;
            button59.ForeColor = main_Fore_Color;
            button60.ForeColor = main_Fore_Color;
            button61.ForeColor = main_Fore_Color;
            button62.ForeColor = main_Fore_Color;
            button63.ForeColor = main_Fore_Color;


            button64.ForeColor = main_Fore_Color;
            button65.ForeColor = main_Fore_Color;
            button66.ForeColor = main_Fore_Color;
            button67.ForeColor = main_Fore_Color;
            button68.ForeColor = main_Fore_Color;
            button69.ForeColor = main_Fore_Color;
            button70.ForeColor = main_Fore_Color;
            button71.ForeColor = main_Fore_Color;
            button72.ForeColor = main_Fore_Color;

            button73.ForeColor = main_Fore_Color;
            button74.ForeColor = main_Fore_Color;
            button75.ForeColor = main_Fore_Color;
            button76.ForeColor = main_Fore_Color;
            button77.ForeColor = main_Fore_Color;
            button78.ForeColor = main_Fore_Color;
            button79.ForeColor = main_Fore_Color;
            button80.ForeColor = main_Fore_Color;
            button81.ForeColor = main_Fore_Color;

        }
        #endregion


        //Thiết lập các nút của lươí Sudoku
        #region Button 
        private void button1_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 1;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 2;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 3;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 4;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 5;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 6;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 7;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 8;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 9;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 10;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 11;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 12;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 13;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 14;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 15;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 16;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 17;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 18;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 19;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 20;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 21;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 22;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 23;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 24;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button25_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 25;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 26;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button27_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 27;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button28_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 28;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button29_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 29;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button30_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 30;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button31_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 31;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button32_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 32;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button33_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 33;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button34_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 34;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button35_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 35;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button36_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 36;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button37_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 37;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button38_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 38;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button39_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 39;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button40_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 40;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button41_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 41;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button42_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 42;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button43_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 43;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button44_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 44;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button45_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 45;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button46_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 46;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button47_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 47;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button48_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 48;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button49_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 49;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button50_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 50;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button51_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 51;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button52_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 52;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button53_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 53;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button54_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 54;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button55_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 55;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button56_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 56;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button57_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 57;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button58_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 58;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button59_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 59;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button60_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 60;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button61_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 61;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button62_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 62;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button63_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 63;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button64_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 64;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button65_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 65;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button66_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 66;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button67_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 67;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button68_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 68;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button69_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 69;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button70_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 70;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button71_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 71;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button72_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 72;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button73_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 73;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button74_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 74;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button75_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 75;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button76_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 76;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button77_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 77;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button78_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 78;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button79_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 79;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button80_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 80;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }

        private void button81_Click(object sender, EventArgs e)
        {
            if (!_isReady) MessageBox.Show("Please select mode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                numbOfButton = 81;
                LoadData frm2 = new LoadData(this);
                frm2.Show();
            }
        }
        #endregion Button


        //Thiêt lâp các nút chức năng
        #region FuncButton
        private void buttonSolve_Click(object sender, EventArgs e)
        {
            if (sdk.CheckValid(_Arr) == false) MessageBox.Show("Lưới Sudoku đề bài không hợp lệ!\nVui lòng kiểm tra lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                sdk.isWin = false;
                sdk.FindWay(_Arr, 0, 0);
                _Arr = sdk.ResArr;
                ShowData();
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            //Reset data cũ
            _Arr = new int[9, 9]
            {
                {0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0}
            };
            sdk.ResArr = new int[9, 9]
            {
                {0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0}
            };
            sdk.isWin = false;
            ResetBoard();
            ShowData();
            
        }

        private void btn_Playgame_Click(object sender, EventArgs e)
        {
            _Mode = 2;
            btn_Solve.Enabled = false;
            btn_Clear.Enabled = false;
            btn_CheckWin.Enabled = true;
            SelectDifficulty frm2 = new SelectDifficulty(this);
            lbl_Points.Text = "0";
            frm2.Show();
            ResetBoard();
            
        }

        private void btn_SolverMode_Click(object sender, EventArgs e)
        {
            _Mode = 1;
            _isReady = true;
            sdk.isWin = false;

            btn_Solve.Enabled = true;
            btn_Clear.Enabled = true;
            btn_CheckWin.Enabled = false;
            
            _Arr = new int[9, 9]
            {
                {0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0}
            };
            sdk.ResArr = new int[9, 9]
            {
                {0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0}
            };
            

            ResetBoard();
            ShowData();
            MessageBox.Show("Input the original sudoku board, and press Solve", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btn_CheckWin_Click(object sender, EventArgs e)
        {
            if (sdk.CheckWin(_Arr))
            {
                timer1.Stop();
                MessageBox.Show("You are Win!");
                btn_Solve.Enabled = true;
                lbl_Points.Text = sdk.PointCalc(timer_Min, timer_Sec, _Mode).ToString();
            }
            else MessageBox.Show("Wrong answer!");
        }
        #endregion


        //Timer
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer_Sec--;
            if(timer_Sec < 0 && timer_Min > 0)
            {
                timer_Min--;
                timer_Sec = 59;
            }

            if(timer_Min == 0 && timer_Sec < 0)
            {
                timer1.Stop();
                btn_Solve.Enabled = true;
                btn_CheckWin.Enabled = false;
                MessageBox.Show("Time's Up!");
                
            }
            if (timer_Sec < 0) timer_Sec = 0;
            lbl_Minutes.Text = timer_Min.ToString();
            lbl_Seconds.Text = timer_Sec.ToString();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            lbl_GameGuide.Location = new Point(lbl_GameGuide.Location.X, lbl_GameGuide.Location.Y - 1);
            if(lbl_GameGuide.Location.Y + lbl_GameGuide.Height < 0)
            {
                lbl_GameGuide.Location = new Point(lbl_GameGuide.Location.X, pnl_GameGuide.Location.Y);
            }

        }

        
        
    }
}
