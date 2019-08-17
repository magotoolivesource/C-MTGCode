using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InGameEngine
{
    // generic, templete
    //class SingleTon <T1, T2> where T1 : class, new() where T2 : struct
    //{
    //    protected T2 m_TestData;

    class SingleTon<T1> where T1 : class, new()
    {
        // 싱글톤
        protected static T1 m_Instance = null;
        public static T1 GetI
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new T1();
                }
                return m_Instance;
            } 
        }



    }

}
