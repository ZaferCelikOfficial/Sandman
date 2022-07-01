using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CollectableCoin : Collectable
{
    #region Variables
    [SerializeField] ParticleSystem coinCollectedParticle;
    Coroutine rotateRoutine;
    #endregion

    #region Unity
    void Start()
    {        
        rotateRoutine=StartCoroutine(RotateObject(transform));
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sphere"))
        {            
            transform.SetParent(GameManager.Instance.garbage);

            coinCollectedParticle.transform.SetParent(null);
            coinCollectedParticle.Play();

            GetComponent<BoxCollider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;

        }
    }
    #endregion
}
