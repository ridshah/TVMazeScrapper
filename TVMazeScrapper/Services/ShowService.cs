using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TVMazeScrapper.Models;

namespace TVMazeScrapper.Services
{
    public class ShowService : IShowService
    {
        private readonly Scrapper _scrapper;
        public ShowService()
        {
            _scrapper = new Scrapper();
        }
        public Show Get(int i)
        {
            return _scrapper.Shows.Get(i);
        }
        public IQueryable<Show> GetAll(int page)
        {
            return _scrapper.Shows.GetAll(page);
        }
        public void Delete(int id)
        {
            _scrapper.Shows.Delete(s => s.id, id);
        }
        public void Insert(Show show)
        {
            _scrapper.Shows.Add(show);
        }
        public void Update(Show show, int id)
        {
            _scrapper.Shows.Update(s => s.id, id, show);
        }
    }
}
