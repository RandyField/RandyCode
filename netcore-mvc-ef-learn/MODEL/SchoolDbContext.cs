using Microsoft.EntityFrameworkCore;

namespace MODEL
{
    //创建DbSet每个实体集的属性
    //EF中的约定-
    //1.DbSet类型的属性名作为表名,对于那些没有被引用为DbSet属性的,实体类名称作为表名(如下面的Course可以不定义)
    //2.实体属性名称作为数据库中表的列名称
    //3.实体属性名称为ID或者classnameID 会被识别作为主键
    //4.一个属性被解释作为一个外键,那么一定是这种情况(eg:当Student的主键为ID时,
    //那么同时还存在StudentID,那么StudentID作为导航属性)
    //被命名为外键属性的(eg:EnrollmentID ,Enrollment实体的主键是EnrollmentID)
    public class SchoolDbContext : DbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options)
                : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL(@"Data");
            }
        }

        public virtual DbSet<Student> Students { get; set; }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     if (!optionsBuilder.IsConfigured)
        //     {
        //         // #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //         optionsBuilder.UseMySQL(@"Server=localhost;Database=School;user=root;Password=randy1992;pooling=true;CharSet=utf8;port=3306;sslmode=none");
        //     }
        // }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Student>(entity =>
        //    {
        //        entity.Property(e => e.ID).IsRequired();
        //    });

        //    // modelBuilder.Entity<Student>(entity =>
        //    // {
        //    //     entity.HasOne(d => d.FirstMidName)
        //    //         .WithMany(p => p.)
        //    //         .HasForeignKey(d => d.);
        //    // });
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //映射数据库表名
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
            modelBuilder.Entity<Student>().ToTable("Student");
        }
    }
}