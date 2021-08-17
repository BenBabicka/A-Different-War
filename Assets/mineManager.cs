using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mineManager : MonoBehaviour
{
    public int maxAmountOfPeople;
    public List<GameObject> players;

    public List<material> materials;

    [System.Serializable]
    public class material
    {
        public string material_name;
        public float mineTime;
        public int rarityLevel;
        public material(string nam, float tim, int rar)
        {
            material_name = nam;
            mineTime = tim;
            rarityLevel = rar;
        }
    }

    
}
