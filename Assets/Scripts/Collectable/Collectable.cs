﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Collectable : MonoBehaviour
{
    #region ObjectRotator
    public IEnumerator RotateObject(Transform myTransform)
    {
        float rotateTime = 1.8f;
        while (true)
        {
            myTransform.DORotate(new Vector3(0, myTransform.rotation.eulerAngles.y + 360, 0), rotateTime, RotateMode.FastBeyond360).SetEase(Ease.Linear);
            yield return new WaitForSeconds(rotateTime);
        }
    }
    #endregion
}
