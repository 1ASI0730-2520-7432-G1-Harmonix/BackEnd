using com.split.backend.IAM.Domain.Model.Commands;

namespace com.split.backend.IAM.Domain.Model.Aggregates;

public partial class UserIncome
{
    public string Id { get; set; }
    public long UserId { get; set; }
    public string Income { get; set; }

    public UserIncome()
    {
        this.Id = System.Guid.NewGuid().ToString();
        this.UserId = 0;
        this.Income = string.Empty;
        this.CreatedDate = DateTimeOffset.UtcNow;
    }

    public UserIncome(long userId, string income)
    {
        this.Id = "UI" + DateTime.Now.ToString("yyyyMMddhhmmss");
        this.UserId = userId;
        this.Income = income;
    }

    public UserIncome(CreateUserIncomeCommand command)
    {
        this.Id = "UI" + DateTime.Now.ToString("yyyyMMddhhmmss");
        this.UserId = command.UserId;
        this.Income = command.Income;
        this.CreatedDate= DateTimeOffset.UtcNow;
    }

    public UserIncome Update(UpdateUserIncomeCommand command)
    {
        this.Income = command.Income;

        return this;
    }
}