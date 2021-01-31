using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    public int barkPenalty;
    public float visualTimeScoreP1;
    public float visualBarkScoreP1;
    public float visualTotalScoreP1;
    public float visualTimeScoreP2;
    public float visualBarkScoreP2;
    public float visualTotalScoreP2;
    public float scoreUpSpeed;
    private GameObject gameController;
    private int scoreRevealCounter;
    private GameObject player;
    public Text scoreTimeP1;
    public Text scoreBarksP1;
    public Text scoreTotalP1;
    public Text scoreTimeP2;
    public Text scoreBarksP2;
    public Text scoreTotalP2;
    public Text winner;
    private GameObject pauseController;
    public bool singlePlayer;
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindWithTag("GameController");
        player = GameObject.FindWithTag("Player");
        pauseController = GameObject.FindWithTag("PauseController");
        pauseController.GetComponent<Pause>().paused = true;
        // singlePlayer = player.GetComponent<PlayerController>().singlePlayer;
        singlePlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            scoreRevealCounter++;
        }
        if (gameObject.activeSelf)
        {
            if (scoreRevealCounter > 0)
            {
                RevealTimeScores(true);
                if (!singlePlayer)
                {
                    RevealTimeScores(false);
                }
            }
            if (scoreRevealCounter > 1)
            {
                RevealBarkScores(true);
                if (!singlePlayer)
                {
                    RevealBarkScores(false);
                }
            }
            if (scoreRevealCounter > 2)
            {
                RevealTotalScores(true);
                if (!singlePlayer)
                {
                    RevealTotalScores(false);
                }
            }
        }
    }


    public void GoToMainMenu()
    {
        SceneManager.LoadScene(sceneBuildIndex: 1);
    }

    public void RevealTimeScores(bool P1)
    {
        if (P1)
        {
            if (visualTimeScoreP1 < gameController.GetComponent<Scoring>().currentScoreP1)
            {
                visualTimeScoreP1 += Mathf.Min(Time.deltaTime * scoreUpSpeed, gameController.GetComponent<Scoring>().currentScoreP1);
                visualTimeScoreP1 = Mathf.FloorToInt(visualTimeScoreP1);
                scoreTimeP1.text = "Time: " + visualTimeScoreP1;
            }
        }
        else
        {
            if (visualTimeScoreP2 < gameController.GetComponent<Scoring>().currentScoreP2)
            {
                visualTimeScoreP2 += Mathf.Min(Time.deltaTime * scoreUpSpeed, gameController.GetComponent<Scoring>().currentScoreP2);
                visualTimeScoreP2 = Mathf.FloorToInt(visualTimeScoreP2);
                scoreTimeP2.text = "Time: " + visualTimeScoreP2;
            }
        }
    }

    public void RevealBarkScores(bool P1)
    {
        if (P1)
        {
            if (visualBarkScoreP1 < 500) //player.GetComponent<PlayerController>().numBarks * barkPenalty)
            {
                visualBarkScoreP1 += Mathf.Min(Time.deltaTime * scoreUpSpeed, 500);
                visualBarkScoreP1 = Mathf.FloorToInt(visualBarkScoreP1);
                scoreBarksP1.text = "Barks: " + visualBarkScoreP1;
            }
        }
        else
        {
            if (visualBarkScoreP2 < 500) //player.GetComponent<PlayerController>().numBarks * barkPenalty)
            {
                visualBarkScoreP2 += Mathf.Min(Time.deltaTime * scoreUpSpeed, 500);
                visualBarkScoreP2 = Mathf.FloorToInt(visualBarkScoreP2);
                scoreBarksP2.text = "Barks: " + visualBarkScoreP2;
            }
        }
    }
    public void RevealTotalScores(bool P1)
    {
        if (P1)
        {
            if (visualTotalScoreP1 < (visualTimeScoreP1 - visualBarkScoreP1))
            {
                visualTotalScoreP1 += Mathf.Min(Time.deltaTime * scoreUpSpeed, visualTimeScoreP1 - visualBarkScoreP1);
                visualTotalScoreP1 = Mathf.FloorToInt(visualTotalScoreP1);
                scoreTotalP1.text = "Total: " + visualTotalScoreP1;
            }
        }
        else
        {
            if (visualTotalScoreP2 < (visualTimeScoreP2 - visualBarkScoreP2))
            {
                visualTotalScoreP2 += Mathf.Min(Time.deltaTime * scoreUpSpeed, visualTimeScoreP2 - visualBarkScoreP2);
                visualTotalScoreP2 = Mathf.FloorToInt(visualTotalScoreP2);
                scoreTotalP2.text = "Total: " + visualTotalScoreP2;
            }
            else
            {
                RevealWinner();
            }
        }
    }

    public void RevealWinner()
    {
        if (visualTotalScoreP1 >= visualTotalScoreP2)
        {
            winner.text = "Player 1 Wins!";
        }
        else
        {
            winner.text = "Player 2 Wins!";
        }
    }
}
