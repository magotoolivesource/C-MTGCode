using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InGameEngine;


namespace MTG_2048
{
    class MainGameSource : DefaultGameSource 
    {
        //List<int> testint = new List<int>();


        //int[][] BoardTile = new int[4][]
        //{
        //    new int[4]
        //    {
        //        0,0,0,0
        //    }
        //    , new int[4]
        //    {
        //        0,0,0,0
        //    }
        //    , new int[4]
        //    {
        //        0,0,0,0
        //    }
        //    , new int[4]
        //    {
        //        0,0,0,0
        //    }
        //};

        const int GRIDSIZE = 4;
        int[,] BoardTile = new int[GRIDSIZE, GRIDSIZE]
        {
              {0,0,0,0 }
            , {0,0,0,0 }
            , {0,0,0,0 }
            , {0,0,0,0 }
        };


        #region 랜덤값 찾기용 소스
        //// 0, 1, 2, 3  -> 인덱스
        //// 2, 0, 0, 0  -> 실제값
        //int[] TempArray = new int[GRIDSIZE]; 
        //int m_TempInputSize = 0;

        //List<int> m_TempArray3 = new List<int>();
        //List<int> m_TempArray4 = new List<int>();

        //void InitRandomTile()
        //{
        //    for (int i = 0; i < TempArray.Length; i++)
        //    {
        //        m_TempArray4.Add(i); // 0, 1, 2, 3
        //    }
        //}
        //void AddRandomTile4()
        //{
        //    for (int i = 0; i < 3; i++)
        //    {
        //        if (m_TempArray4.Count <= 0)
        //        {
        //            return;
        //        }

        //        int randomindex = DefaultGameSource.Range(m_TempArray4.Count); // 1
        //        int arrindex = m_TempArray4[randomindex];
        //        TempArray[arrindex] = 2;

        //        m_TempArray4.RemoveAt(randomindex); // 0, 2, 3 -> 0, 3
        //    }

        //}

        //void AddRandomTime3()
        //{
        //    for (int x = 0; x < 3; x++)
        //    {
        //        m_TempArray3.Clear();
        //        for (int i = 0; i < TempArray.Length; i++)
        //        {
        //            if (TempArray[i] == 0)
        //            {
        //                m_TempArray3.Add(i);
        //            }
        //        }

        //        if(m_TempArray3.Count <= 0)
        //            break;

        //        int randomindex = DefaultGameSource.Range(m_TempArray3.Count);
        //        TempArray[randomindex] = 2;
        //    }

        //}

        //void AddRandomTile2()
        //{
        //    int temparray = 0;
        //    int[] m_TempArray2 = new int[GRIDSIZE];
        //    for (int i = 0; i < TempArray.Length; i++)
        //    {
        //        m_TempArray2[i] = -1;

        //        if (TempArray[i] == 0)
        //        {
        //            m_TempArray2[temparray] = i; // 1, 2, 3, -1
        //            temparray++;
        //        }
        //    }

        //    if( temparray <= 0 )
        //    {
        //        // 에러 코드 적용하기
        //        return;
        //    }

        //    int index = DefaultGameSource.Range(0, temparray);
        //    int arrindex = m_TempArray2[index];
        //    TempArray[arrindex] = 2;
        //}

        //// 중복 되지 않는 랜덤 타일 적용하기
        //void AddRandTile0()
        //{

        //    if(m_TempInputSize >= GRIDSIZE)
        //    {
        //        return;
        //    }

        //    int temparray = 0;
        //    while(true)
        //    {
        //        ++temparray;
        //        int randx = DefaultGameSource.Range(GRIDSIZE);
        //        if(TempArray[randx] == 0 )
        //        {
        //            TempArray[randx] = 2;
        //            m_TempInputSize++;
        //            break;
        //        }

        //        if(temparray >= 99999 )
        //        {
        //            break;
        //        }

        //    }


        //    //int randx = DefaultGameSource.Range(GRIDSIZE);
        //    //int randy = DefaultGameSource.Range(GRIDSIZE);

        //    //BoardTile[randy, randx] = DefaultGameSource.Range(2) == 0 ? 2 : 4;
        //}

        #endregion



