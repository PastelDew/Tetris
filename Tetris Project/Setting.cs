using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Tetris_Project
{
    public partial class Setting : Form
    {
        static bool isfirst = false;
        public Setting()
        {
            InitializeComponent();
            try
            {
                string str = "";
                StreamReader sreader;
                FileInfo title = new FileInfo(@"./SettingsP1.INF");
                if (!title.Exists)
                {
                    FileStream t = title.Create();
                    t.Close();
                }
                sreader = new StreamReader(@"./SettingsP1.INF", Encoding.UTF8);
                str = sreader.ReadToEnd();
                if (str == "")
                {
                    isfirst = true;
                    str = "Up,HanjaMode,Down,Left,Right,Space,ShiftKey,";
                }
                int j = 0, k = 0;
                k = str.IndexOf(',', j);
                textBox1.Text = str.Substring(j, k - j);
                j = k + 1;
                k = str.IndexOf(',', j);
                textBox2.Text = str.Substring(j, k - j);
                j = k + 1;
                k = str.IndexOf(',', j);
                textBox3.Text = str.Substring(j, k - j);
                j = k + 1;
                k = str.IndexOf(',', j);
                textBox4.Text = str.Substring(j, k - j);
                j = k + 1;
                k = str.IndexOf(',', j);
                textBox5.Text = str.Substring(j, k - j);
                j = k + 1;
                k = str.IndexOf(',', j);
                textBox6.Text = str.Substring(j, k - j);
                j = k + 1;
                k = str.IndexOf(',', j);
                textBox7.Text = str.Substring(j, k - j);
                sreader.Close();
                StreamWriter SWriter = new StreamWriter(@"./SettingsP1.INF", false, Encoding.UTF8);
                SWriter.Write(str);
                SWriter.Close();


                title = new FileInfo(@"./SettingsP2.INF");
                if (!title.Exists)
                {
                    FileStream t = title.Create();
                    t.Close();
                }
                sreader = new StreamReader(@"./SettingsP2.INF", Encoding.UTF8);
                str = sreader.ReadToEnd();
                if (str == "")
                {
                    isfirst = true;
                    str = "Up,HanjaMode,Down,Left,Right,Space,ShiftKey,";
                }
                j = 0;
                k = 0;
                k = str.IndexOf(',', j);
                textBox14.Text = str.Substring(j, k - j);
                j = k + 1;
                k = str.IndexOf(',', j);
                textBox13.Text = str.Substring(j, k - j);
                j = k + 1;
                k = str.IndexOf(',', j);
                textBox12.Text = str.Substring(j, k - j);
                j = k + 1;
                k = str.IndexOf(',', j);
                textBox11.Text = str.Substring(j, k - j);
                j = k + 1;
                k = str.IndexOf(',', j);
                textBox10.Text = str.Substring(j, k - j);
                j = k + 1;
                k = str.IndexOf(',', j);
                textBox9.Text = str.Substring(j, k - j);
                j = k + 1;
                k = str.IndexOf(',', j);
                textBox8.Text = str.Substring(j, k - j);
                sreader.Close();
                SWriter = new StreamWriter(@"./SettingsP2.INF", false, Encoding.UTF8);
                SWriter.Write(str);
                SWriter.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
                textBox1.Text = "";
            else
                textBox14.Text = "";
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                textBox1.Text = e.KeyCode.ToString();
                textBox2.Focus();
            }
            else
            {
                textBox14.Text = e.KeyCode.ToString();
                textBox13.Focus();
            }
        }
        private void textBox2_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
                textBox2.Text = "";
            else
                textBox13.Text = "";
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                textBox2.Text = e.KeyCode.ToString();
                textBox3.Focus();
            }
            else
            {
                textBox13.Text = e.KeyCode.ToString();
                textBox12.Focus();
            }
        }
        private void textBox3_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
                textBox3.Text = "";
            else
                textBox12.Text = "";
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                textBox3.Text = e.KeyCode.ToString();
                textBox4.Focus();
            }
            else
            {
                textBox12.Text = e.KeyCode.ToString();
                textBox11.Focus();
            }
        }
        private void textBox4_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
                textBox4.Text = "";
            else
                textBox11.Text = "";
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                textBox4.Text = e.KeyCode.ToString();
                textBox5.Focus();
            }
            else
            {
                textBox11.Text = e.KeyCode.ToString();
                textBox10.Focus();
            }
        }
        private void textBox5_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
                textBox5.Text = "";
            else
                textBox10.Text = "";
        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                textBox5.Text = e.KeyCode.ToString();
                textBox6.Focus();
            }
            else
            {
                textBox10.Text = e.KeyCode.ToString();
                textBox9.Focus();
            }
        }
        private void textBox6_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
                textBox6.Text = "";
            else
                textBox9.Text = "";
        }

        private void textBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                textBox6.Text = e.KeyCode.ToString();
                textBox7.Focus();
            }
            else
            {
                textBox9.Text = e.KeyCode.ToString();
                textBox8.Focus();
            }
        }
        private void textBox7_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
                textBox7.Text = "";
            else
                textBox8.Text = "";
        }

        private void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                textBox7.Text = e.KeyCode.ToString();
                button1.Focus();
            }
            else
            {
                textBox8.Text = e.KeyCode.ToString();
                button2.Focus();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" &&
                textBox5.Text != "" && textBox6.Text != "" && textBox7.Text != "")
            {
                try
                {
                    string str = "";
                    FileInfo title = new FileInfo(@"./SettingsP1.INF");
                    if (!title.Exists)
                    {
                        FileStream t = title.Create();
                        t.Close();
                    }
                    str = textBox1.Text + "," + textBox2.Text + "," + textBox3.Text + ","
                        + textBox4.Text + "," + textBox5.Text + "," + textBox6.Text + "," + textBox7.Text + ",";
                    StreamWriter SWriter = new StreamWriter(@"./SettingsP1.INF", false, Encoding.UTF8);
                    SWriter.Write(str);
                    SWriter.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
                MessageBox.Show("키 지정을 완료하십시오");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox14.Text != "" && textBox13.Text != "" && textBox12.Text != "" && textBox11.Text != "" &&
                textBox10.Text != "" && textBox9.Text != "" && textBox8.Text != "")
            {
                try
                {
                    string str = "";
                    FileInfo title = new FileInfo(@"./SettingsP2.INF");
                    if (!title.Exists)
                    {
                        FileStream t = title.Create();
                        t.Close();
                    }
                    str = textBox1.Text + "," + textBox2.Text + "," + textBox3.Text + ","
                        + textBox4.Text + "," + textBox5.Text + "," + textBox6.Text + "," + textBox7.Text + ",";
                    StreamWriter SWriter = new StreamWriter(@"./SettingsP2.INF", false, Encoding.UTF8);
                    SWriter.Write(str);
                    SWriter.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
                MessageBox.Show("키 지정을 완료하십시오");
        }
    }
}
