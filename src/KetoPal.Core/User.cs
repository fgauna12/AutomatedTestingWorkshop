using System.Collections.Generic;

namespace KetoPal.Core
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public Preference Preference { get; set; }
        public List<CarbConsumption> CarbConsumption { get; set; }
    }
}