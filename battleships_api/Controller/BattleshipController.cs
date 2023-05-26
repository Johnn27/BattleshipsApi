public class BattleshipController {

    public BattleshipController (){
    }

    /// <summary>
    /// Check if the given coordinate will intersect with a ship
    /// Given a given x location and y location return if that given ship exists
    /// Used to check if a ship is "hit" or if the tile is already occupied
    /// </summary>
    /// <returns>
    /// True if a spot is already occupied by a ship
    /// </returns>
    public bool CoordinateContainsShip(int newXLoc, int newYLoc, Battleship existingShip)
    {
        //Go along the ship's length and check if the newXLoc and newYLoc intersects
        for(int i = 0; i < existingShip.ShipLength; i++){ //go through n size of ship
            if(existingShip.IsVertical){
                // increment yLoc as ship is vertical
                if(existingShip.xLoc == newXLoc && existingShip.yLoc + i == newYLoc){
                    return true;
                }
            }
            else{ //increment xLoc as it's horizontal
                if(existingShip.xLoc + i == newXLoc && existingShip.yLoc == newYLoc){
                    return true;
                }
            }
        }
        return false; // No overlap, co-ordinate does not contain ship
    }

    /// <summary>
    /// Verify if the new ship already exists in the board, 
    /// </summary>
    /// <returns>
    /// True if a spot is already occupied by a ship
    /// </returns>
    public bool ShipAlreadyExists(Battleship newShip, List<Battleship> battleShips)
    {
        int newXLoc = newShip.xLoc;
        int newYLoc = newShip.yLoc;
        foreach(Battleship existingShip in battleShips)
        {
            //Check all tiles the new ship will occupy and check for overlap
            for(int i = 0; i < newShip.ShipLength; i++){ //go through n size of ship
                if(newShip.IsVertical){
                    // Increment yLoc of new ship since it's vertical
                    // CoordinateContainsShip will check all tiles of the existing ship so both new ship and existing ship will not overlap
                    if(CoordinateContainsShip(newXLoc, newYLoc+i, existingShip)){
                        return true;
                    }
                }
                else
                {
                    // Increment yLoc of new ship since it's horiztonal
                    if(CoordinateContainsShip(newXLoc+i, newYLoc, existingShip)){
                        return true;
                    }
                }
            }
        }
        return false;
    }

    /// <summary>
    /// Check if ship x or y location is out of board length (board is assumed as square) 
    /// </summary>
    public bool ShipOutOfRange(Battleship ship, Board board){
        return ship.xLoc >= board.BoardLength || ship.yLoc >= board.BoardLength;
    }


}