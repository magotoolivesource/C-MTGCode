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
        Tank m_EnemyTank = null;

        Tank[] m_TankArray = null;

        protected override void Initialze()
        {
            m_InGameStage = new Stage();
            m_InGameStage.InitStage();

            m_TankArray = new Tank[10];


            Vector2 pos = new Vector2();
            pos.X = 5;
            pos.Y = 8;

            m_MyTank = new Tank();
            m_MyTank.InitTankData(pos);


            m_EnemyTank = new Tank();
            pos.Y = 3;
            m_EnemyTank.InitTankData(pos);


            m_TankArray[0] = m_MyTank;
            m_TankArray[1] = m_EnemyTank;

        }

        protected override void LoopDraw()
        {
            //m_Buffer.Draw('', 2, 3);

            m_InGameStage.DrawStage( m_Buffer );


            //m_MyTank.DrawTank(m_Buffer);
            //m_EnemyTank.DrawTank(m_Buffer);

            foreach (var tankcls in m_TankArray)
            {
                if(tankcls != null)
                    tankcls.DrawTank(m_Buffer);
            }

        }

        protected override void LoopInputFN()
        {
            if (m_CurrentKeyInfo == null)
                return;


            E_Driection movedirection = E_Driection.Max;
            m_MyTank.Move(movedirection);


            //foreach (var tankcls in m_TankArray)
            //{
            //    tankcls.Move( );
            //}

            //// 소스 로직 적용하기
            //m_MyTank.UpdateMove( m_CurrentKeyInfo.Value );

            //m_EnemyTank.UpdateAI();

        }

    }
}
