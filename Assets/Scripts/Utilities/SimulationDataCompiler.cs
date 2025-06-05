using UnityEngine;
using System.Collections.Generic;

public class SimulationEndData
{
    public List<float> timeStamps = new List<float>();
    public List<Vector3> positions = new List<Vector3>();
    public List<float> stretches = new List<float>();
}

[RequireComponent(typeof(PhysicsController))]
public class SimulationDataCompiler : MonoBehaviour // to be used as a singleton
{
    public SimulationEndData simulationEndData = new SimulationEndData();
    
    public float plotScale = 1f;
    public Vector3 plotOrigin = new Vector3(10, 0, 0);
    
    void FixedUpdate()
    {
        for (int i = 1; i < simulationEndData.timeStamps.Count; i++)
        {
            Vector3 p1 = plotOrigin + new Vector3(simulationEndData.timeStamps[i-1] * plotScale, simulationEndData.stretches[i-1] * plotScale, 0);
            Vector3 p2 = plotOrigin + new Vector3(simulationEndData.timeStamps[i] * plotScale, simulationEndData.stretches[i] * plotScale, 0);
            Debug.DrawLine(p1, p2, Color.cyan);
        }
    }
    
    
}
