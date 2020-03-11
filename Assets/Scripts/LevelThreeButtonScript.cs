using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelThreeButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Button butn;

    void Start()
    {
        Text text = butn.GetComponentInChildren<Text>();
        text.text = "Level Three";
        butn.onClick.AddListener(LoadLevel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void LoadLevel()
    {
        SceneManager.LoadScene("LevelThree");
    }
}
