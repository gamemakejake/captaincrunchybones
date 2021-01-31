using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
    public float maxScore;
    public float timer;
    public float currentScoreP1;
    public float currentScoreP2;
    public Text scoreText;
    public float scoreDropSpeed;
    public GameObject pauseController;
    public bool P1;
    public GameObject endScreen;
    // Start is called before the first frame update
    void Start()
    {
        //pauseController = GameObject.FindGameObjectWithTag("GameController");
        //P1 = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Hi I exist.");
        //Debug.Log("Is Paused: " + pauseController.GetComponent<Pause>().paused);
        if (pauseController.GetComponent<Pause>().paused == false)
        {
            if (P1)
            {
                timer += Time.deltaTime * scoreDropSpeed;
                currentScoreP1 = maxScore - timer;
                currentScoreP1 = Mathf.FloorToInt(currentScoreP1);
                scoreText.text = "Score: " + currentScoreP1;
            }
            else
            {
                timer += Time.deltaTime * scoreDropSpeed;
                currentScoreP2 = maxScore - timer;
                currentScoreP2 = Mathf.FloorToInt(currentScoreP2);
                scoreText.text = "Score: " + currentScoreP2;
            }
        }

        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    SwitchPlayer();
        //}

        //if (Input.GetKeyDown(KeyCode.V))
        //{
        //    endScreen.SetActive(true);
        //}
    }

    public void SwitchPlayer()
    {
        P1 = !P1;
        timer = 0;
        scoreText.text = null;
    }
}
