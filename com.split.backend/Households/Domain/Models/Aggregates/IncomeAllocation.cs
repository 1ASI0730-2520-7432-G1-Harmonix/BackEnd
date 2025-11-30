using com.split.backend.Households.Domain.Models.Commands;

namespace com.split.backend.Households.Domain.Models.Aggregates;

public partial class IncomeAllocation
{
    public string Id { get; set; }
    public long UserId { get; set; }
    public string HouseholdId { get; set; }
    public Decimal Percentage { get; set; }


    public IncomeAllocation()
    {
        this.Id = Guid.NewGuid().ToString();
        this.UserId = -1;
        this.HouseholdId = string.Empty;
        this.Percentage = 0;
        this.CreatedDate = DateTime.Now;
        this.UpdatedDate = DateTime.Now;
    }

    public IncomeAllocation(long userId, string householdId, Decimal percentage)
    {
        this.Id = "IA" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
        this.UserId = userId;
        this.HouseholdId = householdId;
        this.Percentage = percentage;
        this.CreatedDate = DateTime.Now;
        this.UpdatedDate = DateTime.Now;
    }

    public IncomeAllocation(CreateIncomeAllocationCommand command)
    {
        this.Id = "IA" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
        this.UserId = command.UserId;
        this.HouseholdId = command.HouseholdId;
        this.Percentage = command.Percentage;
        this.CreatedDate = this.UpdatedDate=DateTime.Now ;
    }

    public IncomeAllocation Update(UpdateIncomeAllocationCommand command)
    {
        if(command.UserId != null) this.UserId = (long)command.UserId;
        if(!string.IsNullOrWhiteSpace(command.HouseholdId)) this.HouseholdId = command.HouseholdId;
        if(command.Percentage != null) this.Percentage = (decimal)command.Percentage;

        return this;
    }
    
}