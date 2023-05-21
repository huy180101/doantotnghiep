using hanhkiemUtehy.Entity;
using hanhkiemUtehy.Extensions;
using hanhkiemUtehy.Model;

namespace hanhkiemUtehy.Repository
{
    public class ClassRepository : IClassRepository
    {
        private readonly ApplicationContext _context;

        public ClassRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<Class> ClassCreate(Class model)
        {
            _context.Class.Add(model);
            _context.SaveChanges();
            return model;
        }
        public async Task<Class> ClassModify(Class model)
        {
            _context.Class.Update(model);
            _context.SaveChanges();
            return model;
        }
        public async Task<bool> ClassDelete(long class_id)
        {
            var classdb = _context.Class.Where(r => r.id == class_id).FirstOrDefault();
            classdb.is_delete = true;
            classdb.dateUpdated = DateTime.Now;
            _context.Class.Update(classdb);
            _context.SaveChanges();
            return true;
        }
        public async Task<Class> ClassAddTeacher(long class_id, long teacher_id)
        {
            var classdb = _context.Class.Where(r => r.id == class_id).FirstOrDefault();
            classdb.teacher_id = teacher_id;
            classdb.dateUpdated = DateTime.Now;
            _context.Class.Update(classdb);
            _context.SaveChanges();
            return classdb;
        }
        public async Task<List<ClassModel>> ClassList()
        {
            List<ClassModel> response = new List<ClassModel>();
            IEnumerable<ClassModel> listItem = from a in _context.Class where !a.is_delete
                                               select new ClassModel 
                                               {
                                                   name = a.name,
                                                   status = a.status,
                                                   code = a.code,
                                                   id = a.id,
                                                   teacher_id = a.teacher_id,
                                                   conduct_Forms = _context.Conduct_Form.Where(x => x.class_id == a.id).ToList(),
                                               };
            response = listItem.OrderByDescending(r => r.id).ToList();
            List<long> list_teacher_id = response.Select(x=>x.teacher_id).ToList();
            var list_teacher    = _context.Teacher_User.Where(x=>list_teacher_id.Contains(x.id)).ToList();
            foreach (var item in response)
            {
                var teacher = list_teacher.FirstOrDefault(x => x.id == item.id);
                if (teacher!=null)
                {
                    item.teacher_name = teacher.full_name;
                }
            }
            return response;
        }
        public async Task<Class> ClassUpdateStatus(long class_id, int status_id)
        {
            var classdb = _context.Class.Where(r => r.id == class_id).FirstOrDefault();
            classdb.status = status_id;
            _context.Class.Update(classdb);
            _context.SaveChanges();
            return classdb;
        }
        public async Task<ClassModel> ClassDetail(long teacher_id)
        {
            ClassModel response = new ();
            IEnumerable<ClassModel> listItem = from a in _context.Class
                                               where a.is_delete == false && a.teacher_id == teacher_id
                                               select new ClassModel
                                               {
                                                   name = a.name,
                                                   code = a.code,
                                                   status = a.status,
                                                   id = a.id,
                                                   teacher_id = a.teacher_id,
                                                   conduct_Forms = _context.Conduct_Form.Where(x => x.class_id == a.id).ToList(),
                                               };
            response = listItem.OrderByDescending(r => r.id).FirstOrDefault();
            var teacher = _context.Teacher_User.FirstOrDefault(x => x.id == response.teacher_id);
            if (teacher != null!)
            {
                response.teacher_name = teacher.full_name;
            }
            return response;
        }
        public async Task<ClassModel> ClassDetailAdmin(long class_id)
        {
            ClassModel response = new ();
            IEnumerable<ClassModel> listItem = from a in _context.Class
                                               where !a.is_delete && a.id == class_id
                                               select new ClassModel
                                               {
                                                   name = a.name,
                                                   code = a.code,
                                                   status = a.status,
                                                   id = a.id,
                                                   teacher_id = a.teacher_id,
                                                   conduct_Forms = _context.Conduct_Form.Where(x => x.class_id == a.id).ToList(),
                                               };
            response = listItem.OrderByDescending(r => r.id).FirstOrDefault();
            var teacher  = _context.Teacher_User.FirstOrDefault(x => x.id == response.teacher_id );
            if (teacher!=null!)
            {
                response.teacher_name = teacher.full_name;
            }
            return response;
        }
        public async Task<bool> ClassSetOfficer(long student_id, bool is_officer)
        {
            var student_db = _context.Student_User.FirstOrDefault(x => x.id == student_id);
            if (student_db != null)
            {
                student_db.is_officer = is_officer;
                _context.Student_User.Update(student_db);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public async Task<Conduct_Form> UpdateForm(Conduct_Form model)
        {
            _context.Conduct_Form.Update(model);
            _context.SaveChanges();
            return model;
        }
        public async Task<List<Student_User>> AddStudentToClass(List<Student_User> model)
        {
            List<Conduct_Form> ClassForms = new();
            foreach (var item in model)
            {
                item.pass_code = Encryptor.RandomPassword();
                item.password = Encryptor.MD5Hash("123456"+ item.pass_code);
            }
            _context.Student_User.AddRange(model);
            _context.SaveChanges();
            foreach (var item in model)
            {
                Conduct_Form form = new()
                {
                    class_id = item.class_id,
                    student_id = item.id,
                    teacher_id = item.teacher_id
                };
                ClassForms.Add(form);
            }
            _context.Conduct_Form.AddRange(ClassForms);
            _context.SaveChanges();
            return model;
        }
        public async Task<bool> RemoveStudent(long student_id)
        {
            var student_db = _context.Student_User.FirstOrDefault(x => x.id == student_id);
            var student_form = _context.Conduct_Form.FirstOrDefault(x => x.student_id == student_id);
            _context.Student_User.Remove(student_db);
            _context.Conduct_Form.Remove(student_form);
            _context.SaveChanges();
            return true;
        }

    }
}
