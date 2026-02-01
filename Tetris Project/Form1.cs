using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;
using System.IO;

using System.Runtime.InteropServices;

namespace Tetris_Project
{
    public partial class Form1 : Form
    {
        bool Pause = true;
        bool StyleMode = false;
        bool Player2Mode = false;
        int Delay = 900;
        bool DelayOn = false;
        int PlayTime = 0;
        bool isshadowused = false;
        Blockset Blocksetting = new Blockset();          //블록 설정, 회전 클래스
        BlockMove BlockMoving = new BlockMove();          //블록 이동 클래스
        Draw Drawing = new Draw();
        BlockCreate BlockCreating = new BlockCreate();      //블록 생성 클래스
        BlockCreate BlockCreating_P2 = new BlockCreate();
        int[,] TETRIS = new int[12, 23];
        int[,] TETRIS_P2 = new int[12, 23];
        int[,] S_TETRIS = new int[12, 23];
        int[,] S_TETRIS_P2 = new int[12, 23];
        Select frmForm2 = new Select();
        SCORECLASS SC = new SCORECLASS();
        SCORECLASS SC_P2 = new SCORECLASS();
        RankingClass RC = new RankingClass();
        SoundPlayer Sound = new SoundPlayer();
        Setting sett = new Setting();

        [DllImport("winmm.dll")]
        private static extern long mciSendString(string strCommand, StringBuilder strReturn, int iReturnLength, IntPtr hwndCallback);

        public Form1()
        {
            InitializeComponent();
            string str = Application.StartupPath;

            if (checkBox1.Checked)
            {
                if(radioButton1.Checked)
                    mciSendString("open \"" + str + "\\Resources\\Opening.mp3" + "\" type mpegvideo alias MediaFile", null, 0, IntPtr.Zero);
                else
                    mciSendString("open \"" + str + "\\Resources\\Opening.mp3" + "\" type mpegvideo alias MediaFile", null, 0, IntPtr.Zero);
                mciSendString("play MediaFile", null, 0, IntPtr.Zero);
            }

            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            frmForm2.radioButton1.CheckedChanged += new EventHandler(formchecker);
            frmForm2.radioButton2.CheckedChanged += new EventHandler(formchecker);
            frmForm2.checkBox1.CheckedChanged += new EventHandler(formchecker);
            frmForm2.BGI_Color.CheckedChanged += new EventHandler(BGI_Color_CheckedChanged);
            frmForm2.BLK_Color.CheckedChanged += new EventHandler(BLK_Color_CheckedChanged);
            frmForm2.SDW_Color.CheckedChanged += new EventHandler(SDW_Color_CheckedChanged);
            frmForm2.radioButton5.CheckedChanged += new EventHandler(radioButton5_CheckedChanged);
            frmForm2.button1.Click += new EventHandler(runed);

        }

        void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (frmForm2.radioButton5.Checked)
                isshadowused = true;
            else
                isshadowused = false;
        }

        void SDW_Color_CheckedChanged(object sender, EventArgs e)
        {
            if (frmForm2.panel6.Enabled)
                Drawing.customizedchange(frmForm2.BGI_Color.Checked, frmForm2.BLK_Color.Checked, frmForm2.SDW_Color.Checked, this);
        }

        void BLK_Color_CheckedChanged(object sender, EventArgs e)
        {
            if (frmForm2.panel6.Enabled)
                Drawing.customizedchange(frmForm2.BGI_Color.Checked, frmForm2.BLK_Color.Checked, frmForm2.SDW_Color.Checked, this);
        }

