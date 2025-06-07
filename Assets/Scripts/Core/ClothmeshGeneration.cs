using UnityEngine;
using System.Collections.Generic;

public class ClothmeshGeneration : MonoBehaviour
{
    public int detail = 10; //10 means the mesh will be a 10x10 square grid (200 triangles)
    public float meshSize = 0.1f;
    public float meshDamping = 0.5f;
    
    private List<Spring> edges = new List<Spring>();
    private List<Pointmass> vertices = new List<Pointmass>();
    
    private PhysicsController physicsController;

    void Awake()
    {
        physicsController = GetComponent<PhysicsController>();
        
        for (int i = 0; i < detail; i++)
        {
            for (int j = 0; j < detail; j++)
            {
                Pointmass currentPointmass = new Pointmass();
                
                vertices.Add(currentPointmass);
                currentPointmass.position = new Vector3(i * meshSize, 0, j * meshSize);
                
                //check neighbors and make springs (right and down are not used because they will always not be created yet. j - 1 will always exist (other than index 0).
                
                if (j > 0)
                {
                    Pointmass ln = vertices[i * detail + j - 1];
                    
                    edges.Add(new Spring
                    {
                        rootLinkedIndex = i * detail + j,
                        endLinkedIndex = i * detail + j - 1,
                        restLength = (currentPointmass.position - ln.position).magnitude,
                        dampingCoefficient = meshDamping,
                        springConstant = 1f,
                    });
                }
                
                if (i > 0)
                {
                    Pointmass un = vertices[i * detail + j - detail];
                    
                    edges.Add(new Spring
                    {
                        rootLinkedIndex = i * detail + j,
                        endLinkedIndex = i * detail + j - detail,
                        restLength = (currentPointmass.position - un.position).magnitude,
                        dampingCoefficient = meshDamping,
                        springConstant = 1f,
                    });
                }
            }
        }

        for (int i = 0; i < detail; i++)
        {
            for (int j = 0; j < detail; j++)
            {
                Pointmass currentPointmass = vertices[i * detail + j];
                int currentIndex = i * detail + j;

                if (i > 0 && j > 0)
                {
                    int topLeftNeighborIndex = (i - 1) * detail + (j - 1);
                    Pointmass topLeftNeighbor = vertices[topLeftNeighborIndex];
    
                    edges.Add(new Spring
                    {
                        rootLinkedIndex = currentIndex,
                        endLinkedIndex = topLeftNeighborIndex,
                        restLength = (currentPointmass.position - topLeftNeighbor.position).magnitude,
                        dampingCoefficient = meshDamping,
                        springConstant = 1f,
                    });
                }
                
                if (i > 0 && j < detail - 1)
                {
                    int topRightNeighborIndex = (i - 1) * detail + (j + 1);
                    Pointmass topRightNeighbor = vertices[topRightNeighborIndex];
    
                    edges.Add(new Spring
                    {
                        rootLinkedIndex = currentIndex,
                        endLinkedIndex = topRightNeighborIndex,
                        restLength = (currentPointmass.position - topRightNeighbor.position).magnitude,
                        dampingCoefficient = meshDamping,
                        springConstant = 1f,
                    });
                }
            }
        }

        physicsController.masses.Clear();
        physicsController.springs.Clear();
        
        physicsController.masses = vertices;
        physicsController.springs = edges;
    }
}
