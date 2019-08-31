using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatServer
{
    static class Program
    {



        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

            


            //SocketDataCls tempdata = new SocketDataCls();
            //tempdata.Name = "우리들은";
            //tempdata.Message = "1학년";
            //tempdata.SendTime = 1234567;
            //tempdata.Icontype = "-_-;";
            //byte[] bytedataarr = tempdata.GetByteData();



            //// 소켓에서 보내기

            
            //// 받기쪽 
            //SocketDataCls resivedata = new SocketDataCls();
            //resivedata = SocketDataCls.GetDeserialize(bytedataarr);
            //Console.WriteLine( "출력 : " + resivedata.ToString() );





            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
