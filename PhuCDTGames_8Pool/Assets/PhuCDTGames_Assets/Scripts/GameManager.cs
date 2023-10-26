using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private enum CurrentPlayer
    {
        P1,
        P2
    }

    private CurrentPlayer currentPlayer;
    private bool isWinningShotP1 = false;
    private bool isWinningShotP2 = false;
    private int ballsRemainingP1 = 7;
    private int ballsRemainingP2 = 7;
    private bool isP1Solid; //This use to assign ball type
    private bool onBreak = true; //This will be true whenever the game start

    private GameObject cueBall;
    private GameObject eightBall;

    [SerializeField] private TextMeshProUGUI p1Stat;
    [SerializeField] private TextMeshProUGUI p2Stat;
    [SerializeField] private TextMeshProUGUI bigText;
    [SerializeField] private GameObject restartButton;
    [SerializeField] Transform headPosition;
    [SerializeField] private PlayerController player1;
    [SerializeField] private PlayerController player2;

    private void Start()
    {
        cueBall = GameObject.FindGameObjectWithTag("Cue Ball");
        eightBall = GameObject.FindGameObjectWithTag("8 Ball");
    }

    private void SwitchPlayer()
    {
        if (currentPlayer == CurrentPlayer.P1)
        {
            currentPlayer = CurrentPlayer.P2;
        }
        else if (currentPlayer == CurrentPlayer.P2)
        {
            currentPlayer = CurrentPlayer.P1;
        }
    }

    private void OnBallDrop(Ball ball)
    {
        if (ball.getBallValue() == 0) //Cue ball
        {
            if (!eightBall.GetComponent<Ball>().getIsAppear()) //If 8 ball is not appeared (pocketed)
            {
                //Make the opposite player win
            }
        }
        else if (ball.getBallValue() == 8) //8 ball
        {
            if (onBreak) //If this is the break, set the ball to the head Pos
            {
                ball.transform.position = headPosition.position;
            }
            else //If this is the winning shot, then if the cue ball is potted
            {
            }
        }
        else if (ball.getBallValue() >= 1 && ball.getBallValue() <= 7) //Solid ball
        {
            if (currentPlayer == CurrentPlayer.P1)
            {
                if (player1.getBallType() == 0) //Player 1 assigned with solid
                {

                }
                else //Player 1 assigned with stripe
                {

                }
            }
            else
            {
                if (player2.getBallType() == 0) //Player 1 assigned with solid
                {

                }
                else //Player 1 assigned with stripe
                {

                }
            }
        }
        else if (ball.getBallValue() >= 9 && ball.getBallValue() <= 15) //Stripe ball
        {
            if (currentPlayer == CurrentPlayer.P1)
            {
                if (player1.getBallType() == 1) //Player 1 assigned with stripe
                {

                }
                else //Player 1 assigned with stripe
                {

                }
            }
            else
            {
                if (player2.getBallType() == 1) //Player 1 assigned with stripe
                {

                }
                else //Player 1 assigned with stripe
                {

                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        OnBallDrop(other.gameObject.GetComponent<Ball>());

        if (other.gameObject.CompareTag("Cue Ball")) //Cue ball pocketed
        {
            //Switch Player
            //Or Becoming Lose when 8 ball and cue ball pocketed at the same time (On winning shot)
        }
        else if (other.gameObject.CompareTag("8 Ball")) //8 ball pocketed
        {
            //Place the ball at the head position (At the break)
            //Or Becoming Lose when 8 ball and cue ball pocketed at the same time (On winning shot)
            //Or Becmoming Lose
        }
        else if (other.gameObject.CompareTag("Solid Ball")) //Solid ball pocketed
        {
            //If the current player assigned solid, keep continue
            //If not, switch player
            //Minus the remaining
        }
        else if (other.gameObject.CompareTag("Cue Ball")) //Stripe ball pocketed
        {
            //If the current player assigned stripe, keep continue
            //If not, switch player
            //Minus the remaining
        }
    }
}
