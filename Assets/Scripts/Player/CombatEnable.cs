using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class CombatEnable : MonoBehaviour
{
    public bool isInCombat;
    public bool Drafted;

    public Toggle combatToggel;

    private Shooting shooting;


    void Start()
    {
        shooting = gameObject.GetComponent<Shooting>();
        
        isInCombat = combatToggel.isOn;
    }


    public void OnToggle(bool Combat)
    {
        isInCombat = Combat;
        if(Combat == true)
        {
            shooting.enabled = true;
            isInCombat = true;
            Drafted = true;
            shooting.canShoot = true;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
        if (Combat == false)
        {
            shooting.enabled = false;
            shooting.isFiring = false;
            shooting.canShoot = false;
            isInCombat = false;
          Drafted = false;
            gameObject.GetComponent<Rigidbody>().isKinematic = false;

        }
    }
}
