namespace HouseRentingSystem.Common
{
    public static class EntityValidationConstants
    {
        public static class Category
        {
            public const int CategoryNameMaxLength = 50;
            public const int CategoryNameMinLength = 2;
        }

        public static class House
        {
            public const int TitleMinLength = 10;
            public const int TitleMaxLength = 50;

            public const int AddressMinLength = 30;
            public const int AddressMaxLength = 150;

            public const int DescriptionMinLength = 50;
            public const int DescriptionMaxLength = 500;

            public const int ImageUrlMaxLength = 2048;

            public const string PricePermounthMinValue = "0";
            public const string PricePermounthMaxValue = "2000";
        }

        public static class Agent
        {
            public const int PhoneNumberMaxLength = 15;
            public const int PhoneNumberMinLength = 7;
        }

        public static class User
        { 
            public const int FirstNameMinLength = 1;
            public const int FirstNameMaxLength = 15;

            public const int LastNameMinLength = 1;
            public const int LastNameMaxLength = 15;
        }

    }
}
