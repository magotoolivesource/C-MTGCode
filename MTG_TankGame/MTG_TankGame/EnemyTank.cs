using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTG_TankGame
{
    class EnemyTank : Tank
    {
        public Tank m_PlayerTank = null;


        protected float Speed = 1f;
        int m_CurrentTick = 0;
        int m_NextTick = 0;


        public void InitSettings( Tank p_playertank )
        {
            m_PlayerTank = p_playertank;
            m_CurrentTick = Environment.TickCount;
            m_NextTick = (int)(Speed * 1000f) + m_CurrentTick;

        }

        
        public override void UpdateTank()
        {
            // FSM, 상태머신, 

            base.UpdateTank();

            if (m_NextTick > Environment.TickCount)
            {
                return;
            }
            m_NextTick = Environment.TickCount + (int)(Speed * 1000f);


            E_Driection movetype = E_Driection.Max;
            Vector2 offsetpos = m_PlayerTank.CurrentPos - this.CurrentPos;
            if( Math.Abs( offsetpos.X ) > Math.Abs(offsetpos.Y) )
            {
                if (offsetpos.X < 0)
                {
                    movetype = E_Driection.Left;
                }
                else if(offsetpos.X > 0)
                {
                    movetype = E_Driection.Right;
                }

                //this.CurrentPos.X += offsetpos.X >= 0 ? 1 : -1;
            }
            else
            {
                if (offsetpos.Y < 0)
                {
                    movetype = E_Driection.Up;
                }
                else if (offsetpos.Y > 0)
                {
                    movetype = E_Driection.Down;
                }

                //this.CurrentPos.Y += offsetpos.Y > 0 ? 1 : -1;
            }

            if(movetype != E_Driection.Max)
            {
                this.Move(movetype);

                this.Fire();
            }
            



        }

    }
}
