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
    public partial class Form1 : Form
    {

        Socket m_ServerSocket = null;


        public void ServerAcceptAsyncCallback(IAsyncResult ar)
        {

        }

        void StartServer()
        {
            m_ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );

            // 문자를 숫자로 바꾸기 함수 C++ 함수 찾기
            IPAddress ipaddress = IPAddress.Parse(IPText.Text);
            IPEndPoint endpoint = new IPEndPoint(ipaddress, 25000 );

            try
            {
                m_ServerSocket.Bind(endpoint);
                m_ServerSocket.Listen(10);
                m_ServerSocket.BeginAccept(new AsyncCallback(ServerAcceptAsyncCallback), null);
            }
            catch (Exception e)
            {
                Debug.WriteLine("서버 에러 : {0} ", e.Message);
            }




        }



        public Form1()
        {
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
