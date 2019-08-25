using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;



namespace ChatServer
{

    //[System.Serializable]
    public partial class Form1 : Form, testInterface
    {
        Socket m_ServerSocket = null;
        private byte[] m_ResiveBuffer = new byte[1024];


        public void ReceiveAsyncCallback(IAsyncResult ar)
        {
            // 클라이언트에서 보내는 데이터 받기 위한 함수
            Socket clientsocket = (Socket)ar.AsyncState;
            clientsocket.EndReceive(ar);


            //// 데이터 방식 01
            //// 클라이언트에서 날아온 데이터 파싱하기
            //string userstring = Encoding.UTF8.GetString(m_ResiveBuffer);
            //string temstr = string.Format("{0}\r\n", userstring);
            //textBox1.AppendText(temstr);


            // 데이터 방식 02
            int useridsize = BitConverter.ToInt32(m_ResiveBuffer, 0); // userid 사이즈
            int msgsize = BitConverter.ToInt32(m_ResiveBuffer, 4);

            string userstr = Encoding.UTF8.GetString(m_ResiveBuffer, 8, useridsize);
            string msgstr = Encoding.UTF8.GetString(m_ResiveBuffer, 8 + useridsize, msgsize);

            string temstr = string.Format("{0} : {1}\r\n", userstr, msgstr);
            textBox1.AppendText(temstr);




            m_ResiveBuffer = new byte[1024];
            // 클라이언트 데이터 연결 대기
            clientsocket.BeginReceive(m_ResiveBuffer, 0, m_ResiveBuffer.Length, SocketFlags.None
                , new AsyncCallback(ReceiveAsyncCallback)
                , clientsocket );
        }


        

        public void ServerAcceptAsyncCallback(IAsyncResult ar)
        {
            //m_ServerSocket.Receive();
            //m_ServerSocket.Connect();
            //m_ServerSocket.Accept();
            //m_ServerSocket.Send();
            //m_ServerSocket.Close();


            // 다른 클라이언트 연결 대기
            m_ServerSocket.BeginAccept(new AsyncCallback(ServerAcceptAsyncCallback), null);



            // 클라이언트와 데이터 통신위한 처리
            Socket clientsocket = m_ServerSocket.EndAccept(ar);
            // byte[] buffer, int offset, int size, SocketFlags socketFlags, AsyncCallback callback
            // , object state
            
            clientsocket.BeginReceive(m_ResiveBuffer
                , 0
                , m_ResiveBuffer.Length
                , SocketFlags.None
                , new AsyncCallback(ReceiveAsyncCallback)
                , clientsocket);


            //// 클라이언트 데이터
            //Socket clientSocket = m_listen_socket.EndAccept(p_ar);

            //// 소켓 연결된 유저를 리시브에서 받을수 있도록 처리
            //clientSocket.BeginReceive(byteData, 0,
            //    byteData.Length, SocketFlags.None,
            //    //new AsyncCallback( (ar) => { OnReceive(ar); } ), clientSocket
            //    new AsyncCallback(OnReceive), clientSocket
            //    );

            string tempstr = string.Format(" {0} 접속했습니다.\r\n", clientsocket.RemoteEndPoint.ToString());
            textBox1.AppendText(tempstr);

            //throw new Exception("가나다");
            //m_ServerSocket.BeginReceive();
        }


        public void TestFN()
        {
            if(true)
            {
                throw new NotImplementedException("가나다");
            }
            
            //throw new Exception("가나다");

            Debug.WriteLine("ABCDEFG");
        }

        void UpdateUI( )
        {
            if(m_ServerSocket != null)
            {
                button2.Text = "서버실행중..";
                button2.Enabled = false;
            }
            else
            {
                button2.Text = "서버시작";
                button2.Enabled = true;
            }
            

        }

        void StartServer()
        {
            m_ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );

            //Convert.ToInt32();
            //atoi()

            IPAddress ipaddress = null;
            if (IPText.Text == "0.0.0.0")
            {
                // 문자를 숫자로 바꾸기 함수 C++ 함수 찾기
                ipaddress = IPAddress.Parse(IPText.Text);
            }
            else
            {
                ipaddress = IPAddress.Any;
            }


            IPEndPoint endpoint = new IPEndPoint(ipaddress, int.Parse( PortText.Text ) );

            try
            {
                m_ServerSocket.Bind(endpoint);
                m_ServerSocket.Listen(10);
                m_ServerSocket.BeginAccept(new AsyncCallback(ServerAcceptAsyncCallback), null);
                UpdateUI();
            }
            catch ( Exception e )
            {
                string msg = string.Format("예외생김 : {0}", e.Message);
                //Debug.WriteLine(msg);
                MessageBox.Show(msg, "Error", MessageBoxButtons.OK);
                UpdateUI();
            }
            
        }



        public Form1()
        {
            this.Text = "서버창";
            // https://westwoodforever.blogspot.com/2012/08/xxx.html
            // 폼에서 사용하는 컨트롤을 다른곳에서 사용할때 디버그 모드에서 에러가 생긴다
            // 쓰레드 에러를 없애기 위한 값
            CheckForIllegalCrossThreadCalls = false;


            InitializeComponent();


            textBox1.Text = "가나다\r\n";

            //this.button1.Click += new System.EventHandler(this.Button1_Click2);

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("클릭1");

            //textBox1.Text = textBox1.Text + "\n\r" + InputTextBox.Text;
            textBox1.AppendText(InputTextBox.Text + "\r\n");
            InputTextBox.Text = "";
        }

        private void Button1_Click2(object sender, EventArgs e)
        {
            Debug.WriteLine("클릭2");
        }

        private void InputTextBox_TextChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("확인01");
        }

        private void InputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter )
            {
                Button1_Click( null, EventArgs.Empty );
            }

        }

        private void PortText_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            StartServer();
        }

        
    }
}
