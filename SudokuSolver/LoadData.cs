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
    public partial class LoadData : Form
    {
        private Form1 mainfrm;
        public LoadData(Form1 frm)
        {
            InitializeComponent();
            mainfrm = frm;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập dữ liệu!", "Thông báo");
            }
            else
            {
                int i = (mainfrm.numbOfButton - 1) / 9, j = (mainfrm.numbOfButton - 1) % 9;
                mainfrm.Arr[i, j] = Int32.Parse(textBox1.Text);

                if (mainfrm.Mode == 1) mainfrm.Highlighter();
                mainfrm.ShowData();
                
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
