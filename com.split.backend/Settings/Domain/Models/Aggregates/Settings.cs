using System.ComponentModel.DataAnnotations.Schema;
using com.split.backend.Shared.Domain.Models.Aggregates;

namespace com.split.backend.Settings.Domain.Models.Aggregates;

public partial class Settings
{
    [Column("Id")]
    private string Id { get; set; }
    [Column("UserId")]
    private long UserId { get; set; }
    [Column("Language")]
    private string Language { get; set; }
    [Column("DarkMode")]
    private bool DarkMode { get; set; }
    
    [Column("NotificationEnabled")]
    private bool NotificationEnabled { get; set; }

    public Settings(string id, long userId, string language, bool darkMode, bool notificationEnabled)
    {
        this.Id = id;
        this.UserId = userId;
        this.Language = language;
        this.DarkMode = darkMode;
        this.NotificationEnabled = notificationEnabled;
    }
}