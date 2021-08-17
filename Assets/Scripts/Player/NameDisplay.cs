using UnityEngine;
using System.Collections;
using TMPro;

public class NameDisplay : MonoBehaviour {

    public GameObject Player;
    public bool isEnemy;
    Camera cam;
    public Color color = Color.white;
    void Start()
    {
        cam = Camera.main;
        gameObject.GetComponent<TextMeshPro>().text = Player.gameObject.name;

        transform.parent.parent.transform.position = new Vector3(Player.transform.position.x, 10, Player.transform.position.z);
        gameObject.GetComponent<RectTransform>().localScale = new Vector3(cam.orthographicSize / 25, cam.orthographicSize / 25, cam.orthographicSize / 25);
        if (cam.orthographicSize >= 28.0f)
        {
            color.a -= .2f;
        }
        if (cam.orthographicSize < 28.0f)
        {
            color.a += .2f;

        }
    }


    void Update()
    {
  if(cam.orthographicSize >= 28.0f)
        {
            color.a -= .2f;
        }
        if (cam.orthographicSize < 28.0f)
        {
            color.a += .2f;

        }
        gameObject.GetComponent<TextMeshPro>().text = Player.gameObject.name;
        transform.parent.parent.transform.position = new Vector3(Player.transform.position.x, 10, Player.transform.position.z);

        gameObject.GetComponent<RectTransform>().localScale = new Vector3( cam.orthographicSize / 25, cam.orthographicSize / 25, cam.orthographicSize / 25);
      
        gameObject.GetComponent<TextMeshPro>().color = color;

    }

}
