using hanhkiemUtehy.Entity;
using hanhkiemUtehy.Model;

namespace hanhkiemUtehy.Repository
{
    public interface IUserRepository
    {
        Task<UserModel> UserGetById(long id);
        Task<UserModel> UserCreate(UserModel model);
        Task<UserModel> UserModify(UserModel model);
        Task<bool> UserDelete(long id, long userUpdate);
        Task<List<UserModel>> UserList();
        int Authenticate(LoginModel login);
        Task<Admin_User> CheckUser(string username);
        Task<int> CheckUserExists(string username, string email);
        
    }
}
