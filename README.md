# Splatship Seven

Steer your fragile spacecraft through dangerous environments with traps hidden at every step! Your every failure contributes to your future attempts.

This game is my submission for the [Wowie Jam 3.0](https://itch.io/jam/wowie-jam-3 "Wowie Jam 3.0"). The jam\'s theme was **Failure is progress**.

Screenshots
-----------

![](/Screenshots/ss1.png)
![](/Screenshots/ss2.png)


Credits
-------

All audio samples taken from freesound.org and mixed by me. Original files can be found in the [Raw Audio](Assets/Raw%20Audio) folder. The sounds used in game are located in the [Audio](Assets/Audio) folder.
All other assets and code were made by me.


Interesting stuff
-----------------

The most interesting part of the code is probably the splatter effect. What\'s cool about them is that they are animated and stick to the dynamic surfaces (see gif below for an example):
![](/Screenshots/gif1.gif)

The animation itself is done using the following simple particle system:
![](/Screenshots/gif2.gif)
If you render this system without clearing the render buffer, you get this nice persistent splatter effect:
![](/Screenshots/gif3.gif)
Relying on not clearing the render target unfortunatelly has its problems (eg. the effect is heavily dependent on the framerate), but this method will be extremely handy once we'll try to deal with the dynamic environment.

To render the splatter in the static scene, we can just place particle effect on separate layer, have an additional camera rendering this layer to the separate render texture with `clearFlags` set to `Nothing`, then use the render texture as a texture for the objects in the scene (also, `SpriteRenderer` doesn\'t support render textures in Unity, so we need to use a custom shader for this :^) ).
For the dynamic scene, we can basically do the same thing, we just need to assign a seperate camera with a seperate render target for every dynamic object in the scene.

Everything related to splatter effect is located in the [SplatSurface.cs](Assets/Scripts/SplatSurface.cs). The script automatically creates render textures, cameras, etc., sets them up and assigns appropriate material to the `Renderer` of the object.