        void BGI_Color_CheckedChanged(object sender, EventArgs e)
        {
            if (frmForm2.panel6.Enabled)
                Drawing.customizedchange(frmForm2.BGI_Color.Checked, frmForm2.BLK_Color.Checked, frmForm2.SDW_Color.Checked, this);
            if (frmForm2.BGI_Color.Checked)
                ComboLabel.ForeColor = Color.Black;
            else
                ComboLabel.ForeColor = Color.WhiteSmoke;
        }
        void runed(object sender, EventArgs e)
        {
            label1.Visible = false;
            button1.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button1.Visible = false;
            button6.Visible = false;
            button7.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            label6.Visible = true;
            button4.Visible = true;
            button5.Visible = true;
            timer1.Enabled = true;
            timer2.Enabled = true;
            timer3.Enabled = true;
            pausing(false);
            for (int i = 0; i < 22; i++)
                TETRIS[0, i] = 8;
            for (int i = 0; i < 22; i++)
                TETRIS[11, i] = 8;
            for (int i = 0; i < 12; i++)
                TETRIS[i, 22] = 8;
            for (int i = 0; i < 22; i++)
                S_TETRIS[0, i] = 8;
            for (int i = 0; i < 22; i++)
                S_TETRIS[11, i] = 8;
            for (int i = 0; i < 12; i++)
                S_TETRIS[i, 22] = 8;
            BlockMoving.BMinit(TETRIS, S_TETRIS, BlockCreating);
            BlockCreating.create(TETRIS);
            Invalidate();
            BlockCreating.init(TETRIS);
            if (Player2Mode)
            {
                for (int i = 0; i < 22; i++)
                    TETRIS_P2[0, i] = 8;
                for (int i = 0; i < 22; i++)
                    TETRIS_P2[11, i] = 8;
                for (int i = 0; i < 12; i++)
                    TETRIS_P2[i, 22] = 8;
                for (int i = 0; i < 22; i++)
                    S_TETRIS_P2[0, i] = 8;
                for (int i = 0; i < 22; i++)
                    S_TETRIS_P2[11, i] = 8;
                for (int i = 0; i < 12; i++)
                    S_TETRIS_P2[i, 22] = 8;
                BlockMoving.BMinit(TETRIS_P2, S_TETRIS_P2, BlockCreating_P2);
                BlockCreating.create(TETRIS_P2);
                BlockCreating.init(TETRIS_P2);
            }

            SC.init();
            PlayTime = 0;
            label4.Text = "PlayTime : 00:00";
            mciSendString("close MediaFile", null, 0, IntPtr.Zero);
            BlockMoving.shadow_block(TETRIS, S_TETRIS, BlockCreating);
            if(Player2Mode)
                BlockMoving.shadow_block(TETRIS_P2, S_TETRIS_P2, BlockCreating_P2);

            Blocksetting.blockinit();
            this.Focus();
        }

