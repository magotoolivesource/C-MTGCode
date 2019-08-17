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

            //UnityCoroutinue.GetI.StartCoroutinue();
            //(UnityCoroutinue.GetI as UnityCoroutinue).StartCoroutinue();



            //TestCoroutinue testcoroutinue = new TestCoroutinue();
            ////testcoroutinue.Step02Init();
            //testcoroutinue.Step03Init();




            //// 참고용 소스
            //TestStep02Coroutinue step02 = new TestStep02Coroutinue();
            //step02.InitSetting01();



            //CoroutinueManager manager = CoroutinueManager.GetI;



            ////UnityCoroutinue coroutinuemanager = UnityCoroutinue.GetI;

            //Test03_UnityCoroutinue test03 = new Test03_UnityCoroutinue();
            //test03.InitSettingTestCoroutinue();


            //while (true)
            //{
            //    manager.UpdateCoroutinue();
            //    UnityCoroutinue.GetI.LoopUpdateCoroutinue();
            //}
            //// 참고용 소스





            //while()
            //{

            //}


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
