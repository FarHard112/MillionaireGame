namespace WhoWantsToBeAMillionaireGame.DataBase.Entities;

public class ColorPrize:IBaseEntity
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public string ColorValue { get; set; }
}