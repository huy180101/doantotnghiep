using hanhkiemUtehy.Entity;

namespace hanhkiemUtehy.Model
{
    public class ClassModel
    {
        public string name { get; set; } = string.Empty;
        public string code { get; set; } = string.Empty;
        public string teacher_name { get; set; } = string.Empty;
        public long teacher_id { get; set; } = 0;
        public long id { get; set; } = 0;
        public int status { get; set; } = 0;
        public List<Conduct_Form> conduct_Forms { get; set; }
    }
}
