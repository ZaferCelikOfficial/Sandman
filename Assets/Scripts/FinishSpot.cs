using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishSpot : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Sphere"))
        {
            GetComponent<BoxCollider>().enabled = false;

            LevelManager.Instance.OnLevelCompleted();
        }
    }

}
