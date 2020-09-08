using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TVMazeScrapper.Models;

namespace TVMazeScrapper.Services
{
    public interface IShowService
    {
        void Insert(Show show);
        Show Get(int i);
        IQueryable<Show> GetAll(int page);
        void Delete(int id);
        void Update(Show show, int id);
    }
}
