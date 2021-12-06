using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudioApp.Model
{
    public partial class Client
    {
        public string FullName { get => $"{Lastname} {Firstname} {Middlename}"; }
        public int OrdersCount { get => MainWindow.DB.PhotoSession.Where(o => o.ClientId == this.Id).Count(); }
        public int Discount
        {
            get
            {
                if (OrdersCount > 20)
                    return 10;
                if (OrdersCount < 20 && OrdersCount > 10)
                    return 5;
                return 0;
            }

        }
    }
}
