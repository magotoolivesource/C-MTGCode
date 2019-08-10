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

            //TestDelegate testdelegate = new TestDelegate();
            //testdelegate.TestCalc();

            //TestSource2 test = new TestSource2();
            //test.TestSource();


            //TestSource test = new TestSource();
            //test.TestData();

            //InGameTank tankgame = new InGameTank();
            //tankgame.InGameStart();


            //InGameTank tankgame = InGameTank.Instance();
            //tankgame.InGameStart();

            InGameTank.Instance().InGameStart();



        }
    }
}
