using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    private LevelManager levelManager;
    private bool isDisplayed = false;
    public Canvas menu;


    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.Find("Game Manager").GetComponent<LevelManager>();
        menu.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !levelManager.getDidWin() && !levelManager.getDidLose()) isDisplayed = !isDisplayed;
        if (levelManager.getDidWin() || levelManager.getDidLose()) isDisplayed = true;
        levelManager.setIsPaused(isDisplayed);
        menu.enabled = isDisplayed;
        GetComponentInChildren<Text>().text = (levelManager.getDidWin()) ? "You Win!" : (levelManager.getDidLose()) ? "=( You lost =(" : "Quit?";
    }
}
