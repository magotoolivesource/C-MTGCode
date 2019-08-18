﻿using System;
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
    public partial class Form1 : Form, testInterface
    {



        Socket m_ServerSocket = null;


        
        public void ServerAcceptAsyncCallback(IAsyncResult ar)
        {
            //throw new Exception("가나다");
            textBox1.AppendText( "접속했습니다.\r\n"  );


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

            // 문자를 숫자로 바꾸기 함수 C++ 함수 찾기
            IPAddress ipaddress = IPAddress.Parse(IPText.Text);

            ipaddress = IPAddress.Any;

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
