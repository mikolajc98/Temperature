using MySQLDb.Service;
using System;
using System.Collections.Generic;
using Dapper;
using System.Linq;

namespace MySQLDb.Models
{
    public class RoomRepository : Repository<Room>
    {
        public override bool Add(Room item)
        {
            if (item.Id != 0)
                throw new ArgumentException("Object already exists.");

            var addedId = DatabaseService.Instance.Connection.QueryFirst<int>($"INSERT INTO ROOM(Name) VALUES ('{item.Name}'); SELECT LAST_INSERT_ID();");
            
            item.SetId(addedId);

            return true;
        }
        public override bool AddOrUpdate(Room item)
        {
            if (item.Id == 0)
                return Add(item);
            else
                return Update(item);
        }
        public override IEnumerable<Room> GetAll()
        {
            var items = DatabaseService.Instance.Connection.Query<Room>($"SELECT Id, Name FROM ROOM");
            return items;
        }
        public override IEnumerable<Room> Find(List<int> ids)
        {
            if (ids is null || ids.Any() == false || ids.Any(x => x == 0))
                throw new ArgumentException("Object does not exists.");

            var items = DatabaseService.Instance.Connection.Query<Room>($"SELECT Id, Name FROM ROOM WHERE ID IN ({string.Join(",",ids)}) ");
            return items;
        }

        public override Room Get(int id)
        {
            if (id == 0)
                throw new ArgumentException("Object does not exists in database.");

            var item = DatabaseService.Instance.Connection.QueryFirst<Room>($"SELECT Id, Name FROM ROOM WHERE ID = {id}");
            return item;
        }
        public override bool Remove(Room item)
        {
            if (item.Id == 0)
                throw new ArgumentException("Object does not exists in database.");

            DatabaseService.Instance.Connection.Query($"DELETE FROM ROOM WHERE ID = {item.Id}");

            return true;
        }
        public override bool Update(Room item)
        {
            if (item.Id == 0)
                throw new ArgumentException("Object does not exists in database.");

            DatabaseService.Instance.Connection.Query($"UPDATE ROOM SET NAME = '{item.Name}' WHERE ID = {item.Id}");
            return true;
        }
    }
}
