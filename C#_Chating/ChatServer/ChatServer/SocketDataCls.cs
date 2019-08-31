using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{

    class TempCls : IDisposable
    {
        public string Name;
        public string Message;

        public void Dispose()
        {
            
        }
    }

    [System.Serializable]
    class SocketDataCls
    {
        public string Name;
        public string Message;
        public long SendTime;
        public string Icontype;
        public int typedata3;

        public override string ToString()
        {
            return string.Format("{0}, {1}", Name, Message);
        }

        public string ToChatMsg()
        {
            return string.Format("{0}: {1}", Name, Message);
        }


        // 직렬화 저장
        public byte[] GetByteData()
        {


            // 기본 방식
            //byte[] outputdata = null;
            //var _MemoryStream = new MemoryStream();
            //IFormatter _binaryformatter = new BinaryFormatter();
            //_binaryformatter.Serialize( _MemoryStream, this);
            //outputdata = _MemoryStream.ToArray();

            using( var tempclsdata = new TempCls() )
            {

            }
            
            byte[] outputdata = null;
            using ( var _MemoryStream = new MemoryStream() )
            {
                IFormatter _binaryformatter = new BinaryFormatter();
                _binaryformatter.Serialize(_MemoryStream, this);

                outputdata = _MemoryStream.ToArray();
            }

            return outputdata;
        }


        // 역직렬화 로드
        public static SocketDataCls GetDeserialize( byte[] p_data )
        {
            //MemoryStream memStream = new MemoryStream();
            //BinaryFormatter binForm = new BinaryFormatter();
            //memStream.Write(p_val, 0, p_val.Length);
            //memStream.Seek(0, SeekOrigin.Begin);
            //LocalTankData outdata = (LocalTankData)binForm.Deserialize(memStream);

            //MemoryStream memstream = new MemoryStream(p_data);
            MemoryStream memstream = new MemoryStream();
            memstream.Write(p_data, 0, p_data.Length);
            memstream.Seek(0, SeekOrigin.Begin);
            IFormatter binform = new BinaryFormatter();

            SocketDataCls cls = binform.Deserialize(memstream) as SocketDataCls;

            
            return cls;
        }

        public void TestFN()
        {
            TestPara("{0}", 10, 20, 30, 50, 20.0f);
        }

        public void TestPara(String format, params object[] args)
        {
            int count = args.Length;
            int a = (int)args[0];
        }

        //public byte[] Serialize()
        //{
        //    byte[] bytes = null;
        //    using (var _MemoryStream = new MemoryStream())
        //    {
        //        IFormatter _BinaryFormatter = new BinaryFormatter();
        //        _BinaryFormatter.Serialize(_MemoryStream, this);
        //        bytes = _MemoryStream.ToArray();
        //    }
        //    return bytes;
        //}



    }



}
