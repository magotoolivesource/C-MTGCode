using InGameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTG_TankGame
{
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


    }
}
