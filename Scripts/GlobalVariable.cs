using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariable 
{
    public static int Difficulty =2;
    public static int bulletDamage = 17;
    public static int zombieDamage = 8;
    public static float zombieMoveSpeed = 5f;
    public static float BGMVolume = 0.2f;
    public static float SoundVolume = 0.2f;

    public static void SetDiffculty(int d)
    {
        //1 简单,2 一般 3 困难

        switch(d)
        {
            case 1:
                bulletDamage = 25;
                zombieDamage = 5;
                zombieMoveSpeed = 4f;
                break;
            case 2:
                bulletDamage = 17;
                zombieDamage = 8;
                zombieMoveSpeed = 5f;
                break;
            default:
                bulletDamage = 10;
                zombieDamage = 12;
                zombieMoveSpeed = 6f;
                break;
        }
    }

}
