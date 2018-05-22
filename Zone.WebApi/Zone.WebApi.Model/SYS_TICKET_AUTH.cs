using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Zone.WebApi.Model
{
    //票据授权表
    public class SYS_TICKET_AUTH
    {

        /// <summary>
        /// Id
        /// </summary>		
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 用户id
        /// </summary>		
        private int _userid;
        public int UserId
        {
            get { return _userid; }
            set { _userid = value; }
        }
        /// <summary>
        /// 用户名
        /// </summary>		
        private string _username;
        public string UserName
        {
            get { return _username; }
            set { _username = value; }
        }
        /// <summary>
        /// 票据
        /// </summary>		
        private string _token;
        public string Token
        {
            get { return _token; }
            set { _token = value; }
        }
        /// <summary>
        /// 票据生成时间
        /// </summary>		
        private DateTime _createtime;
        public DateTime CreateTime
        {
            get { return _createtime; }
            set { _createtime = value; }
        }
        /// <summary>
        /// 票据过期时间
        /// </summary>		
        private DateTime _exprietime;
        public DateTime ExprieTime
        {
            get { return _exprietime; }
            set { _exprietime = value; }
        }

    }
}

