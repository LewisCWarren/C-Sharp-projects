
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarInsuranceProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public ActionResult GetQuote(string firstName, string lastName, string emailAddress, DateTime dob, string carYear, string carMake, string carModel, bool dui, int speedingTickets, bool fullCoverage)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(emailAddress))
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            else
            {
                int quote = 50;
                TimeSpan howOld = DateTime.Now.Date - dob.Date;
                int userAge = Convert.ToInt32(howOld.TotalDays)/365;
                if (userAge > 100 || userAge < 25)
                {
                    quote += 25;
                }
                else if (userAge < 18)
                {
                    quote += 100;
                }

                if (Convert.ToInt32(carYear) > 2015 || Convert.ToInt32(carYear) < 2000)
                {
                    quote += 25;
                }

                if (carMake == "Porsche")
                {
                    quote += 25;
                }

                if (carMake == "Porsche" && carModel == "911 Carrera")
                {
                    quote += 50;
                }

                for (int i = 0; i < speedingTickets; i++)
                {
                    quote += 10;
                }

                if (dui)
                {
                    int percent = quote / 4;
                    quote += quote + percent;
                }

                if (fullCoverage)
                {
                    int percentage = quote / 2;
                    quote += quote + percentage;
                }
               
                using (CarInsuranceEntities db = new CarInsuranceEntities())
                {
                    ClientRecord client = new ClientRecord();
                    client.FirstName = firstName;
                    client.LastName = lastName;
                    client.EmailAddress = emailAddress;
                    client.DOB = dob;
                    client.CarYear = carYear;
                    client.CarMake = carMake;
                    client.CarModel = carModel;
                    client.DUI = dui;
                    client.SpeedingTickets = speedingTickets;
                    client.FullCoverage = fullCoverage;
                    client.Quote = quote;

                    db.ClientRecords.Add(client);
                    db.SaveChanges();
                }
                

                
                
                return View("Quote");
            }
            
        }

      
    }
}