using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GAMESTATE { Cannon, Dog, DiggingUp, End }
    public GAMESTATE currentGameState;

    private int roundCount;
    public int maxRounds;
    public GameObject P1UI, P2UI;
    public GameObject player;
    public GameObject renderTexCamera;
    public EndScreen endScreen;
    public GameObject endScreenUI;
    public void Start()
    {
        //Cursor actually performs like in an FPS
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        ResetGame(0);
    }

    public void Update()
    {
    }

    public void ResetGame(int WaitTime)
    {
        if(roundCount < maxRounds)
        {
            roundCount++;
            StartCoroutine(NextRoundSetup(WaitTime));
        }
        else
        {
            EndGame();
        }
    }

    public void EndGame()
    {
        currentGameState = GAMESTATE.End;
        endScreenUI.SetActive(true);
        endScreen.enabled = true;
    }
    // Start Playing as Dog
    public IEnumerator SwitchToPlayerState()
    {
        yield return new WaitForSeconds(1);
        //Take the Picture
        Debug.Log(FindObjectOfType<CameraCapture>());
        FindObjectOfType<CameraCapture>().GetComponent<CameraCapture>().CamCapture();
        FindObjectOfType<CameraCapture>().gameObject.SetActive(false);
        this.gameObject.GetComponent<Scoring>().enabled = true;
        player.SetActive(true);
        //player.GetComponent<PlayerController>().numBarks = 0;
        player.GetComponent<PlayerController>().currentState = PlayerController.STATES.Moving;
        this.gameObject.GetComponent<Pause>().paused = false;
        currentGameState = GAMESTATE.Dog;
    }

    //Reset after dog found bone :]
    public IEnumerator NextRoundSetup(int WaitTime)
    {
        if (this.GetComponent<Scoring>().P1 == true)
        {
            P1UI.SetActive(true);
        }
        if (this.GetComponent<Scoring>().P1 == false)
        {
            P2UI.SetActive(true);
        }

        //Delete the Bone
        FindObjectOfType<CannonScript>().GetComponent<CannonScript>().DeleteShot();
        yield return new WaitForSeconds(WaitTime);
        //Reenable the render texture camera
        renderTexCamera.SetActive(true);
        player.SetActive(false);
        //Switch Player In Scoring
        this.gameObject.GetComponent<Scoring>().SwitchPlayer();
        this.gameObject.GetComponent<Scoring>().enabled = false;
        //Reset Image
        FindObjectOfType<CameraCapture>().GetComponent<CameraCapture>().EraseImage();
        //Reset Cannon
        FindObjectOfType<CannonScript>().GetComponent<CannonScript>().CannonStarts();
        currentGameState = GAMESTATE.Cannon;
    }
}
