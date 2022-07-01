using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotateAroundObstacle : Obstacle
{
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
    public IEnumerator RotateObject(Transform myTransform)
    {
        float rotateTime = 11.5f;
        while (true)
        {
            myTransform.DORotate(new Vector3(0, myTransform.rotation.eulerAngles.y + 360, 0), rotateTime, RotateMode.FastBeyond360).SetEase(Ease.Linear);
            yield return new WaitForSeconds(rotateTime);
        }

    }
}
