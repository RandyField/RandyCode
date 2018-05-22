using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Zone.WebApi.Model
{
    //合作伙伴
    public class ZYH_PARTNER
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
        /// Pname
        /// </summary>		
        private string _pname;
        public string Pname
        {
            get { return _pname; }
            set { _pname = value; }
        }
        /// <summary>
        /// PLogo
        /// </summary>		
        private string _plogo;
        public string PLogo
        {
            get { return _plogo; }
            set { _plogo = value; }
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
        /// 0-有效 1-已删除  （逻辑删除）
        /// </summary>		
        private int _isdelete;
        public int IsDelete
        {
            get { return _isdelete; }
            set { _isdelete = value; }
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

