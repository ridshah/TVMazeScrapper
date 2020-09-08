using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace TVMazeScrapper.Models
{
    public class Show
    {
        [BsonElement("_id")]
        public int id { get; set; }
        public string name { get; set; }
        public Person[] cast { get; set; }
        public Show OrderByBirthday()
        {
            this.cast = this.cast.OrderByDescending(c => c.birthday).ToArray();
            return this;
        }

    }

    public class SortShow
    {
        public Show Sort(Show show)
        {
            show.cast = show.cast.OrderByDescending(c => c.birthday).ToArray();
            return show;
        }
    }
}