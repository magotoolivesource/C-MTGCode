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

    #region 랜덤 함수용 나중에 복사 해서 사용하기
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
    #endregion

    #region 테스트 클래스 확인용임 없애도 됨
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
    #endregion


    #region 블럭정보 저장용 

    public class BlockTypeData
    {
        public class BlockElementData
        {
            public int[,] BlockData = null;
        }

        public BlockElementData[] AllBlockData = null;


        public int[,] GetBlockRotData(int p_rottype)
        {
            return AllBlockData[p_rottype].BlockData;
        }

        public int GetBlockRotData(int p_rot, int p_x, int p_y)
        {
            return AllBlockData[p_rot].BlockData[p_y, p_x];
        }

    }

    #endregion

    #region 테트리스용 클래스

    public class Tertris
    {
        #region 고정 상수값들
        const int STAGEHEIGHT = 20;
        const int STAGEWIDTH = 10;
        const int BLOCKINITPOSX = (int)((STAGEWIDTH + 2) * 0.5f);
        const int BlockSize = 4;
        #endregion

        // 화면 더블버퍼용 클래스
        public buffer MyBuffer;


        public enum E_BLOCKTYPE
        {
            None = 0,
            BLOCK,
            WALL,

            Max
        }


        int m_Score = 0;


        int DelayTic = 1000;
        int NextTic = 0;

        // 스테이지 정보
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


        // 블럭들 정보 총 4개 가지고 있음 추가적으로 이곳에 내용들 추가하면됨
        public BlockTypeData[] AllBlockTypeDataArray = new BlockTypeData[BlockSize]
        {
            new BlockTypeData()
            , new BlockTypeData()
            , new BlockTypeData()
            , new BlockTypeData()
        };


        int[] BlockTypeAttribute = new int[BlockSize]
        {
            2, 3, 4, 5
        };


        // 게임내에서 떨어지는 하나의 블럭에 대한 데이터값
        int m_CurrentBlockType = 0;
        int m_RotType = 0; // 0 이면
        int m_BlockPosY = 0;
        int m_BlockPosX = BLOCKINITPOSX;


        // 게임 오버용 소스
        bool ISGameFlag = false;


        /// <summary>
        /// 스테이지 드로잉용
        /// </summary>
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
                    else if ((int)E_BLOCKTYPE.WALL == StageArray[y, x])
                    {
                        MyBuffer.Draw("0", x * 2, y);
                    }
                }
            }
        }

        

        /// <summary>
        /// 현재 블럭에 대한 정보를 이용한 화면에 블럭 드로잉
        /// </summary>
        void BlockDraw()
        {
            BlockTypeData blocktypedata = AllBlockTypeDataArray[m_CurrentBlockType];

            int widthsize = 4;// BlockArray.GetLength(2);
            int heightsize = 4; //BlockArray.GetLength(1);
            int tempy = m_BlockPosY;
            int tempx = m_BlockPosX;

            int[,] blockdata = blocktypedata.GetBlockRotData(m_RotType);

            int attributeval = BlockTypeAttribute[m_CurrentBlockType];
            for (int y = 0; y < heightsize; y++)
            {
                for (int x = 0; x < widthsize; x++)
                {
                    if (1 == blocktypedata.GetBlockRotData(m_RotType, x, y))
                    {
                        MyBuffer.Draw("A", (x + tempx) * 2
                            , y + tempy
                            , (short)attributeval);
                    }
                }
            }
        }


        /// <summary>
        /// 화면에 추가적인 설명을 위한 드로잉함수
        /// </summary>
        void HelpDraw()
        {
            int val = 10;
            float val2 = 10;
            //printf("%d %f ", val, val2);

            string.Format("{1}, {0}", val, val2);

            // 점수
            string scoreval = string.Format("Score : {0}", m_Score);
            MyBuffer.Draw(scoreval, 15 * 2, 9);


            // 컨트롤 방법 대한부분
            MyBuffer.Draw("Control : ", 15 * 2, 10);


            // 게임 오버
            if (ISGameFlag)
            {
                MyBuffer.Draw("GameOver", 15 * 2, 15);
                MyBuffer.Draw("Restart 'R' Press ", 15 * 2, 17);
            }
        }


        /// <summary>
        /// 전체 드로잉
        /// </summary>
        public void Draw()
        {
            StageDraw();
            BlockDraw();
            HelpDraw();

            MyBuffer.Print();
            MyBuffer.Clear();
        }


        /// <summary>
        /// 블럭이 맞추어지면 현재 블럭 
        /// 라인부터해서 위에있는 블럭들 하단으로 내려오도록 하는 소슨
        /// </summary>
        /// <param name="p_y"></param>
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


        /// <summary>
        /// 블럭의 라인이 완성 되었는지 소스
        /// </summary>
        void CheckSameBlock()
        {

            for (int y = STAGEHEIGHT - 1; y >= 0; y--)
            {
                bool ischeck = true;
                for (int x = 1; x < STAGEWIDTH + 1; x++)
                {
                    if (StageArray[y, x] != (int)E_BLOCKTYPE.BLOCK)
                    {
                        ischeck = false;
                        break;
                    }
                }

                if (ischeck)
                {
                    // 블럭 지우기
                    // 점수 추가
                    StageDownBlock(y);
                    y++;

                    m_Score++;
                }
            }
        }

        
        /// <summary>
        /// 인풋이 아닌 부분의 처리 데이터 처리부분
        /// </summary>
        void UpdateLogic()
        {
            // 1초에 한번 
            //Environment.TickCount; // 윈도우 시작 했을때의 시간

            // 0.2초에 한번씩 떨어지는 기본 소스
            int currenttic = Environment.TickCount;
            bool isflag = false;
            if (NextTic <= currenttic)
            {
                NextTic = currenttic + DelayTic;
                isflag = true;
            }


            if (isflag)
            {
                m_BlockPosY++;

                // 바닥과 충돌이 되면 새로운 블럭 만들도록 하기
                if (StageNBlockCollision())
                {
                    m_BlockPosY--;

                    CopyBlockData();
                    CheckSameBlock();

                    CreateRandomBlock();

                    // 게임 오버가 되었는지 파악하기 위한 함수
                    if (StageNBlockCollision())
                    {
                        ISGameFlag = true;
                    }
                }
            }
        }


        /// <summary>
        /// 스페이스바 누르면 한번에 떨어지도록 하는 소스
        /// </summary>
        /// <returns></returns>
        bool DirectMoveDown()
        {
            while (true)
            {
                m_BlockPosY++;

                // 바닥과 충돌이 되면 새로운 블럭 만들도록 하기
                if (StageNBlockCollision())
                {
                    m_BlockPosY--;
                    CopyBlockData();
                    CheckSameBlock();
                    CreateRandomBlock();

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 떨어지는 블럭이 다내려오게 되면 그위치에서 스테이지블럭정보에 
        /// 해당 블럭에 대한 정보를 복사하기위한 함수
        /// </summary>
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

        /// <summary>
        /// 다음 블럭에 대해 랜덤하게 적용하기 위한 함수
        /// </summary>
        void CreateRandomBlock()
        {
            // 새로운 블럭 생성
            m_CurrentBlockType = MyRandom.GetRandMax(BlockSize);
            m_BlockPosY = 0;
            m_BlockPosX = BLOCKINITPOSX;
            m_RotType = 0;

        }

        /// <summary>
        /// 스테이지와 현재 블럭이 부딪히는지에 대한 처리
        /// </summary>
        /// <returns></returns>
        bool StageNBlockCollision()
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

        /// <summary>
        /// 인풋 장치 키보드에 받아오는 값들 처리하기위한 함수
        /// </summary>
        void UpdateInput()
        {

            // 넌 블럭킹 방식으로 처리
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo info = Console.ReadKey();
                ConsoleKey keyval = info.Key;

                if (keyval == ConsoleKey.RightArrow
                    || keyval == ConsoleKey.D)
                {
                    m_BlockPosX++;

                    if (StageNBlockCollision())
                    {
                        m_BlockPosX--;
                    }
                }

                if (keyval == ConsoleKey.LeftArrow
                    || keyval == ConsoleKey.A)
                {
                    m_BlockPosX--;

                    if (StageNBlockCollision())
                    {
                        m_BlockPosX++;
                    }
                }

                if (keyval == ConsoleKey.UpArrow
                    || keyval == ConsoleKey.W)
                {
                    m_RotType = (m_RotType + 1) % 4;
                }

                if (keyval == ConsoleKey.Spacebar)
                {
                    DirectMoveDown();
                }
            }
        }

        /// <summary>
        /// 게임 진행 위한 함수
        /// </summary>
        /// <returns></returns>
        public int GameLogic()
        {
            if (!ISGameFlag)
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

        /// <summary>
        /// 처음 시작하거나 재시작시 사용하기 위한 함수
        /// </summary>
        void ResetStageData()
        {
            // 스코어 점수 초기화
            m_Score = 0;

            // 스테이지 전체 초기화
            for (int y = 0; y < STAGEHEIGHT + 1; y++)
            {
                for (int x = 0; x < STAGEWIDTH + 2; x++)
                {
                    StageArray[y, x] = (int)E_BLOCKTYPE.None;
                }
            }

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


            // 스테이지별 블럭에 대한 정보들 적용하기
        }


        /// <summary>
        /// 게임 처음 시작시 기본 세팅하기위한 함수
        /// 버퍼 랜더를 사용하기 위한 초기화를 이곳에서 처리
        /// 블럭에 대한 정보도 이곳에서 처리하고 있음
        /// </summary>
        void InitData()
        {
            // 더블 버퍼 초기화하기
            int width = 80;
            int height = 30;
            System.Console.SetWindowSize(width, height);
            System.Console.SetBufferSize(width, height);
            MyBuffer = new buffer(width, height, width, height);


            // 게임에 대한 속도 제어용 값들
            DelayTic = 200; // 0.5f
            NextTic = Environment.TickCount + DelayTic;




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

            // ㅗ블럭 세팅
            AllBlockTypeDataArray[3].AllBlockData = new BlockTypeData.BlockElementData[4]
            {
                new BlockTypeData.BlockElementData ()
                {
                    BlockData = new int[4, 4]
                    {
                          { 0, 0, 0, 0 }
                        , { 0, 1, 1, 1 }
                        , { 0, 0, 1, 0 }
                        , { 0, 0, 0, 0 }
                    }
                }
                , new BlockTypeData.BlockElementData()
                {
                    BlockData = new int[4, 4]
                    {
                          { 0, 0, 1, 0 }
                        , { 0, 0, 1, 1 }
                        , { 0, 0, 1, 0 }
                        , { 0, 0, 0, 0 }
                    }
                }
                , new BlockTypeData.BlockElementData()
                {
                    BlockData = new int[4, 4]
                    {
                          { 0, 0, 1, 0 }
                        , { 0, 1, 1, 1 }
                        , { 0, 0, 0, 0 }
                        , { 0, 0, 0, 0 }
                    }
                }
                , new BlockTypeData.BlockElementData()
                {
                    BlockData = new int[4, 4]
                    {
                          { 0, 0, 1, 0 }
                        , { 0, 1, 1, 0 }
                        , { 0, 0, 1, 0 }
                        , { 0, 0, 0, 0 }
                    }
                }

           };


            ResetStageData();
        }

        /// <summary>
        /// 게임 로직위한 함수
        /// </summary>
        public void InGame()
        {
            int result = 0;
            InitData();

            while (true)
            {
                result = GameLogic();

                if (result <= -1)
                {
                    break;
                }
            }
        }

    }

    #endregion


    #region 기본 진입함수

    class Program
    {
        static void Main(string[] args)
        {
            Tertris ingameteritrs = new Tertris();
            ingameteritrs.InGame();
        }
    }

    #endregion


}
