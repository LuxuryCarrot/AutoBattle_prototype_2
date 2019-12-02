using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRarity : MonoBehaviour
{
    public static RandomRarity instance;

    private int iRandomCount = 4;

    private int iRandomNum;

    public string sRandomHeroName;

    // Start is called before the first frame update
    void Awake()
    {
        RandomRarity.instance = this;
    }

    public string RandomDawn()
    {
        iRandomNum = Random.Range(0, iRandomCount);

        if (iRandomNum == 0)
        {
            sRandomHeroName = "dCapsule";
        }
        else if (iRandomNum == 1)
        {
            sRandomHeroName = "ParkWarrior";
        }
        else if (iRandomNum == 2)
        {
            sRandomHeroName = "ParkShield";
        }
        else if (iRandomNum == 3)
        {
            sRandomHeroName = "dSphere";
        }
        return sRandomHeroName;
    }

    public string RandomSunrise()
    {
        iRandomNum = Random.Range(0, iRandomCount);

        if (iRandomNum == 0)
        {
            sRandomHeroName = "SrCapsule";
        }
        else if (iRandomNum == 1)
        {
            sRandomHeroName = "SrCube";
        }
        else if (iRandomNum == 2)
        {
            sRandomHeroName = "SrCylinder";
        }
        else if (iRandomNum == 3)
        {
            sRandomHeroName = "SrSphere";
        }
        return sRandomHeroName;
    }

    public string RandomLight()
    {
        iRandomNum = Random.Range(0, iRandomCount);

        if (iRandomNum == 0)
        {
            sRandomHeroName = "LCapsule";
        }
        else if (iRandomNum == 1)
        {
            sRandomHeroName = "LCube";
        }
        else if (iRandomNum == 2)
        {
            sRandomHeroName = "LCylinder";
        }
        else if (iRandomNum == 3)
        {
            sRandomHeroName = "LSphere";
        }
        return sRandomHeroName;
    }

    public string RandomSunset()
    {
        iRandomNum = Random.Range(0, iRandomCount);

        if (iRandomNum == 0)
        {
            sRandomHeroName = "SsCapsule";
        }
        else if (iRandomNum == 1)
        {
            sRandomHeroName = "SsCube";
        }
        else if (iRandomNum == 2)
        {
            sRandomHeroName = "SsCylinder";
        }
        else if (iRandomNum == 3)
        {
            sRandomHeroName = "SsSphere";
        }
        return sRandomHeroName;
    }

    public string RandomTwilight()
    {
        iRandomNum = Random.Range(0, iRandomCount);

        if (iRandomNum == 0)
        {
            sRandomHeroName = "TwCapsule";
        }
        else if (iRandomNum == 1)
        {
            sRandomHeroName = "TwCube";
        }
        else if (iRandomNum == 2)
        {
            sRandomHeroName = "TwCylinder";
        }
        else if (iRandomNum == 3)
        {
            sRandomHeroName = "TwSphere";
        }
        return sRandomHeroName;
    }
}
