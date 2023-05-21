using AutoMapper;
using hanhkiemUtehy.Entity;
using hanhkiemUtehy.Extensions;
using hanhkiemUtehy.Model;
using System.ComponentModel.Design;

namespace hanhkiemUtehy.Repository
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public TeacherRepository(ApplicationContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<UserModel> GetById(long id)
        {
            Teacher_User user = _context.Teacher_User.Find(id);
            UserModel userViewModel = _mapper.Map<UserModel>(user);
            return userViewModel;
        }
        public async Task<UserModel> UserCreate(UserModel model)
        {
            Teacher_User user = _mapper.Map<Teacher_User>(model);
            user.pass_code = Encryptor.RandomPassword();
            user.password = Encryptor.MD5Hash(user.password + user.pass_code);
            _context.Teacher_User.Add(user);
            _context.SaveChanges();
            UserModel userModel = _mapper.Map<UserModel>(user);
            return userModel;
        }
        public async Task<UserModel> UserModify(UserModel model)
        {
            var user = _context.Teacher_User.FirstOrDefault(r => r.id == model.id);
            user.email = model.email;
            user.full_name = model.full_name;
            user.dateUpdated = DateTime.Now;
            _context.Teacher_User.Update(user);
            _context.SaveChanges();
            return model;
        }
        public async Task<bool> Delete(long id, long userUpdate)
        {
            try
            {
                var user = _context.Teacher_User.FirstOrDefault(r => r.id == id);
                user.is_delete = true;
                user.dateUpdated = DateTime.Now;
                user.userUpdated = userUpdate;
                _context.Teacher_User.Update(user);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }


        }
        public async Task<List<Teacher_User>> List()
        {
            return _context.Teacher_User.Where(x=>!x.is_delete).ToList();
        }
        public int Authenticate(LoginModel login)
        {
            Teacher_User user = _context.Teacher_User.Where(r => !r.is_delete && r.username.ToUpper() == login.username.ToUpper() || r.email.ToUpper() == login.username.ToUpper()).FirstOrDefault();
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
        public async Task<Teacher_User> CheckUser(string username)
        {
            var teacher = _context.Teacher_User.Where(r => r.username.ToUpper() == username.ToUpper() || r.email.ToUpper() == username.ToUpper()).FirstOrDefault();
            if (teacher!=null)
            {
                var class_teacher = _context.Class.FirstOrDefault(x => x.teacher_id == teacher.id);
                if (class_teacher != null)
                {
                    teacher.class_id = class_teacher.id;
                }
            }
            return teacher;

        }
        public async Task<int> CheckUserExists(string username, string email)
        {
            return _context.Teacher_User.Where(r => r.username.ToUpper() == username.ToUpper() || r.email.ToUpper() == email.ToUpper()).Count();

        }
    }
}
