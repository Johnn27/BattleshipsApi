public class BattleshipController {

    public BattleshipController (){
    }

    /// <summary>
    /// Check if the given coordinate will intersect with any of the listed ships
    /// Overloaded method, checks all ships on board if list of ships is passed in parameter
    /// </summary>
    /// <returns>
    /// True if a spot is already occupied by any ship on the board
    /// </returns>
    public bool CoordinateContainsShip(int newXLoc, int newYLoc, List<Battleship> battleships)
    {
        foreach(Battleship existingShip in battleships)
        {
            // Check if the individual ship is contained in the x and y location
            if(CoordinateContainsShip(newXLoc, newYLoc, existingShip)){
                return true;
            }
        }
        return false; // No overlap, co-ordinate does not contain ship
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
            else { //increment xLoc as it's horizontal
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
    public bool ShipAlreadyExists(Battleship newShip, ICollection<Battleship> battleShips)
    {
        int newXLoc = newShip.xLoc;
        int newYLoc = newShip.yLoc;
        foreach(Battleship existingShip in battleShips)
        {
            //Check all "tiles" the new ship will occupy and check for overlap
            for(int i = 0; i < newShip.ShipLength; i++){ //go through n size of ship
                if(newShip.IsVertical){
                    // Increment yLoc of new ship since it's vertical
                    // CoordinateContainsShip will check all tiles of the existing ship so both new ship and existing ship will not overlap
                    if(CoordinateContainsShip(newXLoc, newYLoc+i, existingShip)){
                        return true;
                    }
                }
                else {
                    // Increment xLoc of new ship since it's horiztonal
                    if(CoordinateContainsShip(newXLoc+i, newYLoc, existingShip)){
                        return true;
                    }
                }
            }
        }
        return false;
    }

    // Validate if location is non-zero
    public bool LocationOutOfRange(int xLoc, int yLoc){
        return xLoc < 0 || yLoc < 0;
    }

    /// <summary>
    /// Check if ship x or y location is out of board length (board is assumed as square)
    /// Goes through each "tile" the ship occupies (along the ship length) to check out of bounds
    /// </summary>
    public bool ShipOutOfRange(Battleship ship, Board board){
        if(LocationOutOfRange(ship.xLoc, ship.yLoc) || ship.xLoc >= board.BoardLength || ship.yLoc >= board.BoardLength){
            return true;
        }
        for(int i = 1; i < ship.ShipLength; i++){ //go through each other "tile" the ship occupies
            if(ship.IsVertical){
                // Increment yLoc of ship since it's vertical
                if(ship.yLoc + i >= board.BoardLength){
                    return true;
                }
            }
            else {
                // Increment xLoc of ship since it's horiztonal
                if(ship.xLoc + i >= board.BoardLength){
                    return true;
                }
            }
        }
        return false;
    }


}