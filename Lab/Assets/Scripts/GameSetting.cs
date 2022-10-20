using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameSetting : MonoBehaviour
{

    private void OnEnable()
    {
        Time.timeScale = 0f;
    }
    public void BtnResume()
    {
        UIManager.instance.ToGamePlay();
    }
    public void BtnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void BtnHome()
    {

    }
}
