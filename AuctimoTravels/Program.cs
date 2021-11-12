using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuctimoTravels.Helpers;
using AuctimoTravels.Models;
using Newtonsoft.Json;


namespace AuctimoTravels
{
    class Program
    {
        public static HttpDataHelper DataHelper { get; set; }

        static async Task Main(string[] args)
        {
            // initialise the headers
            var headers = new List<(string name, string value)>
            {
                ("Accept", "application/json"),
                ("Duffel-Version", "beta")
            };

            // Initialize http data helper
            DataHelper = new HttpDataHelper("https://api.duffel.com/air/",
                "duffel_test_aHUVJjNpCUBWQfd-4ZLgfmOSvh68E7FJZccISKvOwbZ", headers);

            // Get origin
            Console.WriteLine("Enter the city of origin");
            var origin = GetOrigin();

            // Get destination
            Console.WriteLine("Enter destination city");
            var destination = GetDestination();

            // Get departure date
            Console.WriteLine("Enter your departure date");
            var departureDate = GetDepartureDate();

            // Get passengers
            List<Passenger> passengers = GetPassengers();

            // Get cabin class
            Console.WriteLine("Enter the cabin class.\n 1.first\n 2.business\n 3.premium_economy\n 4.economy");
            var cabinClass = GetCabinClass();

            // Initialise slices
            var slice = new Slice
            {
                Origin = origin,
                DepartureDate = departureDate,
                Destination = destination
            };

            var slices = new List<Slice>
            {
                slice
            };

            // Initialise data
            var data = new Data()
            {
                CabinClass = cabinClass,
                Passengers = passengers,
                Slices = slices
            };

            // Initialise the offer request payload
            var payload = new OfferRequestPayload()
            {
                Data = data
            };

            var result = await DataHelper.PostItemAsync("offer_requests", payload);
            Console.WriteLine();
            Console.ForegroundColor = result.IsSuccessful ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        ///     Get the city of origin for the usr
        /// </summary>
        /// <returns></returns>
        private static string GetOrigin()
        {
            // ask for user input;
            var origin = Console.ReadLine();

            // if input is null request for input recursively 
            if (string.IsNullOrEmpty(origin))
            {
                Console.WriteLine("Invalid input. Enter the city of origin");
                GetOrigin();
            }

            return origin;
        }

        /// <summary>
        ///     Gets the destination city of the user
        /// </summary>
        /// <returns></returns>
        private static string GetDestination()
        {
            // ask for user input
            var destination = Console.ReadLine();

            // if input is null request for input recursively 
            if (string.IsNullOrEmpty(destination))
            {
                Console.WriteLine("Invalid input. Enter the destination city");
                GetOrigin();
            }

            return destination;
        }

        /// <summary>
        ///     Get the departure date
        /// </summary>
        /// <returns></returns>
        private static DateTime GetDepartureDate()
        {
            // ask for user input
            var dateString = Console.ReadLine();
            
            var departureDate = new DateTime();

            // Try converting the input to a datetime object
            try
            {
                // if input is null request for input recursively 
                if (string.IsNullOrEmpty(dateString))
                {
                    Console.WriteLine("Invalid input. Enter the departure date");
                    GetOrigin();
                }
                else
                {
                    departureDate = DateTime.Parse(dateString);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid input. Enter the departure date");
                GetOrigin();
            }

            return departureDate;
        }

        /// <summary>
        ///     Get all the passengers that will be booked included in this flight booking
        /// </summary>
        /// <returns></returns>
        private static List<Passenger> GetPassengers()
        {
            var passengers = new List<Passenger>();

            // get the number of adults
            Console.WriteLine("How many adults will be traveling?");
            var adults = Convert.ToInt32(Console.ReadLine());

            // create passenger objects that have the passenger type as adult
            for (var i = 0; i < adults; i++)
            {
                var passenger = new Passenger()
                {
                    Type = PassengerType.adult.ToString()
                };
                passengers.Add(passenger);
            }

            // get the number of children
            Console.WriteLine("How many children will be traveling?");
            var children = Convert.ToInt32(Console.ReadLine());

            // create passenger objects that have the passenger type as child
            for (var i = 0; i < adults; i++)
            {
                var passenger = new Passenger()
                {
                    Type = PassengerType.child.ToString()
                };
                passengers.Add(passenger);
            }

            // get the number of infants
            Console.WriteLine("How many infants will be traveling?");
            var infants = Convert.ToInt32(Console.ReadLine());

            // create passenger objects that have the passenger type as infant_without_seat
            for (var i = 0; i < adults; i++)
            {
                var passenger = new Passenger()
                {
                    Type = PassengerType.infant_without_seat.ToString()
                };
                passengers.Add(passenger);
            }

            return passengers;
        }


        /// <summary>
        ///     Get the cabin class the passengers will travel in
        /// </summary>
        /// <returns></returns>
        private static string GetCabinClass()
        {
            // Get user input for cabin class
            var cabinClassInput = Console.ReadLine();

            // set default cabin class to economy
            var cabinClass = CabinClass.economy;
            try
            {
                // check if input is empty
                if (string.IsNullOrEmpty(cabinClassInput))
                {
                    Console.WriteLine("Invalid entry. Enter the cabin class.\n 1.first\n 2.business\n 3.premium_economy\n 4.economy");
                    GetCabinClass();
                }
                else
                {

                    // convert string to enum
                    cabinClass = (CabinClass)Enum.Parse((typeof(CabinClass)), cabinClassInput);
                }

            }
            catch (Exception)
            {
                Console.WriteLine("Invalid entry. Enter the cabin class.\n 1.first\n 2.business\n 3.premium_economy\n 4.economy");
                GetCabinClass();
            }

            return cabinClass.ToString();
        }
    }
}
