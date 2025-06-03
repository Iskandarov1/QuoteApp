namespace App.Domain.Entities;

public record Textt(string Value){
    public static implicit operator string(Textt text) => text.Value;
    public static implicit operator Textt(string value) => new(value);
}