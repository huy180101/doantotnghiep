namespace hanhkiemUtehy.Model
{
    public class UserTokenModel
    {
        public long id { get; set; }
        public string username { get; set; }
        public string token { get; set; }
        public string full_name { get; set; }
        public bool is_admin { get; set; }=false;
        public bool is_officer { get; set; }=false;
        public long class_id { get; set; } =0;
        public long teacher_id {get; set; } =0;

    }

    public class LoginModel
    {
        public string username { set; get; }
        public string password { set; get; }
    }

    public class ChangePassModel
    {
        public long id { set; get; }
        public string passwordOld { set; get; }
        public string passwordNew { set; get; }
    }
}
