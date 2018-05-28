using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using Volume;

namespace RecreateXML
{
    class Program
    {
        static float sysVolume = 0f;

        //播放路径
        static string url = System.Configuration.ConfigurationManager.AppSettings["xmlRoute"];
        static VolumeControl volumeControl;

        static void Main(string[] args)
        {
            try
            {
                GetSysVolume();
                SetSysVolume();

                if (!File.Exists(url))
                {
                    WriteConfigFile();
                    Console.WriteLine("xml配置文件初始化成功，可以去相应路径进行查看修改！");
                }
                else
                {
                    Console.WriteLine("该xml配置文件已经存在，如果需要初始化，请去相应路径进行删除后，再运行本程序！");
                }
                Console.WriteLine("请按任意键退出...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        /// <summary>
        /// 获取系统主音量
        /// </summary>
        static void GetSysVolume()
        {
            float a = 0f;
            volumeControl = VolumeControl.Instance;
            double volume = volumeControl.MasterVolume;
            sysVolume = (float)volume;
        }

        /// <summary>
        /// 设置系统主音量
        /// </summary>
        static void SetSysVolume()
        {
            volumeControl.MasterVolume = 100.0f;
            //double temp = volumeControl.MasterVolume; test
        }

        /// <summary>
        /// 写配置文件
        /// </summary>
        static void WriteConfigFile()
        {
            var xDoc = new XDocument(
             new XComment("本xml配置文件初始化由代码生成，可以按需对节点进行修改，详情参考各节点注释"),
             new XElement("root",
                 new XElement("InitializeVoice", new XAttribute("name", "TTPlayer"), new XAttribute("level", Math.Ceiling(sysVolume).ToString())),

                 new XComment("语音播放开始 指定程序设置音量"),
                 new XElement("VoicePlayStart", new XElement("volume", new XAttribute("name", "TTPlayer"), new XAttribute("type", "ad"), new XAttribute("level", "0")),
                                               new XElement("volume", new XAttribute("name", "playerclient"), new XAttribute("type", "voice"), new XAttribute("level", "70"))),

                 new XComment("语音播放停止 指定程序设置音量"),

                 new XElement("VoicePlayStop", new XComment("name 进程名 level音量0-100"),
                                              new XElement("volume", new XAttribute("name", "TTPlayer"), new XAttribute("type", "ad")),
                                              new XElement("volume", new XAttribute("name", "playerclient"), new XAttribute("type", "voice"), new XAttribute("level", "0"))),

                  new XComment("高峰时期"),
                  new XElement("Fastigium",
                                          new XElement("param", new XAttribute("name", "FastigiumTime"),
                                              new XComment("高峰时间段1"),
                                              new XElement("time", new XAttribute("start", "7:00"), new XAttribute("end", "9:00")),
                                              new XComment("高峰时间段2"),
                                              new XElement("time", new XAttribute("start", "17:30"), new XAttribute("end", "19:00"))),
                                          new XComment("播放间隔"),
                                          new XComment("时间间隔 单位分钟"),
                                          new XElement("param", new XAttribute("name", "interval"), new XAttribute("value", "1"))),

                 new XComment("平谷时期"),
                 new XElement("Normal",
                                      new XElement("param", new XAttribute("name", "NormalTime"),
                                          new XComment("平谷时间段1"),
                                          new XElement("time", new XAttribute("start", "6:00"), new XAttribute("end", "7:00")),
                                          new XComment("平谷时间段2"),
                                          new XElement("time", new XAttribute("start", "9:00"), new XAttribute("end", "17:30")),
                                          new XComment("平谷时间段3"),
                                          new XElement("time", new XAttribute("start", "19:00"), new XAttribute("end", "23:00"))),
                                      new XComment("播放间隔"),
                                      new XComment("时间间隔 单位分钟"),
                                      new XElement("param", new XAttribute("name", "interval"), new XAttribute("value", "2")))
                 ));
            //默认是缩进格式化的xml，而无须格式化设置
            xDoc.Save(url);
        }
    }
}
