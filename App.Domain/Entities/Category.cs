namespace App.Domain.Entities;
using App.Domain.Abstractions;
public record Category (string Value){
    public static implicit operator string(Category category) => category.Value;
    public static implicit operator Category(string value) => new(value);
}