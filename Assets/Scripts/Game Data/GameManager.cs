using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public Text requiredBalls;
    public Button resultButton;
    public Button nextButton;
    public Camera mainCamera;
    public GameObject bucketUp;
    public GameObject bucketDown;
    bool gameStarted;
    bool isWon;
    bool movedCameraAndBucket;
    Animator bucketAnim;
    PlayerInput inpBucketUp;
    PlayerInput inpBucketDown;

    // Start is called before the first frame update
    void Start()
    {
        bucketAnim = bucketDown.GetComponent<Animator>();
        inpBucketUp = bucketDown.GetComponent<PlayerInput>();
        inpBucketDown = bucketDown.GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if (requiredBalls.enabled == true)
        {
            requiredBalls.text = GameData.RESULT_BALL_COUNT + "/" + "50";
        }
        if (GameObject.FindGameObjectsWithTag("Ball").Length > 0)
        {
            gameStarted = true;
        }
        if (GameObject.FindGameObjectsWithTag("Ball").Length == 0 && gameStarted)
        {
            GameData.IS_STAGE_TWO = true;
            inpBucketUp.enabled = true;
            inpBucketDown.enabled = false;
            bucketDown.tag = "Untagged";
            if (!movedCameraAndBucket)
            {
                mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position,
                new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y - 9f, mainCamera.transform.position.z), 1f);
                bucketDown.transform.position = Vector3.Lerp(bucketDown.transform.position,
                    new Vector3(bucketDown.transform.position.x, bucketDown.transform.position.y - 1f, bucketDown.transform.position.z), 0.5f);
                requiredBalls.enabled = true;
                movedCameraAndBucket = true;
            }
        }

        if (bucketAnim.GetBool("Input") == false && GameData.RESULT_BALL_COUNT < 50)
        {
            resultButton.enabled = true;
            resultButton.GetComponentInChildren<Text>().text = "Game Over!";
            nextButton.enabled = true;
            nextButton.GetComponentInChildren<Text>().text = "Try Again";
            isWon = false;
        }

        else if (bucketAnim.GetBool("Input") == false && GameData.RESULT_BALL_COUNT >= 50)
        {
            resultButton.enabled = true;
            resultButton.GetComponentInChildren<Text>().text = "Well Done!";
            nextButton.enabled = true;
            nextButton.GetComponentInChildren<Text>().text = "Next";
            isWon = true;
        }
    }

    public void RestartGame()
    {
        if(!isWon)
            GameData.LEVEL = 1;
        else
            GameData.LEVEL++;
        SceneManager.LoadScene("GameplayScene");
    }
}