﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

/// <summary>
/// 어떤 블럭을 만들것이냐?
/// 어떤 블럭은 어떤 데이터로 들고 있을것이냐
/// 
/// 블럭이 떨어진다
///     좌우이동
///     회전
///     
/// 블럭이 채워지면 블럭이 사라진다.
///     한줄 블럭이 사라지면 그 블럭에 있는 상단 오브젝트들을 내려오게한다
///     여러 블럭일때 상단 오브젝트들을 한줄씩 내려오도록 한다
///     
/// </summary>
namespace MTG_Tetris
{

    public class BlockTypeData
    {
        public class BlockElementData
        {
            public int[,] BlockData = null;
        }

        public BlockElementData[] AllBlockData = null;

        



        //public int[,,] BlockArray = new int[4, 4, 4];
        //{
        //    {
        //        { 0, 0, 0, 0 }
        //        , { 1, 1, 1, 1 }
        //        , { 0, 0, 0, 0 }
        //        , { 0, 0, 0, 0 }
        //    }
        //    , {
        //          { 0, 0, 1, 0 }
        //        , { 0, 0, 1, 0 }
        //        , { 0, 0, 1, 0 }
        //        , { 0, 0, 1, 0 }
        //    }
        //    , {
        //          { 0, 0, 0, 0 }
        //        , { 0, 0, 0, 0 }
        //        , { 1, 1, 1, 1 }
        //        , { 0, 0, 0, 0 }
        //    }
        //    , {
        //          { 0, 1, 0, 0 }
        //        , { 0, 1, 0, 0 }
        //        , { 0, 1, 0, 0 }
        //        , { 0, 1, 0, 0 }
        //    }
        //};


    }


    public class Tertris
    {



        const int STAGEHEIGHT = 20;
        const int STAGEWIDTH = 10;

        int DelayTic = 1000;

        int NextTic = 0;

        public enum E_BLOCKTYPE
        {
            None = 0,
            BLOCK,
        }

        public int[,] StageArray = new int[STAGEHEIGHT, STAGEWIDTH];


        public BlockTypeData[] AllBlockTypeDataArray = new BlockTypeData[2];


        void StageDraw()
        {
            
            int len = StageArray.Length;
            int widthsize = StageArray.GetLength(1);
            int heightsize = StageArray.GetLength(0);

            for (int y = 0; y < heightsize; y++)
            {
                for (int x = 0; x < StageArray.GetLength(1); x++)
                {
                    if (StageArray[y, x] == (int)E_BLOCKTYPE.None)
                    {
                        Console.SetCursorPosition(x * 2, y);
                        Console.Write("·");
                    }
                    else if ((int)E_BLOCKTYPE.BLOCK == StageArray[y, x])
                    {
                        Console.SetCursorPosition(x * 2, y);
                        Console.Write("■");
                    }
                }
            }
        }

        int m_BlockPosY = 0;
        int m_BlockPosX = 0;
        int m_RotType = 0; // 0 이면
        void BlockDraw()
        {
            int widthsize = BlockArray.GetLength(2);
            int heightsize = BlockArray.GetLength(1);
            int tempy = m_BlockPosY;
            int tempx = m_BlockPosX;
            for (int y = 0; y < heightsize; y++)
            {
                for (int x = 0; x < widthsize; x++)
                {
                    if( 1==  BlockArray[m_RotType, y, x]  )
                    {
                        Console.SetCursorPosition( (x + tempx) * 2
                            , y + tempy);
                        Console.Write("■");
                    }
                    
                }
            }

        }
        public void Draw()
        {
            StageDraw();
            BlockDraw();


        }

        void UpdateLogic()
        {
            // 1초에 한번 
            //Environment.TickCount; // 윈도우 시작 했을때의 시간

            int currenttic = Environment.TickCount;
            bool isflag = false;
            if( NextTic <= currenttic)
            {
                NextTic = currenttic + DelayTic;
                isflag = true;
            }
            

            if (isflag)
            {
                m_BlockPosY++;
                if (m_BlockPosY >= 20)
                {
                    m_BlockPosY = 0;
                }
            }
        }

        void UpdateInput()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo info = Console.ReadKey();
                ConsoleKey keyval = info.Key;

                if (keyval == ConsoleKey.RightArrow
                    || keyval == ConsoleKey.D)
                {
                    m_BlockPosX++;

                    if (m_BlockPosX >= 6)
                    {
                        m_BlockPosX = 6;
                    }
                }

                if (keyval == ConsoleKey.LeftArrow
                    || keyval == ConsoleKey.A)
                {
                    m_BlockPosX--;

                    if (m_BlockPosX < 0)
                    {
                        m_BlockPosX = 0;
                    }
                }

                if( keyval == ConsoleKey.UpArrow
                    || keyval == ConsoleKey.W)
                {
                    m_RotType = (m_RotType + 1) % 4;

                }



            }
            

        }

        public int GameLogic()
        {
            UpdateInput();

            UpdateLogic();

            Draw();

            return 0;
        }

        
        void InitData()
        {
            //StageArray[3, 2] = (int)E_BLOCKTYPE.BLOCK;

            DelayTic = 200; // 0.5f
            NextTic = Environment.TickCount + DelayTic;


            // 초기화 부분

            // 긴블럭에 대한 데이터 초기화
            AllBlockTypeDataArray[0].AllBlockData = new BlockTypeData.BlockElementData[4]
            {
                new BlockTypeData.BlockElementData{ BlockData = new int[4, 4]
                    {
                        { 0, 0, 0, 0 }
                        , { 1, 1, 1, 1 }
                        , { 0, 0, 0, 0 }
                        , { 0, 0, 0, 0 }
                    }
                }
                , new BlockTypeData.BlockElementData{ BlockData = new int[4, 4]
                    {
                        { 0, 0, 0, 0 }
                        , { 1, 1, 1, 1 }
                        , { 0, 0, 0, 0 }
                        , { 0, 0, 0, 0 }
                    }
                }
                , new BlockTypeData.BlockElementData{ BlockData = new int[4, 4]
                    {
                        { 0, 0, 0, 0 }
                        , { 1, 1, 1, 1 }
                        , { 0, 0, 0, 0 }
                        , { 0, 0, 0, 0 }
                    }
                }
                , new BlockTypeData.BlockElementData{ BlockData = new int[4, 4]
                    {
                        { 0, 0, 0, 0 }
                        , { 1, 1, 1, 1 }
                        , { 0, 0, 0, 0 }
                        , { 0, 0, 0, 0 }
                    }
                }
            };



            //AllBlockTypeDataArray[0].AllBlockData[0].BlockData = new int[4, 4]
            //        {
            //            { 0, 0, 0, 0 }
            //            , { 1, 1, 1, 1 }
            //            , { 0, 0, 0, 0 }
            //            , { 0, 0, 0, 0 }
            //        };

            AllBlockTypeDataArray[0].AllBlockData[1].BlockData = new int[4, 4];
            AllBlockTypeDataArray[0].AllBlockData[2].BlockData = new int[4, 4];
            AllBlockTypeDataArray[0].AllBlockData[3].BlockData = new int[4, 4];
        }


        public void InGame()
        {
            int result = 0;
            InitData();

            while (true)
            {
                result = GameLogic();

                if ( result <= -1)
                {
                    break;
                }
            }
        }

    }


    class Program
    {
        static void Main(string[] args)
        {
            Tertris ingameteritrs = new Tertris();
            ingameteritrs.InGame();




        }
    }
}