using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Zone.WebApi.Model
{
    //ZYH_IMG
    public class ZYH_IMG
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
        /// TypeCode
        /// </summary>		
        private string _typecode;
        public string TypeCode
        {
            get { return _typecode; }
            set { _typecode = value; }
        }
        /// <summary>
        /// DetailId
        /// </summary>		
        private int _detailid;
        public int DetailId
        {
            get { return _detailid; }
            set { _detailid = value; }
        }
        /// <summary>
        /// ImgUrl
        /// </summary>		
        private string _imgurl;
        public string ImgUrl
        {
            get { return _imgurl; }
            set { _imgurl = value; }
        }

    }
}

