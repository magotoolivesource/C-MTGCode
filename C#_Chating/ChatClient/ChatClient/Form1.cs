using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChatServer;

namespace ChatClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void OnSend(IAsyncResult ar)
        {

        }

        protected void SendMessage(string p_userid, string p_msg)
        {
            // 데이터 방식 01
            //string sendmsg = string.Format("{0} : {1}", p_userid, p_msg);
            //byte[] senddata = Encoding.UTF8.GetBytes(sendmsg);

            //// 데이터보내기
            //m_Connect.BeginSend(senddata, 0, senddata.Length, SocketFlags.None, new AsyncCallback(OnSend), null);


            // 데이터 방식 02
            //List<byte> senddatalist = new List<byte>();
            //byte[] userid = Encoding.UTF8.GetBytes(p_userid);
            //Int32 size = userid.Length;
            //byte[] idsize = BitConverter.GetBytes(size);
            //senddatalist.AddRange(idsize);

            //byte[] msgbyte = Encoding.UTF8.GetBytes(p_msg);
            //Int32 msgsize = msgbyte.Length;
            //byte[] msgsizebyte = BitConverter.GetBytes(msgsize);
            //senddatalist.AddRange(msgsizebyte );

            //senddatalist.AddRange(userid);
            //senddatalist.AddRange(msgbyte);


            // 데이터 방식 3
            SocketDataCls senddata = new SocketDataCls();
            senddata.Name = p_userid;
            senddata.Message = p_msg;
            List<byte> senddatalist = new List<byte>();
            senddatalist.AddRange( senddata.GetByteData() );

            m_Connect.BeginSend(senddatalist.ToArray()
                , 0
                , senddatalist.Count
                , SocketFlags.None
                , new AsyncCallback(OnSend)
                , null);

        }

        public void OnConnect(IAsyncResult ar)
        {
            //m_Connect.EndConnect(ar);

            MessageBox.Show("접속 성공", "확인", MessageBoxButtons.OK);


            // 서버 접속 이후 처리할부분
            // 연결 된것 확인
            m_Connect.EndConnect(ar);
            
            // 내용 보내기
            //byte[] senddata = new byte[1024];
            //string userid = textBox2.Text;
            //byte[] senddata = Encoding.UTF8.GetBytes(userid);
            //// 데이터보내기
            //m_Connect.BeginSend(senddata, 0, senddata.Length, SocketFlags.None, new AsyncCallback(OnSend), null );
            SendMessage(textBox2.Text, "");
            // 내용 보내기



            // 데이터 받기 처리
            //m_Connect.BeginReceive();


        }

        Socket m_Connect = null;
        void ConnectServer()
        {
            m_Connect = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPAddress ipaddress = IPAddress.Parse(IPText.Text);
            IPEndPoint endpoint = new IPEndPoint(ipaddress, int.Parse(PortText.Text));

            try
            {
                m_Connect.BeginConnect(endpoint, new AsyncCallback(OnConnect),  null );

            }
            catch (Exception e)
            {
                string msg = string.Format("예외생김 : {0}", e.Message);
                MessageBox.Show(msg, "Error", MessageBoxButtons.OK);
            }


        }

        private void Button2_Click(object sender, EventArgs e)
        {
            ConnectServer();

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string userid = textBox2.Text;
            string sendmessage = InputTextBox.Text;
            SendMessage(textBox2.Text, InputTextBox.Text);
            InputTextBox.Text = "";

        }
    }
}
