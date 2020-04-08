using System;




namespace Foodies.Models 
{
    public class FacebookData
    {
        public string id { get; set; }
        public string name { get; set; }
        public Age_Range age_range { get; set; }
        public string gender { get; set; }
        public Payment_Pricepoints payment_pricepoints { get; set; }
        public Likes likes { get; set; }
    }

    public class Age_Range
    {
        public int min { get; set; }
    }

    public class Payment_Pricepoints
    {
        public Mobile[] mobile { get; set; }
    }

    public class Mobile
    {
        public int credits { get; set; }
        public string local_currency { get; set; }
        public string user_price { get; set; }
    }

    public class Likes
    {
        public Datum[] data { get; set; }
        public Paging paging { get; set; }
        public Summary summary { get; set; }
    }

    public class Paging
    {
        public Cursors cursors { get; set; }
        public string next { get; set; }
    }

    public class Cursors
    {
        public string before { get; set; }
        public string after { get; set; }
    }

    public class Summary
    {
        public int total_count { get; set; }
    }

    public class Datum
    {
        public string name { get; set; }
        public string id { get; set; }
        public DateTime created_time { get; set; }
    }

}