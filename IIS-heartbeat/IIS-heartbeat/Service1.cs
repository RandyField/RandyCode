using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace IIS_heartbeat
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Elapsed += new System.Timers.ElapsedEventHandler(TimedEvent);
            timer.Interval = 1000 * 10 * 60;//每10分钟执行一次
            //timer.Interval = 10000;//每19分钟执行一次
            timer.Enabled = true;
        }

        //定时执行事件
        private void TimedEvent(object sender, System.Timers.ElapsedEventArgs e)
        {
            //Debugger.Launch();
            Common.HttpHelper httphelper = new Common.HttpHelper();

            //业务逻辑代码
            //this.WriteLog("iis-heartbeat：【服务业务执行一次】");
            //Untility.XmlParseHelper.SetPath(false);
            List<string> list = Untility.XmlParseHelper.GetNodeList("Url", "url");
            foreach (var url in list)
            {
                httphelper.HttpGet(url);
                this.WriteLog(string.Format("iis-heartbeat：HttpGet-{0}", url));
            }
        }

        protected override void OnStart(string[] args)
        {

            this.WriteLog("iis-heartbeat：【服务启动】");

        }

        protected override void OnStop()
        {
            this.WriteLog("iis-heartbeat：【服务停止】");
        }
        protected override void OnShutdown()
        {
            this.WriteLog("iis-heartbeat：【计算机关闭】");
        }

        #region 记录日志
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="msg"></param>
        private void WriteLog(string msg)
        {

            //string path = @"C:\log.txt";

            //该日志文件会存在windows服务程序目录下
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\log.txt";
            FileInfo file = new FileInfo(path);
            if (!file.Exists)
            {
                FileStream fs;
                fs = File.Create(path);
                fs.Close();
            }

            using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(DateTime.Now.ToString() + "   " + msg);
                }
            }
        }
        #endregion

    }
}
