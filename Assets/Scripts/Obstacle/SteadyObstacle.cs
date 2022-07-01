using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteadyObstacle : Obstacle
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sphere") && other.GetComponentInParent<SpherePositioner>() != null)
        {
            other.GetComponentInParent<SpherePositioner>().ReleaseSpheres(other.transform.GetSiblingIndex());
        }
    }
}