        class POINT2D
        {
            public POINT2D(int p_x, int p_y)
            {
                x = p_x;
                y = p_y;
            }
            public int x;
            public int y;

        }

        
        // 중복 되지 않는 랜덤 타일 적용하기
        bool AddRandTile()
        {
            List<int> tempindexarr = new List<int>();
            List<POINT2D> tempindexarr2 = new List<POINT2D>();
            for (int y = 0; y < GRIDSIZE; y++)
            {
                for (int x = 0; x < GRIDSIZE; x++)
                {
                    if(BoardTile[y, x] == 0 )
                    {
                        tempindexarr2.Add(new POINT2D(x, y));
                        tempindexarr.Add(y * GRIDSIZE + x);
                    }
                }
            }

            if (tempindexarr.Count <= 0)
            {
                return false;
            }

            // 방법2
            int randindex = DefaultGameSource.Range(tempindexarr.Count);
            int val = tempindexarr[randindex];
            int indexy = (int)(val / GRIDSIZE); //  6 -> 1
            int indexx = val % GRIDSIZE; // 6%4 -> 2
            // 방법2


            if (tempindexarr2.Count <= 0)
            {
                // 게임오버가 되도록
                return false;
            }

            randindex = DefaultGameSource.Range(tempindexarr2.Count);
            POINT2D temppos = tempindexarr2[randindex];
            BoardTile[temppos.y, temppos.x] = 2;

            return true;
        }



        public override void Init()
        {
            base.Init();

            //BoardTile = new int[4][];
            //BoardTile[0] = new int[4];
            //BoardTile[0][0] = 0;
            //BoardTile[0][1] = 2;
            //BoardTile[1] = new int[4];

            
            AddRandTile();
            AddRandTile();
            AddRandTile();
            AddRandTile();
            AddRandTile();
            AddRandTile();
            AddRandTile();
            AddRandTile();
            AddRandTile();
            AddRandTile();
            AddRandTile();
            AddRandTile();
            AddRandTile();
            AddRandTile();
            AddRandTile();
            AddRandTile();
            AddRandTile();
            AddRandTile();
            AddRandTile();

        }

        void DrawStage()
        {
            m_Buffer.Draw("+----+----+----+----+", 0, 0);
            m_Buffer.Draw("|    |    |    |    |", 0, 1);
            m_Buffer.Draw("|    |    |    |    |", 0, 2);
            m_Buffer.Draw("|    |    |    |    |", 0, 3);
            m_Buffer.Draw("+----+----+----+----+", 0, 4);
            m_Buffer.Draw("|    |    |    |    |", 0, 5);
            m_Buffer.Draw("|    |    |    |    |", 0, 6);
            m_Buffer.Draw("|    |    |    |    |", 0, 7);
            m_Buffer.Draw("+----+----+----+----+", 0, 8);
            m_Buffer.Draw("|    |    |    |    |", 0, 9);
            m_Buffer.Draw("|    |    |    |    |", 0, 10);
            m_Buffer.Draw("|    |    |    |    |", 0, 11);
            m_Buffer.Draw("+----+----+----+----+", 0, 12);
            m_Buffer.Draw("|    |    |    |    |", 0, 13);
            m_Buffer.Draw("|    |    |    |    |", 0, 14);
            m_Buffer.Draw("|    |    |    |    |", 0, 15);
            m_Buffer.Draw("+----+----+----+----+", 0, 16);

        }


        int m_RandPosx = 0;
        int m_RandPosy = 0;

        Random rand = new Random();

        void DrawBlock()
        {
            //MainGameSource: DefaultGameSource

            // as 참고용 소스  ------------------
            //MainGameSource test = this;
            //dynamiccast<>()
            //bool source = this is InGameEngine.buffer;
            // as 참고용 소스 ------------------


            //Random rand = new Random();
            //int posx = rand.Next(0, 4);
            //int posy = rand.Next(0, 4);


            //int posx = rand.Next(GRIDSIZE);// DefaultGameSource.Range(GRIDSIZE);
            //int posy = DefaultGameSource.Range(GRIDSIZE);

            //m_Buffer.Draw("2", (posx * 4) + (posx + 1)
            //    , (posy * 3) + ( posy + 1 )
            //    , (short)ConsoleColor.Red );

            //Console.SetCursorPosition(6, 9);
            //Console.Write("2");




            int ysize = BoardTile.GetLength(0);
            int xsize = BoardTile.GetLength(1);

            int posx = 0;
            int posy = 0;
            for (int y = 0; y < ysize; y++)
            {
                for (int x = 0; x < xsize; x++)
                {
                    if( BoardTile[y, x] != 0 )
                    {
                        posx = (x * GRIDSIZE) + (x + 1);
                        posy = (y * (GRIDSIZE - 1)) + (y + 1);

                        //string tempstr = string.Format( "{0}", BoardTile[y, x]);
                        m_Buffer.Draw(BoardTile[y, x].ToString() , posx, posy );
                    }
                    

                }
            }


        }

        void DrawHelper()
        {
            m_Buffer.Draw("2048InGame Ver 0.0.1", 40, 10);
        }

        protected override void LoopDraw()
        {
            DrawStage();
            DrawBlock();
            DrawHelper();
            //m_Buffer.Draw("2048InGame Ver 0.0.1", 40, 10);

        }

        protected override void LoopInputFN()
        {
            
        }
    }
}
