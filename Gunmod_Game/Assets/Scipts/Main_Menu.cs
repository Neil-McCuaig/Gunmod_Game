using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    GameManager manager;
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindAnyObjectByType<GameManager>();
        player = FindAnyObjectByType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayButtonPressed()
    {
        SceneManager.LoadScene(1);
    }

    public void OnQuitButtonPressed()
    {
        Application.Quit();
    }

    public void OnReturnButtonPressed()
    {
        SceneManager.LoadScene(0);
    }

    public void OnBeginNearsightPressed()
    {
        //manager.gamemodeNearsight = true;
        manager.NearSightBegin();
        player.playerActive = true;
    }
}
