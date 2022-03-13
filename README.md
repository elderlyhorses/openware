# What
- Wario Ware inspired open source minigames
- [Download on itch.io](https://aidanwaite.itch.io/openware)

# Minigames

### Alphabet
![Alphabet gameplay](https://media1.giphy.com/media/GoB7E91rYrQUtgB9MU/giphy.gif)

### Alphabetize
![Alphabetize gameplay](https://media3.giphy.com/media/ZgzuIFsFxU96E1UR51/giphy.gif)

### Awp
![Awp gameplay](https://media2.giphy.com/media/d1NyfVyGOA2SIbMx8T/giphy.gif)

### Bubble Pop
![Bubble Pop gameplay](https://media3.giphy.com/media/suZPWuZ991FdWASa5c/giphy.gif)

### Button Mash
![Button Mash gameplay](https://media1.giphy.com/media/SrnbMjMVKzOiflwo8I/giphy.gif)

### Capsta
![Capsta gameplay](https://media4.giphy.com/media/XU3VmcbFwakcjKzHKY/giphy.gif)

### Dash
![Dash gameplay](https://media2.giphy.com/media/Jw0RJ736CgWdGpAHOv/giphy.gif)

### Fally Bird
![Fally Bird gameplay](https://media0.giphy.com/media/Lv5GajdOHOnu5yYLp2/giphy.gif)

### Fast or Slow You Decide
![Fast or Slow You Decide gameplay](https://media3.giphy.com/media/O0hMEdvqJqwaxixUUw/giphy.gif)

### Field Goal
![Field Goal gameplay](https://media2.giphy.com/media/cGlESOtufsFdyzbxKU/giphy.gif)

### Graduation
![Graduation gameplay](https://media3.giphy.com/media/veQSOdt9qOWqihBgOU/giphy.gif)

### Jump Rope
![Jump Rope gameplay](https://media0.giphy.com/media/isF0OhuTTw1JkY6TDv/giphy.gif)

### Kart Parking
![Kart Parking gameplay](https://media1.giphy.com/media/s9dvvvTiqA4iaNiDSi/giphy.gif)

### Keepie Uppie
![Keepie Uppie gameplay](https://media1.giphy.com/media/GVLHl1wKwjXwnnYZjP/giphy.gif)

### Mouse Maze
![Mouse Maze gameplay](https://media2.giphy.com/media/3VX2t2H4KielbNEd3I/giphy.gif)

### Punch
![Punch gameplay](https://media1.giphy.com/media/yzQmLMNPkarRPCoizU/giphy.gif)

### Relax
![Relax gameplay](https://media1.giphy.com/media/GgSuMG7lhfasv66fM3/giphy.gif)

### Rhythm
![Rhythm gameplay](https://media4.giphy.com/media/UpxMVINchIC3Hfum1O/giphy.gif)

### Split
![Split](https://media0.giphy.com/media/uGuNQZIgQ21FF74TTs/giphy.gif)

### Texas Hold em
![Texas Hold em](https://media0.giphy.com/media/kghyugA00l0b71HRgU/giphy.gif)

# Vision
- A collection of simple minigames that are fun to make and fun to play
- Players should be able to learn and beat minigames in roughly 10 seconds
- Make sure the player can see the result of the minigame. Before calling a `MinigameCompletionHandler` callback, spend a few seconds clearly showing that the minigame has been won or lost.

# Adding a minigame
1. Install Unity 2019.4.12f1 LTS.
2. Fork the repo and clone your fork. You may need ssh auth setup for this to work.
3. Open project.
4. Set the `Game` resolution to 800x800.
5. Open the `SceneCoordinator` scene and hit play. This will give you an idea of everything fits together.
6. Create a new folder in Minigames.
7. Put your scene and all related files in your new folder.
8. Add your new scene to Build Settings.
9. Use a minigame-specific namespace for all of your scripts.
10. Make the minigame!
11. Check out another minigame scene and see how `Minigame Completion Handler` is used. You'll need to invoke the win and lose callbacks in the same way in your new minigame.
12. Open SceneCoordinator.cs and search for the comments `// Add new minigame here`
13. Make a pull request with your new minigame from your fork back to the original repo and tag `Aidan Waite` as a reviewer.

# Art and sound assets
Art and sound assets can only be used if we've got the appropriate license. For this reason, making original art is encouraged. If you source art or sound from somewhere please link it here after you check that the license permits its use. Note "original" here means it was created specifically for Openware and thus it uses Openware's license.

- Playing cards from [this Wikipedia article](https://en.wikipedia.org/wiki/Standard_52-card_deck) ([CC BY-SA 3.0](http://creativecommons.org/licenses/by-sa/3.0/))
- Casino felt from [davidgsteadman](https://flic.kr/p/ibmNwe)
- [Orbiting the Earth by UltraCat](https://freemusicarchive.org/music/UltraCat/Orbiting_the_Earth/ultracat_-_01_-_orbiting_the_earth)
