
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
        
        //Posting user input into the database
        [HttpPost]
        public ActionResult GetQuote(string firstName, string lastName, string emailAddress, DateTime dob, string carYear, string carMake, string carModel, bool dui, int speedingTickets, bool fullCoverage)
        {
            //Testing to ensure the user has input name and email address, routing to error page otherwise
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(emailAddress))
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            else
            {
                //The business logic for the user's quote. I create the variable integer at a baseline of 50 dollars.
                int quote = 50;
                //Here I am figuring out their age by subtracting their DOB input from todays date.
                TimeSpan howOld = DateTime.Now.Date - dob.Date;
                int userAge = Convert.ToInt32(howOld.TotalDays)/365;
                //If the user is older than 100 or less than 25, we tack on 25 dollars.Under 18, we add 100.
                if (userAge > 100 || userAge < 25)
                {
                    quote += 25;
                }
                else if (userAge < 18)
                {
                    quote += 100;
                }
                //if the car is newer or much older, again we tack on 25 dollars.
                if (Convert.ToInt32(carYear) > 2015 || Convert.ToInt32(carYear) < 2000)
                {
                    quote += 25;
                }
                //For clients with the very expensive Porsche, we also collect more monthly.
                if (carMake == "Porsche")
                {
                    quote += 25;
                }

                if (carMake == "Porsche" && carModel == "911 Carrera")
                {
                    quote += 50;
                }
                //If the user has any speeding tickets, we add 10 dollars for each one
                for (int i = 0; i < speedingTickets; i++)
                {
                    quote += 10;
                }
                //If the user has had a DUI, we add 25% to their total
                if (dui)
                {
                    int percent = quote / 4;
                    quote += quote + percent;
                }
                //We add 50% to the total if they would like full coverage.
                if (fullCoverage)
                {
                    int percentage = quote / 2;
                    quote += quote + percentage;
                }
               //Here we instantiate the database
                using (CarInsuranceEntities db = new CarInsuranceEntities())
                {
                    //Now we instatiate the model class "ClientRecord" and fill in the record using the data provided by the user.
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
                    //We then add it to the database and save the database.
                    db.ClientRecords.Add(client);
                    db.SaveChanges();
                }
                

                
                
                return View("Quote");
            }
            
        }

      
    }
}
