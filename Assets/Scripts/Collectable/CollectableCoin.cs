using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CollectableCoin : Collectable
{
    [SerializeField] ParticleSystem coinCollectedParticle;
    Coroutine rotateRoutine;
    void Start()
    {        
        rotateRoutine=StartCoroutine(RotateObject(transform));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sphere"))
        {
            //StopCoroutine(rotateRoutine);   

            transform.SetParent(GameManager.Instance.garbage);

            coinCollectedParticle.transform.SetParent(null);
            coinCollectedParticle.Play();

            GetComponent<BoxCollider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;

        }

    }
    public IEnumerator RotateObject(Transform myTransform)
    {
        float rotateTime = 1.8f;
        while (true)
        {
            myTransform.DORotate(new Vector3(0, myTransform.rotation.eulerAngles.y + 360, 0), rotateTime, RotateMode.FastBeyond360).SetEase(Ease.Linear);
            yield return new WaitForSeconds(rotateTime);
        }
    }
}
