using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Zone.WebApi.Model
{
    //ZYH_BRANCH
    public class ZYH_BRANCH
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
        /// 分公司编码
        /// </summary>		
        private string _branchcode;
        public string BranchCode
        {
            get { return _branchcode; }
            set { _branchcode = value; }
        }
        /// <summary>
        /// 分公司名称
        /// </summary>		
        private string _branchname;
        public string BranchName
        {
            get { return _branchname; }
            set { _branchname = value; }
        }
        /// <summary>
        /// 分公司网址
        /// </summary>		
        private string _branchsite;
        public string BranchSite
        {
            get { return _branchsite; }
            set { _branchsite = value; }
        }
        /// <summary>
        /// 分公司电话
        /// </summary>		
        private string _branchphone;
        public string BranchPhone
        {
            get { return _branchphone; }
            set { _branchphone = value; }
        }
        /// <summary>
        /// 分公司地址
        /// </summary>		
        private string _branchaddress;
        public string BranchAddress
        {
            get { return _branchaddress; }
            set { _branchaddress = value; }
        }
        /// <summary>
        /// 分公司邮件
        /// </summary>		
        private string _branchemail;
        public string BranchEmail
        {
            get { return _branchemail; }
            set { _branchemail = value; }
        }
        /// <summary>
        /// 分公司传真
        /// </summary>		
        private string _branchfax;
        public string BranchFax
        {
            get { return _branchfax; }
            set { _branchfax = value; }
        }
        /// <summary>
        /// BranchQcode
        /// </summary>		
        private string _branchqcode;
        public string BranchQcode
        {
            get { return _branchqcode; }
            set { _branchqcode = value; }
        }
        /// <summary>
        /// 分公司简介标题
        /// </summary>		
        private string _summarytitle;
        public string SummaryTitle
        {
            get { return _summarytitle; }
            set { _summarytitle = value; }
        }
        /// <summary>
        /// 子公司简介内容
        /// </summary>		
        private string _summarycontent;
        public string SummaryContent
        {
            get { return _summarycontent; }
            set { _summarycontent = value; }
        }
        /// <summary>
        /// 0-有效 1-已删除  （逻辑删除）
        /// </summary>		
        private int _isdelete;
        public int IsDelete
        {
            get { return _isdelete; }
            set { _isdelete = value; }
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

