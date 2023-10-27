using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private TextMeshProUGUI p1Stat;
    [SerializeField] private TextMeshProUGUI p2Stat;
    [SerializeField] private TextMeshProUGUI bigText;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private TextMeshProUGUI playerTurn;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void setP1Stat(int number, int ballType)
    {
        if (ballType == 0) //Mean Solids
        {
            if (number > 1)
            {
                p1Stat.text = "Player 1 has " + number + " solids left";
            }
            else
            {
                p1Stat.text = "Player 1 has " + number + " solid left";
            }
        }
        else //Stripes
        {
            if (number > 1)
            {
                p1Stat.text = "Player 1 has " + number + " stripes left";
            }
            else
            {
                p1Stat.text = "Player 1 has " + number + " stripe left";
            }
        }
    }

    public void setP2Stat(int number, int ballType)
    {
        if (ballType == 0) //Mean Solids
        {
            if (number > 1)
            {
                p2Stat.text = "Player 2 has " + number + " solids left";
            }
            else
            {
                p2Stat.text = "Player 2 has " + number + " solid left";
            }
        }
        else //Stripes
        {
            if (number > 1)
            {
                p2Stat.text = "Player 2 has " + number + " stripes left";
            }
            else
            {
                p2Stat.text = "Player 2 has " + number + " stripe left";
            }
        }
    }

    public void setBigText(string text, float time)
    {
        bigText.gameObject.SetActive(true);
        bigText.text = text;
        StartCoroutine(TextDisplay(bigText.gameObject, time));
    }

    IEnumerator TextDisplay(GameObject text , float time)
    {
        yield return new WaitForSeconds(time);
        text.SetActive(false);
    }

    public void setPlayerTurn(string playerName)
    {
        playerTurn.text = playerName + "'s Turn";
    }

    public void ShowRestartButton()
    {
        restartButton.SetActive(true);
    }


    public void RestartButton_OnClick()
    {
        restartButton.SetActive(false);
        SceneManager.LoadScene(0);
    }
}
