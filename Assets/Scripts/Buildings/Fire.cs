using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {

    public Light fire;
    public float intensity;

    public  float time;

    public Sprite[] fireSprites;
    public ParticleSystem[] paricleEmitters;
    int sprite;
    public bool fireOn;

    void Start()
    {
        foreach (var Emitter in paricleEmitters)
        {
            Emitter.enableEmission = false;
        }
        gameObject.GetComponent<SpriteRenderer>().sprite = null;
        fire.intensity = 0;
    }

    void FixedUpdate()
    {
        if (fireOn)
        {
            time -= Time.fixedDeltaTime;
            fire.enabled = true;
            foreach (var Emitter in paricleEmitters)
            {
                Emitter.enableEmission = true;
            }
            if (time <= 0)
            {

                sprite = Random.Range(0, fireSprites.Length);

                intensity = Random.Range(0.00f, 1.00f);
                time = 0.1f;
                gameObject.GetComponent<SpriteRenderer>().sprite = fireSprites[sprite];
               
            }
            fire.intensity = intensity;
        }
        else
        {
            foreach (var Emitter in paricleEmitters)
            {
                Emitter.enableEmission = false;
            }
            gameObject.GetComponent<SpriteRenderer>().sprite = null;
            fire.intensity = 0;
        }
    }
}
