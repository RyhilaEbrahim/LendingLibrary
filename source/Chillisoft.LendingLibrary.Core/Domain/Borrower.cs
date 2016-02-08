namespace Chillisoft.LendingLibrary.Core.Domain
{
    public class Borrower
    {
        public int Id { get; set; } 
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public virtual Title Title { get; set; }
        public int TitleId { get; set; }
         
       
    }
}