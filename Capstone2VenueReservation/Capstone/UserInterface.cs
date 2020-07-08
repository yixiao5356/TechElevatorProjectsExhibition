using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Capstone.DAL;
using System.Runtime.CompilerServices;

namespace Capstone
{
    public class UserInterface
    {
        //ALL Console.ReadLine and WriteLine in this class
        //NONE in any other class

        private string connectionString;
        string input = "";
        public VenueDAL accessVenues;
        public SpaceDAL accessSpace;
        public ReservationDAL accessReservation;

        public UserInterface(string connectionString)
        {
            this.connectionString = connectionString;
            accessVenues = new VenueDAL(connectionString);
            accessSpace = new SpaceDAL(connectionString);
            accessReservation = new ReservationDAL(connectionString);
        }

        public void Run()
        {
            MainMenu();
        }

        public void MainMenu()
        {
            input = "";
            while (input != "Q")
            {
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("1) List Venues");
                Console.WriteLine("Q) Quit");
                input = Console.ReadLine().ToUpper();
                if (input == "1")
                {
                    //Console.Clear();
                    ViewVenue();
                }
            }

        }

        public void ViewVenue()
        {
            input = "";
            while (input != "R")
            {

                List<string> Venues = accessVenues.NameList();
                Console.WriteLine("Which venue would you like to view?");
                for (int i = 1; i < Venues.Count; i++)
                {
                    Console.WriteLine(i + ") " + Venues[i - 1]);
                }
                Console.WriteLine("R) Return to Previous Screen");
                input = Console.ReadLine().ToUpper();
                VenueDetailScreen(input);

            }

        }
        public void VenueDetailScreen(string input)
        {
            Venue VenuesDetail = accessVenues.VenueDetails(connectionString, input);
            while (input != "R")
            {
                Console.WriteLine(VenuesDetail.Name);
                Console.WriteLine("Location: " + VenuesDetail.Location);
                Console.WriteLine("category: " + VenuesDetail.category);
                Console.WriteLine();
                Console.WriteLine(VenuesDetail.description);
                Console.WriteLine();
                Console.WriteLine("What would you like to do next?");
                Console.WriteLine("1) View Spaces");
                Console.WriteLine("2) Search for Reservation");
                Console.WriteLine("R) Return to Previous Screen");
                input = Console.ReadLine().ToUpper();
                if (input == "1")
                {
                    VenueSpace(VenuesDetail.Id, VenuesDetail.Name);
                }
                else if (input == "2")
                {
                    ReserveSpace(VenuesDetail.Id);
                }

            }
        }
        public void VenueSpace(int id, string name)
        {
            input = "";
            while (input != "R")
            {

                Console.WriteLine(name);
                List<Space> spaces = accessSpace.SpaceList(id);
                Console.WriteLine();
                Console.WriteLine("Name".PadLeft(35) + "Open".PadLeft(10) + "Close".PadLeft(10) + "Daily Rate".PadLeft(20) + "Max. Occupancy".PadLeft(20));
                foreach (Space item in spaces)
                {
                    Console.WriteLine("#" + item.Id + item.Name.PadLeft(35) + item.Open.PadLeft(10) + item.Close.PadLeft(10) + "$ ".PadLeft(10) + item.DailyRate.ToString() + item.MaxOccupancy.ToString().PadLeft(10));
                }
                Console.WriteLine();
                Console.WriteLine("What would you like to do next?");
                Console.WriteLine("1) Reserve a Space");
                Console.WriteLine("R) Return to Previous Screen");
                input = Console.ReadLine().ToUpper();
                if (input == "1")
                {
                    ReserveSpace(id);
                }
            }
        }
        public void ReserveSpace(int id)
        {
            input = "";
            bool isGood = false;
            DateTime when = DateTime.Now;
            int manyday = 0;
            int manypeople = 0;
            int month = 0;
            while (!isGood)
            {
                Console.WriteLine("When do you need the space? yyyy/mm/dd");
                string whenInput = Console.ReadLine();
                try
                {

                    when = DateTime.Parse(whenInput);
                    isGood = true;
                }
                catch
                {
                    isGood = false;
                }
                month = when.Month;
            }
            isGood = false;
            while (!isGood)
            {
                Console.WriteLine("How many days will you need the space?");
                string daysInput = Console.ReadLine();
                isGood = int.TryParse(daysInput, out manyday);
            }
            isGood = false;
            while (!isGood)
            {
                Console.WriteLine("How many people will be in attendance?");
                string peopleInput = Console.ReadLine();
                isGood = int.TryParse(peopleInput, out manypeople);
            }

            Console.WriteLine("The following spaces are available based on your needs:");
            Console.WriteLine();
            Console.WriteLine("Space #" + "Name".PadLeft(25) + "Daily Rate".PadLeft(25) + "Max Occup.".PadLeft(25) + "Accessible?".PadLeft(25) + "Total Cost".PadLeft(25));
            List<Space> availablelist = accessReservation.AvailableSpaceList(when, manyday, month, manypeople, id);
            foreach (Space item in availablelist)
            {
                Console.WriteLine(item.Id.ToString() + item.Name.PadLeft(30) + "$ ".PadLeft(20) + item.DailyRate.ToString() + item.MaxOccupancy.ToString().PadLeft(20) + item.Accessible.PadLeft(20) + "$ ".PadLeft(20) + (item.DailyRate * manyday).ToString());
            }
            Console.WriteLine();
            isGood = false;
            int passDown = 0;
            while (input != "0" && !isGood)
            {

                Console.WriteLine("Which space would you like to reserve (enter 0 to cancel)?");
                input = Console.ReadLine();
                isGood = int.TryParse(input, out passDown);


            }
            if (passDown > 0)
            {
                Reserve(passDown, when, manyday, manypeople);
            }

        }
        public void Reserve(int id, DateTime when, int days, int attendee)
        {
            Console.WriteLine("Who is this reservation for?");
            string reservefor = Console.ReadLine();
            Reservation result = accessReservation.toReserve(id, reservefor, when, days, attendee);
            if (result.Id != 0)
            {
                Console.WriteLine();
                Console.WriteLine("Thanks for submitting your reservation! The details for your event are listed below:");
                Console.WriteLine();
                Console.WriteLine("Confirmation #: " + result.Id);
                Console.WriteLine("Venue: " + result.Venue);
                Console.WriteLine("Space: " + result.Space);
                Console.WriteLine("Reserved For: " + result.ReserveFor);
                Console.WriteLine("Attendees: " + result.NumberOfAttendee);
                Console.WriteLine("Arrival Date: " + result.StartDate);
                Console.WriteLine("Depart Date: " + result.EndDate);
                Console.WriteLine("Total Cost: $" + result.Total);
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("The space you put in is not Available. Please hit enter and try again!");
                Console.ReadLine();
            }
        }
    }
}
