using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovesScript : MonoBehaviour
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
        GetComponent<Text>().text = "Moves: " + levelManager.getMoveCount().ToString();
    }
}
