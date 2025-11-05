using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
    


}