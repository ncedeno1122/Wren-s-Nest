# Mining Party Game

## Players
Can do 2 to 4 players or *maybe* more. At least 2.

## Mechanics

### The Board / Mines
Players will play the game on the board, which aesthetically is The Mines.
Proc-Gen'd Board?

#### Mine Tiles
Mine Tiles can be mined, which may yield gems of varying values. When mined, the gems will be added to the player's inventory.

##### Mining
Tiles can be mined up to 3 times.
Each time, there is a pre-baked chance that the player will find a gem at that level.
The third time a tile is mined (and cannot be anymore), there is a pre-baked chance that that tile will uncover a tunnel to another tile.
- Does the tunnel's target location open upon finding a tunnel? Or do you need to mine it?

#### Treasures

##### Gems
Gems have varying values on their own (flat rates), but may have their values augmented by sales at the gem market / appraiser.
Gems have different types and corresponding values:
- Diamond: 20g
- Ruby: 15g
- Emerald: 12.5g
- Sapphire: 7.5g

Naturally, each gem type has its own droptable.
- I may want to do this by factors instead of top-decking values...

| Gem Type | Drop Chance |
| -------- | ----------- |
| Diamond  |    0.05%    |
| Ruby     |    0.10%    |
| Emerald  |    0.15%    |
| Sapphire |    0.35%    |


Every 4 gems will cost a point of movement until 1 point remains.

##### Relics
Relics have a flat price, but are heavy; every relic will cost a point of movement until 1 point remains.

### Player Mechanics

#### Inventory
The player can hold up to 3 gems of each type, but will be *weighed down* when carrying too many.

#### Move
The player can move up to 5 spaces when not weighed down.

#### Mine
The player can use their turn's action to mine a Mine Tile if possible.


## Win Conditions

