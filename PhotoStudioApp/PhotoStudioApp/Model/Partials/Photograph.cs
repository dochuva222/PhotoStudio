using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudioApp.Model
{
    public partial class Photograph
    {
        public string FullNameSkill { get => $"{Lastname} {Firstname} {Skill.Name}"; }
        public string FullName { get => $"{Lastname} {Firstname} {Middlename}"; }
    }
}
