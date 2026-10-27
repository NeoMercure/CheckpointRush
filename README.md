# CheckpointRush
A small top-down racing game where you race against the clock to reach checkpoints.

# How to Play
- Use WASD to drive the vehicle (controls can be changed in the Unity Inspector).
- Drive through the five checkpoints in the correct order.
- The next checkpoint is highlighted with a particle effect.
- Reach all five checkpoints before the timer runs out to win.
- If the timer reaches zero, the game ends.
- Click the Restart button to start a new run.

#Features
- Top-down vehicle movement.
- Five checkpoints to reach in the correct order.
- Particle effects to highlight the next checkpoint.
- Countdown timer.
- UI displaying the timer and checkpoint progress.
- Win and lose messages.
- Restart functionality.
- Adjustable vehicle speed and starting timer through the Inspector.
  
# Known Issues
- The vehicle can sometimes pass through obstacles or invisible walls.
- Particle effects do not always stop immediately when a checkpoint is reached; existing particles may remain visible briefly.

# Unfinished Features
- Random checkpoint placement was started but not completed due to time constraints. Checkpoints are currently placed manually in the scene.
  
# Design Decision
Separating game management from checkpoint behaviour
I chose to separate the main game logic into a GameManager and individual Checkpoint components.
The GameManager handles the timer, checkpoint progression, and win/loss conditions, while each checkpoint detects when the player reaches it and manages its particle effects.
This separation makes the code easier to understand and keeps each script focused on a specific responsibility.

# External Resources and AI Tools
Unity Documentation
- Input: https://docs.unity.com/en-us/engine/6000.6/manual/input/legacy/class-input-manager
- Object.Instantiate: https://docs.unity.com/en-us/engine/6000.6/script-reference/unityengine/object/instantiate
- Object.FindAnyObjectByType: https://docs.unity.com/en-us/engine/6000.6/script-reference/unityengine/object/findanyobjectbytype
- ParticleSystem: https://docs.unity.com/en-us/engine/6000.6/script-reference/unityengine/particlesystem
AI Tool
- used for debugging assistance / understanding Unity errors
