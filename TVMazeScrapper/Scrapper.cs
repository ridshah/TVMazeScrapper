using Microsoft.AspNetCore.Mvc.Formatters.Internal;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Protocols;
using MongoDB.Driver;
using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Handlers;
using System.Threading;
using System.Threading.Tasks;
using TVMazeScrapper.Models;
using TVMazeScrapper.Repositories;
using TVMazeScrapper.Services;
using TVMazeScrapper.Controllers;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace TVMazeScrapper
{
    public class Scrapper
    {
        private MongoDatabase _database;
        protected ShowRepository<Show> _shows;

        public Scrapper()
        {
            var dbclient = new MongoClient(ConfigurationManager.AppSettings["Mongo ConnectionString"].ToString());
            var dbserver = dbclient.GetServer();
            _database = dbserver.GetDatabase(ConfigurationManager.AppSettings["Mongo DatabaseName"].ToString());
        }
        
        public ShowRepository<Show> Shows
        {
            get
            {
                if (_shows == null)
                {
                    _shows = new ShowRepository<Show>(_database, "shows");
                }
                return _shows;
            }
        }
        public static void GetData()
        {
            int id = 0;
            //count of retry for not found
            int count = 0;
            string responseBody;
            dynamic response;
            
            do
            {
                id++;
                responseBody = "";
                string requestURL = " http://api.tvmaze.com/shows/" + id + "?embed=cast";
                using (HttpClient client = new HttpClient())
                {
                    response = client.GetAsync(requestURL);
                    responseBody = response.Result.Content.ReadAsStringAsync().Result;
                    if (response.Result.StatusCode.ToString() == "OK")
                    {
                        count = 0;
                        Show show = new Show();
                        var obj = JObject.Parse(responseBody);
                        show.id = (int)obj["id"];
                        show.name = (string)obj["name"];
                        var castarray = obj["_embedded"]["cast"].ToList();
                        List<Person> cast = new List<Person>();
                        foreach(var member in castarray)
                        {
                            Person person = new Person();
                            person = JsonConvert.DeserializeObject <Person>(member.SelectToken("person").ToString());
                            cast.Add(person);
                        }
                        show.cast = cast.OrderByDescending(c=>c.birthday).ToArray();
                        
                        //Insert into DB
                        ShowController sc = new ShowController();
                        sc.Post(show);

                    }
                    //Handle Rate Limit
                    else if(response.Result.StatusCode.ToString() == "TooManyRequests")
                    {
                        Thread.Sleep(12000);
                        continue;
                    }
                    else if(response.Result.StatusCode.ToString() == "NotFound") 
                    {
                        count++;
                        continue;
                    }
                    else { 
                        //Send Exception to Exception Logger
                    }
                }
            }
            
            while (count <= 3);
            
        }
    }
}
