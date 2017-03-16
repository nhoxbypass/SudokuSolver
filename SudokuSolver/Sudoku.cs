using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SudokuSolver
{
    class Sudoku
    {
        
        private const int Rows = 9;
        private const int Cols = 9;
        public bool isWin = false;
        
        public int[,] ResArr = new int[9, 9];
      
        public void DrawBoard(Graphics g, Pen p)
        {
            int CountCol = 0, CountRow = 0;
            for(int i = 0; i < 10; i++)
            {
                if (i % 3 == 0)
                {
                    g.DrawLine(p, i * 60 + CountCol * 3, 0, i * 60 + CountCol * 3, 560);
                    g.DrawLine(p, i * 60 + CountCol * 3 + 1, 0, i * 60 + CountCol * 3 + 1, 560);
                    g.DrawLine(p, i * 60 + CountCol * 3 + 2, 0, i * 60 + CountCol * 3 + 2, 560);
                    CountCol++;
                }
                else
                {
                    g.DrawLine(p, i * 60 + CountCol * 3, 0, i * 60 + CountCol * 3, 560);
                }
            }

            for (int i = 0; i < 10; i++)
            {
                if (i % 3 == 0)
                {
                    g.DrawLine(p, 0, i * 60 + CountRow * 3, 560, i * 60 + CountRow * 3);
                    g.DrawLine(p, 0, i * 60 + CountRow * 3 + 1, 560, i * 60 + CountRow * 3 + 1);
                    g.DrawLine(p, 0, i * 60 + CountRow * 3 + 2, 560, i * 60 + CountRow * 3 + 2);
                    CountRow++;
                }
                else
                {
                    g.DrawLine(p, 0, i * 60 + CountRow * 3, 560, i * 60 + CountRow * 3);
                }
            }

       }

        public bool CheckValid(int[,] Arr)
        {
            
           
           

            //Duyet dong
            for (int i = 0; i < 9; i++ )
            {
                int[] Exist1 = new int[10];
                for(int j = 0; j < 9; j++)
                {
                    Exist1[Arr[i, j]]++;
                }
                for(int k = 1; k <= 9; k++)
                {
                    if (Exist1[k] > 1) return false;
                }
            }

            //Duyet cot
            for (int i = 0; i < 9; i++)
            {
                int[] Exist1 = new int[10];
                for (int j = 0; j < 9; j++)
                {
                    Exist1[Arr[j, i]]++;
                }
                for (int k = 1; k <= 9; k++)
                {
                    if (Exist1[k] > 1) return false;
                }
            }

            //duyet tung o nho
            for (int i = 0; i < 9; i += 3)
            {
                //duyet lan luot 3 dong, sau do trong 3 dong duyet lan luot tung 3 cot * 3 lần -> se duyet het dc 3 o nho cua 1 dong
                for (int k = 0; k < 9; k += 3)
                {
                    //Duyet 3 dong trong 1 lan.
                    int[] found3 = new int[10];
                    for (int iRow = i; iRow < i + 3; iRow++)
                    {

                        for (int iCol = k; iCol < k + 3; iCol++)
                        {
                            
                            found3[Arr[iRow, iCol]]++;

                        }
                    }

                    //kiem tra
                    for (int m = 1; m <= 9; m++)
                    {
                        if (found3[m] > 1)
                        {
                            return false;
                        }
                    }
                }
            }

                return true;
        }

        public bool CheckWin(int[,] Arr)
        {
           
            //duyet dong
            for(int i = 0; i < 9; i++)
            {
                int[] found = new int[9];
                for(int j = 0; j < 9; j++)
                {
                    if (Arr[i, j] == 0) return false;
                    found[Arr[i, j] - 1]++;
                }
                for(int k = 0; k < 9; k++)
                {
                    if(found[k] == 0)
                    {
                        return false;
                    }
                }

            }

            //duyet cot
            for (int i = 0; i < 9; i++)
            {
                int[] found = new int[9];
                for (int j = 0; j < 9; j++)
                {
                    if (Arr[j, i] == 0) return false;
                    found[Arr[j, i] - 1]++;
                }
                for (int k = 0; k < 9; k++)
                {
                    if (found[k] == 0)
                    {
                        return false;
                    }
                }

            }



//             //duyet duong cheo
//             
//                 int[] found1 = new int[9];
//                 int[] found2 = new int[9];
//                 for (int j = 0; j < 9; j++)
//                 {
//                     found1[Arr[j, j] - 1]++;
//                     found2[Arr[j, 9 - 1 - j] - 1]++;
//                 }
//                 for (int k = 0; k < 9; k++)
//                 {
//                     if (found1[k] == 0)
//                     {
//                         return false;
//                     }
//                     if (found2[k] == 0)
//                         return false;
//                 }

           //duyet tung o nho
                for (int i = 0; i < 9; i+= 3 )
                {
                    //duyet lan luot 3 dong, sau do trong 3 dong duyet lan luot tung 3 cot * 3 lần -> se duyet het dc 3 o nho cua 1 dong
                    for (int k = 0; k < 9; k += 3)
                    {
                        //Duyet 3 dong trong 1 lan.
                        int[] found3 = new int[9];
                        for (int iRow = i; iRow < i + 3; iRow++)
                        {
                            
                            for (int iCol = k; iCol < k + 3; iCol++)
                            {
                                if (Arr[iRow, iCol] == 0) return false;
                                found3[Arr[iRow, iCol] - 1]++;

                            }
                        }

                        //kiem tra
                        for (int m = 0; m < 9; m++)
                        {
                            if (found3[m] == 0)
                            {
                                return false;
                            }
                        }
                    }
                }


                    //win
                    return true;
        }

        public void FindWay(int[,] Arr, int x, int y)
        {

            if (isWin) return;
            
            int tmp = Arr[x, y];
            int[] locate = FindNumberAt(Arr, x, y);
            if (!isWin)
            {


                if (CountElementInArr(locate, 0) == 9 && !isWin)
                {
                    if (x < 9 && y < 8 && !isWin)
                    {
                        FindWay(Arr, x, y + 1);
                    }
                    else if (x < 8 && y == 8 && !isWin)
                    {
                        FindWay(Arr, x + 1, 0);
                    }
                    else if (x == 8 && y == 8)
                    {
                        
                        if (CheckWin(Arr))
                        {
                            isWin = true;
                            
                            ResArr = Arr;
                            return;
                        }
                    }
                }
            }

            if (!isWin)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (locate[i] >= 1 && !isWin)
                    {

                        Arr[x, y] = i + 1;
                       
                        if (x < 9 && y < 8 && !isWin)
                        {
                            FindWay(Arr, x, y + 1);
                        }
                        else if (x < 8 && y == 8 && !isWin)
                        {
                            FindWay(Arr, x + 1, 0);
                        }
                        else if (x == 8 && y == 8)
                        {

                            if (CheckWin(Arr))
                            {
                                isWin = true;
                                
                                ResArr = Arr;
                                return;
                            }
                        }
                        /* System.Windows.Forms.MessageBox.Show("Backtrack");*/
                        if (!isWin) Arr[x, y] = tmp;
                    }
                }
            }

            if (isWin) return;
            
            
        }

        public int PointCalc(int Remain_min, int Remain_sec, int Mode)
        {
            int Points = 0;
            Points = Remain_min * (Mode - 1)*10 + Remain_sec;
            return Points;
        }
        //Additional func
        private int CountElementInArr(int[] Arr, int ele)
        {
            int _Count = 0;
            for(int i = 0; i < Arr.Length; i++)
            {
                if (Arr[i] == ele)
                    _Count++;
            }
            return _Count;
        }

        private int[] FindNumberAt(int[,] Arr, int x, int y)
        {
            int[] res = new int[9];
            if (Arr[x, y] != 0) return res;

            //Duyet hang
            for(int i = 1; i <= 9; i++)
            {
                bool found = false;
                for(int j = 0; j < 9; j++)
                {
                    if (Arr[x, j] == i)
                    {
                        found = true;
                        res[i - 1] = -10;
                        break;
                    }
                }
                if (!found) res[i - 1]++;
            }

            //Duyet cot
            for (int i = 1; i <= 9; i++)
            {
                bool found = false;
                for (int j = 0; j < 9; j++)
                {
                    if (Arr[j, y] == i)
                    {
                        found = true;
                        res[i - 1] = -10;
                        break;
                    }
                }
                if (!found) res[i - 1]++;
            }

//             //Duyet duong cheo
//             if(x == y)
//             {
//                 for (int j = 1; j <= 9; j++)
//                 {
//                     bool found = false;
//                     for (int i = 0; i < 9; i++)
//                     {
// 
//                         if (j == Arr[i, i])
//                         {
//                             found = true;
//                             res[j - 1] = -10;
//                             break;
//                             
//                         }
//                     }
//                     if (!found) res[j - 1]++;
//                 }
//             }
//             else if(x == 9 - 1 - y || y == 9 - 1 - x)
//             {
//                 for (int j = 1; j <= 9; j++)
//                 {
//                     bool found = false;
//                     for (int i = 0; i < 9; i++)
//                     {
// 
//                         if (j == Arr[i, 9 - 1 - i])
//                         {
//                             found = true;
//                             res[j - 1] = -10;
//                             break;
//                         }
//                     }
//                     if (!found) res[j - 1]++;
//                 }
//             }


            //Duyet o nho
            int startX = x / 3;
            int startY = y / 3;
            
                    
                    for (int iNumber = 1; iNumber <= 9; iNumber++)
                    {
                        bool found = false;
                        for (int iRow = 3 * startX; iRow < 3 * startX + 3; iRow++)
                        {

                            for (int iCol = 3 * startY; iCol < 3 * startY + 3; iCol++)
                            {
                                if (iNumber == Arr[iRow, iCol])
                                {
                                    found = true;
                                    res[iNumber - 1] = -10;
                                }

                            }
                        }
                        //kiem tra
                        if (!found) res[iNumber - 1]++;
                  
            }


            /*int max = 1;*/
            //lay truong hop max
//             for(int i = 2; i <= 9; i++)
//             {
//                 if((res[i] > res[max] && res[i] !=0) || (res[max] == 0 && res[i] != 0))
//                 {
//                     max = i;
//                 }
//             }
//             if (res[max] != 0) return max;
//             else return 0;

            return res;
        }
    }
}
