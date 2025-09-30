namespace com.split.backend.Households.Domain.Models.Aggregates;

public partial class Household
{
    private long Id { get; }
    private PersonName Name { get; set; }
    private string Description { get; set; }
    
    private User Representative {get; set;}
    
    public Household(){}
    
    public Household()


}