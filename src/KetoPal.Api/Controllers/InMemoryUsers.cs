using System.Collections.Generic;

namespace KetoPal.Api.Controllers
{
    public class InMemoryUsers
    {
        public static List<User> GetUsers()
        {
            return new List<User>()
            {
                new User()
                {
                    Id = 1,
                    UserName = "fgauna",
                    Preference = new Preference()
                    {
                        MaxCarbsPerDayInGrams = 50
                    },
                    CarbConsumption = new List<CarbConsumption>()
                    {

                    }
                }
            };
        }
    }
}