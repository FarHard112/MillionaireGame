namespace WhoWantsToBeAMillionaireGame.DataBase.Entities;

public class SocialMediaLink:IBaseEntity
{
    public Guid Id { get; set; }
    public string? FacebookUrl { get; set; }
    public string? TikTokUrl { get; set; }
    public string? InstagramUrl { get; set; }
    
}