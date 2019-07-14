using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{

    struct POINT2
    {
        public POINT2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int x;
        public int y;


    }

    class SokobanCls : DefaultGameSource
    {
        //m_Title = "소코반";
        public SokobanCls(string p_title)
        {
            m_Title = p_title;
        }

        // c++          c#
        // vector   -> List
        // set      -> Set
        // map      -> Dictionary
        List<POINT2> m_GoalPos = new List<POINT2>();



        public override void Init()
        {
            m_Title = "소코반";
            base.Init();



            #region 현재 스테이지 m_CurrentStage 로 복사
            int ysize = m_StageArray.Length;
            int xsize = m_StageArray[0].Length;

            m_CurrentStage = new char[ysize ,xsize];

            m_XStageSize = xsize;
            m_YStageSize = ysize;

            //"==============================",
            //"==                          ==",
            //"==      @    o     ===========",
            // 위치값 세팅
            for (int y = 0; y < m_StageArray.Length; y++)
            {
                string tempstr = m_StageArray[y];
                //char[] tempchararr = tempstr.ToArray();

                for (int x = 0; x < xsize; x++)
                {
                    m_CurrentStage[y, x] = tempstr[x];// tempchararr[x];

                    if( tempstr[x].CompareTo( '@' ) == 0 )
                    {
                        PlayerPosX = x;
                        PlayerPosY = y;
                        m_CurrentStage[y, x] = ' ';
                    }

                    if(tempstr[x].CompareTo('.') == 0)
                    {
                        m_GoalPos.Add(new POINT2(x, y) );
                    }
                }
            }

            #endregion

        }

        bool Move( int p_x, int p_y
            , int p_directionx, int p_dirctiony)
        {
            if( p_x < 0 || p_x >= m_XStageSize
                || p_y < 0 || p_y >= m_YStageSize)
            {
                return false;
            }

            if (p_directionx < 0 || p_directionx >= m_XStageSize
                || p_dirctiony < 0 || p_dirctiony >= m_YStageSize)
            {
                return false;
            }


            // ->   @ === 
            if ( m_CurrentStage[p_y, p_x] == '=' )
            {
                return false;
            }
            else
            {
                if(m_CurrentStage[p_y, p_x] == 'o')
                {
                    if(m_CurrentStage[p_dirctiony, p_directionx] == '=')
                    {
                        return false;
                    }

                    m_CurrentStage[p_y, p_x] = ' ';
                    m_CurrentStage[p_dirctiony, p_directionx] = 'o';
                }
            }

            return true;
        }

        protected override void LoopInputFN()
        {
            if( m_CurrentKeyInfo == null)
                return;

            // 소스 로직 적용하기

            int tempposx = PlayerPosX;
            int tempposy = PlayerPosY;
            bool ismove = false;
            if ( m_CurrentKeyInfo.Value.Key == ConsoleKey.DownArrow )
            {
                //++PlayerPosY;
                tempposy++;
                if( Move(tempposx, tempposy, tempposx, tempposy + 1) )
                {
                    PlayerPosY = tempposy;
                    PlayerPosX = tempposx;
                    ismove = true;
                }
            }

            if (m_CurrentKeyInfo.Value.Key == ConsoleKey.UpArrow)
            {
                tempposy--;
                if ( Move(tempposx, tempposy, tempposx, tempposy - 1) )
                {
                    PlayerPosY = tempposy;
                    PlayerPosX = tempposx;
                    ismove = true;
                }
            }


            if (m_CurrentKeyInfo.Value.Key == ConsoleKey.RightArrow)
            {
                tempposx++;
                if ( Move(tempposx, tempposy, tempposx + 1, tempposy) )
                {
                    PlayerPosY = tempposy;
                    PlayerPosX = tempposx;
                    ismove = true;
                }
            }

            if (m_CurrentKeyInfo.Value.Key == ConsoleKey.LeftArrow)
            {
                tempposx--;
                if ( Move(tempposx, tempposy, tempposx - 1, tempposy) )
                {
                    PlayerPosY = tempposy;
                    PlayerPosX = tempposx;
                    ismove = true;
                }
            }

            if(ismove)
            {
                m_ISStageClear = ISStageClear();
            }
                

        }



        protected char[] m_tempstr = new char[]
        {
            'a', 'b', 'c'
        };
        protected string m_tempstr2 = "abc";





        //protected char[,] m_StageArray = new char[,]
        //{
        //      { '=', '=' }
        //    , { '=', '=' }
        //    //"==============================",
        //};

        
        protected string[] m_StageArray = new string[]
        {
            "==============================",
            "==  @                       ==",
            "==           o     =======  ==",
            "==      o  ===             .==",
            "==                         .==",
            "==============================",
        };

        protected int m_XStageSize = 0;
        protected int m_YStageSize = 0;
        protected char[,] m_CurrentStage = null;






        bool m_ISStageClear = false;
        bool ISStageClear()
        {

            //m_GoalPos.Count;
            for (int i = 0; i < m_GoalPos.Count; i++)
            {
                POINT2 temppos = m_GoalPos[i];

                if(m_CurrentStage[temppos.y, temppos.x] != 'o')
                {
                    return false;
                }
            }

            return true;

            //for (int y = 0; y < m_YStageSize; y++)
            //{
            //    for (int x = 0; x < m_XStageSize; x++)
            //    {
            //        if( m_CurrentStage[y, x] == 'o'
            //            && m_CurrentStage[y, x] == '.' )
            //        {

            //        }
            //    }
            //}

            return true;
        }


        void LoopStageDraw()
        {
            // 스테이지 그리기
            //for (int i = 0; i < m_StageArray.Length; i++)
            //{
            //    m_Buffer.Draw(m_StageArray[i], 0, i);

            //}

            int ysize = m_CurrentStage.GetLength(0);
            int xsize = m_CurrentStage.GetLength(1);
            
            for (int y = 0; y < ysize; y++)
            {
                for (int x = 0; x < xsize; x++)
                {
                    //string tempstr = string.Format( "{0}", m_CurrentStage[y, x] );
                    //m_Buffer.Draw(tempstr, x, y);
                    m_Buffer.Draw(m_CurrentStage[y, x], x, y);
                }
            }


        }

        int PlayerPosX = 0;
        int PlayerPosY = 0;

        void LoopPlayerDraw()
        {
            m_Buffer.Draw("@", PlayerPosX, PlayerPosY);

        }

        void LoopHelpDraw()
        {
            if(m_ISStageClear)
            {
                m_Buffer.Draw("Stage Clear : ", 35, 10);
            }

        }
        protected override void LoopDraw()
        {
            LoopStageDraw();
            LoopPlayerDraw();
            LoopHelpDraw();
        }


    }
}
