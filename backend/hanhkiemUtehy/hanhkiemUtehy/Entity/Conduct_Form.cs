using System.ComponentModel.DataAnnotations.Schema;

namespace hanhkiemUtehy.Entity
{
    [Table("conduct_form")]
    public class Conduct_Form : IAuditableEntity
    {
        public int student_point_1 { get; set; } = 0;
        public int student_point_1_1 { get; set; } = 0;
        public int student_point_1_2 { get; set; } = 0;
        public int student_point_1_3 { get; set; } = 0;
        public int student_point_1_4 { get; set; } = 0;
        public int student_point_1_5 { get; set; } = 0;
        public int student_point_2 { get; set; } = 0;
        public int student_point_2_1 { get; set; } = 0;
        public int student_point_2_2 { get; set; } = 0;
        public int student_point_2_3 { get; set; } = 0;
        public int student_point_2_4 { get; set; } = 0;
        public int student_point_2_5 { get; set; } = 0;
        public int student_point_2_6 { get; set; } = 0;
        public int student_point_3 { get; set; } = 0;
        public int student_point_3_1 { get; set; } = 0;
        public int student_point_3_2 { get; set; } = 0;
        public int student_point_3_3 { get; set; } = 0;
        public int student_point_4 { get; set; } = 0;
        public int student_point_4_1 { get; set; } = 0;
        public int student_point_4_2 { get; set; } = 0;
        public int student_point_4_3 { get; set; } = 0;
        public int student_point_5 { get; set; } = 0;
        public int student_point_5_1 { get; set; } = 0;
        public int student_point_5_2 { get; set; } = 0; 
        public int officer_point_1 { get; set; } = 0;
        public int officer_point_1_1 { get; set; } = 0;
        public int officer_point_1_2 { get; set; } = 0;
        public int officer_point_1_3 { get; set; } = 0;
        public int officer_point_1_4 { get; set; } = 0;
        public int officer_point_1_5 { get; set; } = 0;
        public int officer_point_2 { get; set; } = 0;
        public int officer_point_2_1 { get; set; } = 0;
        public int officer_point_2_2 { get; set; } = 0;
        public int officer_point_2_3 { get; set; } = 0;
        public int officer_point_2_4 { get; set; } = 0;
        public int officer_point_2_5 { get; set; } = 0;
        public int officer_point_2_6 { get; set; } = 0;
        public int officer_point_3 { get; set; } = 0;
        public int officer_point_3_1 { get; set; } = 0;
        public int officer_point_3_2 { get; set; } = 0;
        public int officer_point_3_3 { get; set; } = 0;
        public int officer_point_4 { get; set; } = 0;
        public int officer_point_4_1 { get; set; } = 0;
        public int officer_point_4_2 { get; set; } = 0;
        public int officer_point_4_3 { get; set; } = 0;
        public int officer_point_5 { get; set; } = 0;
        public int officer_point_5_1 { get; set; } = 0;
        public int officer_point_5_2 { get; set; } = 0;
        public int total_point { get; set; } = 0;
        public long student_id { get; set; } = 0;
        public long teacher_id { get; set; } = 0;
        public long class_id { get; set; } = 0;
        public int status { get; set; } = 0;  //  la moi tao, 1 la can bo da cham, 2 la giao vien da cham => hoan thanh
    }
}
