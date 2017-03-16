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
    public partial class SelectDifficulty : Form
    {
        private Form1 mainfrm;
        public SelectDifficulty(Form1 frm)
        {
            InitializeComponent();
            mainfrm = frm;
        }

        private void btnEasy_Click(object sender, EventArgs e)
        {
            mainfrm.Mode = 2;
            mainfrm.isReady = true;
            //Set timer
            mainfrm.timer_Min = 10;
            mainfrm.timer_Sec = 0;
            mainfrm.LoadData();
            mainfrm.timer1.Start();
            mainfrm.Highlighter();
            mainfrm.ShowData();
            this.Close();
        }

        private void btn_Normal_Click(object sender, EventArgs e)
        {
            mainfrm.Mode = 3;
            mainfrm.isReady = true;
            //Set timer
            mainfrm.timer_Min = 20;
            mainfrm.timer_Sec = 0;
            mainfrm.LoadData();
            mainfrm.timer1.Start();
            mainfrm.Highlighter();
            mainfrm.ShowData();
            this.Close();
        }

        private void btn_Hard_Click(object sender, EventArgs e)
        {
            mainfrm.Mode = 4;
            mainfrm.isReady = true;
            //Set timer
            mainfrm.timer_Min = 30;
            mainfrm.timer_Sec = 0;
            mainfrm.LoadData();
            mainfrm.timer1.Start();
            mainfrm.Highlighter();
            mainfrm.ShowData();
            this.Close();
        }
    }
}
