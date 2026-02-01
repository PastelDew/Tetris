using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tetris_Project
{
    class SCORECLASS
    {
        static int SCORE = 0;
        static int lines = 0;
        static int Level = 1;
        static int linecombo = 0;
        static int combotimer = 0;
        const int scoreDown = 10;
        const int scoreLine = 750;
        public int call_lev()
        {
            return Level;
        }
        public int call_line()
        {
            return lines;
        }
        public int call_sore()
        {
            return SCORE;
        }
        public void linecomboinit()
        {
            linecombo = 0;
        }
        public void scc()
        {
            linecombo++;
            if(linecombo < 4)
                SCORE += scoreLine * linecombo;
            else
                SCORE += scoreLine * 4 + linecombo;
            lines++;
            lev();
        }
        protected void lev()
        {
            if (SCORE > (Level * Level * 3000))
                Level++;
        }

        public void downscore(bool isshadowused)
        {
            if (!isshadowused)
            {
                SCORE += scoreDown;
                lev();
            }
        }

        public int view(Label l1, Label l2, Label l3, Label l4)
        {
            l1.Text = "SCORE : " + SCORE.ToString();
            l2.Text = "LINES : " + lines.ToString();
            l3.Text = "LEVEL : " + Level.ToString();
            if (linecombo > 1)
            {
                l4.Visible = true;
                l4.Text = linecombo.ToString() + " Combo!!";
                combotimer = 5;
            }
            else if (combotimer <= 0)
                l4.Visible = false;
            else
                combotimer--;
            return (1000/(Level));
        }

        public int calltotalscore(int pt)
        {
            return (pt > 0) ? (Level * lines * SCORE / pt) : (Level * lines * SCORE);
        }

        public void init()
        {
            SCORE = 0;
            lines = 0;
            Level = 1;
        }
    }
}
