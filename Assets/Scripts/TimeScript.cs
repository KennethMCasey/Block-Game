using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeScript : MonoBehaviour
{
    private LevelManager levelManager;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.Find("Game Manager").GetComponent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {

        GetComponent<Text>().text = "Time: " + Mathf.Round(levelManager.time).ToString();
    }
}
