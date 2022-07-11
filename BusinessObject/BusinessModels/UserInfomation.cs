using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.BusinessModels
{
    public class UserInformation
    {
        public string UserId { get; set; }
        public string Fullname { get; set; }
        public bool IsStaff { get; set; }
        public bool Status { get; set; }
        public bool IsResident { get; set; }
        public string ManagerType { get; set; }
    }
}
