using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovementController : MonoBehaviour
{
    #region variables
    Rigidbody rb;            

    float lastXDirection;
    float floorWidth = 2.1f;    
    float speed = 5f;

    Vector3 moveDirection;
    #endregion

    #region Unity
    void Start()
    {
        rb = GetComponent<Rigidbody>();        
    }
    void Update()
    {
        if(LevelManager.isGameStarted&&!LevelManager.isGameEnded)
        {
            InputControl();
        }        
    }
    #endregion

    #region PlayerStopper
    public void StopPlayerWon()
    {
        WAController.WaFunction(() => { 
            rb.velocity = Vector3.zero;
            PlayerAnimationController.Instance.PlayIdle();
        }, 1);
    }
    public void StopPlayerFailed()
    {
        WAController.WaFunction(() => { rb.velocity = Vector3.zero; }, 0.1f);
    }
    #endregion

    #region UserInputs
    void InputControl()
    {
        moveDirection.z = 1;
        if (Input.GetMouseButtonDown(0))
        {
            lastXDirection = Input.mousePosition.x;
        }
        else if (Input.GetMouseButton(0))
        {
            moveDirection.x = (Input.mousePosition.x - lastXDirection) * .045f;
            lastXDirection = Input.mousePosition.x;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            moveDirection.x = 0;
        }
        if (transform.position.x <= -floorWidth + 0.25f)
        {
            if (transform.position.x < -floorWidth)
            {
                moveDirection.x = 0;
                transform.DOMoveX(-floorWidth + 0.1f, .01f);
            }
            if (moveDirection.x < 0)
            {
                moveDirection.x = 0;
            }
        }
        else if (transform.position.x >= floorWidth - 0.25f)
        {
            if (transform.position.x > floorWidth)
            {
                moveDirection.x = 0;
                transform.DOMoveX(floorWidth - 0.1f, .01f);
            }
            if (moveDirection.x > 0)
            {
                moveDirection.x = 0;
            }
        }
        rb.velocity = moveDirection * speed;

        transform.DORotate(new Vector3(0, moveDirection.x * 36f, 0), .5f);
    }
    #endregion
}