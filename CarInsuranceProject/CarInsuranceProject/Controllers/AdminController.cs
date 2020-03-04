
using CarInsuranceProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarInsuranceProject.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
        //Calling database created using Entity Framework database first
            using (CarInsuranceEntities db = new CarInsuranceEntities())
            {
                //Creating a list<> of client records, and instanting an empty list for the view model
                var clients = db.ClientRecords.ToList();
                List<ClientRecordVm> clientRecordVm = new List<ClientRecordVm>();
                    
                //Passing relevant data from the database record to the view model "clientVm" which will be displayed in the view    
                foreach (var client in clients)
                {
                    var clientVm = new ClientRecordVm();
                    clientVm.Id = client.Id;
                    clientVm.FirstName = client.FirstName;
                    clientVm.LastName = client.LastName;
                    clientVm.Quote = Convert.ToInt32(client.Quote);
                    clientVm.EmailAddress = client.EmailAddress;
                    
                    clientRecordVm.Add(clientVm);
                }
                return View(clientRecordVm);
            }
            
        }
    }
}
