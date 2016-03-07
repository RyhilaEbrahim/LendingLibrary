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
                    public const string ContentType = "ContentType";
                   }

            }
            public class Item
            {
                public const string TableName = "Item";

                public class Columns
                {
                    public const string ItemId = "ItemId";
                    public const string Description = "Description";
                 
                   }

            }
          public class BorrowersItem
            {
                public const string TableName = "BorrowersItem";

                public class Columns
                {
                    public const string BorrowersItemId = "BorrowersItemId";
                    public const string BorrowerId = "BorrowerId";
                    public const string ItemId = "ItemId";
                    public const string DateBorrowed = "DateBorrowed";
                    public const string DateReturned = "DateReturned";
                 
                   }

            }
          
        }
    }
}