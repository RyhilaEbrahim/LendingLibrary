namespace Chillisoft.LendingLibrary.DB
{
    public class DataConstants
    {
        public const string DefaultConnectionName = "DefaultConnection";

        public class Tables
        {

            public class Title
            {
                public const string TableName = "Title";

                public class Columns
                {
                    public const string TitleId = "TitleId";
                    public const string Description = "Description";
                   }

            }
            public class Borrower
            {
                public const string TableName = "Borrower";

                public class Columns
                {
                    public const string BorrowerId = "BorrowerId";
                    public const string TitleId = "TitleId";
                    public const string FirstName = "FirstName";
                    public const string Surname = "Surname";
                    public const string Email = "Email";
                    public const string ContactNumber = "ContactNumber";
                    public const string Photo = "Photo";
                   }

            }
        }
    }
}