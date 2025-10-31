using System.ComponentModel.DataAnnotations.Schema;

namespace com.split.backend.Settings.Domain.Models.Aggregates;

public partial class Settings
{
    [Column("Id")]
    public string Id { get; set; }
    [Column("UserId")]
    public long UserId { get; set; }
    [Column("Language")]
    public string Language { get; set; }
    [Column("DarkMode")]
    public bool DarkMode { get; set; }
    [Column("NotificationEnabled")]
    public bool NotificationEnabled { get; set; }

    public Settings()
    {
        this.Id = String.Empty;
        this.UserId = 0;
        this.Language = String.Empty;
        this.DarkMode = false;
        this.NotificationEnabled = false;
    }
    public Settings(string id, long userId, string language, bool darkMode, bool notificationEnabled)
    {
        this.Id = "ST" + DateTime.Now.Ticks;
        this.UserId = userId;
        this.Language = language;
        this.DarkMode = darkMode;
        this.NotificationEnabled = notificationEnabled;
    }
}