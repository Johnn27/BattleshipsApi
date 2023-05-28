using System.ComponentModel.DataAnnotations.Schema;

public class Battleship : Ship
{
    public int Id { get; set; }
    //public int PlayerId { get; set;} //Owner of battleship TODO: Future feature
    public bool IsVertical { get; set; }
    public int xLoc { get; set;}
    public int yLoc { get; set;}

    [ForeignKey("Board")]
    public int BoardId { get; set; }
    public Board? Board {get; set;}

    
}