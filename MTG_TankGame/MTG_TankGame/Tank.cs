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

        protected char[] Model = new char[(int)E_Driection.Max]
        {
            '>'
            , 'V'
            , '<'
            , '^'
        };

        public Vector2 CurrentPos;
        public int HP = 2;
        Stage m_CurrentStageData = null;
        public E_Driection m_DirectionVal = E_Driection.Max;
        InGameTank m_LinkInGameTank = null;

        ConsoleColor m_Color = ConsoleColor.Red;

        public virtual void InitTankData( Vector2 p_initpos
            , E_Driection p_direction
            , ConsoleColor p_color = ConsoleColor.Green
            //, Stage p_stagedata
            //, InGameTank p_tankmanager
            )
        {
            m_DirectionVal = p_direction;
            m_CurrentStageData = InGameTank.Instance().m_InGameStage;// p_stagedata;
            CurrentPos = p_initpos;
            HP = 2;
            m_LinkInGameTank = InGameTank.GetI;

            m_Color = p_color;
        }

        public void DrawTank(buffer p_buff )
        {
            //char model = Model[ (int)m_DirectionVal ];
            p_buff.Draw(Model[(int)m_DirectionVal], CurrentPos.X, CurrentPos.Y, (short)m_Color);


        }

        // class Tank 
        public virtual void UpdateTank()
        {

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

        public void SetDamage(int p_damage)
        {
            HP -= p_damage;
            if( HP <= 0)
            {
                // 지워라 

            }
        }
        
        public void Fire()
        {
            

            //m_LinkInGameTank.CreateBullet(this);

            InGameTank.Instance().CreateBullet(this);

            ;
        }

    }
}
