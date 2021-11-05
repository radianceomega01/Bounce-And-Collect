using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollision : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bucket")
        {
            Destroy(gameObject,0.1f);
        }
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Multiplier")
        {
           string mul =  other.GetComponent<TextMesh>().text;
            if (mul == "X2")
            {
                InstantiateMoreBalls(2);
            }
            else if (mul == "X2")
            {
                InstantiateMoreBalls(3);
            }
            else if (mul == "X2")
            {
                InstantiateMoreBalls(4);
            }
            else
            {
                InstantiateMoreBalls(5);
            }
        }
    }*/

    void InstantiateMoreBalls(int n)
    {
        for (int i = 0; i < n; i++)
        {
            Instantiate(gameObject, transform.position, transform.rotation) ;
        }
    }
}
