using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;

namespace SerialPortHelper
{
    public class Helper
    {
        //获取配置文件COM口
        private static string strPort = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["COMPORT"]);

        SerialPort com;

        public void InitSerialPort(string port = null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(strPort))
                {
                    port = strPort;
                }

                com = new SerialPort();

                //串口接收函数绑定
                com.DataReceived += new SerialDataReceivedEventHandler(ReceiveData);

                //获取或设置串行波特率
                com.BaudRate = 9600;

                //获取或设置通信端口，包括但不限于所有可用的 COM 端口。
                com.PortName = strPort;

                //获取或设置每个字节的标准数据位长度。
                com.DataBits = 8;

                //打开串口
                com.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        byte[] readBuffer = new byte[5];
        int nTotal = 5;

        private void ReceiveData(object sender, SerialDataReceivedEventArgs e)
        {
            //接收缓冲区中数据的字节数
            int n = com.BytesToRead;
            
            byte[] buf = new byte[n];

            com.Read(readBuffer, 0, n);
        }
    }
}
