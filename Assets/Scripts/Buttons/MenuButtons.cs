using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    private GameObject _gameManagerOB;
    private GameManagerController _gameManagerScript;

    private void Awake() {
        _gameManagerOB = GameObject.FindGameObjectWithTag("GameManager");
        _gameManagerScript = _gameManagerOB.GetComponent<GameManagerController>();
    }
    public void StartGame(){
        SceneManager.LoadScene(1);
    }

    public void QuitGame(){
        Application.Quit();
    }

    public void RestartGame(){
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void ResumeGame(){
        _gameManagerScript.ClosePauseMenu();
        Time.timeScale = 1;
    }


    
}
