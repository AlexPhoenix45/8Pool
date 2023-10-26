using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool isMyTurn;
    private bool isWin;
    private bool isWinningShot;
    private bool isSolid;

    public void setIsMyTurn(bool value)
    {
        isMyTurn = value;
    }

    public void setIsWin(bool value)
    {
        isWin = value;
    }

    public void setIsWinningShot(bool value)
    {
        isWinningShot = value;
    }

    public void setSolid()
    {
        isSolid = true;
    }

    public void setStripe()
    {
        isSolid = false;
    }

    public bool getIsMyTurn()
    {
        return isMyTurn;
    }

    public bool getIsWin()
    {
        return isWin;
    }

    public bool getIsWinningShot()
    {
        return isWinningShot;
    }

    public int getBallType()
    {
        if (isSolid)
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }
}
