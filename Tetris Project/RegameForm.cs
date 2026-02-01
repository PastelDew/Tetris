using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tetris_Project
{
    public partial class RegameForm : Form
    {
        public RegameForm()
        {
            InitializeComponent();
            label2.Text = "레벨 : -- \n";
            label2.Text += "파괴 라인 : -- \n";
            label2.Text += "점수 : -- \n";
            label3.Text = "플레이 타임 : -- \n";
            label3.Text += "최종 점수 : -- \n";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_main_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btn_regame_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        public void labelinit(int lev, int line, int score, string pt, int totalsc)
        {
            label2.Text = "레벨 : " + lev.ToString() + "\n";
            label2.Text += "파괴 라인 : " + line.ToString() + "\n";
            label2.Text += "점수 : " + score.ToString() + "\n";
            label3.Text = pt.ToString() + "\n";
            label3.Text += "최종 점수 : " + totalsc.ToString() + "\n";
        }
    }
}