        void formchecker(object sender, EventArgs e)
        {
            if (frmForm2.radioButton1.Checked)
                Player2Mode = false;
            else
                Player2Mode = true;
            if (frmForm2.checkBox2.Checked)
            {
                Drawing.stylechange(true);
                this.BackgroundImage = Image.FromFile(@"./Resources/BGI_Black.png");
                ComboLabel.ForeColor = Color.WhiteSmoke;
            }
            else
            {
                Drawing.stylechange(false);
                this.BackgroundImage = Image.FromFile(@"./Resources/BGI.png");
                ComboLabel.ForeColor = Color.Black;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {//초기 화면의 게임 종료
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {//어바웃..
            MessageBox.Show("Copyright(c). Inje Univ. \n20123146 송희준 \nVersion. 1.0.1.19");
        }

        private void button1_Click(object sender, EventArgs e)
        {//게임 시작
            frmForm2.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {//게임종료
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (Pause)
                pausing(false);
            else
                pausing(true);
        }
        private void pausing(bool ispuse)
        {//일시정지
            if (ispuse)
            {
                Pause = true;
                timer1.Enabled = false;
                timer2.Enabled = false;
                button5.Text = "UNPAUSE (P)";
            }
            else
            {
                Pause = false;
                timer1.Enabled = true;
                timer2.Enabled = true;
                button5.Text = "PAUSE (P)";
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {//딜레이
            if (DelayOn)
                timer1.Interval = 9;
            else
                timer1.Interval = Delay;
            fastdown();
        }

        private void fastdown()
        {
            BlockMoving.move(TETRIS, S_TETRIS, BlockCreating, SC);
            if(Player2Mode)
                BlockMoving.move(TETRIS_P2, S_TETRIS_P2, BlockCreating_P2, SC_P2);
            Invalidate();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {//플레이타임
            int i, j;
            PlayTime += 1;
            if (PlayTime % 100 > 59)
                PlayTime += 40;
            i = PlayTime / 100;
            j = PlayTime % 100;
            label4.Text = "PlayTime : ";
            if (i > 9)
                label4.Text += i;
            else
                label4.Text += "0" + i;
            label4.Text += ":";
            if (j > 9)
                label4.Text += j;
            else
                label4.Text += "0" + j;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.P)
            {
                if (Pause)
                    pausing(false);
                else
                    pausing(true);
            }
            if (!Pause && !BlockCreating.isover())
            {
                if (e.KeyCode.ToString() == sett.textBox1.Text)
                    BlockMoving.turn(TETRIS, S_TETRIS, BlockCreating, frmForm2.turn_cl.Checked, SC);
                if (e.KeyCode.ToString() == sett.textBox2.Text)
                    BlockMoving.turn(TETRIS, S_TETRIS, BlockCreating, !frmForm2.turn_cl.Checked, SC);
                if (e.KeyCode.ToString() == sett.textBox4.Text)
                    BlockMoving.left(TETRIS, S_TETRIS, BlockCreating);
                if (e.KeyCode.ToString() == sett.textBox5.Text)
                    BlockMoving.right(TETRIS, S_TETRIS, BlockCreating);
                if (e.KeyCode.ToString() == sett.textBox6.Text)
                    BlockMoving.space(TETRIS, SC, BlockCreating, S_TETRIS, isshadowused);
                if (e.KeyCode.ToString() == sett.textBox7.Text)
                    BlockMoving.Holding(TETRIS, S_TETRIS, BlockCreating,Blocksetting);
                if (e.KeyCode.ToString() == sett.textBox3.Text)
                {
                    DelayOn = true;
                    fastdown();
                    SC.downscore(isshadowused);
                }
                else
                    Invalidate();
                if (Player2Mode)
                {
                    if (e.KeyCode.ToString() == sett.textBox14.Text)
                        BlockMoving.turn(TETRIS_P2, S_TETRIS_P2, BlockCreating_P2, frmForm2.turn_cl.Checked, SC_P2);
                    if (e.KeyCode.ToString() == sett.textBox13.Text)
                        BlockMoving.turn(TETRIS_P2, S_TETRIS_P2, BlockCreating_P2, !frmForm2.turn_cl.Checked, SC_P2);
                    if (e.KeyCode.ToString() == sett.textBox11.Text)
                        BlockMoving.left(TETRIS_P2, S_TETRIS_P2, BlockCreating_P2);
                    if (e.KeyCode.ToString() == sett.textBox10.Text)
                        BlockMoving.right(TETRIS_P2, S_TETRIS_P2, BlockCreating_P2);
                    if (e.KeyCode.ToString() == sett.textBox9.Text)
                        BlockMoving.space(TETRIS_P2, SC_P2, BlockCreating_P2, S_TETRIS_P2, isshadowused);
                    if (e.KeyCode.ToString() == sett.textBox8.Text)
                        BlockMoving.Holding(TETRIS_P2, S_TETRIS_P2, BlockCreating_P2, Blocksetting);
                    if (e.KeyCode.ToString() == sett.textBox12.Text)
                    {
                        DelayOn = true;
                        fastdown();
                        SC_P2.downscore(isshadowused);
                    }
                    else
                        Invalidate();
                }
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
                DelayOn = false;
        }


        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Drawing.Drawing(g, TETRIS, S_TETRIS, isshadowused);
            Blocksetting.preb(g);
            Blocksetting.HoldF(g);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X >= 208 && e.Y >= 331)
            {
                button4.Enabled = true;
                button5.Enabled = true;
            }
            else
            {
                button4.Enabled = false;
                button5.Enabled = false;
            }
            if (e.X >= 270 && e.Y <= 183)
                panel1.Enabled = true;
            else
                panel1.Enabled = false;
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            Delay = SC.view(label3,label6,label5, ComboLabel);
            BlockMoving.line_finder(TETRIS);
            if (BlockCreating.isover())
            {
                pausing(true);
                timer3.Enabled = false;
                int total = SC.calltotalscore(PlayTime);
                if(RC.isranker(total)!=0)
                {
                    DialogResult rank = MessageBox.Show(RC.isranker(total) + "등 하셨습니다! 등록하시겠습니까?"
                        , "랭킹 등록!", MessageBoxButtons.YesNo);
                    if (rank == DialogResult.Yes)
                    {
                        RC.upload(RC.isranker(total)-1,PlayTime,SC.call_lev(),SC.call_line(),SC.call_sore(),SC.calltotalscore(PlayTime));
                    }
                }
                RegameForm RF = new RegameForm();
                RF.btn_main.Click += btn_main_Click;
                RF.btn_regame.Click += btn_regame_Click;
                RF.labelinit(SC.call_lev(), SC.call_line(), SC.call_sore(), label4.Text, SC.calltotalscore(PlayTime));
                RF.ShowDialog();
            }
            Invalidate();
        }

        void btn_regame_Click(object sender, EventArgs e)
        {
            BlockMoving.BMinit(TETRIS, S_TETRIS, BlockCreating);
            BlockCreating.create(TETRIS);
            Invalidate();
            BlockCreating.init(TETRIS);
            SC.init();
            PlayTime = 0;
            label4.Text = "PlayTime : 00:00";
            mciSendString("close MediaFile", null, 0, IntPtr.Zero);
            BlockMoving.shadow_block(TETRIS, S_TETRIS, BlockCreating);
            Blocksetting.blockinit();
            this.Focus();
            RC.call_ranking();
            timer3.Enabled = true;
            pausing(false);
        }

        void btn_main_Click(object sender, EventArgs e)
        {
            RC.call_ranking();
            label1.Visible = true;
            button1.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button1.Visible = true;
            button6.Visible = true;
            button7.Visible = true;
            button2.Visible = true;
            button3.Visible = true;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            button4.Visible = false;
            button5.Visible = false;
            timer1.Enabled = false;
            timer2.Enabled = false;
            timer3.Enabled = false;
            pausing(false);
            for (int i = 0; i < 22; i++)
                TETRIS[0, i] = 8;
            for (int i = 0; i < 22; i++)
                TETRIS[11, i] = 8;
            for (int i = 0; i < 12; i++)
                TETRIS[i, 22] = 8;
            for (int i = 0; i < 22; i++)
                S_TETRIS[0, i] = 8;
            for (int i = 0; i < 22; i++)
                S_TETRIS[11, i] = 8;
            for (int i = 0; i < 12; i++)
                S_TETRIS[i, 22] = 8;
            for (int i = 0; i < 22; i++)
                TETRIS_P2[0, i] = 8;
            for (int i = 0; i < 22; i++)
                TETRIS_P2[11, i] = 8;
            for (int i = 0; i < 12; i++)
                TETRIS_P2[i, 22] = 8;
            for (int i = 0; i < 22; i++)
                S_TETRIS_P2[0, i] = 8;
            for (int i = 0; i < 22; i++)
                S_TETRIS_P2[11, i] = 8;
            for (int i = 0; i < 12; i++)
                S_TETRIS_P2[i, 22] = 8;
            BlockCreating.create(TETRIS);
            for (int i = 0; i < 12; i++)
                for (int j = 0; j < 23; j++)
                    S_TETRIS[i, j] = TETRIS[i, j];
            Blocksetting.blockinit();
            Invalidate();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            RC.call_ranking();
            RC.ranking_read();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Application.StartupPath);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                string str = Application.StartupPath;
                if (radioButton1.Checked)
                    mciSendString("open \"" + str + "\\Resources\\Opening.mp3" + "\" type mpegvideo alias MediaFile", null, 0, IntPtr.Zero);
                else
                    mciSendString("open \"" + str + "\\Resources\\Opening.mp3" + "\" type mpegvideo alias MediaFile", null, 0, IntPtr.Zero);
                mciSendString("play MediaFile", null, 0, IntPtr.Zero);
            }
            else
                mciSendString("close MediaFile", null, 0, IntPtr.Zero);
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            sett.ShowDialog();
        }
    }
}