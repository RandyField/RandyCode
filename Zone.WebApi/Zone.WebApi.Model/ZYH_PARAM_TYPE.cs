using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Zone.WebApi.Model
{
    //ZYH_PARAM_TYPE
    public class ZYH_PARAM_TYPE
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
        /// ParentCode
        /// </summary>		
        private string _parentcode;
        public string ParentCode
        {
            get { return _parentcode; }
            set { _parentcode = value; }
        }
        /// <summary>
        /// 0101-互动营销
        /// 0102-常规广告
        /// 0103-电子票务
        /// 0201-行业新闻
        /// 0202-公司新闻
        /// 0203-媒体报道
        /// 0301-新闻图片
        /// 0302-案例图片
        /// </summary>		
        private string _typecode;
        public string TypeCode
        {
            get { return _typecode; }
            set { _typecode = value; }
        }
        /// <summary>
        /// TypeName
        /// </summary>		
        private string _typename;
        public string TypeName
        {
            get { return _typename; }
            set { _typename = value; }
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

    }
}

