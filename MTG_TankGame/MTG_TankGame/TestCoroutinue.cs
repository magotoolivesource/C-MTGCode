using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTG_TankGame
{
    class TestCoroutinue
    {
        static IEnumerable<int> GetNumber()
        {
            yield return 10;
            yield return 20;
            yield return 30;

            //yield return 0;

            yield return 10;
            yield return 20;
            yield return 30;
        }


        public void Step01Init()
        {
            //for (int i = 0; i < 1000; i++)
            //{

            //}
            foreach( var testval in GetNumber() )
            {
                Console.WriteLine("Val : {0}", testval);

            }
        }



        class MyTestList
        {
            private int[] data = { 1, 3, 5, 10 };

            public IEnumerator<int> GetEnumerator()
            {
                int i = 0;
                while( i < data.Length )
                {
                    yield return data[i];
                    ++i;
                }

            }
        }

        public void Step02Init()
        {
            var list = new MyTestList();
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }


            // 1, 3, 5, 10
            IEnumerator<int> it = list.GetEnumerator();
            while (it.MoveNext())
            {
                Console.WriteLine("수동 : " + it.Current); // 1
            }


            IEnumerator<int> it2 = list.GetEnumerator();

            

            it.MoveNext();
            Console.WriteLine( "수동 : " + it.Current ); // 1

            it2.MoveNext();
            Console.WriteLine("수동-- : " + it2.Current); // 1

            it.MoveNext();
            Console.WriteLine("수동 2 : " + it.Current); // 3
            it.MoveNext();
            Console.WriteLine("수동 3 : " + it.Current); // 5

            it2.MoveNext();
            Console.WriteLine("수동-- : " + it2.Current); // 3




            it = list.GetEnumerator();
            it.MoveNext();
            Console.WriteLine("수동 >>  : " + it.Current); // 1

            it.MoveNext();
            Console.WriteLine("수동 >> 2 : " + it.Current); // 3
            it.MoveNext();
            Console.WriteLine("수동 >> 3 : " + it.Current); // 5
            it.MoveNext();
            Console.WriteLine("수동 >> 4 : " + it.Current); // 10
            it.MoveNext();
            Console.WriteLine("수동 >> 5 : " + it.Current); // 3
            it.MoveNext();
            Console.WriteLine("수동 >> 6 : " + it.Current); // 3




        }


        // Vector, queue, Map, Linkdlist
        public class MyList3
        {
            // public static IEnumerator<object> GetEnumrator2233()
            public static IEnumerator GetEnumrator2233()
            {
                yield return 10;
                yield return 4;
                yield return "abc";
                yield return null;
                yield return 7;
            }

        }

        public void Step03Init()
        {
            IEnumerator it = MyList3.GetEnumrator2233();
            IEnumerator it2 = MyList3.GetEnumrator2233();


            it.MoveNext();
            Console.WriteLine("Step03 01 : {0}", it.Current); // 10
            it.MoveNext();
            Console.WriteLine("Step03 02 : {0}", it.Current); // 4
            it.MoveNext();
            Console.WriteLine("Step03 03 : {0}", it.Current); // string "abc"

            it.MoveNext();
            Console.WriteLine("Step03 04 : {0}", it.Current); // null 

            it.MoveNext();
            Console.WriteLine("Step03 05 : {0}", it.Current); // 7
            it.MoveNext();
            Console.WriteLine("Step03 06 : {0}", it.Current); // 7



        }




    }
}

