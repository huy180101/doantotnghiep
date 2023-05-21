using AutoMapper;
using hanhkiemUtehy.Entity;
using hanhkiemUtehy.Extensions;
using hanhkiemUtehy.Model;

namespace hanhkiemUtehy.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationContext _context;
        public StudentRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<Student_User> GetById(long id)
        {
            return _context.Student_User.Find(id);
        }
        public async Task<Conduct_Form> GetFormByStudentId(long student_id)
        {
            return _context.Conduct_Form.FirstOrDefault(x=>x.student_id== student_id);
        } 
        public async Task<Conduct_Form> UpdateForm(Conduct_Form model)
        {
             _context.Conduct_Form.Update(model);
            _context.SaveChanges();
            return model;
        }
        public async Task<Student_User> Modify(Student_User model)
        {
            var user = _context.Student_User.FirstOrDefault(r => r.id == model.id);
            user.email = model.email;
            user.full_name = model.full_name;
            user.email = model.email;
            user.dateUpdated = DateTime.Now;
            _context.Student_User.Update(user);
            _context.SaveChanges();
            return model;
        }
        public async Task<bool> Delete(long id, long userUpdate)
        {
            try
            {
                var user = _context.Student_User.FirstOrDefault(r => r.id == id);
                user.is_delete = true;
                user.dateUpdated = DateTime.Now;
                user.userUpdated = userUpdate;
                _context.Student_User.Update(user);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public async Task<List<Student_User>> List(long class_id)
        {
            return _context.Student_User.Where(x => !x.is_delete && x.class_id == class_id).ToList();
        }
        public int Authenticate(LoginModel login)
        {
            Student_User user = _context.Student_User.Where(r => !r.is_delete && r.username.ToUpper() == login.username.ToUpper() || r.email.ToUpper() == login.username.ToUpper()).FirstOrDefault();
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
        public async Task<Student_User> CheckUser(string username)
        {
            return _context.Student_User.Where(r => r.username.ToUpper() == username.ToUpper() || r.email.ToUpper() == username.ToUpper()).FirstOrDefault();

        }
        public async Task<int> CheckUserExists(string username, string email)
        {
            return _context.Student_User.Where(r => r.username.ToUpper() == username.ToUpper() || r.email.ToUpper() == email.ToUpper()).Count();

        }
    }
}
