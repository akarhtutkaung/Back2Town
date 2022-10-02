# Back2Town

CSCI 5611 Project 1

@Author: Akar (Ace) Kaung

Software: Unity

Language: C#

## Summary
This gameplay implements the Dijkstra pathfinding algorithm to find the path for the agents to move toward their goals. 
The goal of the agents will be determined randomly toward any of the existing building IDs inside the landscape.
Once the agent arrived at its destination, it will choose a new goal randomly again to always keep moving.
User will be able to observe the environment freely, meaning it will be able to pass and go inside the buidlings instead of colliding with it.

## Usage

Press play button in Unity to get started. This will lead to set-up environment. <br>

During the set-up environment:
  - Press mouse `left-click` button to generate new obstacle.
  - While pressing `left-click` button, use the `scrolling wheel` to move the obstacle near or further away.
  - Release `lef-click` button to place the obstacle.
  - Press `"p"` key to start the gameplay.

During gameplay:
  - Press mouse `left-click` button to generate laser pointer
  - While pressing `left-click` button, move the mouse around to see the movable modles/obstacle 
    - movable: Blue
    - non-movable: Red
  - Press `"space-bar"`key to select the obstacle and to move around the obstacle, use mouse to move while pressing on space-bar key
  - Release `"space-bar"`key to place the obstacle

Anytime:
  - Press `v` key to change the view format (eagle view and ground view) 

## Gameplay

### Set-up environment (creating new obstacle and moving it around)

https://user-images.githubusercontent.com/76828992/193470396-2e9ae774-80c1-4ba4-94e1-72ee220617dc.mp4

### During gameplay (moving an existing obstacle)

https://user-images.githubusercontent.com/76828992/193470420-02f74ef1-e133-451f-84b4-e0266ac65118.mp4

### Moving around

https://user-images.githubusercontent.com/76828992/193470424-62c4aef8-8694-4343-9d46-2f4732240ee3.mp4

### Eagle view vs Ground view


https://user-images.githubusercontent.com/76828992/193470429-3be2277b-a139-4d9d-ada2-d1cf84ab635f.mp4

## Assets
### Owned \[Created by Akar (Ace)]
Robot 1 (Floats in the air)

https://user-images.githubusercontent.com/76828992/193470442-27e3ed6f-59b1-4483-afd5-ff8e3f694e5d.mp4

Robot 2 

![image](https://media.github.umn.edu/user/17933/files/d8332845-7ac1-4df1-8d1c-7535943f126f)

### Credits
Buildings: <br>
  @Author: Szymon Lukaszuk<br>
  @Link: https://assetstore.unity.com/packages/3d/environments/historic/lowpoly-medieval-buildings-58289
  
Ground Texture: <br>
  @Author: Casual2D<br>
  @Link: https://assetstore.unity.com/packages/2d/textures-materials/nature/snow-cliff-materials-137086
