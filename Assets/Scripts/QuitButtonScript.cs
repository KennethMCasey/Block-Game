using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class QuitButtonScript : MonoBehaviour
{
    public Button butn;
    // Start is called before the first frame update
    void Start()
    {
        Text text = butn.GetComponentInChildren<Text>();
        text.text = "Quit";
        butn.onClick.AddListener(Quit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Quit()
    {
        Application.Quit();
    }
}
