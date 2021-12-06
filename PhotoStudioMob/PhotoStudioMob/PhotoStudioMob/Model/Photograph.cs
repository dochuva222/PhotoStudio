using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoStudioMob.Model
{
    public partial class Photograph
    {

        public int Id { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public int SkillId { get; set; }
        public byte[] Image { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

    }
}
