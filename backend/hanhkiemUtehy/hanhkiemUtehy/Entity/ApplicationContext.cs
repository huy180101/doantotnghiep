using Microsoft.EntityFrameworkCore;

namespace hanhkiemUtehy.Entity
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        { }
        public virtual DbSet<Student_User> Student_User { set; get; }
        public virtual DbSet<Conduct_Form> Conduct_Form { set; get; }
        public virtual DbSet<Class> Class { set; get; }
        public virtual DbSet<Admin_User> Admin_User { set; get; }
        public virtual DbSet<Teacher_User> Teacher_User { set; get; }
    }
}

