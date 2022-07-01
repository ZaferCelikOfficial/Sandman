using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PartsOfBody
{
    head,
    leftArm,
    rightArm,
    body,
    leftLeg,
    rightLeg,
}
public class BodyCountController : LocalSingleton<BodyCountController>
{
    [System.Serializable]
    public class BodyPartInformations
    {
        public PartsOfBody partsOfBody;
        public SpherePositioner spherePositioner;
        public int initialMemberCount;
        public float currentPercentage=100;
    }
    public BodyPartInformations[] bodyPartInformations;

    List<PartsOfBody> crawlEquipingSpherePriorityOrder = new List<PartsOfBody>();
    List<PartsOfBody> walkEquipingSpherePriorityOrder = new List<PartsOfBody>();

    void Start()
    {
        CrawlEquipOrder();
        WalkEquipOrder();

        CalculateInitialMemberCounts();
    }
    #region EquipingSphereOrders
    void CrawlEquipOrder()
    {
        crawlEquipingSpherePriorityOrder.Add(PartsOfBody.leftArm);
        crawlEquipingSpherePriorityOrder.Add(PartsOfBody.rightArm);
        crawlEquipingSpherePriorityOrder.Add(PartsOfBody.body);
        crawlEquipingSpherePriorityOrder.Add(PartsOfBody.head);
        crawlEquipingSpherePriorityOrder.Add(PartsOfBody.leftLeg);
        crawlEquipingSpherePriorityOrder.Add(PartsOfBody.rightLeg);
    }
    void WalkEquipOrder()
    {
        walkEquipingSpherePriorityOrder.Add(PartsOfBody.body);
        walkEquipingSpherePriorityOrder.Add(PartsOfBody.leftLeg);
        walkEquipingSpherePriorityOrder.Add(PartsOfBody.rightLeg);
        walkEquipingSpherePriorityOrder.Add(PartsOfBody.head);
        walkEquipingSpherePriorityOrder.Add(PartsOfBody.leftArm);
        walkEquipingSpherePriorityOrder.Add(PartsOfBody.rightArm);
    }
    #endregion
    void CalculateInitialMemberCounts()
    {
        for (int i = 0; i < bodyPartInformations.Length; i++)
        {
            bodyPartInformations[i].initialMemberCount=bodyPartInformations[i].spherePositioner.transform.childCount;
        }
    }
    public void CalculateCurrentPercentages()
    {
        for (int i = 0; i < bodyPartInformations.Length; i++)
        {
            bodyPartInformations[i].currentPercentage = (bodyPartInformations[i].spherePositioner.transform.childCount)*(100)/ bodyPartInformations[i].initialMemberCount;
        }
    }
    public BodyPartInformations GetBodyPartInfo(PartsOfBody myPartOfBody)
    {
        for (int i = 0; i < bodyPartInformations.Length; i++)
        {
            if (bodyPartInformations[i].partsOfBody==myPartOfBody)
            {
                return bodyPartInformations[i];
            }
        }
        return null;
    }
    public void CheckBodyParameters()
    {        
        CheckBodyCounts();
        CheckTotalBodyCount();
    }
    public void CheckGrounding()
    {
        float leftLegCount=0;
        float rightLegCount=0;
        for (int i = 0; i < bodyPartInformations.Length; i++)
        {

            if (bodyPartInformations[i].partsOfBody==PartsOfBody.leftLeg)
            {
                leftLegCount = bodyPartInformations[i].spherePositioner.transform.childCount;
            }
            else if (bodyPartInformations[i].partsOfBody == PartsOfBody.rightLeg)
            {
                rightLegCount= bodyPartInformations[i].spherePositioner.transform.childCount;
            }
        }
        if(leftLegCount>=rightLegCount)
        {
            GetBodyPartInfo(PartsOfBody.leftLeg).spherePositioner.StartCoroutine(GetBodyPartInfo(PartsOfBody.leftLeg).spherePositioner.GroundCheck());
        }
        else if(leftLegCount < rightLegCount)
        {
            GetBodyPartInfo(PartsOfBody.rightLeg).spherePositioner.StartCoroutine(GetBodyPartInfo(PartsOfBody.leftLeg).spherePositioner.GroundCheck());
        }
    }
    public void CheckGrounding(PartsOfBody myPartOfBody)
    {
        SpherePositioner mySpherePositioner = GetBodyPartInfo(myPartOfBody).spherePositioner;
        mySpherePositioner.StartCoroutine(mySpherePositioner.GroundCheck());       
    }
    void CheckTotalBodyCount()
    {
        int totalObject = 0;
        int currentObjectCount = 0;
        
        for (int i = 0; i < bodyPartInformations.Length; i++)
        {
            totalObject += bodyPartInformations[i].initialMemberCount;
            currentObjectCount += Mathf.FloorToInt(bodyPartInformations[i].currentPercentage * bodyPartInformations[i].initialMemberCount / 100);
        }

        float myPercentage=((100 * currentObjectCount) / totalObject);        
        if (((myPercentage<4)||GetBodyPartInfo(PartsOfBody.body).currentPercentage==0)&&!LevelManager.isGameEnded)
        {
            LevelManager.Instance.OnLevelFailed();
            for (int i = 0; i < bodyPartInformations.Length; i++)
            {
                bodyPartInformations[i].spherePositioner.ReleaseSpheres(0);                
            }
        }
    }
    void CheckBodyCounts()
    {
        CalculateCurrentPercentages();
        if(GetBodyPartInfo(PartsOfBody.leftLeg).currentPercentage>80&& GetBodyPartInfo(PartsOfBody.rightLeg).currentPercentage>80)
        {
            PlayerAnimationController.Instance.PlayWalkNormal();
            CheckGrounding();            
        }
        else if (GetBodyPartInfo(PartsOfBody.leftLeg).currentPercentage - GetBodyPartInfo(PartsOfBody.rightLeg).currentPercentage > 20)
        {
            PlayerAnimationController.Instance.PlayWalkInjuredRight();
            CheckGrounding(PartsOfBody.leftLeg);            
        }
        else if (GetBodyPartInfo(PartsOfBody.rightLeg).currentPercentage - GetBodyPartInfo(PartsOfBody.leftLeg).currentPercentage > 20)
        {
            PlayerAnimationController.Instance.PlayWalkInjuredLeft();
            CheckGrounding(PartsOfBody.rightLeg);            
        }
        else if (GetBodyPartInfo(PartsOfBody.leftLeg).currentPercentage<3&& GetBodyPartInfo(PartsOfBody.rightLeg).currentPercentage<3)
        {
            PlayerAnimationController.Instance.PlayWalkLegless();
            CheckGrounding(PartsOfBody.body);            
        }
        else
        {
            PlayerAnimationController.Instance.PlayWalkNormal();
            CheckGrounding();            
        }        
    }
    public int GetFinishPercentage()
    {
        int totalObject=0;
        int currentObjectCount=0;
        CalculateCurrentPercentages();
        for (int i = 0; i < bodyPartInformations.Length; i++)
        {
            totalObject += bodyPartInformations[i].initialMemberCount;
            currentObjectCount+= Mathf.FloorToInt(bodyPartInformations[i].currentPercentage * bodyPartInformations[i].initialMemberCount / 100);
        }
        return ((100 * currentObjectCount )/ totalObject);
    }
    public void ReleaseLegs()
    {
        GetBodyPartInfo(PartsOfBody.leftLeg).spherePositioner.ReleaseSpheres(0);
        GetBodyPartInfo(PartsOfBody.rightLeg).spherePositioner.ReleaseSpheres(0);
    }
    public void EquipSphere(GameObject sphereObject)
    {
        if(PlayerAnimationController.Instance.characterAnimationState==CharacterAnimationState.walkLegless)
        {            
            PlaceSphereWithOrder(crawlEquipingSpherePriorityOrder, sphereObject);
        }
        else
        {            
            PlaceSphereWithOrder(walkEquipingSpherePriorityOrder, sphereObject);
        }        
    }
    void PlaceSphereWithOrder(List<PartsOfBody> myPartOfBodyOrder,GameObject myObj)
    {        
        for (int i = 0; i < myPartOfBodyOrder.Count; i++)
        {
            var currentBodyPartInfo = GetBodyPartInfo(myPartOfBodyOrder[i]);
            
            if (currentBodyPartInfo.currentPercentage < 100)
            {                
                currentBodyPartInfo.spherePositioner.PlaceSphere(myObj);
                currentBodyPartInfo.currentPercentage = (currentBodyPartInfo.spherePositioner.transform.childCount) * (100) / currentBodyPartInfo.initialMemberCount;
                break;
            }
        }
    }
}
