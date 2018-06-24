using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScreenManager : MonoBehaviour {

    [SerializeField]
    Button restart;
    [SerializeField]
    Button exit;

    void Start()
    {
        restart.onClick.AddListener(RestartScene);
        exit.onClick.AddListener(ExitScene);
    }

    void RestartScene()
    {
        Debug.Log("Load Scene");
        SceneManager.LoadScene("East");
    }

    void ExitScene()
    {
        Debug.Log("Exit Scene");
        Application.Quit();
    }
}
