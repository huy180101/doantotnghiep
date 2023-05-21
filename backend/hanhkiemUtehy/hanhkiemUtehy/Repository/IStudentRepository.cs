using hanhkiemUtehy.Entity;
using hanhkiemUtehy.Extensions;
using hanhkiemUtehy.Model;
using Microsoft.EntityFrameworkCore;

namespace hanhkiemUtehy.Repository
{
    public interface IStudentRepository
    {
        Task<Student_User> GetById(long id);
        Task<Conduct_Form> GetFormByStudentId(long student_id);
        Task<Conduct_Form> UpdateForm(Conduct_Form model);
        Task<Student_User> Modify(Student_User model);
        Task<bool> Delete(long id, long userUpdate);
        Task<List<Student_User>> List(long class_id);
        int Authenticate(LoginModel login);
        Task<Student_User> CheckUser(string username);
        Task<int> CheckUserExists(string username, string email);
    }
}
