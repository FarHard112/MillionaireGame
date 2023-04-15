using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WhoWantsToBeAMillionaireGame.DataBase.Entities;

public class Logs
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Message { get; set; }
    public string MessageTemplate { get; set; }
    public string Level { get; set; }
    public DateTimeOffset TimeStamp { get; set; }
    public string Exception { get; set; }
    public string Properties { get; set; }
}