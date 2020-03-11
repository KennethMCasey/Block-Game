using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuButtonScript : MonoBehaviour
{
    public Button butn;
    // Start is called before the first frame update
    void Start()
    {
        Text text = butn.GetComponentInChildren<Text>();
        text.text = "Main Menu?";
        butn.onClick.AddListener(Back);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Back() {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
        
    }
}
