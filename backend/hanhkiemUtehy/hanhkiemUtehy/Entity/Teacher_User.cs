using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hanhkiemUtehy.Entity
{
    [Table("teacher_user")]
    public class Teacher_User : IAuditableEntity
    {
        public string username { get; set; } = string.Empty;
        [StringLength(50)]
        public string password { get; set; } = string.Empty;
        [StringLength(50)]
        public string pass_code { set; get; } = string.Empty;
        [StringLength(50)]
        public string email { get; set; } = string.Empty;
        public string full_name { get; set; } = string.Empty;
        public long class_id  { get; set; } = 0;
    }
}
