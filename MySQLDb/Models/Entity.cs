namespace MySQLDb.Models
{
    public abstract class Entity
    {
        public Entity()
        {
            Id = 0;
        }
        public int Id { get; protected set; }

        public void SetId(int id)
        {
            Id = id;
        }

        public override bool Equals(object obj)
        {
            if(GetType() == obj.GetType())
            {
                var entity = (Entity)obj;

                return Id == entity.Id;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
