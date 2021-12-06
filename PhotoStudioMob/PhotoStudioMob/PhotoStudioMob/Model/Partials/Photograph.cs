using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace PhotoStudioMob.Model
{
    public partial class Photograph
    {
        [JsonIgnore]
        public ImageSource PhotographImage
        {
            get
            {
                return ImageSource.FromStream(() => new MemoryStream(this.Image));
            }
        }

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
