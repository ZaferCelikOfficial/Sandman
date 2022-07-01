using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SpherePositioner : MonoBehaviour
{
    public PartsOfBody myPartOfBody;

    List<Vector3> spherePositions = new List<Vector3>();

    LayerMask groundLayer;

    float playerHeightWhenCrawling = 0.165f,playerHeightWhenWalkingWithLeg=-0.03f, playerHeightWhenWalkingWithNoLeg = -0.8f;

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            spherePositions.Add(transform.GetChild(i).localPosition);
        }
        groundLayer = GameManager.Instance.groundLayer;
        garbage = GameManager.Instance.garbage;
    }

    Transform garbage;
    public void ReleaseSpheres(int whichChild)
    {        
        while(transform.childCount>whichChild)
        {
            Transform objOnProcess = transform.GetChild(whichChild);

            objOnProcess.SetParent(garbage);           
            objOnProcess.GetComponent<Rigidbody>().isKinematic=false;
            objOnProcess.GetComponent<SphereCollider>().isTrigger = false;

            //WAController.WaFunction(() => {objOnProcess.gameObject.SetActive(false);}, 5);
        }
        if(myPartOfBody==PartsOfBody.body&&!LevelManager.isGameEnded)
        {
            float percent = transform.childCount * 100 /spherePositions.Count ;

            if(percent<90)
            {
                BodyCountController.Instance.ReleaseLegs();               
            }
        }
        WAController.WaFunction(() => { BodyCountController.Instance.CheckBodyParameters(); }, 0.15f);                           
    }
    public void PlaceSphere(GameObject mySphere)
    {
        mySphere.transform.SetParent(transform);
        mySphere.transform.DOLocalMove(spherePositions[mySphere.transform.GetSiblingIndex()],1.1f).SetEase(Ease.OutQuint).onComplete = () =>
        {
            mySphere.GetComponent<SphereCollider>().enabled = true;
        };
    }        
    public IEnumerator GroundCheck()
    {        
        if(transform.childCount>0)
        {
            if(myPartOfBody==PartsOfBody.body)
            {
                GameManager.Instance.player.DOKill();
                GameManager.Instance.player.DOMoveY(playerHeightWhenCrawling, 0.15f);
            }
            else
            {                
                float movePositionInY = transform.childCount * (playerHeightWhenWalkingWithLeg - playerHeightWhenWalkingWithNoLeg) / spherePositions.Count + playerHeightWhenWalkingWithNoLeg;

                GameManager.Instance.player.DOKill();
                GameManager.Instance.player.DOMoveY(movePositionInY, 0.25f);            
            }
        }
        yield return null;
    }
    //public IEnumerator GroundCheck()
    //{
    //    if (transform.childCount > 0)
    //    {
    //        if (myPartOfBody == PartsOfBody.body)
    //        {
    //            GameManager.Instance.player.DOKill();
    //            GameManager.Instance.player.DOMoveY(playerHeightWhenCrawling, 0.15f);
    //        }
    //        else
    //        {
    //            Transform objClosestToGround = transform.GetChild(transform.childCount - 1);

    //            float waitDelay = 0.1f;
    //            float unitMoveDistance = 0.05f;

    //            if (objClosestToGround.position.y < yGroundHeight)
    //            {
    //                unitMoveDistance *= -1;
    //            }
    //            while (!Physics.CheckSphere(objClosestToGround.position, .1f, groundLayer))
    //            {
    //                GameManager.Instance.player.DOKill();
    //                GameManager.Instance.player.DOMoveY(GameManager.Instance.player.position.y - unitMoveDistance, waitDelay).SetEase(Ease.Linear);
    //                yield return null;
    //            }
    //        }
    //    }
    //    yield return null;
    //}
}
