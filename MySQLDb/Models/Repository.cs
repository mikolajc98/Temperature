using System.Collections.Generic;

namespace MySQLDb.Models
{
    public abstract class Repository<T> where T : Entity
    {
        public const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

        public abstract IEnumerable<T> GetAll();
        public abstract T Get(int id);
        public abstract bool Add(T item);
        public abstract bool Update(T item);
        public abstract bool AddOrUpdate(T item);
        public abstract IEnumerable<T> Find(List<int> ids);
        public abstract bool Remove(T item);
    }
}