# ObjectPoolForUnity
Simple and flexible implementation of Object pool pattern for Unity.

## Using
1. Create empty object an add ObjectPool.cs to it. 
2. Get your prefab you'd like to be used with the pool, and add to it DefaultPoolableBehaviour.cs and set lifetime for this object.
3. Assign this prefab to Object Pool's Prefabs in the inspector.
You may add more prefabs, if you'd like to make your pool more randomly.
4. Pool capacity min - is the count of objects, that will be instantiated on startup. In the case if all pool objects are in using, new ones will be instantiated, but the total number of objects won't be more than Pool capacity max.
5. Call ObjectPool.UseObjectAt(Vector3 pos) to use the object on required position.

## Additional info
If you want to make extra actions with an object from pool, you always may inherit a new class from DefaulPoolableBehavior and override OnUseActions() и OnBeforeInterruptActions(), OnUseActions() called right before object will be used, and OnBeforeInterrupt() called right before lifetime of object will be ended.

DefaultPoolableObject may be freed by call its method Interrupt(), if you don't want to wait when objects lifetime ended. **NOTE:** in this case OnBeforeInterruptActions() won't be called.

## License 

There is used https://gist.github.com/frarees/9791517 expansion for Unity inspector made by Francisco Requena (default gist license). But anyway it's not required for Object Pool, it just makes using of it more friendly, so you may remove it.

Speaking of object pool: use it as you wish.
