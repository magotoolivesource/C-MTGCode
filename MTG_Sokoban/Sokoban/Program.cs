using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{

    class Program
    {
        static void Main(string[] args)
        {
            SokobanCls sokoban = new SokobanCls("소코반");
            //sokoban.SetTitle("소코반");
            sokoban.Init();


            SokobanCls tempsokoban = new SokobanCls("aa");
            sokoban = tempsokoban;


            sokoban.InGameStart();


        }
    }
}
