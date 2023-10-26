using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private bool isSolidBall;
    private bool is8Ball = false;
    private bool isCueBall = false;
    private int ballValue;
    private bool isAppear;

    public bool getIsSolidBall()
    {
        return isSolidBall;
    }

    public bool getIs8Ball()
    {
        return is8Ball;
    }

    public bool getIsCueBall()
    {
        return isCueBall;
    }

    public int getBallValue()
    {
        return ballValue;
    }

    public bool getIsAppear()
    {
        return isAppear;
    }

    public void setSolidBall(bool value)
    {
        isSolidBall = value;
    }

    public void set8Ball(bool value)
    {
        is8Ball = value;
    }

    public void setCueBall(bool value)
    {
        isCueBall = value;
    }

    public void setBallValue(int value)
    {
        ballValue = value;
    }

    public void setIsAppear(bool value)
    {
        isAppear = value;
    }
}
