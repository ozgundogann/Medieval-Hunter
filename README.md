# Medieval-Hunter

A simple survival-action game where your only goal is to survive and crush enemies to earn points and unlock talents.

## Description

Medieval-Hunter is a raw survival-action game created using the Unity game engine. In this game, your primary objective is to survive in a challenging medieval world filled with enemies. As you crush your foes, you'll earn points, which can be used to unlock various talents and reduce cooldowns, making your character more powerful and enhancing your chances of survival.

## Features

- Engaging and challenging gameplay set in a medieval world.
- Crush enemies to earn points and unlock new talents.
- Reduce cooldowns and enhance your character's abilities.

## How to Run

### Prerequisites

Before you begin, ensure you have the following prerequisites:

- [Unity Game Engine](https://unity.com/) - Download and install Unity.

### Installation

1. Clone the repository to your local machine.
   
   ```bash
   git clone https://github.com/ozgundogann/Medieval-Hunter

2. Open the project in Unity.

### In Unity
1. Locate Assets/GameFiles/Scenes
2. Double click SampleScene
3. Hit play button ![image](https://github.com/ozgundogann/Medieval-Hunter/assets/63861431/7f900cd2-a235-464c-b9d7-ac34d1c32210)

## What do my script files do?
In this section, I will make explanations about my script files. You can examine my script files by following these steps
   1. Locate Assets/GameFiles
   2. In GameFiles write
      ```bash
      t:Script
   to search bar.<br></br>
   ![image](https://github.com/ozgundogann/Medieval-Hunter/assets/63861431/e3030f17-5df5-4565-97b5-1e3ad9919a7a)




### Enemy

`GetPlayerFeatures():` This function finds the player object in the scene by name ("PlayerPlaceholder") and stores its transform in the playerTransform variable.

`ResetEnemyFeatures():` This method is responsible for resetting the enemy's features after it dies. It restores the enemy's speed, health, resets collision status, and makes the enemy visible.

`EnemyMovement():` This function calculates the player's position and adjusts the enemy's position and rotation to look at the player. It then moves the enemy towards the player's location at the specified speed.

### EnemyCollisionHandler

`GetVariousComponents():` This method is responsible for finding and storing references to various components such as player movement, the score board, enemy script, rigidbody, and renderer.

`OnCollisionEnter(Collision collision):` This function is called when a collision occurs with the enemy. It checks if the collision is with the player and if the enemy has not entered before. It then decreases the player's speed and resets the enemy's kinematics.

`ResetKinematics():` This function resets the enemy's kinematics to handle physics applied to the enemy from other entities upon collision.

`OnCollisionExit(Collision collision):` This method is called when a collision with the player ends, and it increases the player's speed.

`OnParticleCollision(GameObject other):` This function is invoked when the enemy is hit by particles and calls ProcessHit().

`OnTriggerEnter(Collider other):` This method is called when a trigger collider is entered, specifically when the player's weapon collides with the enemy. It checks if the player is attacking and if the enemy has not entered before calling ProcessHit().

`ProcessHit():` This function processes the hit on the enemy, decreasing its health, playing animations, and adding points to the score. It also manages the enemy's state and timing for various actions.

`PlayParticles():` This method hides the enemy renderer and triggers the particle system to play when the enemy is defeated.

`DisableEnemy():` This function deactivates the enemy game object when it is defeated.

`SetHasEnteredFalse():` This function resets the HasEntered flag to false after the enemy has been hit.

`DecreasePlayerSpeed():` This function decreases the player's speed and temporarily stops the enemy when the player collides with the enemy.

`StopEnemy():` This method stops the enemy's movement by setting its speed to 0 for a brief moment.

`MoveEnemy():` This function resumes the enemy's movement by setting its speed back to the maximum speed after a short delay.

### FireScript
`OnEnable():` Adds listener to listen button inputs and sets fireButtonObject active. 

`OnEnable():` Removes listener and sets fireButtonObject deactive.

`Fire()`: Plays the particle system if it's not already playing, creating a fire effect.

### HealthHandler
`OnDisable()`: Schedules the `EnableObject()` function to be called after a delay specified by `healthCooldown` when the script is disabled.

`EnableObject()`: Re-enables the game object, making it active again.

### Navigator
`FixedUpdate():` This function is responsible for positioning the object at a fixed height and navigating towards the closest health pack using the Navigate() function.

`Transform FindCloser():` This function finds the closest health pack among the ones stored in the healthPacks array. It iterates through the health packs, calculates the distance between each health pack and the player's position, and returns the transform of the closest health pack.

`void Navigate(Transform target):` This function determines the direction to rotate towards the target (the closest health pack) and smoothly rotates the object in that direction based on the specified speed. It ensures that the object faces the target.

### ObjectPool
`Awake():` It populates a pool of enemy objects, finds the player, and sets up initial parameters for enemy spawning.

`Start():` This function is called at the start of the object's lifecycle and starts a coroutine to repeatedly spawn enemies at a specific rate.

`PopulatePool():` This function populates a pool of enemy objects. It creates instances of enemy prefabs and deactivates them to reuse later.

`IEnumerator SpawnEnemy():` This coroutine repeatedly relocates and enables enemy objects at specified intervals, creating a continuous spawning effect.

`RelocateEnemy():` This function is responsible for relocating and enabling an enemy by calling SetRandomPosition() and EnableEnemy().

`SetRandomPosition():` This function randomly determines a spawn point for enemies based on the player's position, defining positions above, below, left, and right of the player.

`EnableEnemy():` This function activates an available enemy object from the pool at the specified spawn point. It also resets certain features of the enemy using a script called ResetEnemyFeatures.

### PlayerCollisionHandler
`Start():` It initializes references to various game objects and scripts, including the UI, player movement, and a point handler.

`Update():` It checks if the player is out of the designated playing area and reduces the player's health if they are.

`bool IsPlayerOutOfPlayground():` This function determines if the player is outside the specified playing area and triggers actions if the player is out of bounds.

`OnCollisionEnter(Collision collision):` This function handles collisions with enemy objects and reduces the player's health based on the type of enemy (small or large).

`PlayerGetsDamage(int damageMultiplier):` This function decreases the player's health and manages UI elements when the player takes damage.

`ReloadScene():` This function reloads the current scene when the player's health reaches zero.

`OnTriggerEnter(Collider other):` This function handles trigger collisions, specifically with objects tagged as "Health."

`HealthHandler(Collider other):` This function processes interactions with health pickups, updating the player's health and hiding the collected health object.

`ShowCurrentHealth():` This function updates the UI to display the player's current health value.

### PlayerMovement
`Start():` This function sets up the attack button listener, retrieves references to the button text, and the player's Rigidbody.

`FixedUpdate():` This function handles player movement, rotation, and attack cooldown counting.

`OnDisable():` This function removes the attack button listener when the script is disabled.

`MovePlayer():` Responsible for moving the player character based on joystick input.

`RotatePlayer():` Handles player rotation based on joystick input.

`CooldownCounter():` Manages the attack cooldown, displaying the cooldown time on the button text.

`Attack():` Handles player attacks, triggering animations and managing cooldown.

`SetIsAttakingFalse():` A helper function for use with Invoke to reset the isAttacking flag.

`DecreaseSpeed():` Decreases the player's speed.

`IncreaseSpeed():` Restores the player's speed to the maximum value.

`AutoAttackCaller():` Initiates auto-attacks when the player is in the "isDead" state.

`AutoAttack():` Performs automatic attacks and manages cooldown, similar to the manual attack function.

### PointHandler
`Start():` This function initializes references to the player's movement script, the fire script, and disables the fire script initially.

`SetNewReward(int point):` This method handles the awarding of various rewards to the player based on their performance. It activates status messages and applies rewards such as enabling auto-attack, increasing weapon size, and modifying attack time.

`ReturnPlaygroundMessage():` This function displays a "Return to Playground" message when the player leaves the designated play area.

`DisableMessage():` Disables the status message, hiding it from the screen after a specified duration.

### ResetEnemyFeatures
`Start():` This function initializes the childObjectScale variable to store the initial scale of the child object.

`ResetChildObject():` This method is used to reset the scale of a child object. It skips first iteration of loop of `EnableEnemy()` function in ObjectPool script and resets the child object's scale to its initial value stored in childObjectScale.

### ScoreBoard
`Start():` It initializes the score text to display the initial scoreCount value and finds and references the PointHandler script. In the future points will be saved to player's device.

`AddPoints(int point):` This method is used to add points to the player's score. It updates the scoreCount, updates the text to display the new score, and checks if the score is a multiple of 20 to trigger the SetNewReward function in the PointHandler script.
