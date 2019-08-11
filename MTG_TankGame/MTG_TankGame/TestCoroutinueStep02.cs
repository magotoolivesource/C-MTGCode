using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTG_TankGame
{


    class Coroutinue02
    {
        public IEnumerator CurrentEnumrator = null;

        public Coroutinue02(IEnumerator p_enumrator)
        {
            CurrentEnumrator = p_enumrator;
        }

    }


    class CoroutinueManager
    {

        #region 싱글톤

        private static CoroutinueManager m_Instance = null;
        public static CoroutinueManager GetI
        {
            get
            {
                if (CoroutinueManager.m_Instance == null)
                {
                    CoroutinueManager.m_Instance = new CoroutinueManager();
                }

                return CoroutinueManager.m_Instance;
            }
        }

        #endregion



        private List<Coroutinue02> m_CoroutinueList = new List<Coroutinue02>();

        public void StartCoroutinue( IEnumerator p_enumrator )
        {
            Coroutinue02 addcoroutinue = new Coroutinue02(p_enumrator);
            m_CoroutinueList.Add(addcoroutinue);

        }

        public void UpdateCoroutinue()
        {

            foreach (var item in m_CoroutinueList.Reverse<Coroutinue02>() )
            {
                if( item.CurrentEnumrator.MoveNext() )
                {

                }
                else
                {
                    m_CoroutinueList.Remove(item);
                }
            }

        }

    }




    class TestStep02Coroutinue
    {

        private void WriteData(int p_data)
        {
            Console.WriteLine("WriteData : {0} ", p_data);
        }


        public IEnumerator TestEnum2()
        {
            WriteData(100);
            yield return null;
            WriteData(101);
            yield return null;
            WriteData(102);
            yield return null;
            WriteData(103);
            yield return null;
        }

        private IEnumerator TestEnum()
        {
            //Console.WriteLine("TestEnum 001 ");
            WriteData(1);
            yield return null;
            //Console.WriteLine("TestEnum 002 ");
            WriteData(2);
            yield return null;
            //Console.WriteLine("TestEnum 003 ");
            WriteData(3);
            yield return null;
            Console.WriteLine("TestEnum 004 ");
            yield return null;
        }

        IEnumerator TestDelaySec( int p_tick )
        {
            int nexttickcount = 0;// Environment.TickCount + p_tick;
            int count = 0;

            while(true)
            {
                yield return null;

                if(Environment.TickCount > nexttickcount )
                {
                    if ( count > 10)
                    {
                        yield break;
                    }

                    WriteData(count);
                    count++;
                    nexttickcount = Environment.TickCount + p_tick;
                }
            }

        }

        public void InitSetting01()
        {
            CoroutinueManager manager = CoroutinueManager.GetI;
            manager.StartCoroutinue(TestEnum());
            manager.StartCoroutinue(TestEnum2());

            manager.StartCoroutinue( TestDelaySec(1000) );

        }

        //public void TestCortouinueInit()
        //{
        //    // 개인적으로 적용하는 방식
        //    //Coroutinue02 testaa = new Coroutinue02( TestEnum() );

        //    //testaa.CurrentEnumrator.MoveNext();
        //    //testaa.CurrentEnumrator.MoveNext();
        //    //testaa.CurrentEnumrator.MoveNext();
        //    //testaa.CurrentEnumrator.MoveNext();



        //    CoroutinueManager manager = new CoroutinueManager();
        //    manager.StartCoroutinue( TestEnum() );
        //    manager.StartCoroutinue( TestEnum2() );

        //    manager.UpdateCoroutinue();
        //    manager.UpdateCoroutinue();
        //    manager.UpdateCoroutinue();
        //    manager.UpdateCoroutinue();
        //    manager.UpdateCoroutinue();
        //    manager.UpdateCoroutinue();
        //    manager.UpdateCoroutinue();
        //    manager.UpdateCoroutinue();
        //    manager.UpdateCoroutinue();

        //}


    }

}

