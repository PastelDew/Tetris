using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Tetris_Project
{
    public class RankingClass
    {
        static string rankstr = "";
        static string[] name = new string[10];
        static string[] playtime = new string[10];
        static int[] level = new int[10];
        static int[] lines = new int[10];
        static int[] score = new int[10];
        static double[] totalscore = new double[10];
        StreamReader sreader;
        public RankingClass()
        {
            try
            {
                FileInfo title = new FileInfo(@"./Rankers.INF");
                if (!title.Exists)
                {
                    FileStream t = title.Create();
                    t.Close();
                }
                sreader = new StreamReader(@"./rankers.INF", Encoding.UTF8);
                rankstr = sreader.ReadToEnd();
                if (rankstr == "")
                {
                    for (int i = 0; i < 10; i++)
                        rankstr += "---,00:00,01,000,0000,0000\n";
                }
                int j = 0, k = 0;
                for (int i = 0; i < 10; i++)
                {
                    k = rankstr.IndexOf(',', j);
                    name[i] = rankstr.Substring(j, k - j);
                    j = k + 1;
                    k = rankstr.IndexOf(',', j);
                    playtime[i] = rankstr.Substring(j, k - j);
                    j = k + 1;
                    k = rankstr.IndexOf(',', j);
                    level[i] = int.Parse(rankstr.Substring(j, k - j));
                    j = k + 1;
                    k = rankstr.IndexOf(',', j);
                    lines[i] = int.Parse(rankstr.Substring(j, k - j));
                    j = k + 1;
                    k = rankstr.IndexOf(',', j);
                    score[i] = int.Parse(rankstr.Substring(j, k - j));
                    j = k + 1;
                    k = rankstr.IndexOf('\n', j);
                    totalscore[i] = double.Parse(rankstr.Substring(j, k - j));
                    j = k + 1;
                }
                sreader.Close();
                StreamWriter SWriter = new StreamWriter(@"./Rankers.INF", false, Encoding.UTF8);
                SWriter.Write(rankstr);
                SWriter.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void call_ranking()
        {
            try
            {
                FileInfo title = new FileInfo(@"./Rankers.INF");
                if (!title.Exists)
                {
                    FileStream t = title.Create();
                    t.Close();
                }
                sreader = new StreamReader(@"./rankers.INF", Encoding.UTF8);
                rankstr = sreader.ReadToEnd();
                if (rankstr == "")
                {
                    for (int i = 0; i < 10; i++)
                        rankstr += "---,00:00,01,000,0000,0000\n";
                }
                int j = 0, k = 0;
                for (int i = 0; i < 10; i++)
                {
                    k = rankstr.IndexOf(',', j);
                    name[i] = rankstr.Substring(j, k - j);
                    j = k + 1;
                    k = rankstr.IndexOf(',', j);
                    playtime[i] = rankstr.Substring(j, k - j);
                    j = k + 1;
                    k = rankstr.IndexOf(',', j);
                    level[i] = int.Parse(rankstr.Substring(j, k - j));
                    j = k + 1;
                    k = rankstr.IndexOf(',', j);
                    lines[i] = int.Parse(rankstr.Substring(j, k - j));
                    j = k + 1;
                    k = rankstr.IndexOf(',', j);
                    score[i] = int.Parse(rankstr.Substring(j, k - j));
                    j = k + 1;
                    k = rankstr.IndexOf('\n', j);
                    totalscore[i] = int.Parse(rankstr.Substring(j, k - j));
                    j = k + 1;
                }
                sreader.Close();
                StreamWriter SWriter = new StreamWriter(@"./Rankers.INF", false, Encoding.UTF8);
                SWriter.Write(rankstr);
                SWriter.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void ranking_read()
        {
            RankingUpdate RU = new RankingUpdate(rankstr, name, playtime, level, lines, score, totalscore);
            for (int i = 0; i < 10; i++)
                RU.viewdata(i,name[i],playtime[i],level[i],lines[i],score[i],totalscore[i]);
            RU.ShowDialog();
        }
        public void upload(int rank, int pt, int lev, int lin, int sco, double tot)
        {
            RankingUpdate RU = new RankingUpdate(rankstr, name, playtime, level, lines, score, totalscore);
            for (int i = 9; i > rank; i--)
            {
                name[i] = name[i - 1];
                playtime[i] = playtime[i - 1];
                level[i] = level[i - 1];
                lines[i] = lines[i - 1];
                score[i] = score[i - 1];
                totalscore[i] = totalscore[i - 1];
            }
            for (int i = 0; i < rank; i++)
                RU.viewdata(i,name[i], playtime[i], level[i], lines[i], score[i], totalscore[i]);
            //////////////////////////////////////////
            int a, b;
            string temp = "";
            if (pt % 100 > 59)
                pt += 40;
            a = pt / 100;
            b = pt % 100;
            if (a > 9)
                temp += a;
            else
                temp += "0" + a;
            temp += ":";
            if (b > 9)
                temp += b;
            else
                temp += "0" + b;
            //////////////////////////////////////////
            RU.wrightform(rank,temp,lev,lin,sco,tot);
            for (int i = rank+1; i < 10; i++)
                RU.viewdata(i,name[i], playtime[i], level[i], lines[i], score[i], totalscore[i]);
            RU.ShowDialog();
        }
        public int isranker(int total)
        {
            for(int i = 0;i<10;i++)
                if (total > totalscore[i])
                {
                    return i+1;
                }
            return 0;
        }
    }
}
