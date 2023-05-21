using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hanhkiemUtehy.Entity
{
    [Table("student_user")]
    public class Student_User : IAuditableEntity
    {
        public string username { get; set; } = string.Empty;
        [StringLength(50)]
        public string password { get; set; } = string.Empty;
        [StringLength(50)]
        public string pass_code { set; get; } = string.Empty;
        [StringLength(50)]
        public string email { get; set; } = string.Empty;
        public string student_code { get; set; } = string.Empty;
        public string full_name { get; set; } = string.Empty;
        public bool is_officer { get; set; } = false;
        public long class_id { get; set; } = 0;
        public long teacher_id { get; set; } = 0;
        public DateTime birthday { get; set; }
    }
}
