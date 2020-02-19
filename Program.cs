/* Version: 1.0.0
 * Author: Reece Pieri
 * Date: 17/02/2020
 * Name: Exchange Rate Converter
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;

namespace ExchangeRateConverter
{
    class Program
    {
        static bool exit = false;
        static string input;
        static void Main(string[] args)
        {
            string convertFrom;
            string convertTo;
            string url;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--- Exchange Rate Converter ---");
            Console.ResetColor();
            Console.WriteLine("Enter \"exit\" to exit the program.\n");

            while (!exit)
            {
                // Prompt user to select conversion currencies.
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("For a list of currency codes, enter \"help\".");
                Console.ResetColor();
                Console.Write("Enter currency to convert from (eg. AUD): ");
                input = Console.ReadLine();
                if (InputValidation(input))
                {
                    convertFrom = input.ToUpper();
                }
                else
                {
                    continue;
                }

                Console.Write("Enter currency to convert to (eg. AUD): ");
                input = Console.ReadLine();
                if (InputValidation(input))
                {
                    convertTo = input.ToUpper();
                }
                else
                {
                    continue;
                }

                // Request json from API.
                url = "https://api.exchangerate-api.com/v4/latest/" + convertFrom;
                WebRequest requestObjGet = WebRequest.Create(url);
                HttpWebResponse responseObjGet;
                responseObjGet = (HttpWebResponse)requestObjGet.GetResponse();

                // Convert json to string.
                string jsonstr;
                using Stream stream = responseObjGet.GetResponseStream();
                StreamReader sr = new StreamReader(stream);
                jsonstr = sr.ReadToEnd();
                sr.Close();

                // Parse json string to json object.
                JObject parsed = JObject.Parse(jsonstr);

                // Print exchange rates from json object.
                Console.WriteLine(convertFrom + ": " + parsed["rates"][convertFrom]);
                Console.WriteLine(convertTo + ": " + parsed["rates"][convertTo] + "\n");
            }

            // Method to validate user input.
            static bool InputValidation(string input)
            {
                if (input == "exit")
                {
                    exit = true;
                    return false;
                }
                else if (input == "help")
                {
                    DisplayHelp();
                    return false;
                }
                else if (input.Length != 3)
                {
                    Error();
                    return false;
                }
                else if (input.Length == 3)
                {
                    foreach (char c in input)
                    {
                        if (!char.IsLetter(c))
                        {
                            Error();
                            return false;
                        }
                    }
                    return true;
                }
                else
                {
                    return true;
                }
            }

            // Method to display an error message.
            static void Error()
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input. Please enter a valid currency code (eg. AUD).\n");
                Console.ResetColor();
            }

            static void DisplayHelp()
            {
                Dictionary<string, string> currencyList = new Dictionary<string, string>
                {
                    {"AED", new string("United Arab Emirates") },
                    {"ARS", new string("Argentina") },
                    {"AUD", new string("Australia") },
                    {"BGN", new string("Bulgaria") },
                    {"BRL", new string("Brazil") },
                    {"BSD", new string("Bahamas") },
                    {"CAD", new string("Canada") },
                    {"CHF", new string("Switzerland") },
                    {"CLP", new string("Chile") },
                    {"CNY", new string("China") },
                    {"COP", new string("Colombia") },
                    {"CZK", new string("Czech Republic") },
                    {"DKK", new string("Denmark") },
                    {"DOP", new string("Dominican Republic") },
                    {"EGP", new string("Egypt") },
                    {"EUR", new string("Euro") },
                    {"FJD", new string("Fiji") },
                    {"GBP", new string("United Kingdom") },
                    {"GTQ", new string("Guatemala") },
                    {"HKD", new string("Hong Kong") },
                    {"HRK", new string("Croatia") },
                    {"HUF", new string("Hungary") },
                    {"IDR", new string("Indonesia") },
                    {"ILS", new string("Israel") },
                    {"INR", new string("India") },
                    {"ISK", new string("Iceland") },
                    {"JPY", new string("Japan") },
                    {"KRW", new string("Korea") },
                    {"KZT", new string("Kazakhstan") },
                    {"MXN", new string("Mexico") },
                    {"MYR", new string("Malaysia") },
                    {"NOK", new string("Norway") },
                    {"NZD", new string("New Zealand") },
                    {"PAB", new string("Panama") },
                    {"PEN", new string("Peru") },
                    {"PHP", new string("Philippines") },
                    {"PKR", new string("Pakistan") },
                    {"PLN", new string("Poland") },
                    {"PYG", new string("Paraguay") },
                    {"RON", new string("Romania") },
                    {"RUB", new string("Russia") },
                    {"SAR", new string("Saudi Arabia") },
                    {"SEK", new string("Sweden") },
                    {"SGD", new string("Singapore") },
                    {"THB", new string("Thailand") },
                    {"TRY", new string("Turkey") },
                    {"TWD", new string("Taiwan") },
                    {"UAH", new string("Ukraine") },
                    {"USD", new string("United States") },
                    {"UYU", new string("Uruguay") },
                    {"ZAR", new string("South Africa") }
                };

                Console.WriteLine("\n*** CURRENCY LIST ***");
                foreach (var pair in currencyList)
                {
                    Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
                }
                Console.WriteLine("");
            }
        }
    }
}
