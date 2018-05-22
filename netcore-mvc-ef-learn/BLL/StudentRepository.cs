using MODEL;
using System.Data;
using Dapper;
using System.Collections;

namespace BLL
{
    public class StudentRepository
    {
        //新增
        public bool CreateEntity(Student entity, string connectionString = null)
        {
            using (IDbConnection conn = DapperDataBaseConfig.GetSqlConnection(connectionString))
            {

                // new Student{FirstMidName="Nino",LastName="Olivetto",EnrollmentDate=DateTime.Parse("2005-09-01")}
                string insertSql = @"
                                    INSERT INTO Student 
                                        (
                                            FirstMidName
                                            ,LastName
                                            ,EnrollmentDate
                                        )
                                    VALUES 
                                        (
                                            @FirstMidName
                                            ,@LastName
                                            ,@EnrollmentDate
                                        )
                                    ";
                return conn.Execute(insertSql, entity) > 0;

            }
        }

        //删除
        public bool DeleteEntityById(int id, string connectionString = null)
        {
            using (IDbConnection conn = DapperDataBaseConfig.GetSqlConnection(connectionString))
            {
                string deleteSql = @"DELETE FROM Student WHERE Id=@Id";

                return conn.Execute(deleteSql, new { id = id }) > 0;
            }
        }

        //获取所有
        // public IEnumerable<Student> RetriveAllEntiry(string connectionString = null)
        // {
        //     using (IDbConnection conn = DapperDataBaseConfig.GetSqlConnection(connectionString))
        //     {
        //         string querySql = @"SELECT * FROM Student";
        //         return conn.Query<Student>(querySql);
        //     }
        // }

        //根据主键Id获取一个用户
        public Student RetriveOndeEntiry(int id, string connectionString = null)
        {
            using (IDbConnection conn = DapperDataBaseConfig.GetSqlConnection(connectionString))
            {
                string querySql = @"SELECT * FROM Student WHERE Id=@Id";

                return conn.QueryFirstOrDefault<Student>(querySql, new { id = id });
            }
        }


        //修改
        public bool UpdateEntity(Student entity, string connectionString = null)
        {
            using (IDbConnection conn = DapperDataBaseConfig.GetSqlConnection(connectionString))
            {
                string updateSql = @"UPDATE Student
                                       SET [UserName] = @UserName
                                          ,[Password] = @Password
                                          ,[Gender] = @Gender
                                          ,[Birthday] = @Birthday
                                          ,[UpdateUserId] = @UpdateUserId
                                          ,[UpdateDate] = @UpdateDate
                                          ,[IsDeleted] = @IsDeleted
                                     WHERE Id = @Id";
                return conn.Execute(updateSql, entity) > 0;
            }
        }
    }
}