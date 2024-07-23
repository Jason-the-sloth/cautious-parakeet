# cautious-parakeet
## Prerequisites:

- Unity Editor (download from https://unity.com/download)
- Preferred C# IDE (Visual Studio recommended https://visualstudio.microsoft.com/downloads/)
## Steps:

### Install Unity:

- Download and install Unity from the official website.
- Install C# IDE:

  - Install your preferred C# development environment. Visual Studio is a popular choice.

### Open Unity Hub and Project:

- Open Unity Hub.
  - Click ```Add``` and select the cautious-parakket project folder from your local machine.

### Configure Prefabs (Assets > Prefabs):

- Open the ```Assets``` folder in the Project panel.

- Navigate to the ```Prefabs``` folder.

- Player Prefab:

  - Open the ```Player Prefab```.
  - If the ```Bot Script``` component is missing, click ```Add Component``` and search for ```Bot Script```. Attach it to the Player Prefab.
    
- Bullet Prefab:

  - Open the ```Bullet Prefab```.
  - If the ```Bullet Script``` component is missing, follow the same steps as above to add it.

- Stats Prefab:

  - Open the ```Stats Prefab```.
  - If the ```Bot Stats Script``` component is missing, repeat the process to add it.

### Set Up Global Variables (Assets > Resources > GlobalVariables):

- Player Prefab:

  - Next to the ```Bullet Field```, click the ```+``` icon and select the ```Bullet Prefab``` from the list.
  - Repeat the same process for the ```Stats Field```, selecting the ```Stats Prefab```.
- Player Set Up:

  - Next to the ```Player Field```, click the ```+``` icon and select the ```Player Prefab``` from the list.
  - The ```P1``` and ```P2``` fields are where the bots will spawn. You can configure their positions here.

### Configure Scenes (Assets > Scenes):

- Home Scene:

  - Open the ```Home Scene```.
  - Click on the ```Canvas``` object.
  - If the ```Home Scene Script``` component is missing, click ```Add Component``` and search for it. Attach it to the Canvas.

- Training Mode Scene:

  - Open the ```Training Mode Scene```.
  - Click on the ```Canvas``` object.
  - If the ```Training Mode Script``` component is missing, follow the steps above to add it.

- Base Scene:

  - Open the ```Base Scene```.
  - Click on the ```Board``` object.
  - If the ```BoardSetUpScript``` component is missing, add it as described earlier.
  - Click on the ```Players``` object.
  - If the ```PlayerSetUpScript``` component is missing, repeat the process to attach it.

### Running the Game:

  - Once you've completed these setup steps, you should be able to run the game within the Unity Editor.
  - Click the ```Play``` button in the Editor to launch the game.
