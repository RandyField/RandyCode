using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using MODEL;
using Dapper;

namespace BLL
{
    public class DapperDataBaseConfig
    {
        private static string DefaultMySqlConnectionString=@"Server=localhost;Database=School;user=root;Password=a287572291;pooling=true;CharSet=utf8;port=3306;sslmode=none";


        //数据库配置
        public static IDbConnection GetSqlConnection(string sqlConnectionString =null)
        {
            if (string.IsNullOrWhiteSpace(sqlConnectionString))
            {
                sqlConnectionString=DefaultMySqlConnectionString;
            }

            IDbConnection conn=new MySqlConnection(sqlConnectionString);
            conn.Open();
            return conn;          
        }
    }
}