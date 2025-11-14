using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace com.split.backend.HouseholdMembers.Domain.Models.Aggregates;

public partial class HouseholdMember
{
    [Column("Id")]
    [Required]
    public int Id { get; set; }
    
    [Column("HouseholdId")]
    [Required]
    public string HouseholdId { get; set; }
    
    [Column("UserId")]
    [Required]
    public int UserId { get; set; }
    
    [Column("IsRepresentative")]
    [Required]
    public bool IsRepresentative { get; set; }
    
    [Column("JoinedAt")]
    [Required]
    public DateTime JoinedAt { get; set; }

    public HouseholdMember()
    {
        this.HouseholdId = String.Empty;
        this.UserId = -1;
        this.IsRepresentative = false;
        this.JoinedAt = DateTime.UtcNow;
    }

    public HouseholdMember(string householdId, int userId, bool isRepresentative)
    {
        this.HouseholdId = householdId;
        this.UserId = userId;
        this.IsRepresentative = isRepresentative;
        this.JoinedAt = DateTime.UtcNow;
    }

    public HouseholdMember PromoteToRepresentative()
    {
        this.IsRepresentative = true;
        return this;
    }

    public HouseholdMember DemoteRepresentative()
    {
        this.IsRepresentative = false;
        return this;
    }
}

