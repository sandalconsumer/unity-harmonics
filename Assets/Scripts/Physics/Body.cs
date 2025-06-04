using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Spring // in this simulation, springs are considered to all be acting on one body, and class "Body" is a singleton although not explicitly being defined as that.
{
    public Vector3 rootPosition = new Vector3(0,0,0);
    public float springConstant = 100;

    [HideInInspector]public Vector3 restorativeForce = Vector3.zero;
    [HideInInspector]public Vector3 stretch; //how far the mass is from the root position
    public Vector3 restPosition = new Vector3(3, 0, 0);
}

public class Body : MonoBehaviour
{
    Vector3 position;
    Vector3 acceleration;
    Vector3 velocity;

    float mass = 100;
    
    Vector3 leadForce;
    
    public List<Spring> connectedSprings = new List<Spring>();

    private float timestep;
    
    public float plotScale = 1f;
    public Vector3 plotOrigin = new Vector3(0, 0, -10);
    
    private List<float> timeStamps = new List<float>();
    private List<Vector3> positions = new List<Vector3>();
    private List<float> stretches = new List<float>();
    
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
        
        timeStamps.Add(Time.fixedTime);
        positions.Add(position);
        stretches.Add(connectedSprings[0].stretch.magnitude);
        
        for (int i = 1; i < timeStamps.Count; i++)
        {
            Vector3 p1 = plotOrigin + new Vector3(timeStamps[i-1] * plotScale, stretches[i-1] * plotScale, 0);
            Vector3 p2 = plotOrigin + new Vector3(timeStamps[i] * plotScale, stretches[i] * plotScale, 0);
            Debug.DrawLine(p1, p2, Color.cyan);
        }
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
