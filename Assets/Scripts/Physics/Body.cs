using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Spring 
{
    public Vector3 rootPosition = new Vector3(0,0,0);
    public float springConstant = 100;

    [HideInInspector]public Vector3 restorativeForce = Vector3.zero;
    [HideInInspector]public Vector3 stretch;
    public Vector3 restPosition = new Vector3(3, 0, 0);
}

public class Body : MonoBehaviour
{
    public SimulationDataCompiler dataCompiler;
    
    Vector3 position;
    Vector3 acceleration;
    Vector3 velocity;

    float mass = 100;
    
    Vector3 leadForce;
    
    public List<Spring> connectedSprings = new List<Spring>();

    private float timestep;
    
    void Start()
    {
        position = transform.position;

        foreach (Spring spring in connectedSprings)
        {
            spring.stretch = position - spring.restPosition;
        }
        
        timestep = Time.fixedDeltaTime;
    }

    void FixedUpdate()
    {
        foreach (Spring spring in connectedSprings)
        {
            Debug.DrawLine(spring.rootPosition, position, Color.yellow);
        }
        
        VerletHalfstep(timestep);
        UpdateSpringDisplacement(connectedSprings);

        foreach (Spring s in connectedSprings)
        {
            FormulateSpringforces(s);
        }
        
        AccumulateForce(connectedSprings);
        
        VerletFinalstep(timestep);
        
        dataCompiler.simulationEndData.timeStamps.Add(Time.fixedTime);
        dataCompiler.simulationEndData.positions.Add(position);
        dataCompiler.simulationEndData.stretches.Add(connectedSprings[0].stretch.magnitude);
        
    }

    void UpdateSpringDisplacement(List<Spring> springs)
    {
        foreach (Spring s in springs)
        {
            s.stretch = position - s.restPosition;
        }
    }

    void FormulateSpringforces(Spring spring)
    {
        Vector3 restorativeForce = -spring.springConstant * spring.stretch;
        spring.restorativeForce =  restorativeForce;
    }

    void AccumulateForce(List<Spring> springs)
    {
        leadForce = Vector3.zero;

        foreach (Spring s in springs)
        {
            leadForce += s.restorativeForce;
        }
    }

    void VerletHalfstep(float deltaTime)
    {
        position += velocity * deltaTime + 0.5f * acceleration * deltaTime * deltaTime;
    }

    void VerletFinalstep(float deltaTime)
    {
        Vector3 nA = leadForce / mass;
        velocity += 0.5f * (acceleration + nA) * deltaTime;
        acceleration = leadForce / mass;
        
        transform.position = position;
    }
}
