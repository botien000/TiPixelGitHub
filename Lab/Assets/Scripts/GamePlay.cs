using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GamePlay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtCore;
    [SerializeField] private List<GameObject> hearts;

    private int curScore;
    // Start is called before the first frame update
    void Start()
    {
        curScore = 0;
        ShowTextScore(curScore);
    }
    private void OnEnable()
    {
        Time.timeScale = 1;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public int IncrScore()
    {
        return ++curScore;
    }
    public void ShowTextScore(int score)
    {
        txtCore.text = "x" + score;
    }
    public void ToSetting()
    {
        UIManager.instance.ToGameSetting();
    }
    public void HitPlayer()
    {
        Destroy(hearts[hearts.Count - 1]);
        hearts.RemoveAt(hearts.Count - 1);
    }
}
