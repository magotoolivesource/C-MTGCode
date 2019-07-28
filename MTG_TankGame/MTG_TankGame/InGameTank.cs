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
        /// <summary>
        /// 싱글톤
        /// </summary>
        static InGameTank m_Instance = null;

        public static InGameTank Instance()
        {
            if (m_Instance == null)
            {
                m_Instance = new InGameTank();
            }

            return m_Instance;
        }

        public static InGameTank GetI
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new InGameTank();
                }

                return m_Instance;
            }
        }






        public Stage m_InGameStage = null;

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
            pos.Y = 5;

            m_MyTank = new Tank();
            m_MyTank.InitTankData(pos);
            m_TankArray[0] = m_MyTank;


            //m_EnemyTank = new Tank();
            //pos.Y = 3;
            //m_EnemyTank.InitTankData(pos);
            //m_TankArray[1] = m_EnemyTank;



            m_ControlKeyDic.Add(ConsoleKey.UpArrow, E_Driection.Up);
            m_ControlKeyDic.Add(ConsoleKey.W, E_Driection.Up);
            m_ControlKeyDic.Add(ConsoleKey.NumPad8, E_Driection.Up);

            m_ControlKeyDic.Add(ConsoleKey.DownArrow, E_Driection.Down);
            m_ControlKeyDic.Add(ConsoleKey.S, E_Driection.Down);

            m_ControlKeyDic.Add(ConsoleKey.RightArrow, E_Driection.Right);
            m_ControlKeyDic.Add(ConsoleKey.D, E_Driection.Right);

            m_ControlKeyDic.Add(ConsoleKey.LeftArrow, E_Driection.Left);
            m_ControlKeyDic.Add(ConsoleKey.A, E_Driection.Left);


            E_Driection dic = m_ControlKeyDic[ConsoleKey.UpArrow];

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


            foreach (var bullet in m_BulletList)
            {
                bullet.DrawBullet(m_Buffer);
            }
        }

        Dictionary<ConsoleKey, E_Driection> m_ControlKeyDic = new Dictionary<ConsoleKey, E_Driection>();

        void LoopPlayerControl()
        {
            ConsoleKey key = m_CurrentKeyInfo.Value.Key;

            if( m_ControlKeyDic.ContainsKey(key) )
            {
                //E_Driection val = m_ControlKeyDic[key];
                m_MyTank.Move( m_ControlKeyDic[key] );
            }

            if( key == ConsoleKey.Spacebar )
            {
                m_MyTank.Fire();
            }


            ////E_Driection movedirection = E_Driection.Max;
            //if (key == ConsoleKey.UpArrow
            //    || key == ConsoleKey.W)
            //{
            //    m_MyTank.Move(E_Driection.Up);
            //}
            //else if (key == ConsoleKey.RightArrow
            //    || key == ConsoleKey.D)
            //{
            //    m_MyTank.Move(E_Driection.Right);
            //}
            //else if (key == ConsoleKey.DownArrow
            //    || key == ConsoleKey.S)
            //{
            //    m_MyTank.Move(E_Driection.Down);
            //}
            //else if (key == ConsoleKey.LeftArrow
            //    || key == ConsoleKey.A)
            //{
            //    m_MyTank.Move(E_Driection.Left);
            //}


        }

        void LoopUpdateBullet()
        {
            foreach (var bullet in m_BulletList)
            {
                bullet.UpdateMove();
            }
        }

        protected override void LoopInputFN()
        {
            //m_MyTank.UpdateTank();

            LoopUpdateBullet();

            if (m_CurrentKeyInfo == null)
                return;

            LoopPlayerControl();







            //foreach (var tankcls in m_TankArray)
            //{
            //    tankcls.Move( );
            //}

            //// 소스 로직 적용하기
            //m_MyTank.UpdateMove( m_CurrentKeyInfo.Value );

            //m_EnemyTank.UpdateAI();

        }


        List<Bullet> m_BulletList = new List<Bullet>();
        public void CreateBullet(Tank p_tank)
        {
            Bullet bullet = new Bullet();
            bullet.InitSetting(p_tank.CurrentPos, p_tank.m_DirectionVal, 0.2f);
            m_BulletList.Add(bullet);

        }

    }
}
