using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    float movementSpeed = 3f;
    public GameObject balls;
    Touch touch;
    Vector2 instantiatePos;
    Animator anim;
    int ballCount;
    bool calledOnce;
    public Image handImg;
    public Text guideTxt;
    // Update is called once per frame

    private void Awake()
    {
        anim = GetComponent<Animator>();
        if (GameData.BALL_COUNT == 0)
        {
            ballCount = 10;
        }
    }
    void Update()
    {
        DesktopTouchInput();
        if (GameData.BALL_COUNT==0 && !GameData.IS_SET_BALL_COUNT)
        {
            ballCount = 10;
            GameData.IS_SET_BALL_COUNT = true;
        }
        if (GameData.IS_STAGE_TWO && !GameData.IS_SET_BALL_COUNT_NEXT)
        {
            
            ballCount = GameData.BALL_COUNT;
            GameData.IS_SET_BALL_COUNT_NEXT = true;
        }
    }

    void DesktopTouchInput()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            handImg.gameObject.SetActive(false);
            guideTxt.gameObject.SetActive(false);
            anim.SetBool("Input", true);
            instantiatePos = new Vector2(transform.position.x + 0.7f, transform.position.y);
            if(Input.GetKey(KeyCode.RightArrow))
                transform.position = new Vector2(transform.position.x + movementSpeed * Time.deltaTime, transform.position.y);
            else if (Input.GetKey(KeyCode.LeftArrow))
                transform.position = new Vector2(transform.position.x - movementSpeed * Time.deltaTime, transform.position.y);
            if (!calledOnce)
            {
                Debug.Log(calledOnce);
                InvokeRepeating("WaitForBall", 0.5f, 0.3f);
                calledOnce = true;
            }
                
        }
        if (ballCount == 0)
        {
            CancelInvoke("WaitForBall");
            anim.SetBool("Input", false);
        }

    }
    void AndroidTouchInput()
    {
        if (Input.touchCount > 0)
        {
            //GameData.BALL_COUNT = 0;
            handImg.enabled = false;
            guideTxt.enabled = false;
            anim.SetBool("Input", true);
            touch = Input.GetTouch(0);
            instantiatePos = new Vector2(transform.position.x + 0.7f, transform.position.y);
            transform.position = new Vector2(transform.position.x + touch.deltaPosition.x * movementSpeed * Time.deltaTime, transform.position.y);
            if (touch.phase == TouchPhase.Moved && !calledOnce)
            {
                InvokeRepeating("WaitForBall", 0.5f, 0.3f);
                calledOnce = true;

            }
        }
        if (ballCount == 0)
        {
            CancelInvoke("WaitForBall");
            anim.SetBool("Input", false);
        }
    }
    void WaitForBall()
    {
        Instantiate(balls, instantiatePos, transform.rotation);
        ballCount--;
        if (GameData.IS_STAGE_TWO)
            GameData.IS_STAGE_TWO_STARTED = true;
        else
            GameData.IS_STAGE_TWO_STARTED = false;
        //Debug.Log(ballCount);
    }
}
