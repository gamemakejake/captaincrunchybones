using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject creditsScreen;
    public GameObject howToPlayScreen;
    public string sceneNameP2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Start1P()
    {
        SceneManager.LoadScene(sceneName: "1P");
    }

    public void Start2P()
    {
        SceneManager.LoadScene(sceneName: sceneNameP2);
    }

    public void HowToPlay()
    {
        mainMenu.SetActive(false);
        howToPlayScreen.SetActive(true);
    }

    public void Credits()
    {
        mainMenu.SetActive(false);
        creditsScreen.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
