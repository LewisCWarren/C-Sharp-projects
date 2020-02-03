
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
            using (CarInsuranceEntities db = new CarInsuranceEntities())
            {
                var clients = db.ClientRecords.ToList();
                List<ClientRecordVm> clientRecordVm = new List<ClientRecordVm>();

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