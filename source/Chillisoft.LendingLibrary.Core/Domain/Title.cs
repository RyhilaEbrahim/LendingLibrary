namespace Chillisoft.LendingLibrary.Core.Domain
{
    public class EntityBase
    {
        public int Id { get; set; }
    }

    public class Title : EntityBase
    {

        public  string Description { get; set; }
    }
}