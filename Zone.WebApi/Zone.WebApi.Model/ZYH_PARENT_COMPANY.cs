using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Zone.WebApi.Model
{
    //ZYH_PARENT_COMPANY
    public class ZYH_PARENT_COMPANY
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
        /// 母公司名称
        /// </summary>		
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        /// <summary>
        /// 母公司网址
        /// </summary>		
        private string _site;
        public string Site
        {
            get { return _site; }
            set { _site = value; }
        }
        /// <summary>
        /// 母公司电话
        /// </summary>		
        private string _phone;
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }
        /// <summary>
        /// 母公司地址
        /// </summary>		
        private string _address;
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }
        /// <summary>
        /// 电子邮件
        /// </summary>		
        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        /// <summary>
        /// 传真
        /// </summary>		
        private string _fax;
        public string Fax
        {
            get { return _fax; }
            set { _fax = value; }
        }
        /// <summary>
        /// 二维码图片
        /// </summary>		
        private string _qcode;
        public string Qcode
        {
            get { return _qcode; }
            set { _qcode = value; }
        }
        /// <summary>
        /// 简介标题
        /// </summary>		
        private string _summarytitle;
        public string SummaryTitle
        {
            get { return _summarytitle; }
            set { _summarytitle = value; }
        }
        /// <summary>
        /// 简介内容
        /// </summary>		
        private string _summarycontent;
        public string SummaryContent
        {
            get { return _summarycontent; }
            set { _summarycontent = value; }
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
        /// 创建时间
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
        /// <summary>
        /// 保留字段3
        /// </summary>		
        private string _reserved3;
        public string Reserved3
        {
            get { return _reserved3; }
            set { _reserved3 = value; }
        }

    }
}

