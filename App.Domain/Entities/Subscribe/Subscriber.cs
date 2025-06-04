using App.Domain.Abstractions;

namespace App.Domain.Entities.Subscribe;

public class Subscriber: Entity
{
    private Subscriber() : base(Guid.Empty){}
    private Subscriber(
        Guid id,
        Email? email,
        PhoneNumber? phoneNumber
    ) : base(id)
    {
        Email = email;
        PhoneNumber = phoneNumber;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
        LastNotificationSent = null;
    }

    public static Subscriber CreateWithEmail(Email email)
    {
        var subscriber = new Subscriber(Guid.NewGuid(), email,null);
        subscriber.PreferredNotificationMethod = NotificationPreference.Email;
        return subscriber;
    }

    public static Subscriber CreateWithPhoneNumber(PhoneNumber phoneNumber)
    {
        var subscriber = new Subscriber(Guid.NewGuid(),null,phoneNumber);
        subscriber.PreferredNotificationMethod = NotificationPreference.Sms;
        return subscriber;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void Activate()
    {
        IsActive = true;
    }
    
    
    public Email? Email { get; private set; }
    public PhoneNumber? PhoneNumber { get; private set; }
    public NotificationPreference PreferredNotificationMethod { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? LastNotificationSent { get; private set; }
    
    
    public enum NotificationPreference
    {
        Email=1,
        Sms = 2
    }
    

}