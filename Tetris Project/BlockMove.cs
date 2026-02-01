using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tetris_Project
{
    class BlockMove
    {
        //0 = 빈공간, 1~7 = n번블럭, 8 = 벽, 9~15 = 쌓인 블럭, 21~27 = 임시 블럭
        //28~34 = 그림자 노트 35~41 = 임시 그림자 노트
        /* 
         * CX와 CY가 가리키는 부분 : ■
         * 블럭 배열
         * ■□□□
         * □□□□
         * □□□□
         * □□□□
         */
        int CX = 4, CY = 1;
        int[,] Holder = new int[4, 4];
        bool Holded = false;

        public void BMinit(int[,] TETRIS, int[,] S_TETRIS, BlockCreate BlockCreating)
        {
            CX = 4;
            CY = 1;
            Holded = false;
        }
        public void move(int[,] TETRIS, int[,] S_TETRIS, BlockCreate BlockCreating, SCORECLASS SC)
        {
            for (int i = 0; i < 12; i++)
                for (int j = 0; j < 23; j++)
                    if (TETRIS[i, j] >= 28)
                        TETRIS[i, j] = 0;
            int a, b;
            bool impact = false;
            for (a = 1; a < 11; a++)
                for (b = 0; b < 22; b++)
                    if ((TETRIS[a, b] > 0 && TETRIS[a, b] < 8) && TETRIS[a, b + 1] >= 8 && TETRIS[a, b + 1] < 28)
                        impact = true;
            if (!impact)
            {
                bool crash = false;
                do
                {
                    crash = false;
                    for (a = 1; a < 11; a++)
                    {
                        for (b = 0; b < 22; b++)
                        {
                            if ((TETRIS[a, b] > 0 && TETRIS[a, b] < 8) && TETRIS[a, b + 1] == 0)
                            {
                                TETRIS[a, b + 1] = TETRIS[a, b] + 20;
                                TETRIS[a, b] = 0;
                            }
                        }
                    }
                    for (a = 1; a < 11; a++)
                        for (b = 0; b < 22; b++)
                            if ((TETRIS[a, b] > 0 && TETRIS[a, b] < 8) && TETRIS[a, b + 1] == 0)
                                crash = true;
                } while (crash);
                CY += 1;
            }
            else
            {
                for (a = 1; a < 11; a++)
                    for (b = 0; b < 22; b++)
                        if ((TETRIS[a, b] > 0 && TETRIS[a, b] < 8))
                            TETRIS[a, b] += 8;
                combochk(TETRIS, SC);
                BlockCreating.create(TETRIS);
                CX = 4;
                CY = 1;
            }
            for (a = 1; a < 11; a++)
                for (b = 0; b < 22; b++)
                    if (TETRIS[a, b] >= 21 && TETRIS[a,b] < 28)
                        TETRIS[a, b] -= 20;
            shadow_block(TETRIS, S_TETRIS, BlockCreating);
        }

        public void shadow_block(int[,] TETRIS, int[,] S_TETRIS, BlockCreate BlockCreating)
        {
            int a, b;
            bool impact = false;
            for (int i = 0; i < 12; i++)
                for (int j = 0; j < 23; j++)
                    S_TETRIS[i, j] = TETRIS[i, j];
            do
            {
                for (a = 1; a < 11; a++)
                    for (b = 0; b < 23; b++)
                        if ((S_TETRIS[a, b] > 0 && S_TETRIS[a, b] < 8) && S_TETRIS[a, b + 1] >= 8 && S_TETRIS[a, b + 1] < 28)
                            impact = true;
                if (!impact)
                {
                    bool crash = false;
                    do
                    {
                        crash = false;
                        for (a = 1; a < 11; a++)
                        {
                            for (b = 0; b < 22; b++)
                            {

                                if ((S_TETRIS[a, b] > 0 && S_TETRIS[a, b] < 8) && S_TETRIS[a, b + 1] == 0)
                                {
                                    S_TETRIS[a, b + 1] = S_TETRIS[a, b] + 20;
                                    S_TETRIS[a, b] = 0;
                                }
                            }
                        }
                        for (a = 1; a < 11; a++)
                            for (b = 0; b < 22; b++)
                                if ((S_TETRIS[a, b] > 0 && S_TETRIS[a, b] < 8) && S_TETRIS[a, b + 1] == 0)
                                    crash = true;
                    } while (crash);
                }
                for (a = 1; a < 11; a++)
                    for (b = 0; b < 22; b++)
                        if (S_TETRIS[a, b] >= 21 && S_TETRIS[a, b] < 28)
                            S_TETRIS[a, b] -= 20;
            } while (!impact && !BlockCreating.isover());
        }

        public void line_finder(int[,] TETRIS)
        {
            int a, b;
            for (a=21; a > 0; a--)
            {
                bool chk = true;
                for (b = 1; b < 11; b++)
                    if (TETRIS[b, a] < 8)
                        chk = false;
                if (chk)
                {
                    for (b = 1; b < 12; b++)
                        if (TETRIS[b, a] > 8 && TETRIS[b, a] < 16)
                            TETRIS[b, a] = 16;
                }
            }
        }

        private void combochk(int[,] TETRIS, SCORECLASS SC)
        {
            int a, b;
            bool combochker = false;
            for (a = 21; a > 0; a--)
            {
                bool chk = true;
                for (b = 1; b < 11; b++)
                    if (TETRIS[b, a] < 8)
                        chk = false;
                if (chk)
                {
                    for (b = 1; b < 12; b++)
                        if (TETRIS[b, a] > 8 && TETRIS[b, a] < 16)
                            TETRIS[b, a] = 16;
                    combochker = true;
                }
            }
            if(!combochker)
                SC.linecomboinit();
        }

        public void Holding(int[,] TETRIS, int[,] S_TETRIS, BlockCreate BlockCreating, Blockset BlockSetting)
        {
            int a,b,i=3;
            while (CX <= -1)
                right(TETRIS, S_TETRIS, BlockCreating);
            while (CX >= 9)
                left(TETRIS, S_TETRIS, BlockCreating);
            for (a = 0; a < 3; a++)
                for (b = 0; b < 3; b++)
                    if (TETRIS[CX + a, CY + b] == 2)
                        i=4;
            if (Holded)
            {
                for (a = 0; a < i; a++)
                    for (b = 0; b < i; b++)
                        if ((TETRIS[CX + b, CY + a] > 0 && TETRIS[CX + b, CY + a] < 8))
                            TETRIS[CX + b, CY + a] = 0;
                BlockSetting.change(TETRIS);
            }
            else
            {
                for (a = 0; a < i; a++)
                    for (b = 0; b < i; b++)
                        if ((TETRIS[CX + b, CY + a] > 0 && TETRIS[CX + b, CY + a] < 8))
                            TETRIS[CX + b, CY + a] = 0;
                BlockSetting.change(TETRIS);
                Holded = true;
                BlockCreating.create(TETRIS);
            }
            CX = 4;
            CY = 1;
            shadow_block(TETRIS, S_TETRIS, BlockCreating);
        }

        public int turn(int[,] TETRIS, int[,] S_TETRIS, BlockCreate BlockCreating, bool turnchd, SCORECLASS SC)
        {
            try
            {
                int a, chk = 0;
                bool impact = false;
                bool crash = false;
                bool square = true;
                bool bar = false;
                for (int i = 0; i < 12; i++)
                    for (int j = 0; j < 23; j++)
                        if (TETRIS[i, j] >= 28)
                            TETRIS[i, j] = 0;
                if (CX > -1 && CX < 10)
                {
                    for (a = 0; a < 2; a++)
                        if (TETRIS[CX + a, CY] != 1)
                            square = false;
                    for (a = 0; a < 2; a++)
                        if (TETRIS[CX + a, CY + 1] != 1)
                            square = false;
                }
                else
                    square = false;
                if (square)
                    return 0;
                int r = 0;
                int l = 0;
                int u = 0;
                do
                {
                    impact = false;
                    while (CX <= -1)
                        right(TETRIS, S_TETRIS, BlockCreating);
                    while (CX >= 9)
                        left(TETRIS, S_TETRIS, BlockCreating);
                    for (a = 0; a < 4; a++)
                        for (int b = 0; b < 4; b++)
                            if (TETRIS[CX + a, CY + b] == 2)
                                bar = true;
                    int i = 3;
                    if (bar)
                        i = 4;
                    for (a = 0; a < i; a++)
                    {
                        if (TETRIS[CX, CY + a] > 7 && TETRIS[CX, CY + a] < 28)
                        {
                            chk++;
                            impact = true;
                            right(TETRIS, S_TETRIS, BlockCreating);
                            r++;
                        }
                    }
                    for (a = 0; a < i; a++)
                    {
                        if (TETRIS[CX + i - 1, CY + a] > 7 && TETRIS[CX + i - 1, CY + a] < 28)
                        {
                            chk++;
                            impact = true;
                            left(TETRIS, S_TETRIS, BlockCreating);
                            l++;
                        }
                    }
                    for (a = 0; a < i; a++)
                    {
                        for (int b = 0; b < i; b++)
                        {
                            if (TETRIS[CX + a, CY + b] > 7 && TETRIS[CX + a, CY + b] < 28)
                            {
                                u++;
                                chk++;
                                up(TETRIS, S_TETRIS, BlockCreating);
                                impact = true;
                            }
                        }
                    }
                    if (chk == 2)
                        up(TETRIS, S_TETRIS, BlockCreating);
                    if (chk > 3)
                    {
                        while (i > 0)
                        {
                            right(TETRIS, S_TETRIS, BlockCreating);
                            i--;
                        }
                        while (r > 0)
                        {
                            left(TETRIS, S_TETRIS, BlockCreating);
                            r--;
                        }
                        while (u > 0)
                        {
                            move(TETRIS, S_TETRIS, BlockCreating, SC);
                            u--;
                        }
                        crash = true;
                        impact = false;
                    }
                } while (impact);
                if (!crash)
                {
                    if (bar)
                    {
                        if (turnchd)
                        {
                            int[] temp = { TETRIS[CX, CY], TETRIS[CX + 1, CY], TETRIS[CX + 2, CY], TETRIS[CX + 3, CY] };
                            for (a = 0; a < 4; a++)
                                TETRIS[CX + 3 - a, CY] = TETRIS[CX, CY + a];
                            for (a = 0; a < 4; a++)
                                TETRIS[CX, CY + a] = TETRIS[CX + a, CY + 3];
                            for (a = 0; a < 4; a++)
                                TETRIS[CX + a, CY + 3] = TETRIS[CX + 3, CY + 3 - a];
                            for (a = 0; a < 4; a++)
                                TETRIS[CX + 3, CY + a] = temp[a];
                            temp[0] = TETRIS[CX + 1, CY + 1];
                            TETRIS[CX + 1, CY + 1] = TETRIS[CX + 1, CY + 2];
                            TETRIS[CX + 1, CY + 2] = TETRIS[CX + 2, CY + 2];
                            TETRIS[CX + 2, CY + 2] = TETRIS[CX + 2, CY + 1];
                            TETRIS[CX + 2, CY + 1] = temp[0];
                        }
                        else
                        {
                            int[] temp = { TETRIS[CX, CY], TETRIS[CX + 1, CY], TETRIS[CX + 2, CY], TETRIS[CX + 3, CY] };
                            for (a = 0; a < 4; a++)
                                TETRIS[CX + 3 - a, CY] = TETRIS[CX + 3, CY + 3 - a];
                            for (a = 0; a < 4; a++)
                                TETRIS[CX + 3, CY + 3 - a] = TETRIS[CX + a, CY + 3];
                            for (a = 0; a < 4; a++)
                                TETRIS[CX + a, CY + 3] = TETRIS[CX, CY + a];
                            for (a = 0; a < 4; a++)
                                TETRIS[CX, CY + 3 - a] = temp[a];
                            temp[0] = TETRIS[CX + 1, CY + 1];
                            TETRIS[CX + 1, CY + 1] = TETRIS[CX + 2, CY + 1];
                            TETRIS[CX + 2, CY + 1] = TETRIS[CX + 2, CY + 2];
                            TETRIS[CX + 2, CY + 2] = TETRIS[CX + 1, CY + 2];
                            TETRIS[CX + 1, CY + 2] = temp[0];
                        }
                    }
                    else
                    {
                        if (turnchd)
                        {
                            int[] temp = { TETRIS[CX, CY], TETRIS[CX + 1, CY], TETRIS[CX + 2, CY] };
                            for (a = 0; a < 3; a++)
                                TETRIS[CX + 2 - a, CY] = TETRIS[CX, CY + a];
                            for (a = 0; a < 3; a++)
                                TETRIS[CX, CY + a] = TETRIS[CX + a, CY + 2];
                            for (a = 0; a < 3; a++)
                                TETRIS[CX + a, CY + 2] = TETRIS[CX + 2, CY + 2 - a];
                            for (a = 0; a < 3; a++)
                                TETRIS[CX + 2, CY + a] = temp[a];
                        }
                        else
                        {
                            int[] temp = { TETRIS[CX, CY], TETRIS[CX + 1, CY], TETRIS[CX + 2, CY] };
                            for (a = 0; a < 3; a++)
                                TETRIS[CX + a, CY] = TETRIS[CX + 2, CY + a];
                            for (a = 0; a < 3; a++)
                                TETRIS[CX + 2, CY + a] = TETRIS[CX + 2 - a, CY + 2];
                            for (a = 0; a < 3; a++)
                                TETRIS[CX + 2 - a, CY + 2] = TETRIS[CX, CY + 2 - a];
                            for (a = 0; a < 3; a++)
                                TETRIS[CX, CY + 2 - a] = temp[a];
                        }
                    }
                }
                shadow_block(TETRIS, S_TETRIS, BlockCreating);
                return 0;
            }
            catch
            {
                return 0;
            }
        }
        private void up(int[,] TETRIS, int[,] S_TETRIS, BlockCreate BlockCreating)
        {
            int a, b;
            bool impact = false;
            for (int i = 0; i < 12; i++)
                for (int j = 0; j < 23; j++)
                    if (TETRIS[i, j] >= 28)
                        TETRIS[i, j] = 0;
            for (a = 1; a < 11; a++)
                for (b = 0; b < 22; b++)
                    if ((TETRIS[a, b] > 0 && TETRIS[a, b] < 8) && TETRIS[a, b - 1] >= 8 && TETRIS[a, b - 1] < 28)
                        impact = true;
            if (!impact)
            {
                bool crash = false;
                do
                {
                    crash = false;
                    for (a = 1; a < 11; a++)
                    {
                        for (b = 0; b < 22; b++)
                        {
                            if ((TETRIS[a, b] > 0 && TETRIS[a, b] < 8) && TETRIS[a, b - 1] == 0)
                            {
                                TETRIS[a, b - 1] = TETRIS[a, b] + 20;
                                TETRIS[a, b] = 0;
                            }
                        }
                    }
                    for (a = 1; a < 11; a++)
                        for (b = 0; b < 22; b++)
                            if ((TETRIS[a, b] > 0 && TETRIS[a, b] < 8) && TETRIS[a, b - 1] == 0)
                                crash = true;
                } while (crash);
                CY -= 1;
                for (a = 1; a < 11; a++)
                    for (b = 0; b < 22; b++)
                        if (TETRIS[a, b] >= 21 && TETRIS[a, b] < 28)
                            TETRIS[a, b] -= 20;
                shadow_block(TETRIS, S_TETRIS, BlockCreating);
            }
        }
        public void left(int[,] TETRIS, int[,] S_TETRIS, BlockCreate BlockCreating)
        {
            int a, b;
            bool impact = false;
            for (int i = 0; i < 12; i++)
                for (int j = 0; j < 23; j++)
                    if (TETRIS[i, j] >= 28)
                        TETRIS[i, j] = 0;
            for (a = 1; a < 11; a++)
                for (b = 0; b < 22; b++)
                    if ((TETRIS[a, b] > 0 && TETRIS[a, b] < 8) && TETRIS[a - 1, b] >= 8 && TETRIS[a - 1, b] < 28)
                        impact = true;
            if (!impact)
            {
                bool crash = false;
                do
                {
                    crash = false;
                    for (a = 1; a < 11; a++)
                    {
                        for (b = 0; b < 22; b++)
                        {
                            if ((TETRIS[a, b] > 0 && TETRIS[a, b] < 8) && TETRIS[a - 1, b] == 0)
                            {

                                TETRIS[a - 1, b] = TETRIS[a, b] + 20;
                                TETRIS[a, b] = 0;
                            }
                        }
                    }
                    for (a = 1; a < 11; a++)
                        for (b = 0; b < 22; b++)
                            if ((TETRIS[a, b] > 0 && TETRIS[a, b] < 8) && TETRIS[a - 1, b] == 0)
                                crash = true;
                } while (crash);
                CX -= 1;
                for (a = 1; a < 11; a++)
                    for (b = 0; b < 22; b++)
                        if (TETRIS[a, b] >= 21 && TETRIS[a, b] < 28)
                            TETRIS[a, b] -= 20;
                shadow_block(TETRIS, S_TETRIS, BlockCreating);
            }
        }
        public void right(int[,] TETRIS, int[,] S_TETRIS, BlockCreate BlockCreating)
        {
            int a, b;
            bool impact = false;
            for (int i = 0; i < 12; i++)
                for (int j = 0; j < 23; j++)
                    if (TETRIS[i, j] >= 28)
                        TETRIS[i, j] = 0;
            for (a = 1; a < 11; a++)
                for (b = 0; b < 22; b++)
                    if ((TETRIS[a, b] > 0 && TETRIS[a, b] < 8) && TETRIS[a + 1, b] >= 8 && TETRIS[a + 1, b] < 28)
                        impact = true;
            if (!impact)
            {
                bool crash = false;
                do
                {
                    crash = false;
                    for (a = 1; a < 11; a++)
                    {
                        for (b = 0; b < 22; b++)
                        {
                            if ((TETRIS[a, b] > 0 && TETRIS[a, b] < 8) && TETRIS[a + 1, b] == 0)
                            {
                                TETRIS[a + 1, b] = TETRIS[a, b] + 20;
                                TETRIS[a, b] = 0;
                            }
                        }
                    }
                    for (a = 1; a < 11; a++)
                        for (b = 0; b < 22; b++)
                            if ((TETRIS[a, b] > 0 && TETRIS[a, b] < 8) && TETRIS[a + 1, b] == 0)
                                crash = true;
                } while (crash);
                CX += 1;
                for (a = 1; a < 11; a++)
                    for (b = 0; b < 22; b++)
                        if (TETRIS[a, b] >= 21 && TETRIS[a, b] < 28)
                            TETRIS[a, b] -= 20;
                shadow_block(TETRIS, S_TETRIS, BlockCreating);
            }
        }
        public void space(int[,] TETRIS, SCORECLASS SC, BlockCreate BlockCreating, int[,] S_TETRIS, bool isshadowused)
        {
            int a, b;
            bool impact = false;
            for (int i = 0; i < 12; i++)
                for (int j = 0; j < 23; j++)
                    if (TETRIS[i, j] >= 28)
                        TETRIS[i, j] = 0;
            do
            {
                for (a = 1; a < 11; a++)
                    for (b = 0; b < 23; b++)
                        if ((TETRIS[a, b] > 0 && TETRIS[a, b] < 8) && TETRIS[a, b + 1] >= 8 && TETRIS[a, b + 1] < 28)
                            impact = true;
                if (!impact)
                {
                    bool crash = false;
                    do
                    {
                        crash = false;
                        for (a = 1; a < 11; a++)
                        {
                            for (b = 0; b < 22; b++)
                            {

                                if ((TETRIS[a, b] > 0 && TETRIS[a, b] < 8) && TETRIS[a, b + 1] == 0)
                                {
                                    TETRIS[a, b + 1] = TETRIS[a, b] + 20;
                                    TETRIS[a, b] = 0;
                                    SC.downscore(isshadowused);
                                }
                            }
                        }
                        for (a = 1; a < 11; a++)
                            for (b = 0; b < 22; b++)
                                if ((TETRIS[a, b] > 0 && TETRIS[a, b] < 8) && TETRIS[a, b + 1] == 0)
                                    crash = true;
                    } while (crash);
                    CY += 1;
                }
                else
                {
                    for (a = 1; a < 11; a++)
                        for (b = 0; b < 22; b++)
                            if ((TETRIS[a, b] > 0 && TETRIS[a, b] < 8))
                                TETRIS[a, b] += 8;
                    combochk(TETRIS, SC);
                    BlockCreating.create(TETRIS);
                    CX = 4;
                    CY = 1;
                }
                for (a = 1; a < 11; a++)
                    for (b = 0; b < 22; b++)
                        if (TETRIS[a, b] >= 21 && TETRIS[a, b] < 28)
                            TETRIS[a, b] -= 20;
            } while (!impact);
            shadow_block(TETRIS, S_TETRIS, BlockCreating);
        }
    }
}
