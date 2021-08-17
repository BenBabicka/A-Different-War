using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class CountryTrade : MonoBehaviour
{

    bool X10;

    public StorageData data;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            X10 = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            X10 = false;
        }

        

    }

    #region Buy

   /* public void BuyWood()
    {
        if(X10)
        {
            if (data.Credits >= 10)
            {
                data.Wood += 10;
                data.Credits -= 10;
            }
        }
        if (!X10)
        {
            if (data.Credits >= 1)
            {
                data.Wood += 1;
                data.Credits -= 1;
            }
        }
    }
  
    public void BuyTextiles()
    {
        if (X10)
        {
            if (data.Credits >= 20)
            {
                data.Textiles += 10;
                data.Credits -= 20;
            }
        }
        if (!X10)
        {
            if (data.Credits >= 2)
            {
                data.Textiles += 1;
                data.Credits -= 2;
            }
        }
    }
    public void BuyFood()
    {
        if (X10)
        {
            if (data.Credits >= 10)
            {
                data.Food += 10;
                data.Credits -= 10;
            }
        }
        if (!X10)
        {
            if (data.Credits >= 1)
            {
                data.Food += 1;
                data.Credits -= 1;
            }
        }
    }
    public void BuyStone()
    {
        if (X10)
        {
            if (data.Credits >= 10)
            {
                data.Stone += 10;
                data.Credits -= 10;
            }
        }
        if (!X10)
        {
            if (data.Credits >= 1)
            {
                data.Stone += 1;
                data.Credits -= 1;
            }
        }
    }*/
    

    #endregion

    #region Sell
    /*
    public void SellWood()
    {
        if (X10)
        {
            if (data.Wood >= 10)
            {
                data.Wood -= 10;
                data.Credits += 10;
            }
        }
        if (!X10)
        {
            if (data.Wood >= 1)
            {
                data.Wood -= 1;
                data.Credits += 1;
            }
        }
    }
  
    public void SellTextiles()
    {
        if (X10)
        {
            if (data.Textiles >= 10)
            {
                data.Textiles -= 10;
                data.Credits += 20;
            }
        }
        if (!X10)
        {
            if (data.Textiles >= 1)
            {
                data.Textiles -= 1;
                data.Credits += 2;
            }
        }
    }
    public void SellFood()
    {
        if (X10)
        {
            if (data.Food >= 10)
            {
                data.Food -= 10;
                data.Credits += 10;
            }
        }
        if (!X10)
        {
            if (data.Food >= 1)
            {
                data.Food -= 1;
                data.Credits += 1;
            }
        }
    }
    public void SellStone()
    {
        if (X10)
        {
            if (data.Stone >= 10)
            {
                data.Stone -= 10;
                data.Credits += 10;
            }
        }
        if (!X10)
        {
            if (data.Stone >= 1)
            {
                data.Stone -= 1;
                data.Credits += 1;
            }
        }
    }
  */
    
    #endregion
}
