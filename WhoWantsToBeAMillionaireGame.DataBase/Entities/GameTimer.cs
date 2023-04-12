namespace WhoWantsToBeAMillionaireGame.DataBase.Entities;

public class GameTimer:IBaseEntity
{
    public Guid Id { get; set; }
    public int? Duration { get; set; }

}