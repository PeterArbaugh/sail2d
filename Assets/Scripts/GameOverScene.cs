using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScene : MonoBehaviour {

    [SerializeField]
    Button restart;
    Button exit;

    void Start()
    {
        restart.onClick.AddListener(RestartScene);
        exit.onClick.AddListener(ExitScene);
    }

    // Update is called once per frame
	void Update () {
		
	}

    void RestartScene()
    {
        //SceneManager.LoadScene("");
    }

    void ExitScene()
    {
        Application.Quit();
    }
}
