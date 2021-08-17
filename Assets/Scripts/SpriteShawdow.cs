using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteShawdow : MonoBehaviour
{
    public float offsetY;

    private SpriteRenderer sprRndCaster;
    private SpriteRenderer sprRndShadow;

    private Transform transCaster;
    private Transform transShadow;

    public Material shadowMaterial;
    public Color shadowcolour;
    void Start()
    {
        transCaster = transform;
        transShadow = new GameObject().transform;
        transShadow.parent = transCaster;
        transShadow.gameObject.name = "shadow";
        transShadow.localRotation = Quaternion.identity;
        transShadow.localScale = transform.GetChild(0).localScale;

        sprRndCaster = transform.GetChild(0).GetComponent<SpriteRenderer>();
        sprRndShadow = transShadow.gameObject.AddComponent<SpriteRenderer>();

        sprRndShadow.material = shadowMaterial;
        sprRndShadow.color = shadowcolour;

        sprRndShadow.sortingLayerName = sprRndCaster.sortingLayerName;
        sprRndShadow.sortingOrder = sprRndCaster.sortingOrder - 1;
        StartCoroutine(LateStart());
    }

    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(.1f);
        transShadow.localPosition = new Vector3(0, offsetY, 0);
        sprRndShadow.sprite = sprRndCaster.sprite;
        sprRndShadow.flipY = true;

        if (sprRndCaster.flipX)
        {
            sprRndShadow.flipX = true;
        }
    }
}
