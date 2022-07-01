using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WAController : LocalSingleton<WAController>
{
    public static void WaFunction(Action action, float waTime)
    {
        Instance.StartCoroutine(Instance.WaFunctionCoroutine(action, waTime));
    }

    IEnumerator WaFunctionCoroutine(Action action,float waTime)
    {
        yield return new WaitForSeconds(waTime);
        if(action != null)
        {
            action();
        }
    }
}