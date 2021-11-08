using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollision : MonoBehaviour
{
    Vector3 instPos;
    Rigidbody ballBody;
    bool ballwasActive;

    private void Awake()
    {
        ballBody = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (ballBody.velocity.magnitude > 0f)
        {
            ballwasActive = true;
        }
        if (ballBody.velocity.magnitude == 0f && ballwasActive)
        {
            Destroy(gameObject, 5f);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bucket")
        {
            GameData.BALL_COUNT++;
            Destroy(gameObject,0.2f);
        }
       else if (collision.gameObject.name == "Bottom Blocker")
        {
            Destroy(gameObject,5f);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Multiplier")
        {
            string mul = other.GetComponentInParent<TextMesh>().text;
            //Debug.Log(mul);
            instPos = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
            if (mul == "X2")
            {
                InstantiateMoreBalls(1, instPos);
            }
            else if (mul == "X3")
            {
                InstantiateMoreBalls(2, instPos);
            }
            else if (mul == "X4")
            {
                InstantiateMoreBalls(3, instPos);
            }
            else
            {
                InstantiateMoreBalls(4, instPos);
            }
        }
        else if (other.gameObject.name == "Balls Counter")
        {
            GameData.RESULT_BALL_COUNT++;
        }
    }

    void InstantiateMoreBalls(int n, Vector3 pos)
    {
        for (int i = 0; i < n; i++)
        {
            Instantiate(gameObject, pos, transform.rotation) ;
        }
    }
}
