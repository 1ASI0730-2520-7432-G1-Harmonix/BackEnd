using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using com.split.backend.Households.Domain.Models.Commands;
using com.split.backend.Households.Domain.Models.ValueObjects;
using com.split.backend.IAM.Domain.Model.Aggregates;

namespace com.split.backend.Households.Domain.Models.Aggregates;

public partial class HouseHold
{
    [Column("Id")] 
    [Required]
    public string Id { get; set; }
    [Column("Name")]
    public string Name { get; set; }
    [Column("RepresentativeId")]
    [Required]
    public long RepresentativeId { get; set; }
    [Column("Currency")]
    [Required]
    public ECurrency? Currency { get; set; }

    public HouseHold()
    {
        this.Id = String.Empty;
        this.Name = String.Empty;
        this.RepresentativeId = -1;
        this.Currency = null;
    }

    public HouseHold(string name,
        long representativeId, string currency)
    {
        this.Id= "HOG" + DateTime.Now.ToString("yyyyMMddHHmmss");
        this.Name = name;
        this.RepresentativeId = representativeId;
        this.Currency = Enum.Parse<ECurrency>(currency);
    }

    public HouseHold(CreateHouseholdCommand command)
    {
        Id = "HOG" + DateTime.Now.ToString("yyyyMMddHHmmss");
        this.Name = command.Name;
        this.RepresentativeId = command.RepresentativeId;
        this.Currency = Enum.Parse<ECurrency>(command.Currency);
    }


    public HouseHold UpdateHouseHold(UpdateHouseHoldCommand command)
    {
        if (!string.IsNullOrWhiteSpace(command.Name)) this.Name = command.Name;
        if(command.RepresentativeId != 0) this.RepresentativeId = command.RepresentativeId;
        if (!string.IsNullOrWhiteSpace(command.Currency)) this.Currency = Enum.Parse<ECurrency>(command.Currency);
        
        return this;
    }
    


}