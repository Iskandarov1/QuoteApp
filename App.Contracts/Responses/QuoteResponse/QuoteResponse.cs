namespace App.Contracts.Responses;

public sealed record QuoteResponse(
     Guid Id ,
     string Author ,
     string Text ,
     string Category) : BaseResponse;