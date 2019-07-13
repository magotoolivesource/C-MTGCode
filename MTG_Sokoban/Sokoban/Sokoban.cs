using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    class SokobanCls : DefaultGameSource
    {
        //m_Title = "소코반";
        public SokobanCls(string p_title)
        {
            m_Title = p_title;
        }

        public override void Init()
        {
            m_Title = "소코반";
            base.Init();

            //"==============================",
            //"==                          ==",
            //"==      @    o     ===========",
            // 위치값 세팅
            for (int i = 0; i < m_StageArray.Length; i++)
            {
                string tempstr = m_StageArray[i];
                //tempstr = "==@ o===========";

                int posx = tempstr.IndexOf('@');

                if( posx >= 0)
                {
                    PlayerPosX = posx;
                    PlayerPosY = i;
                }
            }
            

        }

        
        protected override void LoopInputFN()
        {
            if( m_CurrentKeyInfo == null)
                return;

            // 소스 로직 적용하기

            if( m_CurrentKeyInfo.Value.Key == ConsoleKey.DownArrow )
            {
                ++PlayerPosY;

                if( PlayerPosY >= WindowHeight)
                {
                    PlayerPosY = WindowHeight - 1;
                }
            }

            if (m_CurrentKeyInfo.Value.Key == ConsoleKey.UpArrow)
            {
                --PlayerPosY;

                if (PlayerPosY < 0)
                    PlayerPosY = 0;
            }


            if (m_CurrentKeyInfo.Value.Key == ConsoleKey.RightArrow)
            {
                ++PlayerPosX;

                if (PlayerPosX >= WindowWidth)
                    PlayerPosY = WindowWidth - 1;
            }

            if (m_CurrentKeyInfo.Value.Key == ConsoleKey.LeftArrow)
            {
                --PlayerPosX;

                if (PlayerPosX < 0)
                    PlayerPosY = 0;
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
            "==           o     ===========",
            "==      o  ===             .==",
            "==                         .==",
            "==============================",

        };

        void LoopStageDraw()
        {
            // 스테이지 그리기
            for (int i = 0; i < m_StageArray.Length; i++)
            {
                m_Buffer.Draw(m_StageArray[i], 0, i);

            }
        }

        int PlayerPosX = 0;
        int PlayerPosY = 0;

        void LoopPlayerDraw()
        {
            m_Buffer.Draw("@", PlayerPosX, PlayerPosY);

        }

        protected override void LoopDraw()
        {
            LoopStageDraw();
            LoopPlayerDraw();

        }


    }
}
