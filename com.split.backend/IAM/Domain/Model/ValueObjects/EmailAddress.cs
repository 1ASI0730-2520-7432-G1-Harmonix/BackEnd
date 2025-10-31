namespace com.split.backend.Households.Domain.Models.ValueObjects;

public record EmailAddress
{
    public string Address { get; set; }

    public EmailAddress(): this(string.Empty){}
    public EmailAddress(string address)
    {
        Address = address;
    }
    
    public void Change(EmailAddress email)
    {
        if(email.Address.Length >0)
            this.Address = email.Address;
        else
            throw new ArgumentException("Email address cannot be empty");
    }
    
}