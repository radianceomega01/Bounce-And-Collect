using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    int columnLength;
    int rowLength;
    public float xSpace;
    public float ySpace;
    public float xStart;
    public float yStart;
    public GameObject obstacleLeft;
    public GameObject obstacleStraight;
    public GameObject obstacleRight;
    public GameObject obstacleEmpty;
    public GameObject ballMultiplier;
    int rand;
    float multRand;
    int rightObsIndex;
    int straightObsIndex;
    int multObsIndex;

    private void Awake()
    {
        columnLength = 4;
        rowLength = 4;
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < columnLength * rowLength; i++)
        {
            if (i == 0 || i == 1 || i == 2 || i == 3)
            {
                rand = Random.Range(1, 3);
            }
            else if (i == 12 || i == 13 || i == 14 || i == 15)
            {
                rand = Random.Range(1, 3);
            }

            else if (i == 4 || i == 8)
            {
                rand = Random.Range(1, 4);
            }
            else if (i == 7 || i == 11)
            {
                rand = Random.Range(0, 3);
            }
            else
                rand = Random.Range(0, 4);
            if (Random.Range(0f, 1f) >= 0.7)
            {
                if (rand == 1 || rand == 2)
                {
                    InstantiateWithNum(4, new Vector3(xStart + (xSpace * (i % columnLength)) + 0.7f, yStart + (-ySpace * (i / columnLength)) - 0.5f, 0));
                    multObsIndex = i;
                }
                
            }
            if (rand == 0)
            {
                if (rightObsIndex == i - 1 || straightObsIndex == i - 1 || multObsIndex == i-1)
                {
                    rand = Random.Range(1, 4);
                    InstantiateWithNum(rand, new Vector3(xStart + (xSpace * (i % columnLength)), yStart + (-ySpace * (i / columnLength)), 0));
                }
                else
                {
                    InstantiateWithNum(rand, new Vector3(xStart + (xSpace * (i % columnLength)), yStart + (-ySpace * (i / columnLength)), 0));
                }
            }
            if (rand == 1)
                InstantiateWithNum(rand, new Vector3(xStart + (xSpace * (i % columnLength)), yStart + (-ySpace * (i / columnLength)), 0));
            if (rand == 2)
            {
                /*if (rightObsIndex == i - 1)
                    rand = 0;*/
                InstantiateWithNum(rand, new Vector3(xStart + (xSpace * (i % columnLength)), yStart + (-ySpace * (i / columnLength)), 0));
                straightObsIndex = i;
            }
            if (rand == 3)
            {
                InstantiateWithNum(rand, new Vector3(xStart + (xSpace * (i % columnLength)), yStart + (-ySpace * (i / columnLength)), 0));
                rightObsIndex = i;
            }
            Debug.Log(i+" "+rand);     
        }
    }

    void InstantiateWithNum(int num, Vector3 pos)
    {
        if (num == 0)
        {
            Instantiate(obstacleLeft, pos, Quaternion.identity);
        }
        if (num == 1)
            Instantiate(obstacleEmpty, pos, Quaternion.identity);
        if (num == 2)
            Instantiate(obstacleStraight, pos, Quaternion.identity);
        if (num == 3)
        {
            Instantiate(obstacleRight, pos, Quaternion.identity);
        }
        if (num == 4)
        {
            Instantiate(ballMultiplier, pos, Quaternion.identity);
            int randNum = Random.Range(2, 5);
            ballMultiplier.GetComponent<TextMesh>().text = "X" + randNum;
        }
    }

}
