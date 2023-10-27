using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isMyTurn;
    public bool isWin;
    public bool isWinningShot;
    public bool isSolid;
    public bool hasAssigned = false;

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

    public void setHasAssigned(bool value)
    {
        hasAssigned = value;
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
            return 0; //Is solid
        }
        else
        {
            return 1; //Is Stripe
        }
    }

    public bool getHasAssigned()
    {
        return hasAssigned;
    }
}
