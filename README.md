## Maze Game
* Algorithm: to create the maze, I chose the Aldous-Broder algorithm.
* Randomness: Everytime the game is entered, the game solution is randomly generated, as well as the non-solution paths. The randomness mainly lies in the direction of the search being a random number between 0 and 3, indicating four possible directions to search next. Also, the boxes are generated randomly onto the game map.
* Dynamic: As the time state changes, we show different portions of the correct path, and the game is remapped everything the time state changes. In regards to the time variable, (time+2) tiles of the solution path will be connected visible to the player, and (time+3) tiles of the solution path will be highlighted to guide the player. Other than the solution path, the other dead end paths are created every time the time variable changes.
* Tracking: First I used a random walk to create the solution path, and stored each step into a linked list. To retrieve the solution, I simply just get a portion of the linked list.
* Visuals: I chose pre-made assets mainly for the game's asthetic nature that I wanted wanted to make my game enjoyable visually even before the game became playable.
* List of assets used:
```console
https://assetstore.unity.com/packages/3d/environments/landscapes/polydesert-107196
https://assetstore.unity.com/packages/3d/props/low-poly-rpg-item-pack-76088
https://assetstore.unity.com/packages/3d/characters/low-poly-animated-people-109774
https://assetstore.unity.com/packages/3d/environments/low-poly-pack-94605
```
* Challenges: most of the challenges came from designing the collisions, how they should happen, and how each event should be triggered, which took some time to design. Also, building the 3-dimensional maze that changes with time also took some time to figure out the game logic.