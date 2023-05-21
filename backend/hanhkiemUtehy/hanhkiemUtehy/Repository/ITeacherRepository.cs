using hanhkiemUtehy.Entity;
using hanhkiemUtehy.Model;

namespace hanhkiemUtehy.Repository
{
    public interface ITeacherRepository
    {
        Task<UserModel> GetById(long id);
        Task<UserModel> UserCreate(UserModel model);
        Task<UserModel> UserModify(UserModel model);
        Task<bool> Delete(long id, long userUpdate);
        Task<List<Teacher_User>> List();
        int Authenticate(LoginModel login);
        Task<Teacher_User> CheckUser(string username);
        Task<int> CheckUserExists(string username, string email);
    }
}
