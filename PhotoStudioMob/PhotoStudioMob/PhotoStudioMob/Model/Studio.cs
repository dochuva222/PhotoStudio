using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoStudioMob.Model
{
    public partial class Studio
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public byte[] Image { get; set; }
        public string Description { get; set; }
    }
}
