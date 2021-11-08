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
    public Text levelText;
    public Camera mainCamera;
    public GameObject bucketUp;
    public GameObject bucketDown;
    bool gameStarted;
    bool isWon;
    bool movedCameraAndBucket;
    Animator bucketAnim;
    PlayerInput inpBucketUp;
    PlayerInput inpBucketDown;
    GameObject obstacleParent;
    Vector3 newCameraPos;
    Vector3 newBucketPos;

    // Start is called before the first frame update
    void Awake()
    {
        bucketAnim = bucketDown.GetComponent<Animator>();
        inpBucketUp = bucketUp.GetComponent<PlayerInput>();
        inpBucketDown = bucketDown.GetComponent<PlayerInput>();
        obstacleParent = GameObject.Find("Obstacle Parent");
        newCameraPos = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y - 9f, mainCamera.transform.position.z);
        newBucketPos = new Vector3(bucketDown.transform.position.x, bucketDown.transform.position.y - 1f, bucketDown.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (requiredBalls.enabled == true)
        {
            requiredBalls.text = GameData.RESULT_BALL_COUNT + "/" + "40";
        }
        if (GameObject.FindGameObjectsWithTag("Ball").Length > 2)
        {
            gameStarted = true;
        }
        if (gameStarted && GameObject.FindGameObjectsWithTag("Ball").Length == 0)
        {
            GameData.IS_STAGE_TWO = true;
            inpBucketUp.enabled = false;
            requiredBalls.gameObject.SetActive(true);
            inpBucketDown.enabled = true;
            bucketDown.tag = "Untagged";

            /*if (!movedCameraAndBucket)
            {
                StartCoroutine(WaitForTransition());
                movedCameraAndBucket = true;
            }*/
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position,newCameraPos, 1.5f * Time.deltaTime);
            bucketDown.transform.position = Vector3.Lerp(bucketDown.transform.position,newBucketPos, 1.5f * Time.deltaTime);
        }

        if (GameData.RESULT_BALL_COUNT < 40 && GameData.IS_STAGE_TWO_STARTED && GameObject.FindGameObjectsWithTag("Ball").Length == 0)
        {
            obstacleParent.active = false;
            resultButton.image.color = Color.red;
            resultButton.gameObject.active = true;
            resultButton.GetComponentInChildren<Text>().text = "Game Over!";
            //nextButton.gameObject.active = true;
            nextButton.GetComponentInChildren<Text>().text = "Try Again";
            isWon = false;
        }

        else if (GameData.RESULT_BALL_COUNT >= 40 && GameData.IS_STAGE_TWO_STARTED && GameObject.FindGameObjectsWithTag("Ball").Length == 0)
        {
            obstacleParent.active = false;
            resultButton.image.color = Color.green;
            resultButton.gameObject.active = true;
            resultButton.GetComponentInChildren<Text>().text = "Well Done!";
            //nextButton.gameObject.active = true;
            nextButton.GetComponentInChildren<Text>().text = "Next";
            isWon = true;
        }
    }

    IEnumerator WaitForTransition()
    {
        yield return new WaitForSeconds(1f);
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position,
                new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y - 9f, mainCamera.transform.position.z), 10f *Time.deltaTime);
        bucketDown.transform.position = Vector3.Lerp(bucketDown.transform.position,
            new Vector3(bucketDown.transform.position.x, bucketDown.transform.position.y - 1f, bucketDown.transform.position.z), 5f * Time.deltaTime);
    }
    public void RestartGame()
    {
        if (!isWon)
            GameData.LEVEL = 0;
        else
        {
            GameData.LEVEL++;
            levelText.text = "Level " + "GameData.LEVEL";
        }
        //GameData.BALL_COUNT = 0;
        resultButton.gameObject.SetActive(false);
        resultButton.enabled = false;
        GameData.BALL_COUNT = 0;
        GameData.RESULT_BALL_COUNT = 0;
        GameData.IS_STAGE_TWO = false;
        GameData.IS_STAGE_TWO_STARTED = false;
        GameData.IS_SET_BALL_COUNT = false;
        GameData.IS_SET_BALL_COUNT_NEXT = false;
        obstacleParent.SetActive(true);
        SceneManager.LoadScene("GameplayScene");
    }
}