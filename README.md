# Medieval-Hunter

A simple survival-action game where your only goal is to survive and crush enemies to earn points and unlock talents.

## Description

Medieval-Hunter is a raw survival-action game created using the Unity game engine. In this game, your primary objective is to survive in a challenging medieval world filled with enemies. As you crush your foes, you'll earn points, which can be used to unlock various talents and reduce cooldowns, making your character more powerful and enhancing your chances of survival.

## Features

- Engaging and challenging gameplay set in a medieval world.
- Crush enemies to earn points and unlock new talents.
- Reduce cooldowns and enhance your character's abilities.

## Getting Started

### Prerequisites

Before you begin, ensure you have the following prerequisites:

- [Unity Game Engine](https://unity.com/) - Download and install Unity.

### Installation

1. Clone the repository to your local machine.
   
   ```bash
   git clone https://github.com/ozgundogann/Medieval-Hunter

2. Open the project in Unity.

## What do my script files do?
In this section, I will make explanations about my script files.

### Enemy
`Start():` This function is called when the object is initialized. It retrieves the player's location and features using the GetPlayerFeatures() method.

`OnEnable():` This method is called when the object is enabled or reactivated. It resets the enemy's features, including its speed, health, collision status, and visibility.

`FixedUpdate():` This is called at a fixed time interval and handles the enemy's movement. It makes the enemy move towards the player's location while maintaining a specific speed.

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
### HealthHandler
### Navigator
### ObjectPool
### PlayerCollisionHandler
### PlayerMovement
### PointHandler
### ResetEnemyFeatures
### ScoreBoard
