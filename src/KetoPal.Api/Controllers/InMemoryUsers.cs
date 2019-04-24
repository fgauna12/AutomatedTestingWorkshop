using System.Collections.Generic;

namespace KetoPal.Api.Controllers
{
    public class InMemoryUsers
    {
        private static readonly List<User> Database = new List<User>
        {
            new User
            {
                Id = 1,
                UserName = "fgauna",
                Preference = new Preference
                {
                    MaxCarbsPerDayInGrams = 50
                },
                CarbConsumption = new List<CarbConsumption>()
            }
        };

        public static List<User> GetUsers()
        {
            return Database;
        }
    }
}