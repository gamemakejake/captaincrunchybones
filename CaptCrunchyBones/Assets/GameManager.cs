using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GAMESTATE { Cannon, Dog, End }
    public GAMESTATE currentGameState;

    public GameObject player;
    public void Start()
    {
        //Cursor actually performs like in an FPS
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Update()
    {
    }

    public IEnumerator SwitchToPlayerState()
    {
        yield return new WaitForSeconds(1);
        player.SetActive(true);
        player.GetComponent<PlayerController>().numBarks = 0;
        currentGameState = GAMESTATE.Dog;
    }
}
