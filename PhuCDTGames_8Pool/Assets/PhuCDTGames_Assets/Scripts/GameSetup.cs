using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetup : MonoBehaviour
{
    private int solidBallRemaining = 7;
    private int stripeBallRemaining = 7;
    private float ballRadius;
    private float ballDiameter;

    private List<int> ballHasPlaced = new List<int>();

    [SerializeField] private GameObject[] ballPrefabs;
    [SerializeField] private Transform cueBallPos;
    [SerializeField] private Transform headballPos;

    private void Start()
    {
        //Set the ball radius and diameter
        ballRadius = ballPrefabs[0].GetComponent<SphereCollider>().radius * 100;
        ballDiameter = ballRadius * 2f;
        //Place all ball at the start of the game
        PlaceAllBalls();
    }

    void PlaceAllBalls()
    {
        //Place cue ball and all 15 other balls
        PlaceCueBall();
        PlaceRandomBall();
    }

    void PlaceCueBall() //Place cue ball
    {
        GameObject cueBall = Instantiate(ballPrefabs[0], cueBallPos.position, Quaternion.identity);
        cueBall.GetComponent<Ball>().setCueBall(true);
        //cueBall.GetComponent<Ball>().setIsAppear(true);
        cueBall.GetComponent<Ball>().setBallValue(0);
    }

    void Place8Ball(Vector3 placePos) //Place 8 ball
    {
        GameObject eightBall = Instantiate(ballPrefabs[8], placePos, Quaternion.identity);
        eightBall.GetComponent<Ball>().set8Ball(true);
        //eightBall.GetComponent<Ball>().setIsAppear(true);
        eightBall.GetComponent<Ball>().setBallValue(8);
    }

    void PlaceRandomBall()
    {
        int numInThisRow = 1;
        int rand;
        Vector3 firstInRowPos = headballPos.position;
        Vector3 currentPos = firstInRowPos;

        void PlaceSolidBall(Vector3 pos) //Place solid balls (1 - 7)
        {
            int solidBall;
            do //It will return -1 when the random method pick the occupied ball, random it again until it choose the correct one
            {
                solidBall = GenerateBallNumber(1, 8); //Generate random balls
            }
            while (solidBall == -1);

            if (solidBall != -1) //Instance ball and set the ball parameter
            {
                GameObject ball = Instantiate(ballPrefabs[solidBall], pos, Quaternion.identity);
                ball.GetComponent<Ball>().setSolidBall(true);
                ball.GetComponent<Ball>().setIsAppear(true);
                ball.GetComponent<Ball>().setBallValue(solidBall);
            }
        }

        void PlaceStripeBall(Vector3 pos) //same with solid balls, this is stripe balls (9 - 15)
        {
            int stripeBall;
            do
            {
                stripeBall = GenerateBallNumber(9, 16);
            }
            while (stripeBall == -1);


            if (stripeBall != -1)
            {
                GameObject ball = Instantiate(ballPrefabs[stripeBall], pos, Quaternion.identity);
                ball.GetComponent<Ball>().setSolidBall(true);
                ball.GetComponent<Ball>().setIsAppear(true);
                ball.GetComponent<Ball>().setBallValue(stripeBall);
            }
        }

        for (int i = 0; i < 5; i++) //5 rows
        {
            for (int j = 0; j < numInThisRow; j++) //Each row has 1, 2, 3, 4 and 5 balls
            {
                if (i == 2 && j == 1) //If the ball is centered, place 8 ball
                {
                    Place8Ball(currentPos);
                }
                else if (solidBallRemaining > 0 && stripeBallRemaining > 0) //When balls is not all placed, place one
                {
                    rand = Random.Range(0, 2); //Random if place solid and stripe balls
                    if (rand == 0)
                    {
                        PlaceSolidBall(currentPos);
                    }
                    else
                    {
                        PlaceStripeBall(currentPos);
                    }
                }
                else if (solidBallRemaining > 0) //If stripe balls is all placed, place solid
                {
                    PlaceSolidBall(currentPos);
                }
                else //If solid balls is all placed, place stripe
                {
                    PlaceStripeBall(currentPos);
                }

                currentPos += new Vector3(1, 0, 0).normalized * ballDiameter; //Set ball pos
            }
            firstInRowPos += Vector3.forward * (Mathf.Sqrt(3) * ballRadius) + Vector3.left * ballRadius; //Change ball pos for each one placed
            currentPos = firstInRowPos;
            numInThisRow++;
        }
    }

    int GenerateBallNumber(int rangeLow, int rangeHigh) //The input is Random Range
    {
        int ballNumber = Random.Range(rangeLow, rangeHigh); //Random ball number
        foreach (int ball in ballHasPlaced) //If random method make the one has occupied, return -1 value
        {
            if (ball == ballNumber)
            {
                return -1;
            }
        }

        ballHasPlaced.Add(ballNumber); //If it generated the ball hasn't occupied, add it to a List, called ballHasPlaced

        if (ballNumber >= 1 && ballNumber <= 7) //If random the solid ball, the remaining is less than 1
        {
            solidBallRemaining--;
        }
        else if (ballNumber >= 9 && ballNumber <= 15) //If random the stripe ball, the remaining is less than 1
        {
            stripeBallRemaining--;
        }
        return ballNumber; //Return ball number
    }
}
