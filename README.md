# Asteroids DOTS

The design philosophy was to keep systems as simple as possible, having 5 major ones and some spawners.
Its using the Hybrid Renderer and SpriteRenderers to have a better edition experience, but not being the best since support is limited for 2D, probably 2D.Entities package was more suitable. Which ended up causing some issues but were happily solved with some wit.

*Physics + Input*

Since movement is very simple for all entities, all entities that move, have their position and rotation updated via the same system.
Input feeds into some of the physics values of the entities being controlled by the user, the system is designed to support multiple controlled units if a power up where to spawn multiple ships or something of sorts.
Input is run in main thread for better feel.

*Collisions*

Collisions are batched into the end simulation command buffer, behaviour is obtained via a factory class that depending on what collides with what returns the proper behaviour for the main entity.

*Aiming System*

With the player entity, calculating the direction and instantiating a bullet is fairly easy. The system schedules the aiming for all ufos in parallel.

*Bounds Wrapping System*

With data about the worldspace borders of the frostrum, depending on the entity setting it will move the entities as if the space is wrapped on itself or tag for destruction the entity crossing the border.

*Destruction System*

To avoid having too many sync points, or having a unit destroyed by 2 different systems, entities are tagged and the destruction is batched and run in end of simulation, after collision calculations.

*Spawners*

Spawners all batch their instancing of entities, and have a Required dependency with a data component, that holds the prefab to obtain the entitiy archetype in order to spawn them.
