namespace Pedigree.Domain.Entities
{
    public abstract class Entity
    {

        public Guid Id { get; private set; }
        public string Name { get; protected set; }
        public DateTime CreateTime { get; set; }
        public DateTime ModifyTime { get; set; }

        public Entity(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
    }
}