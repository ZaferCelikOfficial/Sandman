using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotateAroundObstacle : Obstacle
{
    #region Unity
    void Start()
    {
        StartCoroutine(RotateObject(transform));    
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sphere") && other.GetComponentInParent<SpherePositioner>() != null)
        {
            other.GetComponentInParent<SpherePositioner>().ReleaseSpheres(other.transform.GetSiblingIndex());
        }
    }
    #endregion
}
