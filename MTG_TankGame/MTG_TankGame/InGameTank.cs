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
            pos.Y = 5;
            pos.X = 5;

            m_MyTank = new Tank();
            m_MyTank.InitTankData(pos, E_Driection.Up);
            m_TankArray[0] = m_MyTank;
            m_MyTank.HP = 1000;



            m_EnemyTank = new Tank();
            pos.Y = 5;
            pos.X = 3;
            m_EnemyTank.InitTankData(pos, E_Driection.Left, ConsoleColor.Red );
            m_TankArray[1] = m_EnemyTank;



            //EnemyTank tempenemytank = new EnemyTank();
            //tempenemytank.InitSettings();
            m_EnemyTank = new EnemyTank();
            pos.Y = 5;
            pos.X = 11;
            m_EnemyTank.InitTankData(pos, E_Driection.Right, ConsoleColor.Red);

            // (m_EnemyTank as EnemyTank).InitSettings();
            EnemyTank temptank = (m_EnemyTank as EnemyTank);
            if( temptank != null )
            {
                temptank.InitSettings(m_MyTank);
                
            }
            else
            {
                // 에러코드 적용

            }




            m_TankArray[2] = m_EnemyTank;



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

            m_InGameStage.DrawStage(m_Buffer);


            //m_MyTank.DrawTank(m_Buffer);
            //m_EnemyTank.DrawTank(m_Buffer);

            foreach (var tankcls in m_TankArray)
            {
                if (tankcls != null)
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

            if (m_ControlKeyDic.ContainsKey(key))
            {
                //E_Driection val = m_ControlKeyDic[key];
                m_MyTank.Move(m_ControlKeyDic[key]);
            }

            if (key == ConsoleKey.Spacebar)
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

        void LoopUpdateTanks()
        {
            foreach (var tankcls in m_TankArray)
            {
                if (tankcls != null)
                    tankcls.UpdateTank();
            }
        }

        void LoopUpdateBullet()
        {
            //foreach (var bullet in m_BulletList)
            //{
            //    bullet.UpdateMove();
            //}


            int count = m_BulletList.Count;
            for (int i = count - 1; i >= 0; i--)
            {
                m_BulletList[i].UpdateMove();

                //m_BulletList.RemoveAll(Predicate);
                //m_BulletList.RemoveAll( x => m_InGameStage.ISCollision(x.CurrentPos.X, x.CurrentPos.Y) );


                bool iscollision = false;
                int tankcount = m_TankArray.Length;
                for (int j = 0; j < tankcount; j++)
                {
                    if( m_TankArray[j] != null
                        && m_TankArray[j].CurrentPos == m_BulletList[i].CurrentPos )
                    {
                        iscollision = true;
                        m_TankArray[j].SetDamage(1);

                        if(m_TankArray[j].HP <= 0)
                        {
                            m_TankArray[j] = null;
                        }
                    }
                }


                if (m_InGameStage.ISCollision(m_BulletList[i].CurrentPos.X, m_BulletList[i].CurrentPos.Y))
                {
                    iscollision = true;
                }

                if(iscollision)
                {
                    // 총알지우기
                    m_BulletList.RemoveAt(i);
                }
            }
        }

        protected override void LoopInputFN()
        {
            //m_MyTank.UpdateTank();

            LoopUpdateTanks();
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


        public bool Predicate(Bullet obj)
        {
            return m_InGameStage.ISCollision(obj.CurrentPos.X, obj.CurrentPos.Y);
        }
        void UpdateBullet()
        {

            int count = m_BulletList.Count;
            for (int i = count - 1; i >= 0; i--)
            {
                //m_BulletList.RemoveAll(Predicate);
                //m_BulletList.RemoveAll( x => m_InGameStage.ISCollision(x.CurrentPos.X, x.CurrentPos.Y) );

                if(m_InGameStage.ISCollision(m_BulletList[i].CurrentPos.X, m_BulletList[i].CurrentPos.Y))
                {
                    m_BulletList.RemoveAt(i);
                }
            }



            //int count = m_BulletList.Count;
            //for (int i = 0; i < count; i++)
            //{
            //    if (m_InGameStage.ISCollision(m_BulletList[i].CurrentPos.X, m_BulletList[i].CurrentPos.Y))
            //    {
            //        m_BulletList.RemoveAt(i);
            //    }
            //}

            //foreach (Bullet item in m_BulletList)
            //{
            //    if (m_InGameStage.ISCollision(item.CurrentPos.X, item.CurrentPos.Y))
            //    {
            //        m_BulletList.Remove(item);
            //    }
            //}

        }

    }
}
