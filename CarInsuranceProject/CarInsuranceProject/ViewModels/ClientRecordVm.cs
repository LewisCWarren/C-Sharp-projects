using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarInsuranceProject.ViewModels
{
    public class ClientRecordVm
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public int Quote { get; set; }
    }
}