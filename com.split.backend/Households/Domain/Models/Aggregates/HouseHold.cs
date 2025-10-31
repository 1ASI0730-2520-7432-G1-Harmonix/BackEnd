using com.split.backend.Households.Domain.Models.ValueObjects;
using com.split.backend.IAM.Domain.Model.Aggregates;

namespace com.split.backend.Households.Domain.Models.Aggregates;

public partial class HouseHold
{
    private string Id { get; }
    private string Name { get; set; }
    private long RepresentativeId { get; set; }
    public ECurrency? Currency { get; set; }
    public HouseHold(){}
    


}