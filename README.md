# Asteroids Code Design


# Code Architecture
Thinking about how the game should behave; I can see some shared behaviours and thus, some shared code, for example, all the objects in the game move in a similar way (linearly in a zero gravity space). All moving objects also obey the same screen area rules, that is, when an object leaves the screen from the right side it re-enters from the left. 

To Achieve these shared behaviours, I decided to use composition over inheritance to maintain maximum flexibility. For example an Enemy Ship will be composed as a combination of an IMovable and IPathFollower objects.

# Logic-View Separation
To Achieve separation of concerns and to make more of the codebase testable, I decided to implement the Humble Object Patten, that is, moving all of the logic and data outside of the monobehaviour (turning it into a humble object). In fact, the plan is to make monobehaviours’ only responsibility is viewing (input/output/unity-events).

# What’s Next
Now after the main features of the game are ready. The next step is to improve performance. One of the obvious areas to increase performance is to implement object pooling for bullets, asteroids and enemy ships instead of destroying and recreating new objects.

# Few useful links:

https://en.wikipedia.org/wiki/Composition_over_inheritance#:~:text=Composition%20over%20inheritance%20(or%20composite,than%20inheritance%20from%20a%20base

https://martinfowler.com/bliki/HumbleObject.html

https://www.raywenderlich.com/847-object-pooling-in-unity
