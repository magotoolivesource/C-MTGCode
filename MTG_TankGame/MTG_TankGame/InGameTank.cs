using InGameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTG_TankGame
{
    class InGameTank : DefaultGameSource
    {

        Stage m_InGameStage = null;

        Tank m_MyTank = null;

        protected override void Initialze()
        {
            m_InGameStage = new Stage();
            m_InGameStage.InitStage();



            Vector2 pos = new Vector2();
            pos.X = 5;
            pos.Y = 5;

            m_MyTank = new Tank();
            m_MyTank.InitTankData(pos);
        }

        protected override void LoopDraw()
        {
            //m_Buffer.Draw('', 2, 3);

            m_InGameStage.DrawStage( m_Buffer );

            m_MyTank.DrawTank(m_Buffer);

        }

        protected override void LoopInputFN()
        {
            if (m_CurrentKeyInfo == null)
                return;

            // 소스 로직 적용하기


        }

    }
}
