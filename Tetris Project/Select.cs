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
    public partial class Select : Form
    {
        public Select()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox2.Checked = false;
                BGI_Color.Checked = true;
                BGI_Black.Checked = false;
                BLK_Color.Checked = true;
                BLK_Black.Checked = false;
                SDW_Color.Checked = true;
                SDW_Black.Checked = false;
            }
            else
            {
                checkBox2.Checked = true;
                BGI_Color.Checked = false;
                BGI_Black.Checked = true;
                BLK_Color.Checked = false;
                BLK_Black.Checked = true;
                SDW_Color.Checked = false;
                SDW_Black.Checked = true;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox1.Checked = false;
                BGI_Color.Checked = false;
                BGI_Black.Checked = true;
                BLK_Color.Checked = false;
                BLK_Black.Checked = true;
                SDW_Color.Checked = false;
                SDW_Black.Checked = true;
            }
            else
            {
                checkBox1.Checked = true;
                BGI_Color.Checked = true;
                BGI_Black.Checked = false;
                BLK_Color.Checked = true;
                BLK_Black.Checked = false;
                SDW_Color.Checked = true;
                SDW_Black.Checked = false;
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (panel5.Enabled)
            {
                panel5.Enabled = false;
                panel6.Enabled = true;
            }
            else
            {
                panel5.Enabled = true;
                panel6.Enabled = false;
            }
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked)
            {
                DialogResult dr = MessageBox.Show("그림자를 사용하면 블록 내리기 보너스를 받을 수 없습니다.\n"
                    + "그래도 사용하시겠습니까?", "확인", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                    panel4.Enabled = true;
                else
                {
                    radioButton5.Checked = false;
                    radioButton7.Checked = true;
                }

            }
            else
                panel4.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
