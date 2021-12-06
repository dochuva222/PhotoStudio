using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoStudioMob.Model
{
    public partial class Client
    {
        [JsonIgnore]
        public string FullName
        {
            get
            {
                return $"{this.Lastname} {this.Firstname} {this.Middlename}";
            }
        }
    }

}
