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

### Set-up environment
- User Scenario Editing+ 
  - Able to add new obstacle/s during the setup

https://user-images.githubusercontent.com/76828992/193470396-2e9ae774-80c1-4ba4-94e1-72ee220617dc.mp4

### During gameplay
- Realtime User Interaction
  - Able to move existing obstacle during the gameplay

https://user-images.githubusercontent.com/76828992/193470420-02f74ef1-e133-451f-84b4-e0266ac65118.mp4

### NPC Navigating/Movement through environment
- Single Agent Navigation
  - NPC moves through the environment
- Multiple Agents Planning+
  - Multiple NPCs move through the environment
- Improved Agent & Scene Rendering
  - NPCs shapes/geometry are custom created by myself
- Orientation Smoothing
  - NPCs turns smoothly when it reaches to the nodes

https://user-images.githubusercontent.com/76828992/193471370-13a8cd43-1eac-43ca-93eb-8121d3aeae0e.mp4

### User moving around
- 3D Rendering & Camera 
  - Able to look around using mouse

https://user-images.githubusercontent.com/76828992/193470424-62c4aef8-8694-4343-9d46-2f4732240ee3.mp4

### Eagle view vs Ground view

https://user-images.githubusercontent.com/76828992/193470429-3be2277b-a139-4d9d-ada2-d1cf84ab635f.mp4

### Weather
- Incorporate Particle System
  - Snows fall down onto the ground and once it reaches, it disappear

https://user-images.githubusercontent.com/76828992/193471994-3a5c096c-0de1-499c-95e7-5fa32de6498e.mp4

### Collision-Free
- Crowd Simulation
  - Multiple NPCs move through the environment without colliding

https://user-images.githubusercontent.com/76828992/193473043-40819f14-5622-44a7-aabc-b313aabf3bf0.mp4

## Encountered challenges
- Collision free
  - For some reason, while translating Java TTC codes into C#, somehow I mistranslated some variables and its positions. So it took me quite a while to figure out why the collision is still happening even though the implementation is seems to be correct.
- Bound/Collider for assets
  - Some of the objects has specific geometry shape which make it hard to translate its bound into numbers.

## Assets
### Owned \[Created by Akar (Ace)]
Robot 1 (Floats in the air)

https://user-images.githubusercontent.com/76828992/193470442-27e3ed6f-59b1-4483-afd5-ff8e3f694e5d.mp4

Robot 2 

![image](https://media.github.umn.edu/user/17933/files/d8332845-7ac1-4df1-8d1c-7535943f126f)

### Outside resources (Credits)
#### Model
- Buildings: <br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;@Author: Szymon Lukaszuk<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;@Link: https://assetstore.unity.com/packages/3d/environments/historic/lowpoly-medieval-buildings-58289

#### Texture
- Ground: <br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;@Author: Casual2D<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;@Link: https://assetstore.unity.com/packages/2d/textures-materials/nature/snow-cliff-materials-137086

- Sky: <br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Unity built-in

#### Library
- Laser ray: <br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Unity built-in [raycast](https://docs.unity3d.com/ScriptReference/Physics.Raycast.html) and [line renderer](https://docs.unity3d.com/Manual/class-LineRenderer.html) library
