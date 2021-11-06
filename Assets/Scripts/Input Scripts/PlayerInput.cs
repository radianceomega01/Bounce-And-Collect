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
        ballCount = GameData.BALL_COUNT;
    }
    void Update()
    {
        DesktopTouchInput();        
    }

    void DesktopTouchInput()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            GameData.BALL_COUNT = 0;
            handImg.enabled = false;
            guideTxt.enabled = false;
            anim.SetBool("Input", true);
            instantiatePos = new Vector2(transform.position.x + 0.7f, transform.position.y);
            if(Input.GetKey(KeyCode.RightArrow))
                transform.position = new Vector2(transform.position.x + movementSpeed * Time.deltaTime, transform.position.y);
            else if (Input.GetKey(KeyCode.LeftArrow))
                transform.position = new Vector2(transform.position.x - movementSpeed * Time.deltaTime, transform.position.y);
            if (!calledOnce)
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
    void AndroidTouchInput()
    {
        if (Input.touchCount > 0)
        {
            GameData.BALL_COUNT = 0;
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
        Debug.Log(ballCount);
    }
}
