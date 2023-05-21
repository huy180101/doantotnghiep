using hanhkiemUtehy.Entity;
using hanhkiemUtehy.Model;
using Microsoft.EntityFrameworkCore;

namespace hanhkiemUtehy.Repository
{
    public interface IClassRepository
    {
        Task<Class> ClassCreate(Class model);
        Task<Class> ClassModify(Class model);
        Task<bool> ClassDelete(long class_id);
        Task<Class> ClassAddTeacher(long class_id, long teacher_id);
        Task<List<ClassModel>> ClassList();
        Task<Class> ClassUpdateStatus(long class_id, int status_id);
        Task<ClassModel> ClassDetail(long teacher_id);
        Task<ClassModel> ClassDetailAdmin(long class_id);
        Task<bool> ClassSetOfficer(long student_id, bool is_officer);
        Task<Conduct_Form> UpdateForm(Conduct_Form model);
        Task<List<Student_User>> AddStudentToClass(List<Student_User> model);
        Task<bool> RemoveStudent(long student_id);
    }
}
