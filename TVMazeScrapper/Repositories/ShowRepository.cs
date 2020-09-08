using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using TVMazeScrapper.Models;

namespace TVMazeScrapper.Repositories
{
    public class ShowRepository<T>where T:class
    {
        private MongoDatabase _database;
        private string _tableName;
        private MongoCollection<T> _collection;
        private int pagesize = 25;

        public ShowRepository(MongoDatabase db, string tblName)
        {
            _database = db;
            _tableName = tblName;
            _collection = _database.GetCollection<T>(tblName);
        }

        public T Get(int i)
        {
            return _collection.FindOneById(i);
        }

        public IQueryable<T> GetAll(int page)
        {
            int skipsize = pagesize * (page - 1);
            MongoCursor<T> cursor = _collection.FindAll().SetSkip(skipsize).SetLimit(pagesize);
            return cursor.AsQueryable<T>();
        }

        public void Add(T entity)
        {
            _collection.Save(entity);
        }

        public void Delete(Expression<Func<T, int>> queryExpression, int id)
        {
            var query = Query<T>.EQ(queryExpression, id);
            _collection.Remove(query);
        }

        public void Update(Expression<Func<T, int>> queryExpression, int id, T entity)
        {
            var query = Query<T>.EQ(queryExpression, id);
            _collection.Update(query, Update<T>.Replace(entity), UpdateFlags.Upsert);
        }
    }
}
