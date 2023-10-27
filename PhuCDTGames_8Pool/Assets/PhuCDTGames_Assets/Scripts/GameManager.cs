using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Reflection;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private enum CurrentPlayer
    {
        P1,
        P2
    }

    private CurrentPlayer currentPlayer = CurrentPlayer.P1;
    private bool isWinningShotP1 = false;
    private bool isWinningShotP2 = false;
    private int solidBallRemaining = 7;
    private int stripeBallRemaining = 7;
    private bool onBreak = true; //This will be true whenever the game start

    private GameObject cueBall;
    private GameObject eightBall;

    [SerializeField] Transform headPosition;
    [SerializeField] private PlayerController player1;
    [SerializeField] private PlayerController player2;

    private void Start()
    {
        cueBall = GameObject.FindGameObjectWithTag("Cue Ball");
        eightBall = GameObject.FindGameObjectWithTag("8 Ball");
        onBreak = true; //Need to assigned which shot is onBreak (has a temporary assigned)
        if (currentPlayer == CurrentPlayer.P1)
        {
            print("Current Player: Player 1");
        }
        else
        {
            print("Current Player: Player 2");
        }
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
            ball.setIsAppear(false); //For Cue ball, when potted, just set isAppear = false
            if (!eightBall.GetComponent<Ball>().getIsAppear()) //If 8 ball is not appeared (pocketed)
            {
                //Make the opposite player win
            }
            else
            {
                ball.transform.position = headPosition.position;
                UIManager.Instance.setBigText(PlayerName() + " has pocketed the Cue Ball!", 1f);
                SwitchPlayer();
            }
        }
        else if (ball.getBallValue() == 8) //8 ball
        {
            ball.setIsAppear(false); //Same with cue ball, set 8 ball isAppear = false
            if (onBreak) //If this is the break, set the ball to the head Pos
            {
                ball.transform.position = headPosition.position;
            }
            else //If this is the winning shot, then if the cue ball is potted
            {
                if (currentPlayer == CurrentPlayer.P1)
                {
                    if (player1.getIsWinningShot())
                    {
                        if (!cueBall.GetComponent<Ball>().getIsAppear())
                        {
                            //Player 1 Lose because potted both cue ball and eight ball
                            UIManager.Instance.setBigText("Player 1 Lose!", 3f);
                            UIManager.Instance.ShowRestartButton();
                        }
                        else
                        {
                            UIManager.Instance.setBigText("Player 1 Win!", 3f);
                            UIManager.Instance.ShowRestartButton();
                            //restartButton.SetActive(true);
                            //Player 1 Win
                        }
                    }
                    else
                    {
                        //print("Player 1 Lose");
                        UIManager.Instance.setBigText("Player 1 Lose!", 3f);
                        UIManager.Instance.ShowRestartButton();
                        //restartButton.SetActive(true);
                        //Player 1 Lose
                    }
                }
                else
                {
                    if (player2.getIsWinningShot())
                    {
                        if (!cueBall.GetComponent<Ball>().getIsAppear())
                        {
                            //print("Player 2 Lose");
                            UIManager.Instance.setBigText("Player 2 Lose!", 3f);
                            UIManager.Instance.ShowRestartButton();
                            //restartButton.SetActive(true);
                            //Player 2 Lose because potted both cue ball and eight ball
                        }
                        else
                        {
                            //print("Player 2 Win");
                            UIManager.Instance.setBigText("Player 2 Win!", 3f);
                            UIManager.Instance.ShowRestartButton();
                            //restartButton.SetActive(true);
                            //Player 2 Win
                        }
                    }
                    else
                    {
                        print("Player 2 Lose");
                        UIManager.Instance.setBigText("Player 2 Lose!", 3f);
                        UIManager.Instance.ShowRestartButton();
                        //restartButton.SetActive(true);
                        //Player 2 Lose
                    }
                }
            }
        }
        else if (ball.getBallValue() >= 1 && ball.getBallValue() <= 7) //Solid ball
        {
            Destroy(ball.gameObject);
            if (currentPlayer == CurrentPlayer.P1)
            {
                if (player1.getBallType() == 0) //Player 1 assigned with solid, legal hit
                {
                    solidBallRemaining--;
                    UIManager.Instance.setP1Stat(solidBallRemaining, 0);
                    UIManager.Instance.setP2Stat(stripeBallRemaining, 1);
                }
                else //Player 1 assigned with stripe, illegal
                {
                    SwitchPlayer();
                    solidBallRemaining--;
                    UIManager.Instance.setP2Stat(solidBallRemaining, 0);
                    UIManager.Instance.setP1Stat(stripeBallRemaining, 1);
                }
            }
            else
            {
                if (player2.getBallType() == 0) //Player 2 assigned with solid, legal
                {
                    solidBallRemaining--;
                    UIManager.Instance.setP2Stat(solidBallRemaining, 0);
                    UIManager.Instance.setP1Stat(stripeBallRemaining, 1);
                }
                else //Player 2 assigned with stripe, illegal
                {
                    SwitchPlayer();
                    solidBallRemaining--;
                    UIManager.Instance.setP1Stat(solidBallRemaining, 0);
                    UIManager.Instance.setP2Stat(stripeBallRemaining, 1);
                }
            }
        }
        else if (ball.getBallValue() >= 9 && ball.getBallValue() <= 15) //Stripe ball
        {
            Destroy(ball.gameObject);
            if (currentPlayer == CurrentPlayer.P1)
            {
                if (player1.getBallType() == 1) //Player 1 assigned with stripe, legal
                {
                    stripeBallRemaining--;
                    UIManager.Instance.setP1Stat(stripeBallRemaining, 1);
                    UIManager.Instance.setP2Stat(solidBallRemaining, 0);
                }
                else //Player 1 assigned with stripe, illegal
                {
                    SwitchPlayer();
                    stripeBallRemaining--;
                    UIManager.Instance.setP2Stat(stripeBallRemaining, 1);
                    UIManager.Instance.setP1Stat(solidBallRemaining, 0);
                }
            }
            else
            {
                if (player2.getBallType() == 1) //Player 2 assigned with stripe, legal
                {
                    stripeBallRemaining--;
                    UIManager.Instance.setP2Stat(stripeBallRemaining, 1);
                    UIManager.Instance.setP1Stat(solidBallRemaining, 0);
                }
                else //Player 2 assigned with stripe, iilegal
                {
                    SwitchPlayer();
                    stripeBallRemaining--;
                    UIManager.Instance.setP1Stat(stripeBallRemaining, 1);
                    UIManager.Instance.setP2Stat(solidBallRemaining, 0);
                }
            }
        }

        //See if which player has winning shot, base of ball remaining
        if (solidBallRemaining == 0) //If no more solid ball on the table
        {
            if (player1.getBallType() == 0) //If Player1 assigned with solid, wiinning shot = true
            {
                player1.setIsWinningShot(true);
            }
            else //If Player2 assigned with solid, wiinning shot = true
            {
                player2.setIsWinningShot(true);
            }
        }
        else if (stripeBallRemaining == 0) //If no more stripe ball on the table
        {
            if (player1.getBallType() == 1) //If Player1 assigned with stripe, wiinning shot = true
            {
                player1.setIsWinningShot(true);
            }
            else //If Player2 assigned with stripe, wiinning shot = true
            {
                player2.setIsWinningShot(true);
            }
        }

       
    }

    private void AssignBall(Ball ball)
    {
        //Assign player's ball
        onBreak = false; //Set if just a ball pocketed, it is not a break anymore (temporary)
        if (currentPlayer == CurrentPlayer.P1 && !player1.getHasAssigned()) //If current player is player 1 and hasn't get assigned
        {
            if (ball.getBallValue() >= 1 && ball.getBallValue() <= 7) //If player 1 pocketed solid first
            {
                player1.setSolid(); //Set P1 is Solid, P2 is Stripe
                player2.setStripe();
                player1.setHasAssigned(true);
                player2.setHasAssigned(true);
                UIManager.Instance.setBigText("P1 is Solid, P2 is Stripe!", 3f);
            }
            else if (ball.getBallValue() >= 9 && ball.getBallValue() <= 15) //If player 1 pocketed stripe first
            {
                player1.setStripe();
                player2.setSolid();
                player1.setHasAssigned(true);
                player2.setHasAssigned(true);
                UIManager.Instance.setBigText("P1 is Stripe, P2 is Solid!", 3f);
            }
        }
        else if (currentPlayer == CurrentPlayer.P2 && !player2.getHasAssigned()) //If current player is player 2 and hasn't get assigned
        {
            if (ball.getBallValue() >= 1 && ball.getBallValue() <= 7) //If player 2 pocketed solid first
            {
                player2.setSolid();
                player1.setStripe();
                player2.setHasAssigned(true);
                player1.setHasAssigned(true);
                UIManager.Instance.setBigText("P2 is Solid, P1 is Stripe!", 3f);
            }
            else if (ball.getBallValue() >= 9 && ball.getBallValue() <= 15) //If player 2 pocketed stripe first
            {
                player2.setStripe();
                player1.setSolid();
                player2.setHasAssigned(true);
                player1.setHasAssigned(true);
                UIManager.Instance.setBigText("P2 is Stripe, P1 is Solid!", 3f);
            }
        }
    }

    private void OnTriggerEnter(Collider other) //This will trigger when a ball ios pocketed
    {
        ClearLog();
        AssignBall(other.gameObject.GetComponent<Ball>());
        OnBallDrop(other.gameObject.GetComponent<Ball>());
        UIManager.Instance.setPlayerTurn(PlayerName());
        print("Is Cue ball Dropped: " + !cueBall.GetComponent<Ball>().getIsAppear());
        print("Is 8 ball Dropped: " + !eightBall.GetComponent<Ball>().getIsAppear());
    }

    public void ClearLog() //Just for debugging
    {
        var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }

    public string PlayerName()
    {
        if (currentPlayer == CurrentPlayer.P1)
        {
            return "Player 1";
        }
        else
        {
            return "Player 2";
        }
    }
}
