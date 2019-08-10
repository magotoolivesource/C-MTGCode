using InGameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTG_TankGame
{
    class Bullet
    {
        public Vector2 CurrentPos;
        public float Speed; // 0.1 1숫자를 받으면 1초에 한번씩 이동
        public E_Driection MoveDirection;


        int m_CurrentTick = 0;
        int m_NextTick = 0;
        Vector2 m_OffsetPos = new Vector2(0, 0);
        public void InitSetting( Vector2 p_pos
            , E_Driection p_direction
            , float p_movespeed )
        {
            MoveDirection = p_direction;
            CurrentPos = p_pos;
            Speed = p_movespeed;

            m_CurrentTick = Environment.TickCount;
            m_NextTick = (int)(Speed * 1000f) + m_CurrentTick;

            switch (MoveDirection)
            {
                case E_Driection.Right:
                    m_OffsetPos.X = 1;
                    break;
                case E_Driection.Down:
                    m_OffsetPos.Y = 1;
                    break;
                case E_Driection.Left:
                    m_OffsetPos.X = -1;
                    break;
                case E_Driection.Up:
                    m_OffsetPos.Y = -1;
                    break;
                case E_Driection.Max:
                default:
                    break;
            }

            //CurrentPos.X = CurrentPos.X + m_OffsetPos.X;
            //CurrentPos.Y = CurrentPos.Y + m_OffsetPos.Y;

            CurrentPos += m_OffsetPos;
        }

        
        public void UpdateMove()
        {
            //int tick = Environment.TickCount;
            //long tick2 = DateTime.Now.Ticks;

            if(m_NextTick > Environment.TickCount )
            {
                return;
            }

            m_NextTick = Environment.TickCount + (int)(Speed * 1000f);
            // 1초에 한번씩 이동

            //CurrentPos.X = CurrentPos.X + m_OffsetPos.X;
            //CurrentPos.Y = CurrentPos.Y + m_OffsetPos.Y;

            CurrentPos = CurrentPos + m_OffsetPos;


            //CurrentPos.X++;
            //if (CurrentPos.X > 10)
            //{
            //    CurrentPos.X = 2;

            //    //InGameTank.Instance().remove

            //}


        }

        public void DrawBullet( buffer p_buff )
        {
            p_buff.Draw("@", CurrentPos.X, CurrentPos.Y
                , (short)ConsoleColor.DarkMagenta );
        }



    }
}
