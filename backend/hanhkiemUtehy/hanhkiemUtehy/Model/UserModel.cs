﻿using System.ComponentModel.DataAnnotations;

namespace hanhkiemUtehy.Model
{
    public class UserModel
    {
        public long id { set; get; }
        public string username { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public string full_name { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public long userAdded { set; get; }
        public long? userUpdated { set; get; }

    }
}
