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
    public partial class RankingUpdate : Form
    {
        private Panel W_panel = new Panel();
        private TextBox B_name = new TextBox();
        private Label[] L_name = new Label[10];
        private Label[] L_playtime = new Label[10];
        private Label[] L_level = new Label[10];
        private Label[] L_lines = new Label[10];
        private Label[] L_score = new Label[10];
        private Label[] L_totalscore = new Label[10];
        static string rankstr = "";
        static string[] ptr_name;
        static string[] ptr_playtime;
        static int[] ptr_level;
        static int[] ptr_lines;
        static int[] ptr_score;
        static double[] ptr_totalscore;
        static int rank;
        bool iswriting = false;
        public RankingUpdate(string str, string[] ptr_na, string[] ptr_pt, int[] ptr_lev, int[] ptr_lin, int[] ptr_sco, double[] ptr_tot)
        {
            InitializeComponent();

            rankstr = str;
            ptr_name = ptr_na;
            ptr_playtime = ptr_pt;
            ptr_level = ptr_lev;
            ptr_lines = ptr_lin;
            ptr_score = ptr_sco;
            ptr_totalscore = ptr_tot;

            Panel t_panel = new Panel();
            Label t_name = new Label();
            Label t_playtime = new Label();
            Label t_level = new Label();
            Label t_lines = new Label();
            Label t_score = new Label();
            Label t_totalscore = new Label();
            t_name.Location = new Point(20, 10);
            t_playtime.Location = new Point(70, 10);
            t_level.Location = new Point(120, 10);
            t_lines.Location = new Point(170, 10);
            t_score.Location = new Point(220, 10);
            t_totalscore.Location = new Point(270, 10);
            int ht = t_name.Size.Height;
            Label temp = new Label();
            temp.Text = "R";
            temp.Size = new Size(20, ht);
            temp.Location = new Point(0, 10);
            t_name.Size = new Size(50, ht);
            t_playtime.Size = new Size(50, ht);
            t_level.Size = new Size(50, ht);
            t_lines.Size = new Size(50, ht);
            t_score.Size = new Size(50, ht);
            t_totalscore.Size = new Size(50, ht);
            t_name.Text = "이름";
            t_playtime.Text = "플레이";
            t_level.Text = "레벨";
            t_lines.Text = "라인";
            t_score.Text = "점수";
            t_totalscore.Text = "총 점수";

            t_panel.Controls.Add(temp);
            t_panel.Controls.Add(t_name);
            t_panel.Controls.Add(t_playtime);
            t_panel.Controls.Add(t_level);
            t_panel.Controls.Add(t_lines);
            t_panel.Controls.Add(t_score);
            t_panel.Controls.Add(t_totalscore);
            t_panel.BorderStyle = BorderStyle.FixedSingle;
            t_panel.Location = new Point(10,10);
            t_panel.AutoSize = true;
            t_panel.Size = new Size(t_panel.Size.Width, 23);
            t_panel.BackColor = Color.LightGray;
            this.Controls.Add(t_panel);
            for (int i = 0; i < 10; i++)
            {
                L_name[i] = new Label();
                L_playtime[i] = new Label();
                L_level[i] = new Label();
                L_lines[i] = new Label();
                L_score[i] = new Label();
                L_totalscore[i] = new Label();
                ptr_name = new string[10];
                ptr_playtime = new string[10];
                ptr_level = new int[10];
                ptr_lines = new int[10];
                ptr_score = new int[10];
                ptr_totalscore = new double[10];
            }
            W_panel.AutoSize = true;
            W_panel.BorderStyle = BorderStyle.FixedSingle;
            W_panel.BackColor = Color.White;
            W_panel.Location = new Point(10, 43);
            this.Controls.Add(W_panel);
        }
        public void viewdata(int i, string name, string playtime, int level, int lines, int score, double totalscore)
        {
            L_name[i].Text = name;
            L_playtime[i].Text = playtime;
            L_level[i].Text = level.ToString();
            L_lines[i].Text = lines.ToString();
            L_score[i].Text = score.ToString();
            L_totalscore[i].Text = totalscore.ToString();

            ptr_playtime[i] = playtime;
            ptr_level[i] = level;
            ptr_lines[i] = lines;
            ptr_score[i] = score;
            ptr_totalscore[i] = totalscore;

            L_name[i].Location = new Point(20, i*23 + 23);
            L_playtime[i].Location = new Point(70, i * 23 + 23);
            L_level[i].Location = new Point(120, i * 23 + 23);
            L_lines[i].Location = new Point(170, i * 23 + 23);
            L_score[i].Location = new Point(220, i * 23 + 23);
            L_totalscore[i].Location = new Point(270, i * 23 + 23);

            int ht = L_name[i].Size.Height;

            Label temp = new Label();
            temp.Text = (i+1).ToString() + ".";
            temp.Size = new Size(20, ht);
            temp.Location = new Point(0, i * 23 + 23);

            L_name[i].Size = new Size(50, ht);
            L_playtime[i].Size = new Size(50, ht);
            L_level[i].Size = new Size(50, ht);
            L_lines[i].Size = new Size(50, ht);
            L_score[i].Size = new Size(50, ht);
            L_totalscore[i].Size = new Size(50, ht);

            int j = 0;
            j = L_totalscore[i].Text.IndexOf('.');
            if (j > 0)
                L_totalscore[i].Text = L_totalscore[i].Text.Substring(0, j);

            W_panel.Controls.Add(temp);
            W_panel.Controls.Add(L_name[i]);
            W_panel.Controls.Add(L_playtime[i]);
            W_panel.Controls.Add(L_level[i]);
            W_panel.Controls.Add(L_lines[i]);
            W_panel.Controls.Add(L_score[i]);
            W_panel.Controls.Add(L_totalscore[i]);
        }
        public void wrightform(int i,string playtime,int level,int lines,int score,double totalscore)
        {
            ptr_playtime[i] = playtime;
            rank = i;

            B_name.Text = "Wing310";
            L_playtime[i].Text = playtime;
            L_level[i].Text = level.ToString();
            L_lines[i].Text = lines.ToString();
            L_score[i].Text = score.ToString();
            L_totalscore[i].Text = totalscore.ToString();

            B_name.Location = new Point(20, i * 23+23);
            L_playtime[i].Location = new Point(70, i * 23+23);
            L_level[i].Location = new Point(120, i * 23+23);
            L_lines[i].Location = new Point(170, i * 23+23);
            L_score[i].Location = new Point(220, i * 23+23);
            L_totalscore[i].Location = new Point(270, i * 23+23);

            int ht = L_name[i].Size.Height;

            Label temp = new Label();
            temp.Text = (i + 1).ToString() + ".";
            temp.Size = new Size(20, ht);
            temp.Location = new Point(0, i * 23+23);

            B_name.Size = new Size(50, ht);
            L_playtime[i].Size = new Size(50, ht);
            L_level[i].Size = new Size(50, ht);
            L_lines[i].Size = new Size(50, ht);
            L_score[i].Size = new Size(50, ht);
            L_totalscore[i].Size = new Size(50, ht);

            int j = 0;
            j = L_totalscore[i].Text.IndexOf('.');
            if (j > 0)
                L_totalscore[i].Text = L_totalscore[i].Text.Substring(0, j);

            W_panel.Controls.Add(temp);
            W_panel.Controls.Add(B_name);
            W_panel.Controls.Add(L_playtime[i]);
            W_panel.Controls.Add(L_level[i]);
            W_panel.Controls.Add(L_lines[i]);
            W_panel.Controls.Add(L_score[i]);
            W_panel.Controls.Add(L_totalscore[i]);
            iswriting = true;
            button1.Text = "등록";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (iswriting)
            {
                try
                {
                    ptr_name[rank] = B_name.Text;
                    L_name[rank].Text = B_name.Text;
                    rankstr = "";
                    for (int i = 0; i < 10; i++)
                        rankstr += L_name[i].Text + "," + L_playtime[i].Text + "," + L_level[i].Text + "," + L_lines[i].Text + "," + L_score[i].Text + "," + L_totalscore[i].Text + "\n";
                    StreamWriter SWriter = new StreamWriter(@"./Rankers.INF", false, Encoding.UTF8);
                    SWriter.Write(rankstr);
                    SWriter.Close();
                    this.Hide();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
                this.Hide();
        }
    }
}
