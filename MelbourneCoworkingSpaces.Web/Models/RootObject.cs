namespace coworking_spaces.Models
{
    public class Rootobject
    {
        public int total_count { get; set; }
        public Result[] results { get; set; }
    }

    public class Result
    {
        public string organisation { get; set; }
        public string address { get; set; }
        public string website { get; set; }

        public double? latitude { get; set; }
        public double? longitude { get; set; }
        public Geopoint? geopoint { get; set; }

    }

    public class Geopoint
    {
        public double lon { get; set; }
        public double lat { get; set; }
    }

}
