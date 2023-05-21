using AutoMapper;
using hanhkiemUtehy.Entity;
using hanhkiemUtehy.Extensions;
using hanhkiemUtehy.Model;

namespace hanhkiemUtehy.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public UserRepository(ApplicationContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<UserModel> UserGetById(long id)
        {
            Admin_User user = _context.Admin_User.Find(id);
            UserModel userViewModel = _mapper.Map<UserModel>(user);
            return userViewModel;
        }
        public async Task<UserModel> UserCreate(UserModel model)
        {
            Admin_User user = _mapper.Map<Admin_User>(model);
            user.pass_code = Encryptor.RandomPassword();
            user.password = Encryptor.MD5Hash(user.password + user.pass_code);
            _context.Admin_User.Add(user);
            _context.SaveChanges();
            UserModel userModel = _mapper.Map<UserModel>(user);
            return userModel;
        }
        public async Task<UserModel> UserModify(UserModel model)
        {
            var user = _context.Admin_User.FirstOrDefault(r => r.id == model.id);
            user.email = model.email;
            user.full_name = model.full_name;
            user.dateUpdated = DateTime.Now;
            _context.Admin_User.Update(user);
            _context.SaveChanges();
            return model;
        }
        public async Task<bool> UserDelete(long id, long userUpdate)
        {
            try
            {
                var user = _context.Admin_User.FirstOrDefault(r => r.id == id);
                user.is_delete = true;
                user.dateUpdated = DateTime.Now;
                user.userUpdated = userUpdate;
                _context.Admin_User.Update(user);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }


        }
        public async Task<List<UserModel>> UserList()
        {
            List<UserModel> response = new List<UserModel>();
            IEnumerable<UserModel> listItem = from a in _context.Admin_User
                                              select new UserModel
                                              {
                                                  email = a.email,
                                                  username = a.username,
                                                  full_name = a.full_name,
                                                  id = a.id,
                                                  userAdded = a.userAdded,
                                                  userUpdated = a.userUpdated,

                                              };
            response = listItem.OrderByDescending(r => r.id).ToList();
            return response;
        }
        public int Authenticate(LoginModel login)
        {
            Admin_User user = _context.Admin_User.Where(r => !r.is_delete && r.username.ToUpper() == login.username.ToUpper() || r.email.ToUpper() == login.username.ToUpper()).FirstOrDefault();
            if (user == null)
            {
                return -1;
            }
            else
            {
                var passWord = Encryptor.MD5Hash(login.password + user.pass_code);
                return passWord != user.password ? 2 : 1;
            }
        }
        public async Task<Admin_User> CheckUser(string username)
        {
            return _context.Admin_User.Where(r => r.username.ToUpper() == username.ToUpper() || r.email.ToUpper() == username.ToUpper()).FirstOrDefault();

        }
        public async Task<int> CheckUserExists(string username, string email)
        {
            return _context.Admin_User.Where(r => r.username.ToUpper() == username.ToUpper() || r.email.ToUpper() == email.ToUpper()).Count();

        }
     
    }
}
