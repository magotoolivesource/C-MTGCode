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


        public override void Init()
        {
            base.Init();

            //BoardTile = new int[4][];
            //BoardTile[0] = new int[4];
            //BoardTile[0][0] = 0;
            //BoardTile[0][1] = 2;
            //BoardTile[1] = new int[4];

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

            int posx = DefaultGameSource.Range(GRIDSIZE);
            int posy = DefaultGameSource.Range(GRIDSIZE);

            m_Buffer.Draw("2", (posx * 4) + (posx + 1)
                , (posy * 3) + ( posy + 1 )
                , (short)ConsoleColor.Red );
            
            //Console.SetCursorPosition(6, 9);
            //Console.Write("2");

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
