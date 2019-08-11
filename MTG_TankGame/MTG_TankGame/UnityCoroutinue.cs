using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTG_TankGame
{

    // 코루틴 참고용 소스
    // https://github.com/rozgo/Unity.Coroutine/blob/master/Coroutine.cs


    // https://gist.github.com/enghqii/cd989a3f4aad28e858015ed44259dd66
    class UnityCoroutinue : SingleTon<UnityCoroutinue>
    {
        public class Coroutine
        {
            public IEnumerator routine;
            public Coroutine waitForCoroutine;
            public bool finished = false;

            public Coroutine() { }
            public Coroutine(IEnumerator routine) { this.routine = routine; }
        }

        public class WaitCoroutine : Coroutine
        {
            float m_DelaySec = 0f;
            long m_NextTick = 0;
            public WaitCoroutine(float p_sec)
            {
                m_DelaySec = p_sec;
                m_NextTick = DateTime.Now.Ticks + (long)(10000000 * p_sec);
                routine = WaitSecEnum();

                //CoroutineSchedule.GetI.InsertCoroutinueL(this);
            }

            IEnumerator WaitSecEnum()
            {
                while (m_NextTick >= DateTime.Now.Ticks)
                {
                    //System.Console.WriteLine(count);
                    yield return true;  // yield return null; 같은 소스임
                }

                yield break;
            }
        }


        List<Coroutine> m_CoroutineList = new List<Coroutine>();

        public Coroutine StartCoroutinue( IEnumerator p_routine)
        {
            Coroutine addcom = new Coroutine(p_routine);
            m_CoroutineList.Add(addcom);
            return addcom;
        }

        public void LoopUpdateCoroutinue()
        {
            foreach (Coroutine coroutine in m_CoroutineList.Reverse<Coroutine>())
            {
                if (coroutine.routine.Current is Coroutine)
                    coroutine.waitForCoroutine = coroutine.routine.Current as Coroutine;

                if (coroutine.waitForCoroutine != null && coroutine.waitForCoroutine.finished)
                    coroutine.waitForCoroutine = null;


                if (coroutine.waitForCoroutine != null)
                {
                    // 추가됨 ----------
                    if (coroutine.waitForCoroutine.routine.MoveNext())
                    {
                        coroutine.waitForCoroutine.finished = false;
                    }
                    else
                    {
                        coroutine.waitForCoroutine.finished = true;
                    }
                    // 추가됨 ----------

                    continue;
                }

                if (coroutine.routine.MoveNext())
                {
                    coroutine.finished = false;
                }
                else
                {
                    m_CoroutineList.Remove(coroutine);
                    coroutine.finished = true;
                }

            }

        }
    }


    class Test03_UnityCoroutinue
    {

        IEnumerator TestWaitSecCoroutinue(float p_delaysec, int p_maxcount)
        {
            int tempcount = 0;
            Debug.WriteLine("LoopAICoroutinue 시작 : " + tempcount.ToString());

            while(true)
            {
                yield return new UnityCoroutinue.WaitCoroutine(p_delaysec);

                Debug.WriteLine("1초에 한번 : " + tempcount.ToString());
                tempcount++;

                if( tempcount > p_maxcount)
                {
                    break;
                }
            }

            Debug.WriteLine("종료 : " + tempcount.ToString());
            yield break;
        }


        public void InitSettingTestCoroutinue()
        {
            UnityCoroutinue.GetI.StartCoroutinue( TestWaitSecCoroutinue(1f, 10) );

        }


    }

}
