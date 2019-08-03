using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTG_TankGame
{
    class Program
    {
        static void Main(string[] args)
        {
            TestSource test = new TestSource();
            test.TestData();

            //InGameTank tankgame = new InGameTank();
            //tankgame.InGameStart();


            //InGameTank tankgame = InGameTank.Instance();
            //tankgame.InGameStart();

            InGameTank.Instance().InGameStart();



        }
    }
}
