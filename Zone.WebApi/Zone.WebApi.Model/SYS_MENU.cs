using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Zone.WebApi.Model
{
    //网站后台菜单表
    public class SYS_MENU
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
        /// 菜单名称
        /// </summary>		
        private string _menuname;
        public string MenuName
        {
            get { return _menuname; }
            set { _menuname = value; }
        }
        /// <summary>
        /// 菜单层级
        /// </summary>		
        private int _menulevel;
        public int MenuLevel
        {
            get { return _menulevel; }
            set { _menulevel = value; }
        }
        /// <summary>
        /// 菜单图标
        /// </summary>		
        private string _menuicon;
        public string MenuIcon
        {
            get { return _menuicon; }
            set { _menuicon = value; }
        }
        /// <summary>
        /// 菜单路由
        /// </summary>		
        private string _menuurl;
        public string MenuUrl
        {
            get { return _menuurl; }
            set { _menuurl = value; }
        }
        /// <summary>
        /// 菜单编码
        /// </summary>		
        private string _menucode;
        public string MenuCode
        {
            get { return _menucode; }
            set { _menucode = value; }
        }
        /// <summary>
        /// 父菜单编码
        /// </summary>		
        private string _parentcode;
        public string ParentCode
        {
            get { return _parentcode; }
            set { _parentcode = value; }
        }
        /// <summary>
        /// 菜单排序
        /// </summary>		
        private int _sort;
        public int Sort
        {
            get { return _sort; }
            set { _sort = value; }
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
        /// 更新时间
        /// </summary>		
        private DateTime _updatetime;
        public DateTime UpdateTime
        {
            get { return _updatetime; }
            set { _updatetime = value; }
        }
        /// <summary>
        /// 菜单别名
        /// </summary>		
        private string _alias;
        public string Alias
        {
            get { return _alias; }
            set { _alias = value; }
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

