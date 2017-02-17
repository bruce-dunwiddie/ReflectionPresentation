using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSON.NetSimulation
{
    /// <summary>
    ///     Simplistic implementation of a custom object serializer and deserializer with syntax similar
    ///     to JSON.Net to demonstrate parsing, reflection, construction, and invocation concepts.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // http://www.newtonsoft.com/json/help/html/SerializeObject.htm

            Account account = new Account
            {
                Email = "james@example.com",
                Active = true,
                CreatedDate = new DateTime(2013, 1, 20, 0, 0, 0, DateTimeKind.Utc),
                Roles = new List<string>
                {
                    "User",
                    "Admin"
                }
            };

            // serialize the Account instance to a string using syntax like JSON.Net calls use
            // but using custom serialization logic instead

            string bruceon = SimulatedConvert.SerializeObject(
                account,
                Formatting.Indented);

            // in JSON

            // {
            //   "Email": "james@example.com",
            //   "Active": true,
            //   "CreatedDate": "2013-01-20T00:00:00Z",
            //   "Roles": [
            //     "User",
            //     "Admin"
            //   ]
            // }

            // in BruceON™

            // {
            //         Active=True
            //         CreatedDate=1/20/2013 12:00:00 AM
            //         Email=james@example.com
            //         Roles=
            //         [
            //                 User
            //                 Admin
            //         ]
            // }

            Console.WriteLine(bruceon);

            // http://www.newtonsoft.com/json/help/html/DeserializeObject.htm

            // reconstitute the serialized Account instance back into an instance using syntax
            // like JSON.Net calls use
            // but using custom deserialization logic instead

            account = SimulatedConvert.DeserializeObject<Account>(
                bruceon);

            Console.WriteLine(account.Email);

            Console.ReadLine();
        }
    }
}
