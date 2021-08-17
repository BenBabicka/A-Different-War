using UnityEngine;
using System.Collections;

public class WeaponData : MonoBehaviour {
    [Header("Gun")]
    public float damage;
    public float range;
    public float ammo;
    public float fireRate;
    public float bulletSpeed;
    public float reloadAmount;
    public float ReloadSpeed;
    public float aimTime;
    [Range(0, 90)]
    public float spread;

    public bool shotgun;

    [Space]
    [Header("Melee")]
    public bool melee;
    public float meleeRange;

    [HideInInspector]
    public int spawn_ID;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 0, 0.5f, 0.5f);
        Gizmos.DrawSphere(transform.position, range);
    }

}
