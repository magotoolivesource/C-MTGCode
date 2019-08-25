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

        public void OnConnect(IAsyncResult ar)
        {
            //m_Connect.EndConnect(ar);

            MessageBox.Show("접속 성공", "확인", MessageBoxButtons.OK);


            // 서버 접속 이후 처리할부분
            // 연결 된것 확인
            m_Connect.EndConnect(ar);
            
            //byte[] senddata = new byte[1024];
            string userid = textBox2.Text;
            byte[] senddata = Encoding.UTF8.GetBytes(userid);

            // 데이터보내기
            m_Connect.BeginSend(senddata, 0, senddata.Length, SocketFlags.None, new AsyncCallback(OnSend), null );


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
    }
}
