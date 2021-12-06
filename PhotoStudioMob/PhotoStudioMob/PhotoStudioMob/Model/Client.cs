using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoStudioMob.Model
{
    public partial class Client
    {
        public int Id { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public byte[] Image { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
