# BattleshipAPI 

Simple dotnet core state tracking API for the battleship game, uses Restful API to operate basic 
single player operations such as place a battleship, create game board and fire at a battleship.
The API checks the game state and returns Error if actions are invalid else 200 success.
The API can be interacted by the Swagger UI to complete the actions. Contains unit tests.
This was developed through visual studio code

## Installation

Clone or download the repo, 
Open the {repo}/BattleshipApi folder in the IDE (developed in VS Code).
Change Directory to {repo}/BattleshipApi/BattleshipApi folder (cd ./BattleshipApi)
    - Alternatively open the solution folder in {repo}/BattleshipApi/BattleshipApi/BattleshipApi.solution
Run "dotnet build" in {repo}/BattleshipApi/BattleshipApi folder

```bash
pip install foobar
```

## Usage

Run "dotnet build" in {repo}/BattleshipApi/BattleshipApi folder
Run "dotnet test" to run unit tests

In UseSwaggerUI (Call the Restful API via [Try it Out] button and clicking execute)
1) Create Board by calling "/CreateBoard" HTTP Post (Click on [Try it Out] button)
2) Place Battleships by calling the "PlaceBattleship" HTTP Post Request
    i) Enter a valid x Location, by default board is 10x10 and a valid input is between 0-9
    ii)  Enter a valid y Location, by default board is 10x10 and a valid input is between 0-9
    iii) Enter length of ship, the ship is of shape 1xn where n is length of ship
    iv) Enter orientation of ship, the ship can be facing vertically or horizontally
3) Fire at Battleship by calling "/FireBattleship" HTTP Post Request
    i) Enter valid x Location
    ii) Enter valid y Locaition
    iii) The response of the Post request will determine if the hit was successful
    