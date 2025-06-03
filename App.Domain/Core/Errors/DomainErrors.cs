using App.Domain.Core.Primitives;

namespace App.Domain.Core.Errors
{
    /// <summary>
    /// Contains the domain errors.
    /// </summary>
    public static class DomainErrors
    {
        /// <summary>
        /// Contains the user errors.
        /// </summary>
        public static class User
        {
            public static Error NotFound => new Error("User.NotFound", "The user with the specified identifier was not found.");

            public static Error InvalidPermissions => new Error(
                "User.InvalidPermissions",
                "The current user does not have the permissions to perform that operation.");
            
            public static Error DuplicateEmail => new Error("User.DuplicateEmail", "The specified email is already in use.");

            public static Error CannotChangePassword => new Error(
                "User.CannotChangePassword",
                "The password cannot be changed to the specified password.");
        }

     

        /// <summary>
        /// Contains the category errors.
        /// </summary>
        public static class Category
        {
            public static Error NotFound => new Error("Category.NotFound", "The category with the specified identifier was not found.");
        }

       

      


        /// <summary>
        /// Contains the notification errors.
        /// </summary>
        public static class Notification
        {
            public static Error AlreadySent => new Error("Notification.AlreadySent", "The notification has already been sent.");
        }

   

      

       

        /// <summary>
        /// Contains the name errors.
        /// </summary>
        public static class Name
        {
            public static Error NullOrEmpty => new Error("Name.NullOrEmpty", "The name is required.");

            public static Error LongerThanAllowed => new Error("Name.LongerThanAllowed", "The name is longer than allowed.");
        }

        /// <summary>
        /// Contains the first name errors.
        /// </summary>
        public static class FirstName
        {
            public static Error NullOrEmpty => new Error("FirstName.NullOrEmpty", "The first name is required.");

            public static Error LongerThanAllowed => new Error("FirstName.LongerThanAllowed", "The first name is longer than allowed.");
        }

        /// <summary>
        /// Contains the last name errors.
        /// </summary>
        public static class LastName
        {
            public static Error NullOrEmpty => new Error("LastName.NullOrEmpty", "The last name is required.");

            public static Error LongerThanAllowed => new Error("LastName.LongerThanAllowed", "The last name is longer than allowed.");
        }

        /// <summary>
        /// Contains the email errors.
        /// </summary>
        public static class Email
        {
            public static Error NullOrEmpty => new Error("Email.NullOrEmpty", "The email is required.");

            public static Error LongerThanAllowed => new Error("Email.LongerThanAllowed", "The email is longer than allowed.");

            public static Error InvalidFormat => new Error("Email.InvalidFormat", "The email format is invalid.");
        }

        

        /// <summary>
        /// Contains general errors.
        /// </summary>
        public static class General
        {
            public static Error UnProcessableRequest => new Error(
                "General.UnProcessableRequest",
                "The server could not process the request.");

            public static Error ServerError => new Error("General.ServerError", "The server encountered an unrecoverable error.");
        }

        
    }
}
