using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private void OnEnable()
    {
        Time.timeScale = 0f;
    }
    public void BtnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
