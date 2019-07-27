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


        public void InitTankData( Vector2 p_initpos )
        {
            CurrentPos = p_initpos;
            HP = 2;
        }

        public void DrawTank(buffer p_buff )
        {
            p_buff.Draw("A", CurrentPos.X, CurrentPos.Y);
        }


        public void Move(E_Driection p_direction)
        {
            switch (p_direction)
            {
                case E_Driection.Right:
                    {
                        CurrentPos.X++;
                    }
                    break;
                case E_Driection.Down:
                    {
                        CurrentPos.Y++;
                    }
                    break;
                case E_Driection.Left:
                    {
                        CurrentPos.X--;
                    }
                    break;
                case E_Driection.Up:
                    {
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


    }
}
