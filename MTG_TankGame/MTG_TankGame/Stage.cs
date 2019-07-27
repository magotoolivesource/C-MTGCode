using InGameEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTG_TankGame
{
    public enum E_WallType
    {
        None = 0,
        Broken,
        Imun,

        Max
    }


    class Wall2
    {
        public E_WallType WallType;
        public int HP;
    }

    struct Wall
    {
        public Wall(E_WallType p_type = E_WallType.Broken
            , int p_hp = 1 ) 
        {
            WallType = p_type;
            HP = p_hp;
            DrawAsciiCode = ' ';
        }

        public E_WallType WallType;
        public int HP;
        public char DrawAsciiCode;
    }

    struct Vector2
    {
        public int X;
        public int Y;
    }

    //struct Wall2
    //{
    //    public Wall2(int p_val = 0)
    //    {
    //        m_val = 0;
    //        m_Pos = new Vector2();
    //    }

    //    public void GetType()
    //    {

    //    }

    //    public int m_val;
    //    public Vector2 m_Pos;
    //}



    class Stage
    {
        string[][] m_Stage = new string[][]
        {
            new string[]
            {
                 "+=====================+"
                ,"|     |     |         |"
                ,"|     |     |         |"
                ,"|     -     |         |"
                ,"|           |         |"
                ,"|                     |"
                ,"|                     |"
                ,"|                     |"
                ,"|                     |"
                ,"+---------------------+"
            },

            new string[]
            {
                 "+===============+"
                ,"|     |     |   |"
                ,"|     |     |   |"
                ,"|     -     |   |"
                ,"|           |   |"
                ,"|               |"
                ,"|               |"
                ,"+---------------+"
            },

        };

        Wall[,] m_CuurentStageInfo = null;
        string[] m_CurrentStage = null;

        Wall2[,] m_TempStageInfo2 = null;

        public void ResetStage( int p_stageindex )
        {
            int stageindex = p_stageindex;
            m_CurrentStage = m_Stage[stageindex];

            int heightsize = m_CurrentStage.Length;
            int widthsize = m_CurrentStage[0].Length;

            m_CuurentStageInfo = new Wall[heightsize, widthsize];


            for (int y = 0; y < heightsize; y++)
            {
                for (int x = 0; x < widthsize; x++)
                {
                    m_CuurentStageInfo[y, x].DrawAsciiCode = m_CurrentStage[y][x];
                    if (m_CurrentStage[y][x] == '|'
                        || m_CurrentStage[y][x] == '-'
                        || m_CurrentStage[y][x] == '+'
                        )
                    {
                        m_CuurentStageInfo[y, x].WallType = E_WallType.Broken;
                        m_CuurentStageInfo[y, x].HP = 1;
                    }
                    else if (m_CurrentStage[y][x] == '=')
                    {
                        m_CuurentStageInfo[y, x].WallType = E_WallType.Imun;
                        m_CuurentStageInfo[y, x].HP = 1;
                    }
                    else
                    {
                        m_CuurentStageInfo[y, x].WallType = E_WallType.None;
                        m_CuurentStageInfo[y, x].HP = 0;
                    }
                }
            }
        }
        public void InitStage()
        {

            ResetStage(1);


            //m_TempStageInfo2 = new Wall2[heightsize, widthsize];
            //for (int y = 0; y < heightsize; y++)
            //{
            //    for (int x = 0; x < widthsize; x++)
            //    {
            //        m_TempStageInfo2[y, x] = new Wall();
            //    }
            //}


        }

        public void DrawStage(buffer p_buffer)
        {
            int heightsize = m_CuurentStageInfo.GetLength(0);
            int widthsize = m_CuurentStageInfo.GetLength(1);

            //m_CuurentStageInfo = new Wall[heightsize, widthsize];


            for (int y = 0; y < heightsize; y++)
            {
                for (int x = 0; x < widthsize; x++)
                {
                    p_buffer.Draw(m_CuurentStageInfo[y, x].DrawAsciiCode, x, y);
                    //if(m_CuurentStageInfo[y,x].WallType ==  )
                }
            }




            //p_buffer.Draw("Test Draw", 0, 0);

            //Wall wallcls = new Wall(3);
            //Wall2 wallstuct = new Wall2(3);
            //Wall2[] wallarray = new Wall2[3]
            //{
            //    new Wall2()
            //    ,new Wall2()
            //    ,new Wall2()
            //};

            //for (int i = 0; i < 3; i++)
            //{
            //    wallarray[i].m_val = 40;
            //}
            //foreach (Wall2 item in wallarray)
            //{
            //    item.m_val = 30;
            //}
            ////wallarray[0] = 

            //wallcls.m_val = 7;
            // wallstuct.m_val = 5;

            // Wall tempwallcls = wallcls;
            // Wall2 tempwallstruct = wallstuct;

            // tempwallcls.m_val = 10;
            // tempwallstruct.m_val = 20;

            // // 10, 5, 10, 20
            // Debug.Print("{0}, {1}, {2}, {3}", wallcls.m_val, tempwallcls.m_val
            //     , wallstuct.m_val, tempwallstruct.m_val );


        }



    }
}
