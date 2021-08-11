﻿
namespace DiyOmnitheca.Data
{
    public class DataConstants
    {
        public class Product
        {
            public const int ProductNameMaxLength = 70;
            public const int ProductDefaultMinLength = 2;
            public const int ProductDescriptionMinLength = 10;
            public const int ProductBrandMaxLength = 30;
            public const int ProductDescriptionMaxLength = 300;
            public const int ProductLocationMinLength = 4;
            public const int ProductLocationMaxLength = 50;
            public const double ProductRentCostMaxValue = 999999999.99;
        }

        public class Category
        {
            public const int CategoryMaxName = 20;
        }

        public class Person
        {
            public const int PersonNameMaxLength = 30;
            public const int PhoneNumberMaxLength = 20;
        }

    }
}
