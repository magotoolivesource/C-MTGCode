using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InGameEngine
{
    class Singleton<T> where T : class, new()
    {
        static T m_Instance = null;

        public static T Instance()
        {
            if (m_Instance == null)
            {
                m_Instance = new T();
            }

            return m_Instance;
        }
    }



    abstract class DefaultGameSource
    {
        protected static Random m_Rand = new Random();

        public static int Range()
        {
            return m_Rand.Next();
        }

        public static int Range(int p_max)
        {
            return m_Rand.Next(p_max);
        }

        public static bool RangePersent(int p_persent )
        {
            int rand = m_Rand.Next(100);
            return rand <= p_persent ? true : false;
        }

        public static int RangeInclude(int p_max)
        {
            return m_Rand.Next(p_max + 1);
        }

        public static int Range(int p_min, int p_max)
        {
            return m_Rand.Next(p_min, p_max);
        }




        protected buffer m_Buffer;
        protected const int WindowWidth = 80;
        protected const int WindowHeight = 30;
        protected string m_Title = "NoneGame";

        protected string m_TempTitle = "";
        // 프로퍼티
        public string Title
        {
            get { return m_TempTitle; }
            set
            {
                m_Title = value;
                m_TempTitle = value;
            }
        }

        
        protected virtual void Initialze()
        {

        }
        public virtual void Init()
        {
            Console.SetWindowSize(WindowWidth, WindowHeight);
            Console.Title = m_Title;

            m_Buffer = new buffer(80, 30, 80, 30);


            Initialze();
        }

        /// <summary>
        /// 넌블럭킹 방식 기본 참고용 소스
        /// protected override void LoopInputFN()
        //  {
        //    if(m_CurrentKeyInfo == null)
        //        return;
        //
        //    // 소스 로직 적용하기
        //  }
        /// </summary>
        protected abstract void LoopInputFN();
        protected abstract void LoopDraw();

        protected virtual void Release()
        {

        }

        protected ConsoleKeyInfo? m_CurrentKeyInfo = null;

        protected virtual void GetInputKey()
        {
            if (!Console.KeyAvailable)
            {
                return;
            }

            m_CurrentKeyInfo = Console.ReadKey();
        }

        public virtual void InGameStart()
        {
            Init();


            while (true)
            {
                m_CurrentKeyInfo = null;
                GetInputKey();
                LoopInputFN();

                LoopDraw();

                m_Buffer.Print();
                m_Buffer.Clear();
            }

            Release();

        }







    }
}
