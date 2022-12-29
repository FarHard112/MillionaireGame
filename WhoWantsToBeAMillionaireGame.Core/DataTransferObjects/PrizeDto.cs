namespace WhoWantsToBeAMillionaireGame.Core.DataTransferObjects;

public class PrizeDto
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public bool isEnable { get; set; }
    public Guid ColorId { get; set; }
}