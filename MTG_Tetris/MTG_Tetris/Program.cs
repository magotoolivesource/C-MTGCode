using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

using DoubleBuffer;

/// <summary>
/// 어떤 블럭을 만들것이냐?
/// 어떤 블럭은 어떤 데이터로 들고 있을것이냐
/// 
/// 블럭이 떨어진다
///     좌우이동
///     회전
///     
/// 블럭 데이터 타입 수정
///     임시 2개의 블럭 방식으로 처리
///  
///     
/// 블럭이 채워지면 블럭이 사라진다.
///     한줄 블럭이 사라지면 그 블럭에 있는 상단 오브젝트들을 내려오게한다
///     여러 블럭일때 상단 오브젝트들을 한줄씩 내려오도록 한다
///     
/// </summary>
namespace MTG_Tetris
{

    public class MyRandom
    {
        private static Random m_Rand = new Random();

        // 0보다 크거나 같고 System.Int32.MaxValue
        public static int GetRand()
        {
            return m_Rand.Next();
        }

        public static int GetRandMax(int p_max)
        {
            return m_Rand.Next() % p_max;
        }

        public static int GetRand(int p_min, int p_max)
        {
            int gab = p_max - p_min;
            int rand = m_Rand.Next() % gab;

            return rand + p_min;
        }

    }


    public class TestCls
    {
        public int TestVal1;
        public string Name;
        public int HP;

        private int MP;

        //public TestCls()
        //{
        //}
        //public TestCls(int p_hp = 10 )
        //{
        //    HP = p_hp;
        //}

    }



    public class BlockTypeData
    {
        public class BlockElementData
        {
            public int[,] BlockData = null;
        }

        public BlockElementData[] AllBlockData = null;

        
        public int[,] GetBlockRotData( int p_rottype )
        {
            return AllBlockData[p_rottype].BlockData;
        }

        public int GetBlockRotData(int p_rot, int p_x, int p_y)
        {
            return AllBlockData[p_rot].BlockData[p_y, p_x];
        }

    }


    public class Tertris
    {
        const int STAGEHEIGHT = 20;
        const int STAGEWIDTH = 10;
        const int BLOCKINITPOSX = (int)((STAGEWIDTH + 2) * 0.5f);
        const int BlockSize = 3;



        int DelayTic = 1000;

        int NextTic = 0;

        public enum E_BLOCKTYPE
        {
            None = 0,
            BLOCK,
            WALL,

            Max
        }

        // 스테이지
        public int[,] StageArray = new int[STAGEHEIGHT + 1, STAGEWIDTH + 2]
        {
              {  2, 0, 0, 0, 0,       0, 0, 0, 0, 0   , 0, 2 }
            , {  2, 0, 0, 0, 0,       0, 0, 0, 0, 0   , 0, 2 }
            , {  2, 0, 0, 0, 0,       0, 0, 0, 0, 0   , 0, 2 }
            , {  2, 0, 0, 0, 0,       0, 0, 0, 0, 0   , 0, 2 }
            , {  2, 0, 0, 0, 0,       0, 0, 0, 0, 0   , 0, 2 }

            , {  2, 0, 0, 0, 0,       0, 0, 0, 0, 0   , 0, 2 }
            , {  2, 0, 0, 0, 0,       0, 0, 0, 0, 0   , 0, 2 }
            , {  2, 0, 0, 0, 0,       0, 0, 0, 0, 0   , 0, 2 }
            , {  2, 0, 0, 0, 0,       0, 0, 0, 0, 0   , 0, 2 }
            , {  2, 0, 0, 0, 0,       0, 0, 0, 0, 0   , 0, 2 }

            , {  2, 0, 0, 0, 0,       0, 0, 0, 0, 0   , 0, 2 }
            , {  2, 0, 0, 0, 0,       0, 0, 0, 0, 0   , 0, 2 }
            , {  2, 0, 0, 0, 0,       0, 0, 0, 0, 0   , 0, 2 }
            , {  2, 0, 0, 0, 0,       0, 0, 0, 0, 0   , 0, 2 }
            , {  2, 0, 0, 0, 0,       0, 0, 0, 0, 0   , 0, 2 }

            , {  2, 0, 0, 0, 0,       0, 0, 1, 1, 0   , 0, 2 }
            , {  2, 0, 0, 0, 0,       0, 0, 0, 1, 0   , 0, 2 }
            , {  2, 0, 0, 0, 0,       0, 0, 0, 1, 0   , 0, 2 }
            , {  2, 0, 0, 0, 0,       0, 0, 0, 1, 0   , 0, 2 }
            , {  2, 0, 1, 1, 1,       1, 1, 1, 1, 0   , 0, 2 }

            , {  2, 2, 2, 2, 2,       2, 2, 2, 2, 2   , 2, 2 }
        };


