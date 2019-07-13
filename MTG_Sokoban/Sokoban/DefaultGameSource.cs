using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    abstract class DefaultGameSource
    {
        protected DoubleBuffer.buffer m_Buffer;
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

        
        public virtual void Init()
        {
            Console.SetWindowSize(WindowWidth, WindowHeight);
            Console.Title = m_Title;

            m_Buffer = new DoubleBuffer.buffer(80, 30, 80, 30);

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
