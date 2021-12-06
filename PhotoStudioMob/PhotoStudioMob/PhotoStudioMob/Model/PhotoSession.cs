using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoStudioMob.Model
{
    public partial class PhotoSession
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime PhotoSessionDate { get; set; }
        public int PhotographId { get; set; }
        public Nullable<int> StudioId { get; set; }
        public string Address { get; set; }
        public Nullable<int> ThemeId { get; set; }
        public string Comment { get; set; }
        public System.TimeSpan PhotoSessionTime { get; set; }
        public Nullable<int> StatusId { get; set; }
        public string Catalog { get; set; }

        public virtual Studio Studio { get; set; }
        public virtual Theme Theme { get; set; }
        public virtual Client Client { get; set; }
    }
}