    public buffer MyBuffer;

        public BlockTypeData[] AllBlockTypeDataArray = new BlockTypeData[BlockSize]
        {
            new BlockTypeData()
            , new BlockTypeData()
            , new BlockTypeData()
        };


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
                        MyBuffer.Draw(".", x * 2, y);
                    }
                    else if ((int)E_BLOCKTYPE.BLOCK == StageArray[y, x])
                    {
                        MyBuffer.Draw("A", x * 2, y);
                    }
                    else if((int)E_BLOCKTYPE.WALL == StageArray[y, x])
                    {
                        MyBuffer.Draw("0", x * 2, y);
                    }
                }
            }
        }

        // 하나의 블럭에 대한 데이터값
        int m_CurrentBlockType = 0;
        int m_RotType = 0; // 0 이면
        int m_BlockPosY = 0;
        int m_BlockPosX = BLOCKINITPOSX;


        void BlockDraw()
        {
            BlockTypeData blocktypedata = AllBlockTypeDataArray[m_CurrentBlockType];

            int widthsize = 4;// BlockArray.GetLength(2);
            int heightsize = 4; //BlockArray.GetLength(1);
            int tempy = m_BlockPosY;
            int tempx = m_BlockPosX;

            int[,] blockdata = blocktypedata.GetBlockRotData(m_RotType);

            for (int y = 0; y < heightsize; y++)
            {
                for (int x = 0; x < widthsize; x++)
                {
                    if( 1 == blocktypedata.GetBlockRotData(m_RotType, x, y) )
                    {
                        MyBuffer.Draw("A", (x + tempx) * 2
                            , y + tempy );
                    }
                }
            }

        }

        void HelpDraw()
        {
            // 점수


            // 컨트롤 방법 대한부분


            // 게임 오버
            if( ISGameFlag )
            {
                MyBuffer.Draw("GameOver", 15 * 2, 15);
            }
        }

        public void Draw()
        {
            StageDraw();
            BlockDraw();
            HelpDraw();


            MyBuffer.Print();
            MyBuffer.Clear();
        }

        bool ISDownCollision()
        {

            BlockTypeData blocktypedata = AllBlockTypeDataArray[m_CurrentBlockType];
            int widthsize = 4;
            int heightsize = 4;
            int tempy = m_BlockPosY;
            int tempx = m_BlockPosX;
            int[,] blockdata = blocktypedata.GetBlockRotData(m_RotType);

            for (int y = 0; y < heightsize; y++)
            {
                for (int x = 0; x < widthsize; x++)
                {
                    if (1 == blocktypedata.GetBlockRotData(m_RotType, x, y)
                        && (StageArray[y + tempy, x + tempx] == (int)E_BLOCKTYPE.BLOCK
                        || StageArray[y + tempy, x + tempx] == (int)E_BLOCKTYPE.WALL
                        )
                        )
                    {
                        return true;
                    }
                }
            }

            return false;
        }


        void StageDownBlock(int p_y)
        {
            for (int y = p_y; y >= 1; y--)
            {
                for (int x = 1; x < STAGEWIDTH + 1; x++)
                {
                    StageArray[y, x] = StageArray[y - 1, x];
                }
            }
        }

        void CheckSameBlock()
        {

            for (int y = STAGEHEIGHT - 1; y >= 0; y--)
            {
                bool ischeck = true;
                for (int x = 1; x < STAGEWIDTH+1; x++)
                {
                    if(StageArray[y, x] != (int)E_BLOCKTYPE.BLOCK)
                    {
                        ischeck = false;
                        break;
                    }
                }

                if( ischeck )
                {
                    // 블럭 지우기
                    // 점수 추가
                    StageDownBlock(y);
                    y++;
                }
            }


        }

        bool ISGameFlag = false;
        bool ISGameOver()
        {

            BlockTypeData blocktypedata = AllBlockTypeDataArray[m_CurrentBlockType];
            int widthsize = 4;
            int heightsize = 4;
            int tempy = m_BlockPosY;
            int tempx = m_BlockPosX;
            int[,] blockdata = blocktypedata.GetBlockRotData(m_RotType);

            for (int y = 0; y < heightsize; y++)
            {
                for (int x = 0; x < widthsize; x++)
                {
                    if (1 == blocktypedata.GetBlockRotData(m_RotType, x, y)
                        && (StageArray[y + tempy, x + tempx] == (int)E_BLOCKTYPE.BLOCK
                        || StageArray[y + tempy, x + tempx] == (int)E_BLOCKTYPE.WALL
                        )
                        )
                    {
                        return true;
                    }
                }
            }

            return false;
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

                // 바닥과 충돌이 되면 새로운 블럭 만들도록 하기
                if ( ISDownCollision() )
                {
                    m_BlockPosY--;

                    CopyBlockData();
                    CheckSameBlock();

                    CreateRandomBlock();

                    if( ISGameOver() )
                    {
                        ISGameFlag = true;
                    }
                    
                }

            }
        }

        void CopyBlockData()
        {

            BlockTypeData blocktypedata = AllBlockTypeDataArray[m_CurrentBlockType];

            int widthsize = 4;// BlockArray.GetLength(2);
            int heightsize = 4; //BlockArray.GetLength(1);
            int tempy = m_BlockPosY;
            int tempx = m_BlockPosX;

            int[,] blockdata = blocktypedata.GetBlockRotData(m_RotType);

            for (int y = 0; y < heightsize; y++)
            {
                for (int x = 0; x < widthsize; x++)
                {
                    if (1 == blocktypedata.GetBlockRotData(m_RotType, x, y))
                    {
                        //MyBuffer.Draw("A", (x + tempx) * 2
                        //    , y + tempy);

                        StageArray[y + tempy, (x + tempx)] = (int)E_BLOCKTYPE.BLOCK;
                    }
                }
            }

        }

        void CreateRandomBlock()
        {
            // 새로운 블럭 생성
            m_CurrentBlockType = 0;// MyRandom.GetRandMax(BlockSize);
            m_BlockPosY = 0;
            m_BlockPosX = BLOCKINITPOSX;
            m_RotType = 0;
            
        }

        bool SideCollision()
        {
            BlockTypeData blocktypedata = AllBlockTypeDataArray[m_CurrentBlockType];
            int widthsize = 4;
            int heightsize = 4;
            int tempy = m_BlockPosY;
            int tempx = m_BlockPosX;
            int[,] blockdata = blocktypedata.GetBlockRotData(m_RotType);

            for (int y = 0; y < heightsize; y++)
            {
                for (int x = 0; x < widthsize; x++)
                {
                    if (1 == blocktypedata.GetBlockRotData(m_RotType, x, y)
                        && (StageArray[y + tempy, x + tempx] == (int)E_BLOCKTYPE.BLOCK
                        || StageArray[y + tempy, x + tempx] == (int)E_BLOCKTYPE.WALL
                        )
                        )
                    {
                        return true;
                    }
                }
            }

            return false;
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

                    if ( SideCollision() )
                    {
                        m_BlockPosX--;
                    }
                }

                if (keyval == ConsoleKey.LeftArrow
                    || keyval == ConsoleKey.A)
                {
                    m_BlockPosX--;

                    if ( SideCollision() )
                    {
                        m_BlockPosX++;
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
            if( !ISGameFlag )
            {
                UpdateInput();

                UpdateLogic();

                Draw();
            }
            else
            {
                Draw();
            }


            return 0;
        }

        
        void InitData()
        {
            int width = 80;
            int height = 30;
            System.Console.SetWindowSize(width, height);
            System.Console.SetBufferSize(width, height);
            MyBuffer = new buffer(width, height, width, height);

            DelayTic = 200; // 0.5f
            NextTic = Environment.TickCount + DelayTic;


            // 스테이지 벽에 대한 적용
            for (int y = 0; y < STAGEHEIGHT; y++)
            {
                StageArray[y, 0] = (int)E_BLOCKTYPE.WALL;
                StageArray[y, STAGEWIDTH + 1] = (int)E_BLOCKTYPE.WALL;
            }
            for (int x = 0; x < STAGEWIDTH + 2; x++)
            {
                StageArray[STAGEHEIGHT, x] = (int)E_BLOCKTYPE.WALL;
            }


            




            // 긴블럭에 대한 데이터 초기화
            AllBlockTypeDataArray[0].AllBlockData = new BlockTypeData.BlockElementData[]
            {
                new BlockTypeData.BlockElementData ()
                {
                    BlockData = new int[4, 4]
                    {
                        { 0, 0, 0, 0 }
                        , { 1, 1, 1, 1 }
                        , { 0, 0, 0, 0 }
                        , { 0, 0, 0, 0 }
                    }
                }
                , new BlockTypeData.BlockElementData()
                {
                    BlockData = new int[4, 4]
                    {
                          { 0, 0, 1, 0 }
                        , { 0, 0, 1, 0 }
                        , { 0, 0, 1, 0 }
                        , { 0, 0, 1, 0 }
                    }
                }
                , new BlockTypeData.BlockElementData()
                {
                    BlockData = new int[4, 4]
                    {
                          { 0, 0, 0, 0 }
                        , { 0, 0, 0, 0 }
                        , { 1, 1, 1, 1 }
                        , { 0, 0, 0, 0 }
                    }
                }
                , new BlockTypeData.BlockElementData()
                {
                    BlockData = new int[4, 4]
                    {
                          { 0, 1, 0, 0 }
                        , { 0, 1, 0, 0 }
                        , { 0, 1, 0, 0 }
                        , { 0, 1, 0, 0 }
                    }
                }

            };

            // 사각블럭 세팅
            AllBlockTypeDataArray[1].AllBlockData = new BlockTypeData.BlockElementData[4]
            {
                new BlockTypeData.BlockElementData ()
                {
                    BlockData = new int[4, 4]
                    {
                          { 0, 0, 0, 0 }
                        , { 0, 1, 1, 0 }
                        , { 0, 1, 1, 0 }
                        , { 0, 0, 0, 0 }
                    }
                }
                , new BlockTypeData.BlockElementData()
                {
                    BlockData = new int[4, 4]
                    {
                          { 0, 0, 0, 0 }
                        , { 0, 1, 1, 0 }
                        , { 0, 1, 1, 0 }
                        , { 0, 0, 0, 0 }
                    }
                }
                , new BlockTypeData.BlockElementData()
                {
                    BlockData = new int[4, 4]
                    {
                          { 0, 0, 0, 0 }
                        , { 0, 1, 1, 0 }
                        , { 0, 1, 1, 0 }
                        , { 0, 0, 0, 0 }
                    }
                }
                , new BlockTypeData.BlockElementData()
                {
                    BlockData = new int[4, 4]
                    {
                          { 0, 0, 0, 0 }
                        , { 0, 1, 1, 0 }
                        , { 0, 1, 1, 0 }
                        , { 0, 0, 0, 0 }
                    }
                }

           };

            // H블럭 세팅
            AllBlockTypeDataArray[2].AllBlockData = new BlockTypeData.BlockElementData[4]
            {
                new BlockTypeData.BlockElementData ()
                {
                    BlockData = new int[4, 4]
                    {
                          { 0, 1, 0, 0 }
                        , { 0, 1, 1, 0 }
                        , { 0, 0, 1, 0 }
                        , { 0, 0, 0, 0 }
                    }
                }
                , new BlockTypeData.BlockElementData()
                {
                    BlockData = new int[4, 4]
                    {
                          { 0, 1, 1, 0 }
                        , { 1, 1, 0, 0 }
                        , { 0, 0, 0, 0 }
                        , { 0, 0, 0, 0 }
                    }
                }
                , new BlockTypeData.BlockElementData()
                {
                    BlockData = new int[4, 4]
                    {
                          { 0, 1, 0, 0 }
                        , { 0, 1, 1, 0 }
                        , { 0, 0, 1, 0 }
                        , { 0, 0, 0, 0 }
                    }
                }
                , new BlockTypeData.BlockElementData()
                {
                    BlockData = new int[4, 4]
                    {
                          { 0, 1, 1, 0 }
                        , { 1, 1, 0, 0 }
                        , { 0, 0, 0, 0 }
                        , { 0, 0, 0, 0 }
                    }
                }

           };


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
