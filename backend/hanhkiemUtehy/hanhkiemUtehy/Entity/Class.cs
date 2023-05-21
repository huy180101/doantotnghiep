using System.ComponentModel.DataAnnotations.Schema;

namespace hanhkiemUtehy.Entity
{
    [Table("class")]
    public class Class : IAuditableEntity
    {
        public string name { get; set; } = string.Empty;
        public string code { get; set; } = string.Empty;
        public long teacher_id { get; set; } = 0;
        public int status { get; set; } = 0; // 0 la moi khoi tao, 1 la da cham diem, 2 la can cham lai
    }
}
