using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Media;

namespace Tetris_Project
{
    public class Draw
    {
        private Image[] img = {Image.FromFile(@"./Resources/BLOCK1.png"),
                              Image.FromFile(@"./Resources/BLOCK2.png"),
                              Image.FromFile(@"./Resources/BLOCK3.png"),
                              Image.FromFile(@"./Resources/BLOCK4.png"),
                              Image.FromFile(@"./Resources/BLOCK5.png"),
                              Image.FromFile(@"./Resources/BLOCK6.png"),
                              Image.FromFile(@"./Resources/BLOCK7.png"),
                              Image.FromFile(@"./Resources/Break1.png"),
                              Image.FromFile(@"./Resources/Break2.png"),
                              Image.FromFile(@"./Resources/Break3.png"),
                              Image.FromFile(@"./Resources/Break4.png"),
                              Image.FromFile(@"./Resources/S_BLOCK1.png"),
                              Image.FromFile(@"./Resources/S_BLOCK2.png"),
                              Image.FromFile(@"./Resources/S_BLOCK3.png"),
                              Image.FromFile(@"./Resources/S_BLOCK4.png"),
                              Image.FromFile(@"./Resources/S_BLOCK5.png"),
                              Image.FromFile(@"./Resources/S_BLOCK6.png"),
                              Image.FromFile(@"./Resources/S_BLOCK7.png")};

        string str = Application.StartupPath;
        SoundPlayer SoundP = new SoundPlayer();
        
        static bool issound = false;
        public Draw()
        {
            SoundP.SoundLocation = str + @"\Resources\3.WAV";
        }
        public void stylechange(bool chk)
        {
            if (chk)
            {
                for (int i = 0; i < 7; i++)
                    img[i] = Image.FromFile(str + @"\Resources\BLOCK_Black.png");
                for (int i = 11; i < 18; i++)
                    img[i] = Image.FromFile(str + @"\Resources\S_BLOCK_BLACK.png");
            }
            else
            {
                for (int i = 0; i < 7; i++)
                    img[i] = Image.FromFile(str + @"\Resources\BLOCK" + (i + 1).ToString() + @".png");
                for (int i = 11; i < 18; i++)
                    img[i] = Image.FromFile(str + @"\Resources\S_BLOCK" + (i - 10).ToString() + @".png");
            }
        }

        public void customizedchange(bool bgi, bool blk, bool sdw, Form1 fr)
        {
            if (bgi)
                fr.BackgroundImage = Image.FromFile(@"./Resources/BGI.png");
            else
                fr.BackgroundImage = Image.FromFile(@"./Resources/BGI_Black.png");
            if(blk)
                for (int i = 0; i < 7; i++)
                    img[i] = Image.FromFile(str + @"\Resources\BLOCK" + (i + 1).ToString() + @".png");
            else
                for (int i = 0; i < 7; i++)
                    img[i] = Image.FromFile(str + @"\Resources\BLOCK_Black.png");
            if(sdw)
                for (int i = 11; i < 18; i++)
                    img[i] = Image.FromFile(str + @"\Resources\S_BLOCK" + (i - 10).ToString() + @".png");
            else
                for (int i = 11; i < 18; i++)
                    img[i] = Image.FromFile(str + @"\Resources\S_BLOCK_BLACK.png");
        }

        public void Drawing(Graphics g, int[,] TETRIS, int[,] S_TETRIS, bool isshadowused)
        {
            int a, b;
            for (a = 21; a > 0; a--)
            {
                int c;
                bool Del = true;
                for (b = 1; b < 11; b++)
                    if (TETRIS[b, a] != 20)
                    {
                        Del = false;
                        if (issound)
                            issound = false;
                    }

                if (Del)
                {
                    for (b = a; b > 1; b--)
                    {
                        for (c = 1; c < 12; c++)
                        {
                            if ((TETRIS[c, b] > 8 && TETRIS[c, b] < 16) || TETRIS[c, b] > 19 && TETRIS[c,b] < 28)
                                TETRIS[c, b] = TETRIS[c, b - 1];
                            else if (TETRIS[c, b] == 0 && TETRIS[c, b - 1] > 8 && TETRIS[c, b-1] < 28)
                                TETRIS[c, b] = TETRIS[c, b - 1];
                            if (!issound)
                            {
                                SoundP.Play();
                                issound = true;
                            }
                        }
                    }

                    SCORECLASS SC = new SCORECLASS();
                    SC.scc();
                }
            }
            for (a = 0; a < 12; a++)
            {
                for (b = 2; b < 23; b++)
                {
                    if (isshadowused)
                    {
                        for (int i = 0; i < 7; i++)
                            if (S_TETRIS[a, b] == i + 1)
                                g.DrawImage(img[i + 11], new Point((a * 19) - 10, ((b - 2) * 19) + 10));
                    }

                    for (int i = 0; i < 7; i++)
                        if (TETRIS[a, b] == i + 1 || TETRIS[a, b] == i + 9)
                            g.DrawImage(img[i], new Point((a * 19) - 10, ((b - 2) * 19) + 10));

                    for (int i = 7; i < 11; i++)
                    {
                        if (TETRIS[a, b] == i + 9)
                        {
                            g.DrawImage(img[i], new Point((a * 19) - 10, ((b - 2) * 19) + 10));
                            TETRIS[a, b] += 1;
                            break;
                        }
                    }
                }
            }
        }
        public void predraw(Graphics g, int[,] BLOCK)
        {
            int a, b, c;
            for(a=0; a<4; a++)
                for(b=0; b<4; b++)
                    for(c=0; c<7; c++)
                        if (BLOCK[b, a] == c + 1)
                        {
                            if(c != 1)
                                g.DrawImage(img[c], new Point((a * 19) + 203, (b * 19) + 30));
                            else
                                g.DrawImage(img[c], new Point((a * 19) + 203, (b * 19) + 11));
                        }

        }
        public void hold(Graphics g, int[,] BLOCK)
        {
            int a, b, c;
            for(a=0; a<4; a++)
                for(b=0; b<4; b++)
                    for(c=0; c<7; c++)
                        if (BLOCK[b, a] == c + 1)
                        {
                            if (c != 1)
                                g.DrawImage(img[c], new Point((a * 19) + 203, (b * 19) + 124));
                            else
                                g.DrawImage(img[c], new Point((a * 19) + 203, (b * 19) + 105));
                        }
        }
    }
}
