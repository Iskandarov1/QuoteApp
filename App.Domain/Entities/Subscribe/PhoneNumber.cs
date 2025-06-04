using System.Text.RegularExpressions;
using App.Domain.Core.Errors;
using App.Domain.Core.Primitives;
using App.Domain.Core.Result;
using System.Text.RegularExpressions;

namespace App.Domain.Entities.Subscribe;

public sealed class PhoneNumber : ValueObject
{
    private static readonly Regex PhoneRegex = new(@"^\+?[\d\s\-\(\)]+$", RegexOptions.Compiled);
    
    private PhoneNumber(string value) => Value = value;
    public string Value { get; }
    
    public static implicit operator string(PhoneNumber phoneNumber) => phoneNumber?.Value ?? string.Empty;
    
    public static Result<PhoneNumber> Create(string? phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            return Result.Failure<PhoneNumber>(DomainErrors.PhoneNumber.NullOrEmpty);
            
        var cleaned = Regex.Replace(phoneNumber, @"[\s\-\(\)]", "");
        
        if (cleaned.Length < 10 || cleaned.Length > 15)
            return Result.Failure<PhoneNumber>(DomainErrors.PhoneNumber.InvalidLength);
            
        if (!PhoneRegex.IsMatch(phoneNumber))
            return Result.Failure<PhoneNumber>(DomainErrors.PhoneNumber.InvalidFormat);
            
        return new PhoneNumber(phoneNumber);
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}