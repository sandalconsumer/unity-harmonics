# unity-harmonics
Harmonic Motion in Unity.

## Project Status
- This project is still in its **very** early phase. I plan to make this project into a cloth engine to be used in games and for industrial purposes.

## Planned Features
- Higher-Order ODE Integrator (RK4) For more accurate Velocity, Position, and Acceleration calculations  
- Separation of Spring objects from Body objects for more freedom of usage and simpler construction of spring-mass based meshes and structures  
- Self-Collisions and collisions with geometric meshes for spring-mass based meshes  
- Switching to double-precision mathematics for more precise positions with little to no performance loss  

## Setup

1. Clone the Repo   

```
    git clone https://github.com/sandalconsumer/unity-harmonics.git
```
2. Open it in the Unity Hub

- Open Unity Hub
- Click on the **Add Project** button in the top right next to **New Project**.
- This project was made with Unity 6000.1.1f1 so that is where the code is guaranteed to work, but most Unity 6 versions will (most likely) work well.

## Documentation  

There are 2 main scripts you need to know to understand this repo. And they are:
1. Body
2. Simulation Data Compiler

The **Body** script is added to the gameObject you want to simulate a spring system with, and the **Simulation Data Compiler** visualizes the data from the bodies.

### Body Variables
- Data Compiler is the **Simulation Data Compiler** script that will read the data from the current Body.  
- Mass is the mass of the point (the GameObject's transform's position) in kilograms.  
- Connected Springs is a list of all the springs that will Influence the current Body. Currently, the only way to make new springs that the body will recognize is to access this, but I am planning to make a dedicated GameObject for springs in the near future.  

### Simulation Data Compiler Variables
- Plot Scale is a scalar that controls the size of the graph in the unity scene.
- Plot origin is where the graph will start putting points down in the unity scene.

## Notes / Technical Details
- The ODE Integrator used for the point-masses in this simulation is currently Velocity Verlet. I might add RK4 as an option in the future.  
- I am planning for this project to be my graduation project, which means that I will most likely be working on it for the next 1.5 years (or until i have finished all my ambitions for it).  
- Floating-point numbers are currently used for this simulation, but I am planning to switch to Double-Precision soon by bringing in **DVector3** from my other project **unity-n-body**.

## Simulation Flow
On Simulation Start:
1. Initialize all positions for point-masses from preset transform  
2. Calculate initial stretch of all springs

Every Timestep:
1. Represent Springs in Unity Scene using Debug
2. Velocity Verlet's Half-step (for Second-Order Accuracy)
3. Recalculation of spring forces and stretching based on new positions from Velocity Verlet's intermediate step
4. Final Step of Velocity Verlet
5. Sending Data to Simulation Data Compiler

## Credits

[oPhysics Interactive Physics Simulations on SHM](https://ophysics.com/w1.html)  
[Wikipedia Restorative Force of springs from Hooke's Law](https://en.wikipedia.org/wiki/Hooke's_law)  
[Arcane Algorithm Archive's Verlet Integration Documentation](https://www.algorithm-archive.org/contents/verlet_integration/verlet_integration.html)  
