using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public bool paused;
    public GameObject pauseScreen;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (paused)
            {
                UnpauseGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        paused = true;
        pauseScreen.SetActive(true);
    }

    public void UnpauseGame()
    {
        paused = false;
        pauseScreen.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
