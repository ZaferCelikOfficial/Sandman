using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpherePool : LocalSingleton<SpherePool>
{
    public GameObject spherePrefab;
    public float sphereCount = 200;
    void Start()
    {
        CreatePool();
    }
    void CreatePool()
    {
        for (int i = 0; i < sphereCount; i++)
        {
            GameObject instantiatedSphere = Instantiate(spherePrefab, transform);
            instantiatedSphere.SetActive(false);
        }
    }
    public GameObject GetSphereFromPool()
    {
        int countedChild = transform.childCount;
        if (countedChild != 0)
        {
            return transform.GetChild(countedChild - 1).gameObject;
        }
        return null;
    }
    public void PutSphereToPool(GameObject sphere)
    {
        sphere.transform.SetParent(null);
        WAController.WaFunction(() =>
        {
            sphere.transform.SetParent(transform);
            sphere.transform.localPosition = Vector3.zero;
            sphere.transform.localRotation = Quaternion.Euler(0, 0, 0);
            sphere.SetActive(false);
        }, 1);
    }
}
