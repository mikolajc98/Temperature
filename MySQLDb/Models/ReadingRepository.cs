using Dapper;
using MySQLDb.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MySQLDb.Models
{
    public class ReadingRepository : Repository<Reading>
    {
        public override bool Add(Reading item)
        {
            if (item.Id != 0)
                throw new ArgumentException("Object already exists.");

            var addedId = DatabaseService.Instance.Connection.QueryFirst<int>($"INSERT INTO Reading(RoomId,Temperature,Date) VALUES ({item.Room.Id},'{item.TemperatureObj.GetTemperature(TemperatureUnit.Kelvin).ToString().Replace(',', '.')}', '{item.Date.ToString(DateTimeFormat)}'); SELECT LAST_INSERT_ID();");
            
            item.SetId(addedId);

            return true;
        }

        public override bool AddOrUpdate(Reading item)
        {
            if (item.Id == 0)
                return Add(item);
            else
                return Update(item);
        }

        public override IEnumerable<Reading> Find(List<int> ids)
        {
            if (ids is null || ids.Any() == false || ids.Any(x => x == 0))
                throw new ArgumentException("Object does not exists.");

            var query = $"SELECT Rd.*, Room.* FROM Reading Rd JOIN Room Room ON Rd.RoomId = Room.Id WHERE Rd.ID IN ({string.Join(",", ids)})";

            var items = DatabaseService.Instance.Connection.Query<Reading, Room, Reading>(query, (reading, room) =>
            {
                reading.Room = room;
                return reading;
            });

            return items;
        }

        public override IEnumerable<Reading> GetAll()
        {
            var query = $"SELECT Rd.*, Room.* FROM Reading Rd JOIN Room Room ON Rd.RoomId = Room.Id";

            var items = DatabaseService.Instance.Connection.Query<Reading, Room, Reading>(query, (reading, room) =>
            {
                reading.Room = room;
                return reading;
            });

            return items;
        }

        public override Reading Get(int id)
        {
            if (id == 0)
                throw new ArgumentException("Object does not exists.");

            var query = $"SELECT Rd.*, Room.* FROM Reading Rd JOIN Room Room ON Rd.RoomId = Room.Id WHERE Rd.ID = ({id})";

            var item = DatabaseService.Instance.Connection.Query<Reading, Room, Reading>(query, (reading, room) =>
            {
                reading.Room = room;
                return reading;
            }).First();

            return item;
        }

        public override bool Remove(Reading item)
        {
            if (item.Id == 0)
                throw new ArgumentException("Object does not exists.");

            DatabaseService.Instance.Connection.Query($"DELETE FROM Reading WHERE ID = {item.Id}");

            return true;
        }

        public override bool Update(Reading item)
        {
            if (item.Id == 0)
                throw new ArgumentException("Object does not exists.");

            DatabaseService.Instance.Connection.Query($"UPDATE Reading SET RoomId = {item.Room.Id}, Temperature = {item.TemperatureObj.GetTemperature(TemperatureUnit.Kelvin)}, Date = {item.Date} WHERE ID = {item.Id}");
            return true;
        }
    }
}
