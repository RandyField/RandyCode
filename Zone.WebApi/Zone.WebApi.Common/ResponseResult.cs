using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Common
{
    public class ResponseResult
    {
        /// <summary>
        /// 返回状态码
        /// 成功：SUCCESS
        /// 失败：FAIL
        /// </summary>
        public string return_code { get; set; }

        /// <summary>
        /// 返回信息
        /// 如果有错误，即非空，内容为错误原因
        /// </summary>
        public string return_msg { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        public object return_info { get; set; }
    }
}
