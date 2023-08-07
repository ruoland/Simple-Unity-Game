using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    public static UIManager instance { get { return _instance; } }

    public GameObject hpTextObj = null;
    public GameObject gameOverObj = null;

    private void Awake()
    {
        _instance = this;
    }

    public void ShowHP(int currentHP, int maxHP)
    {
        hpTextObj.GetComponent<TextMeshProUGUI>().text = currentHP.ToString() + "//" + maxHP.ToString();
    }
    public void ShowGameOver()
    {
        gameOverObj.GetComponent<TextMeshProUGUI>().text = "GAME OVER";
        gameOverObj.SetActive(true);
    }
    public void ShowStageClear()
    {
        gameOverObj.GetComponent<TextMeshProUGUI>().text = "STAGE CLEAR";
        gameOverObj.SetActive(true);
    }
}
