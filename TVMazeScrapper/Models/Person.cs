using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TVMazeScrapper.Models
{
    public class Person 
    {
        public int id { get; set; }
        public string name { get; set; }
        [DisplayFormat(DataFormatString = "yyyy-MM-dd")]
        [BsonDateTimeOptions(DateOnly = true)]
        [JsonConverter(typeof(DateFormatConverter), "yyyy-MM-dd")]
        public Nullable<DateTime> birthday { get; set; }
        //public int CompareTo(Person b)
        //{
        //    return this.birthday.Value.CompareTo(b.birthday.Value);

        //}

    }
    public class DateFormatConverter : IsoDateTimeConverter
    {
        public DateFormatConverter(string format)
        {
            DateTimeFormat = format;
        }
    }
}
