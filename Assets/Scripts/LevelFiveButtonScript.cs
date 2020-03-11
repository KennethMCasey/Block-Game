﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelFiveButtonScript : MonoBehaviour
{

    public Button butn;
    // Start is called before the first frame update
    void Start()
    {
        Text text = butn.GetComponentInChildren<Text>();
        text.text = "Level Five";
        butn.onClick.AddListener(LoadLevel);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void LoadLevel()
    {
        SceneManager.LoadScene("LevelFive");
    }
}
