using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    public int whoseTurn; //0 = Dennis and 1 = Tjeerd
    public int turnCounter; //Counts the number of turns played
    public GameObject[] turnIcon; //Displays who's turn it is
    public Sprite[] playerIcon; //0 = Dennis icon and 1 = Tjeerd icon
    public Button[] tictactoeSpaces; //playable spaces for the game
    public int[] markedSpaces; //which space is marked by which player
    public Text[] winnerText; //Hold the winning text
    public Text title; //Hold the title
    public GameObject[] winnerLinesDennis; //Hold all the lines when Dennis wins
    public GameObject[] winnerLinesTjeerd; //Hold all the lines when Tjeerd wins
    public GameObject winnerPanel; //Makes buttons uninteractable
    public Text scoreTextDennis; //Shows Dennis score
    public Text scoreTextTjeerd; //Shows Tjeerd score
    public int[] score; //Hold scores
    public GameObject tie;
    public AudioSource clickSound;
    public AudioSource winSound;
    public AudioSource tieSound;


    void Start()
    {
        GameSetup();
        for (int i = 0; i < score.Length; i++)
        {
            score[i] = 0;
        }
    }
    public void GameSetup()
    {
        
        whoseTurn = Random.Range(0,2);
        turnCounter = 0;
        turnIcon[0].SetActive(false);
        turnIcon[1].SetActive(false);
        turnIcon[whoseTurn].SetActive(true);
        tie.gameObject.SetActive(false);       
        for (int i = 0; i < tictactoeSpaces.Length; i++)
        {
            tictactoeSpaces[i].interactable = true;
            tictactoeSpaces[i].GetComponent<Image>().sprite = null;
        }       
        for (int i = 0; i < markedSpaces.Length; i++)
        {
            markedSpaces[i] = -100;
        }
        for (int i = 0; i < winnerText.Length; i++)
        {
            winnerText[i].gameObject.SetActive(false);
        }
        title.gameObject.SetActive(true);
        winnerPanel.gameObject.SetActive(false);
        for (int i = 0; i < winnerLinesDennis.Length; i++)
        {
            winnerLinesDennis[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < winnerLinesTjeerd.Length; i++)
        {
            winnerLinesTjeerd[i].gameObject.SetActive(false);
        }
    }
    void Update()
    {
        
    }
    public void TicTacToeButton(int whichNum)
    {
        clickSound.Play();
        tictactoeSpaces[whichNum].image.sprite = playerIcon[whoseTurn];
        tictactoeSpaces[whichNum].interactable = false;
        markedSpaces[whichNum] = whoseTurn;
        
            WinnerCheck();
            
        
        turnCounter++;
        if (turnCounter == 9)
        {
            tieSound.Play();

            title.gameObject.SetActive(false); 
            tie.gameObject.SetActive(true);
        }
        if (whoseTurn == 0)
        {
            turnIcon[1].SetActive(true);
            turnIcon[0].SetActive(false);
            whoseTurn = 1;
        }
        else
        {
            whoseTurn = 0;
            turnIcon[0].SetActive(true);
            turnIcon[1].SetActive(false);
        }
    }
    void WinnerCheck()
    {
        int s1 = markedSpaces[0] + markedSpaces[1] + markedSpaces[2];
        int s2 = markedSpaces[3] + markedSpaces[4] + markedSpaces[5];
        int s3 = markedSpaces[6] + markedSpaces[7] + markedSpaces[8];
        int s4 = markedSpaces[0] + markedSpaces[3] + markedSpaces[6];
        int s5 = markedSpaces[1] + markedSpaces[4] + markedSpaces[7];
        int s6 = markedSpaces[2] + markedSpaces[5] + markedSpaces[8];
        int s7 = markedSpaces[0] + markedSpaces[4] + markedSpaces[8];
        int s8 = markedSpaces[2] + markedSpaces[4] + markedSpaces[6];
        var solution = new int[] { s1, s2, s3, s4, s5, s6, s7, s8 };
        
        for (int i = 0; i < solution.Length; i++)
        {
            if (solution[i] == 0 || solution[i] == 3)
            {
                score[whoseTurn]++;
                winSound.Play();

                WinnerDisplay(i);
                break;
            }
        }
        turnIcon[whoseTurn].SetActive(false);       
    }
    void WinnerDisplay(int indexIn)
    {
        title.gameObject.SetActive(false);
        winnerText[whoseTurn].gameObject.SetActive(true);
        if (whoseTurn == 0)
        {
            winnerLinesDennis[indexIn].gameObject.SetActive(true);
            scoreTextDennis.text = score[whoseTurn].ToString();            
        }
        else
        {
            winnerLinesTjeerd[indexIn].gameObject.SetActive(true);
            scoreTextTjeerd.text = score[whoseTurn].ToString();
        }                        
        winnerPanel.gameObject.SetActive(true);
        turnCounter = 0;
        
    }
    public void Rematch()
    {
        clickSound.Play();
        GameSetup();
    }
    public void Restart()
    {
        clickSound.Play();
        GameSetup();
        for (int i = 0; i < score.Length; i++)
        {
            score[i] = 0;
        }      
            scoreTextDennis.text = score[whoseTurn].ToString();       
            scoreTextTjeerd.text = score[whoseTurn].ToString();       
    }
}