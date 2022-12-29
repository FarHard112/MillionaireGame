using System.ComponentModel.DataAnnotations.Schema;

namespace WhoWantsToBeAMillionaireGame.DataBase.Entities;

public class Prize:IBaseEntity
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public bool isEnable { get; set; }
    public Guid ColorId { get; set; }
  
}