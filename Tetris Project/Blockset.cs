using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
namespace Tetris_Project
{
    class Blockset
    {
        /// <summary>
        /// 블럭 설정
        /// </summary>
        int[,] Block1 = {
                                    {1,1,0,0},  // ■■□□
                                    {1,1,0,0},  // ■■□□
                                    {0,0,0,0},  // □□□□
                                    {0,0,0,0}   // □□□□
                               };
        int[,] Block2 = {
                                    {0,2,0,0},  // □■□□
                                    {0,2,0,0},  // □■□□
                                    {0,2,0,0},  // □■□□
                                    {0,2,0,0}   // □■□□
                               };
        int[,] Block3 = {
                                    {0,3,0,0},  // □■□□
                                    {3,3,3,0},  // ■■■□
                                    {0,0,0,0},  // □□□□
                                    {0,0,0,0}   // □□□□
                               };
        int[,] Block4 = {
                                    {4,0,0,0},  // ■□□□
                                    {4,4,4,0},  // ■■■□
                                    {0,0,0,0},  // □□□□
                                    {0,0,0,0}   // □□□□
                               };
        int[,] Block5 = {
                                    {0,0,5,0},  // □□■□
                                    {5,5,5,0},  // ■■■□
                                    {0,0,0,0},  // □□□□
                                    {0,0,0,0}   // □□□□
                               };
        int[,] Block6 = {
                                    {0,6,6,0},  // □■■□
                                    {6,6,0,0},  // ■■□□
                                    {0,0,0,0},  // □□□□
                                    {0,0,0,0}   // □□□□
                               };
        int[,] Block7 = {
                                    {7,7,0,0},  // ■■□□
                                    {0,7,7,0},  // □■■□
                                    {0,0,0,0},  // □□□□
                                    {0,0,0,0}   // □□□□
                               };
        static int[] blockstack = new int[7];
        static int[,] Preblock = new int[4, 4];
        static int[,] temp = new int[4, 4];
        static bool loaded = false;
        static int[,] hold = new int[4, 4];
        static bool Holded = false;
        public Blockset()
        {
            for (int i = 0; i < 7; i++)
                blockstack[i] = 0;
            if (!loaded)
            {
                randomize();
                loaded = true;
            }
        }

        public void blockinit()
        {
            for (int a = 0; a < 4; a++)
                for (int b = 0; b < 4; b++)
                    hold[a, b] = 0;
            Holded = false;
        }

        public int[,] setting()
        {
            for (int a = 0; a < 4; a++)
                for (int b = 0; b < 4; b++)
                    temp[a, b] = Preblock[a, b];
            randomize();
            return temp;
        }
        private void trashremover(int num)
        {
            for (int i = 0; i < num; i++)
                blockstack[i]--;
            for (int i = num + 1; i < 7; i++)
                blockstack[i]--;
            for (int i = 0; i < 7; i++)
                if (blockstack[i] < 0)
                    blockstack[i] = 0;
        }
        public void preb(Graphics g)
        {
            Draw drawing = new Draw();
            drawing.predraw(g, Preblock);
        }
        public void HoldF(Graphics g)
        {
            Draw drawing = new Draw();
            drawing.hold(g, hold);
        }
        public void change(int[,] TETRIS)
        {
            int a, b;
            if (Holded)
            {
                int[,] temp2 = new int[4, 4];
                for (a = 0; a < 4; a++)
                {
                    for (b = 0; b < 4; b++)
                    {
                        temp2[a, b] = temp[a, b];
                        TETRIS[4 + b, 1 + a] = hold[a, b];
                        temp[a, b] = hold[a, b];
                        hold[a, b] = temp2[a, b];
                    }
                }
            }
            else
            {
                for (a = 0; a < 4; a++)
                    for (b = 0; b < 4; b++)
                        hold[a, b] = temp[a, b];
                Holded = true;
            }
        }
        private void randomize()
        {
            while (true)
            {
                System.Random ranNum = new System.Random();
                int i = ranNum.Next(0, 7);
                if (i == 0 && blockstack[i] < 3)
                {
                    Preblock = Block1;
                    blockstack[i]++;
                    trashremover(i);
                    break;
                }
                else if (i == 1 && blockstack[i] < 3)
                {
                    Preblock = Block2;
                    blockstack[i]++;
                    trashremover(i);
                    break;
                }
                else if (i == 2 && blockstack[i] < 3)
                {
                    Preblock = Block3;
                    blockstack[i]++;
                    trashremover(i);
                    break;
                }
                else if (i == 3 && blockstack[i] < 3)
                {
                    Preblock = Block4;
                    blockstack[i]++;
                    trashremover(i);
                    break;
                }
                else if (i == 4 && blockstack[i] < 3)
                {
                    Preblock = Block5;
                    blockstack[i]++;
                    trashremover(i);
                    break;
                }
                else if (i == 5 && blockstack[i] < 3)
                {
                    Preblock = Block6;
                    blockstack[i]++;
                    trashremover(i);
                    break;
                }
                else if (i == 6 && blockstack[i] < 3)
                {
                    Preblock = Block7;
                    blockstack[i]++;
                    trashremover(i);
                    break;
                }
                else
                    trashremover(i);
            }
        }
    }
}
