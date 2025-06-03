namespace App.Api.Contracts
{
    public static class ApiRoutes
    {
        public static class Quotes
        {
            public const string GetAll = "";
            public const string GetById = "{quoteId:guid}";
            public const string Create = "quotes";
            public const string Update = "{quoteId:guid}";
            public const string Delete = "quotes/{quotesId:guid}";
        }
        public static class Subscriptions
        {
            public const string Subscribe = "subscriptions";
            public const string Unsubscribe = "subscriptions/subscriptionId:guid}";
        }

    }
}