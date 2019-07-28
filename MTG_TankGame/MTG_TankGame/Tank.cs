using InGameEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTG_TankGame
{

    public enum E_Driection
    {
        Right = 0,
        Down,
        Left,
        Up,

        Max
    }

    class Tank
    {
        public Vector2 CurrentPos;
        public int HP = 2;
        Stage m_CurrentStageData = null;
        E_Driection m_DirectionVal = E_Driection.Max;

        public void InitTankData( Vector2 p_initpos, Stage p_stagedata )
        {
            m_CurrentStageData = p_stagedata;
            CurrentPos = p_initpos;
            HP = 2;
        }

        public void DrawTank(buffer p_buff )
        {
            p_buff.Draw("A", CurrentPos.X, CurrentPos.Y);


            foreach (var item in m_BulletList)
            {
                item.DrawBullet(p_buff);
            }
            
        }

        public void UpdateTank()
        {
            foreach (var item in m_BulletList)
            {
                item.UpdateMove();
            }
        }

        public void Move(E_Driection p_direction)
        {
            switch (p_direction)
            {
                case E_Driection.Right:
                    {
                        if( !m_CurrentStageData.ISCollision(CurrentPos.X + 1, CurrentPos.Y))
                        {
                            CurrentPos.X++;
                        }
                    }
                    break;
                case E_Driection.Down:
                    {
                        if (!m_CurrentStageData.ISCollision(CurrentPos.X, CurrentPos.Y+1))
                            CurrentPos.Y++;
                    }
                    break;
                case E_Driection.Left:
                    {
                        if (!m_CurrentStageData.ISCollision(CurrentPos.X-1, CurrentPos.Y))
                            CurrentPos.X--;
                    }
                    break;
                case E_Driection.Up:
                    {
                        if (!m_CurrentStageData.ISCollision(CurrentPos.X, CurrentPos.Y - 1))
                            CurrentPos.Y--;
                    }
                    break;
                case E_Driection.Max:
                default:
                    {
                        //Debug.Write();
                    }
                    break;
            }

            m_DirectionVal = p_direction;
        }

        //public void UpdateAI()
        //{
        //    CurrentPos.X++;
        //}

        //public void UpdateMove(ConsoleKeyInfo p_keyinfo )
        //{

        //    if( p_keyinfo.Key == ConsoleKey.RightArrow)
        //    {
        //        CurrentPos.X++;
        //    }

        //    if (p_keyinfo.Key == ConsoleKey.LeftArrow)
        //    {
        //        CurrentPos.X--;
        //    }

        //    if (p_keyinfo.Key == ConsoleKey.UpArrow)
        //    {
        //        CurrentPos.Y--;
        //    }

        //    if (p_keyinfo.Key == ConsoleKey.DownArrow)
        //    {
        //        CurrentPos.Y++;
        //    }


        //}


        List<Bullet> m_BulletList = new List<Bullet>();
        public void Fire()
        {
            Bullet bullet = new Bullet();
            bullet.InitSetting(CurrentPos, m_DirectionVal, 1);
            m_BulletList.Add(bullet);

        }

    }
}
