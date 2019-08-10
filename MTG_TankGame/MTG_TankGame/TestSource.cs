using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTG_TankGame
{
    
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
