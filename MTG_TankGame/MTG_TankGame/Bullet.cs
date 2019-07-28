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


        public void InitSetting( Vector2 p_pos
            , E_Driection p_direction
            , float p_movespeed )
        {
            MoveDirection = p_direction;
            CurrentPos = p_pos;
            Speed = p_movespeed;
        }

        public void UpdateMove()
        {
            CurrentPos.X++;
            if(CurrentPos.X > 10)
            {
                CurrentPos.X = 2;
            }
        }
        public void DrawBullet( buffer p_buff )
        {
            p_buff.Draw("@", CurrentPos.X, CurrentPos.Y
                , (short)ConsoleColor.DarkMagenta );
        }



    }
}
