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
        private static HttpDataHelper DataHelper { get; set; }
        static async Task Main(string[] args)
        {
            var headers = new List<(string key, string value)>
            {
             //   ("Accept-Encoding", "gzip"),
                ("Accept", "application/json"),
                ("Duffel-Version", "beta")
            };

            DataHelper = new HttpDataHelper("https://api.duffel.com/air/",
                "duffel_test_aHUVJjNpCUBWQfd-4ZLgfmOSvh68E7FJZccISKvOwbZ", headers);


            Console.WriteLine("Welcome to Auctimo Travels!");

            Console.WriteLine("Enter your destination");
            var destination = GetDestination();

            Console.WriteLine("Enter your origin");
            var origin = GetOrigin();

            Console.WriteLine("Enter your Departure Date");
            var departureDate = GetDepartureDate();

            Console.WriteLine("Enter your passengers");
            var passengers = GetPassengers();

            Console.WriteLine("Enter your Cabin class. i.e \"first\", \"business\", \"premium_economy\", or \"economy\"");
            var cabinClass = GetCabinClass().ToString();

            var slices = new List<Slice>();
            var flight1Slice = new Slice
            {
                DepartureDate = departureDate,
                Destination = destination,
                Origin = origin
            };
            slices.Add(flight1Slice);

            var data = new Data
            {
                CabinClass = cabinClass,
                Passengers = passengers,
                Slices = slices
            };

            var payload = new OfferRequestPayload
            {
                Data = data
            };

            var result = await DataHelper.PostItem(payload, "offer_requests");
            Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
        }

        private static CabinClass GetCabinClass()
        {
            CabinClass cabinClass = CabinClass.economy;
            var cabinClassString = Console.ReadLine();

            if (string.IsNullOrEmpty(cabinClassString))
            {
                Console.WriteLine("Invalid Entry! Enter your Cabin class. i.e \"first\", \"business\", \"premium_economy\", or \"economy\"");
                GetCabinClass();
            }
            else
            {
                try
                {
                    cabinClass = (CabinClass)Enum.Parse(typeof(CabinClass), cabinClassString);
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid Entry! Enter your Cabin class. i.e \"first\", \"business\", \"premium_economy\", or \"economy\"");
                    GetCabinClass();
                }
            }

            return cabinClass;
        }

        private static List<Passenger> GetPassengers()
        {
            var passengers = new List<Passenger>();

            Console.WriteLine("How many adults are traveling?");
            var adults = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < adults; i++)
            {
                var passenger = new Passenger()
                {
                    Type = PassengerType.adult.ToString()
                };

                passengers.Add(passenger);
            }

            Console.WriteLine("How many children are traveling?");
            var children = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < children; i++)
            {
                var passenger = new Passenger()
                {
                    Type = PassengerType.child.ToString()
                };

                passengers.Add(passenger);
            }

            Console.WriteLine("How many infants without seats are traveling?");
            var infants = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < infants; i++)
            {
                var passenger = new Passenger()
                {
                    Type = PassengerType.infant_without_seat.ToString()
                };

                passengers.Add(passenger);
            }

            return passengers;

        }

        private static DateTime GetDepartureDate()
        {
            var departureDateString = Console.ReadLine();
            var departureDate = new DateTime();

            if (string.IsNullOrEmpty(departureDateString))
            {
                Console.WriteLine("Invalid Entry! Enter your departure date");
                GetDepartureDate();
            }

            else
            {
                try
                {
                    departureDate = DateTime.Parse(departureDateString);
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid Entry! Enter your departure date");
                    GetDepartureDate();
                }
            }
            return departureDate;
        }

        private static string GetOrigin()
        {
            var origin = Console.ReadLine();

            if (string.IsNullOrEmpty(origin))
            {
                Console.WriteLine("Invalid Entry! Enter your origin");
                GetOrigin();
            }

            return origin;
        }

        private static string GetDestination()
        {
            var destination= Console.ReadLine();

            if (string.IsNullOrEmpty(destination))
            {
                Console.WriteLine("Invalid Entry! Enter your destination");
                GetDestination();
            }

            return destination;
        }

    }
}
