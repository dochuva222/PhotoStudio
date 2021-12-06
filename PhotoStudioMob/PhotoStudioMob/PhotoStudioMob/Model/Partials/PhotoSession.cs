using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoStudioMob.Model
{
    public partial class PhotoSession
    {
        [JsonIgnore]
        public string Place
        {
            get
            {
                if (StudioId != null)
                    return $"Студия: {this.Studio.Name}";
                else
                    return $"Адрес: {this.Address}";
            }
        }
    }

}
