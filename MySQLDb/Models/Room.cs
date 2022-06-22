namespace MySQLDb.Models
{
    public class Room : Entity
    {
        public const int _maxRoomName = 100;

        public string Name { get; protected set; }

        public bool SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return false;

            if (name.Length > _maxRoomName)
                return false;

            Name = name;
            return true;
        }

        public override string ToString()
        {
            return $"{Id} - {Name}";
        }
    }
}
