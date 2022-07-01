using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSpheres : Collectable
{
    #region Variables
    [SerializeField] GameObject spherePrefab;

    int sphereRadiusLayerCount = 4;
    #endregion

    #region Unity
    void Start()
    {        
        CreateSpheres();
    }
    void OnTriggerEnter(Collider other)
    {        
        if(other.CompareTag("Sphere"))
        {
            GetComponent<BoxCollider>().enabled = false;
            BodyCountController.Instance.CalculateCurrentPercentages();
            StartCoroutine(SpherePlacerNumerator());            
        }        
    }
    #endregion

    #region SphereInstantiation

    void CreateSpheres()
    {        
        int sphereCount=1;
        float myRadius = 0;

        for (int i = 0; i < sphereRadiusLayerCount; i++)
        {
            for (int j = 0; j < sphereCount; j++)
            {
                Transform instantiatedObjectTransform = Instantiate(spherePrefab).transform;

                instantiatedObjectTransform.SetParent(transform);
                instantiatedObjectTransform.localPosition = SpherePosition(j,sphereCount, myRadius);
                instantiatedObjectTransform.GetComponent<SphereCollider>().enabled = false;
                           
            }

            sphereCount += 5;
            myRadius+= 0.15f;
        }
    }
    Vector3 SpherePosition(int objectNumber, int totalObjOnLayer,float currentRadius)
    {
        Vector3 pos = Vector3.zero;

        float angle = objectNumber * (2 * Mathf.PI / totalObjOnLayer);

        pos.x = Mathf.Cos(angle) * currentRadius;
        pos.y = Mathf.Sin(angle) * currentRadius;

        return pos;
    }
    #endregion

    #region CollectableSphereEquiper
    IEnumerator SpherePlacerNumerator()
    {
        int myChildCount = transform.childCount;

        for (int i = 0; i < myChildCount; i++)
        {
            BodyCountController.Instance.EquipSphere(transform.GetChild(0).gameObject);
            yield return null;
        }
        BodyCountController.Instance.CheckBodyParameters();        
    }
    #endregion
}
