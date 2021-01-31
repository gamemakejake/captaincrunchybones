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
    private float autoScroll;
    private GameObject gameController;
    private int scoreRevealCounter;
    public GameObject player;
    public Text scoreTimeP1;
    public Text scoreBarksP1;
    public Text scoreTotalP1;
    public Text scoreTimeP2;
    public Text scoreBarksP2;
    public Text scoreTotalP2;
    public Text winner;
    private GameObject pauseController;
    public bool singlePlayer;
    public GameObject p1WinImage;
    public GameObject p2WinImage;

    // Start is called before the first frame update
    void Start()
    {
        gameController = this.gameObject;
        //player = GameObject.FindGameObjectWithTag("Player");
        pauseController = this.gameObject;
        this.gameObject.GetComponent<Pause>().paused = true;
        autoScroll = 1f;
        // singlePlayer = player.GetComponent<PlayerController>().singlePlayer;
        singlePlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        autoScroll -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) || autoScroll <= 0f)
        {
            scoreRevealCounter++;
            autoScroll = 1f;
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
            /*if (scoreRevealCounter > 1)
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
                        */
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
            if (visualTimeScoreP1 < this.gameObject.GetComponent<Scoring>().currentScoreP1)
            {
                visualTimeScoreP1 += Mathf.Min(Time.deltaTime * scoreUpSpeed, this.gameObject.GetComponent<Scoring>().currentScoreP1);
                visualTimeScoreP1 = Mathf.FloorToInt(visualTimeScoreP1);
                scoreTimeP1.text = "Time: " + visualTimeScoreP1.ToString();
            }
        }
        else
        {
            if (visualTimeScoreP2 < this.gameObject.GetComponent<Scoring>().currentScoreP2)
            {
                visualTimeScoreP2 += Mathf.Min(Time.deltaTime * scoreUpSpeed, this.gameObject.GetComponent<Scoring>().currentScoreP2);
                visualTimeScoreP2 = Mathf.FloorToInt(visualTimeScoreP2);
                scoreTimeP2.text = "Time: " + visualTimeScoreP2.ToString();
            }
            else
            {
                RevealWinner();
            }
        }

    }

    public void RevealBarkScores(bool P1)
    {
        if (P1)
        {
            if (visualBarkScoreP1 < player.GetComponent<PlayerController>().numBarks[0] * barkPenalty)
            {
                visualBarkScoreP1 += Mathf.Min(Time.deltaTime * scoreUpSpeed, player.GetComponent<PlayerController>().numBarks[0] * barkPenalty);
                visualBarkScoreP1 = Mathf.FloorToInt(visualBarkScoreP1);
                scoreBarksP1.text = "Barks: " + visualBarkScoreP1;
            }
        }
        else
        {
            if (visualBarkScoreP2 < player.GetComponent<PlayerController>().numBarks[1] * barkPenalty)
            {
                visualBarkScoreP2 += Mathf.Min(Time.deltaTime * scoreUpSpeed, player.GetComponent<PlayerController>().numBarks[1] * barkPenalty);
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
        }
    }

    public void RevealWinner()
    {
        if (visualTimeScoreP1 >= visualTimeScoreP2)
        {
            winner.text = "Player 1 Wins!";
            p1WinImage.SetActive(true);
        }
        else
        {
            winner.text = "Player 2 Wins!";
            p2WinImage.SetActive(true);
        }
    }
}
