using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Zone.WebApi.Model
{
    //SYS_USER_INFO
    public class SYS_USER_INFO
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
        /// 用户名
        /// </summary>		
        private string _username;
        public string UserName
        {
            get { return _username; }
            set { _username = value; }
        }
        /// <summary>
        /// 密码
        /// </summary>		
        private string _password;
        public string PassWord
        {
            get { return _password; }
            set { _password = value; }
        }
        /// <summary>
        /// 真实姓名
        /// </summary>		
        private string _realname;
        public string RealName
        {
            get { return _realname; }
            set { _realname = value; }
        }
        /// <summary>
        /// 电话号码
        /// </summary>		
        private string _phone;
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }
        /// <summary>
        /// qq号码
        /// </summary>		
        private string _qq;
        public string QQ
        {
            get { return _qq; }
            set { _qq = value; }
        }
        /// <summary>
        /// 账号状态 0-初始化 1-有效 2-锁定禁用
        /// </summary>		
        private int _state;
        public int State
        {
            get { return _state; }
            set { _state = value; }
        }
        /// <summary>
        /// 创建账户
        /// </summary>		
        private string _createuser;
        public string CreateUser
        {
            get { return _createuser; }
            set { _createuser = value; }
        }
        /// <summary>
        /// 创建日期时间
        /// </summary>		
        private DateTime _createtime;
        public DateTime CreateTime
        {
            get { return _createtime; }
            set { _createtime = value; }
        }
        /// <summary>
        /// 操作账户
        /// </summary>		
        private string _updateuser;
        public string UpdateUser
        {
            get { return _updateuser; }
            set { _updateuser = value; }
        }
        /// <summary>
        /// 操作时间
        /// </summary>		
        private DateTime _updatetime;
        public DateTime UpdateTime
        {
            get { return _updatetime; }
            set { _updatetime = value; }
        }
        /// <summary>
        /// 保留字段1
        /// </summary>		
        private string _reserved1;
        public string Reserved1
        {
            get { return _reserved1; }
            set { _reserved1 = value; }
        }
        /// <summary>
        /// 保留字段2
        /// </summary>		
        private string _reserved2;
        public string Reserved2
        {
            get { return _reserved2; }
            set { _reserved2 = value; }
        }

    }
}

