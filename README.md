# unity-harmonics
Harmonic Motion in Unity.

## Project Status
- This project is still in its early phase. I plan to make this project into a cloth engine to be used in games and for industrial purposes.

## Planned Features
- Higher-Order ODE Integrator (RK4) For more accurate Velocity, Position, and Acceleration calculations with more computation
- Lower-Order ODE Integrator (Euler) for less computation but less accuracy
- Collisions with geometric meshes
- Switching to double-precision mathematics for more precise positions with little to no performance loss
- Automatic spring-mass mesh generation from 3D Meshes

## Setup

1. Clone the Repo   

```
    git clone https://github.com/sandalconsumer/unity-harmonics.git
```
2. Open it in the Unity Hub

- Open Unity Hub
- Click on the **Add Project** button in the top right next to **New Project**.
- Navigate to the cloned folder and click **Open** in the bottom right corner.
- This project was made with Unity 6000.1.1f1 so that is where the code is guaranteed to work, but most Unity 6 versions will (most likely) work well.
- once you have opened Unity, Navigate to the scenes folder and open the **SampleScene** scene.

## Documentation  

There are 3 main classes you need to know to understand this repo. And they are:
1. Physics Controller
2. Simulation Data Compiler
3. Physics Objects

The **Physics Objects** script contains 2 classes: Spring and Pointmass.  
Physics Controller makes a list of Springs and Pointmasses which are then linked by the user (similar to vertices and edges).  
Simulation Data Compiler takes data from the **Physics Controller** and visualizes it. I am planning to make an option to export this data.  

### Physics Controller
- Visualize Springs toggles the visualization of springs in the unity scene view.
- Record Data toggles the sending of data from Physics Controller to Simulation Data Compiler.
  
#### Masses

Every element in masses is an individual instance of class **Pointmass**:
- Position is the starting position of the Pointmass. note that this is also used as the current position of the pointmass on runtime.  
- Mass is the mass of the pointmass in kilograms.  

#### Springs

Every element in springs is an individual instance of class **Spring**:
- Spring Constant is the variable `k` in Hooke's law `Fs = -kx` and it controls the "strength" of the spring
- Rest Length is the length of the spring `x` in Hooke's law `Fs = -kx` where if x = 0 there is no restorative force `Fs = 0`.
- Root linked index is the pointmass that is linked to the spring from its root.
- End linked index is the pointmass that is linked to the spring from its end.
- Damping Coefficient determines the amount of damping on the spring. 0 means no damping (perfect oscillation).

Note: If either of the "linked indices" do not exist, the spring is considered to come from the origin.

### Simulation Data Compiler Variables
- Plot Scale is a scalar that controls the size of the graph in the unity scene.
- Plot origin is where the graph will start putting points down in the unity scene.

## Notes / Technical Details
- The ODE Integrator used for the point-masses in this simulation is currently Velocity Verlet. I might add RK4 as an option in the future.  
- I am planning for this project to be my graduation project, which means that I will most likely be working on it for the next 1.5 years (or until i have finished all my ambitions for it).  
- Floating-point numbers are currently used for this simulation, but I am planning to switch to Double-Precision soon by bringing in **DVector3** from my other project **unity-n-body**.

## Simulation Flow
On Simulation Start:
1. Find the simulation end data instance in the scene
2. Find springs' pointmass indices

Every Timestep:
1. Velocity Verlet's Half-step (for Second-Order Accuracy)
2. Recalculation of spring forces and stretching based on new positions from Velocity Verlet's intermediate step
3. Final Step of Velocity Verlet

Note that the sending of simulation data to the instance of simulation end data is accumulated across all steps.

## Credits

[oPhysics Interactive Physics Simulations on SHM](https://ophysics.com/w1.html)  
[Wikipedia Restorative Force of springs from Hooke's Law](https://en.wikipedia.org/wiki/Hooke's_law)  
[Arcane Algorithm Archive's Verlet Integration Documentation](https://www.algorithm-archive.org/contents/verlet_integration/verlet_integration.html)  
