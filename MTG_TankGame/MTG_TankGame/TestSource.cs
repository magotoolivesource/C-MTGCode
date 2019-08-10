using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTG_TankGame
{

    public class TestDelegate
    {
        
        delegate int MyDelegate(int p_src, int p_dest);
        MyDelegate[] tempdelegate = new MyDelegate[10];

        List<MyDelegate> TempDelegateList = new List<MyDelegate>();


        int Add(int p_src, int p_dest)
        {
            return p_dest + p_src;
        }
        int Minus(int p_src, int p_dest)
        {
            return p_src - p_dest;
        }
        int Multi(int p_src, int p_dest)
        {
            return p_dest * p_src;
        }

        int Div(int p_src, int p_dest)
        {
            return (int)((float)p_dest / p_src);
        }

        int Persent(int p_src, int p_dest)
        {
            return (int)(p_dest % p_src);
        }


        public int Comparison(int x, int y)
        {
            return 0;
        }

        public void TestCalc()
        {
            //MyDelegate tempdelegate1 = new MyDelegate( Add );
            //MyDelegate tempdelegate2 = new MyDelegate(Minus);

            //tempdelegate = tempdelegate2;
            //int testval = tempdelegate(10, 20);


            tempdelegate[(int)ConsoleKey.Multiply - (int)ConsoleKey.Multiply] = new MyDelegate(Multi);
            tempdelegate[(int)ConsoleKey.Add - (int)ConsoleKey.Multiply ] = new MyDelegate(Add);
            tempdelegate[(int)ConsoleKey.Subtract - (int)ConsoleKey.Multiply] = new MyDelegate(Minus);
            tempdelegate[(int)ConsoleKey.Divide - (int)ConsoleKey.Multiply] = new MyDelegate(Div);
            tempdelegate[(int)ConsoleKey.Divide - (int)ConsoleKey.Multiply] = new MyDelegate(Div);

            //TempDelegateList.Add(new MyDelegate(Add));

            ConsoleKeyInfo keyinfo = Console.ReadKey();

            int delegateindex = (int)keyinfo.Key - (int)ConsoleKey.Multiply;
            if (delegateindex >= 0
                && delegateindex < tempdelegate.Length )
            {
                if( tempdelegate[delegateindex] != null  )
                {
                    int val = tempdelegate[delegateindex](10, 20);
                }

            }
            
            //Console.Write("값 : {0}", testval);




            List<int> test = new List<int>();
            test.Sort( Comparison );




            //int a = 10;
            //int b = 20;

            //int tempa = b;


            //ConsoleKeyInfo keyinfo2 = Console.ReadKey();
            //if( keyinfo2.Key == ConsoleKey.OemPlus )
            //{
            //    Add(a, b);
            //    //tempa;
            //}
            //else if(keyinfo2.Key == ConsoleKey.OemMinus)
            //{
            //    Minus(a, b);
            //    //tempa;
            //}



        }


    }



    
    //public class TestSource2
    //{

    //    public int[] m_TestIntArray;
    //    public List<int> m_TestIntList;


    //    public IEnumerator GetText()
    //    {
    //        yield return "abc";
    //        yield return "efg";
    //    }

    //    public string GetText2()
    //    {
    //        return "abc";
    //        return "egf";
    //    }

    //    public void TestSource()
    //    {
    //        GetText().MoveNext();
    //        string tempstr = (string)GetText().Current;

    //        GetText().MoveNext();
    //        string tempstr2 = (string)GetText().Current;

    //    }




    //}





    //class TestSource
    //{
    //    public int[] m_ArrayData = new int[5]
    //    {
    //        2, 7, 3, 1, 6
    //    };

    //    public List<int> m_IntList = new List<int>();

    //    static void Write2(IEnumerator<int> e)
    //    {
    //        while (e.MoveNext())
    //        {
    //            int value = e.Current;
    //            Console.WriteLine(value);
    //        }
    //    }


    //    int GetListValue()
    //    {
    //        if(m_Enumrator.MoveNext() )
    //        {
    //            return m_Enumrator.Current;
    //        }

    //        return -1;
    //    }

    //    void GetFN()
    //    {
    //        Debug.Write("GaetValueFN 1");
    //        return;

    //        Debug.Write("GaetValueFN 2");
    //    }

    //    IEnumerator GetValue33()
    //    {
    //        Debug.Write("GaetValueFN 1");

    //        yield return null;

    //        Debug.Write("GaetValueFN 2");

    //        yield return null;

    //        Debug.Write("GaetValueFN 3");
    //    }



    //    List<int>.Enumerator m_Enumrator;
    //    public void TestData()
    //    {
    //        m_IntList.Add(1);
    //        m_IntList.Add(3);
    //        m_IntList.Add(5);
    //        m_IntList.Add(8);

    //        int val0 = m_IntList[0];
    //        int val1 = m_IntList[1];
    //        int val2 = m_IntList[2];

    //        GetValue33();


    //        m_Enumrator = m_IntList.GetEnumerator();

    //        int val00 = GetListValue();
    //        int val01 = GetListValue();
    //        int val02 = GetListValue();





    //        //List<int>.Enumerator enurator = m_IntList.GetEnumerator();
    //        ////Write2(enurator);

    //        //enurator.MoveNext();
    //        //int val001 = enurator.Current;
    //        ////enurator.MoveNext();
    //        ////int val002 = enurator.Current;
    //        ////enurator.MoveNext();
    //        ////int val003 = enurator.Current;

    //        //while (enurator.MoveNext())
    //        //{
    //        //    int testval001 = enurator.Current;
    //        //    Debug.Write("Enumrator : " + testval001.ToString()  + "\n");
    //        //}










    //        int tempa = m_ArrayData[0];
    //        int tempb = m_ArrayData[1];


    //        foreach (int valuea in m_ArrayData)
    //        {
    //            Debug.Write( "Val : " + valuea.ToString() + "\n" );
    //        }
    //    }

    //}
}
