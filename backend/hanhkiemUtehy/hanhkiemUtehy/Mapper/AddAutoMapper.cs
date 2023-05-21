using AutoMapper;
using hanhkiemUtehy.Entity;
using hanhkiemUtehy.Model;

namespace hanhkiemUtehy.Mapper
{
    public class AddAutoMapper : Profile
    {
        public AddAutoMapper()
        {
            CreateMap<Admin_User, UserModel>();
            CreateMap<UserModel, Admin_User>();
            CreateMap<UserModel, Teacher_User>();
            CreateMap<Teacher_User, UserModel>();

        }
    }
}
