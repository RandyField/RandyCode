using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Zone.WebApi.Model
{
    //ZYH_PROJECT_CASE
    public class ZYH_PROJECT_CASE
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
        ///01-互动营销
        ///02-常规广告
        ///03-电子票务
        /// </summary>		
        private string _typecode;
        public string TypeCode
        {
            get { return _typecode; }
            set { _typecode = value; }
        }

        /// <summary>
        /// 新闻标题
        /// </summary>		
        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        /// <summary>
        /// 新闻内容
        /// </summary>		
        private string _content;
        public string Content
        {
            get { return _content; }
            set { _content = value; }
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
        /// Reserved1
        /// </summary>		
        private string _reserved1;
        public string Reserved1
        {
            get { return _reserved1; }
            set { _reserved1 = value; }
        }
        /// <summary>
        /// Reserved2
        /// </summary>		
        private string _reserved2;
        public string Reserved2
        {
            get { return _reserved2; }
            set { _reserved2 = value; }
        }
        /// <summary>
        /// Reserved3
        /// </summary>		
        private string _reserved3;
        public string Reserved3
        {
            get { return _reserved3; }
            set { _reserved3 = value; }
        }

    }
}

