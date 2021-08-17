using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitXp : MonoBehaviour {

    public float level;

    public float xp;

   public float nextLevel;

    float maxLevel;

    void Update()
    {
        if (level <= 14)
        {

            if (xp >= nextLevel)
            {
                nextLevel *= 2f;
                level += 1;
            }
        }
        else
        {
            level = 15;
            xp = nextLevel;
        }
    }


}
