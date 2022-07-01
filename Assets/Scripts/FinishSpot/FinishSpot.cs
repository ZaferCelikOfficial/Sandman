using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishSpot : MonoBehaviour
{
    #region Unity
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Sphere"))
        {
            GetComponent<BoxCollider>().enabled = false;

            LevelManager.Instance.OnLevelCompleted();
        }
    }
    #endregion
}
