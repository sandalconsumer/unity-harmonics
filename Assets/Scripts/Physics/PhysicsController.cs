using UnityEngine;
using System.Collections.Generic;
using System.Drawing;
using Color = UnityEngine.Color;

[RequireComponent(typeof(SimulationDataCompiler))]
public class PhysicsController : MonoBehaviour
{
    private SimulationEndData simulationEndData;

    public bool visualizeSprings = true;
    public bool recordData = true;
    
    public List<Pointmass> masses = new List<Pointmass>();
    public List<Spring> springs = new List<Spring>();

    private float timestep;
    private float totalTime;
    
    void Start()
    {
        simulationEndData = GetComponent<SimulationDataCompiler>().simulationEndData;
        timestep = Time.fixedDeltaTime;
        
        foreach (Spring spring in springs)
        {
            int rootLinkedIndex = spring.rootLinkedIndex;
            int endLinkedIndex = spring.endLinkedIndex;

            if (spring.rootLinkedIndex < masses.Count)
            {
                spring.rootLinked = masses[rootLinkedIndex];
            }

            if (spring.endLinkedIndex < masses.Count)
            {
                spring.endLinked = masses[endLinkedIndex];
            }
        }
    }

    void FixedUpdate()
    {
        foreach (Pointmass mass in masses)
        {
            mass.VerletHalfstep(timestep);
        }

        foreach (Spring spring in springs)
        {
            Vector3 springVector = spring.endLinked.position - spring.rootLinked.position;
            float currentLength = springVector.magnitude;
            float extension = currentLength - spring.restLength;
            
            Vector3 springDirection = springVector.normalized;
            spring.restorativeForce = -spring.springConstant * extension * springDirection;

            spring.rootLinked.leadForce -= spring.restorativeForce;
            spring.endLinked.leadForce += spring.restorativeForce;

            Vector3 relativeVelocity = spring.rootLinked.velocity - spring.endLinked.velocity;
            float velocityOnSpringDirection = Vector3.Dot(relativeVelocity, springDirection);
            Vector3 dampingForce = -spring.dampingCoefficient * velocityOnSpringDirection * springDirection;
            
            spring.rootLinked.leadForce += dampingForce;
            spring.endLinked.leadForce -= dampingForce;
            
            if(visualizeSprings) {Debug.DrawLine(spring.rootLinked.position, spring.endLinked.position, Color.yellow);}
            if(recordData) {simulationEndData.stretches.Add(extension);}
        }
        
        foreach (Pointmass mass in masses)
        {
            mass.VerletFinalstep(timestep);
            
            mass.leadForce = Vector3.zero;
            if(recordData) {simulationEndData.positions.Add(mass.position);}
        }
        
        totalTime += timestep;
        if(recordData) {simulationEndData.timeStamps.Add(totalTime);}
    }
}
