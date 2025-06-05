using UnityEngine;
using System.Collections.Generic;
using Unity.Mathematics;

[System.Serializable]
public class Spring 
{
    public float springConstant = 100;
    
    public float restLength;

    public int rootLinkedIndex;
    public int endLinkedIndex;

    [HideInInspector]public Pointmass rootLinked;
    [HideInInspector]public Pointmass endLinked;
    
    [HideInInspector]public Vector3 restorativeForce = Vector3.zero; 
}

[System.Serializable]
public class Pointmass
{
    public Vector3 position;
    Vector3 acceleration;
    Vector3 velocity;

    public float mass = 100;
    [HideInInspector]public Vector3 leadForce;

    public void VerletHalfstep(float deltaTime)
    {
        position += velocity * deltaTime + acceleration * (0.5f * deltaTime * deltaTime);
    }

    public void VerletFinalstep(float deltaTime)
    {
        Vector3 nA = leadForce / mass;
        velocity += (acceleration + nA) * (0.5f * deltaTime);
        acceleration = leadForce / mass;
    }
}
