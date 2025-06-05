namespace App.Contracts.Responses.EmailQuoteResponse;

public record  SentNotificationResponse
( 
   Guid SubscriberId ,
   string? Email ,
   string? PhoneNumber,
   string NotificationMethod ,
   string? LastSentQuoteText,
   string? LastSentQuoteAuthor,
   DateTime? LastNotificationSent ,
   bool IsActive 
);