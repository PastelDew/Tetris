using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tetris_Project
{
    public class BlockCreate
    {
        Blockset BlockSetting = new Blockset();
        static bool GameOver = false;
        int[,] CurrentBlock = {
                                         {0,0,0,0},
                                         {0,0,0,0},
                                         {0,0,0,0},
                                         {0,0,0,0}
                                     };
        public void create(int[,] TETRIS)
        {
            CurrentBlock = BlockSetting.setting();
            int a, b;
            for (a = 4; a < 8; a++)
                for (b = 1; b < 3; b++)
                    if (TETRIS[a, b] > 8 && TETRIS[a,b] < 16)
                        GameOver = true;
            if (!GameOver)
            {
                for (a = 0; a < 4; a++)
                    for (b = 0; b < 4; b++)
                        TETRIS[4 + b, 1 + a] += CurrentBlock[a, b];
            }
        }
        public void init(int[,] TETRIS)
        {
            int a, b;
            for (a = 1; a < 11; a++)
                for (b = 0; b < 22; b++)
                    TETRIS[a, b] = 0;
            CurrentBlock = BlockSetting.setting();
            for (a = 0; a < 4; a++)
                for (b = 0; b < 4; b++)
                    TETRIS[4 + b, 1 + a] = CurrentBlock[a, b];
            GameOver = false;
        }
        public bool isover()
        {
            return GameOver;
        }
    }
}
