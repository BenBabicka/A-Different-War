using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour {

    [Range(0, 40000)]
    public int count = 250; // Number of cubes we want to place. Don't set this to high or the position picking loop may run forever.

    public  float x = 200f;    // placement area width
    public float z = 200f;   // placement area height

    [Range(0, 5)]
    public float minDistance = 2f; // How far our cubes should stay away from each other. Also: Don't set this to high or the position picking loop may run forever.

    List<Vector3> positions = new List<Vector3>();

    public GameObject prefab;

    public GameObject Parent;

    float ID;

    public GameObject crashSite;

    public bool usePerlinNoise = true;

    public MapGenerationSave mapGenerationSave;
    void Start()
    {

        PlaceStuffRandomly(usePerlinNoise); // Set this to true, to place using perlin noise

        for (int j = 0; j < positions.Count; j++)
        {

            ID += 1;
                var go = Instantiate (prefab, transform.position, Quaternion.Euler(90, 0, 0)) as GameObject;
            go.GetComponent<Nature>().ID = ID;
            
                go.GetComponent<Nature>().randomiseObject();
            
        if(go.GetComponent<TreeManager>())
            {
                go.GetComponent<TreeManager>().randomise();
            }
            go.transform.position = positions[j];
            go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y + 1f, go.transform.position.z);
            if (mapGenerationSave)
            {
                mapGenerationSave.itemList.Add(go);
            }
            if (gameObject.GetComponent<Removing>())
            {
                gameObject.GetComponent<Removing>().Trees.Add(go);
            }
            go.transform.SetParent(Parent.transform);
            go.name = go.GetComponent<Nature>().Name + "_" + go.GetComponent<Nature>().ID;
            if (gameObject.GetComponent<GameManager>())
            {
                if (!gameObject.GetComponent<GameManager>().allNature.Contains(go.transform))
                {
                    gameObject.GetComponent<GameManager>().allNature.Add(go.transform);
                }
            }
                if (crashSite)
                {
                    crashSite.GetComponent<MoveObjects>().array.Add(go);
                }
            
        }
    }


    public void PlaceStuffRandomly(bool UsePerlin)
    {

        Vector3 newPos = transform.position ;
        bool Nope = false;

        for (int i = 0; i < count; i++)
        {

            do
            {
                newPos = new Vector3( x * Random.value - x/2, 0, transform.position.z + z  * Random.value - z / 2);

                if (UsePerlin)
                {
                    Nope = !(PerlinThinksItShouldBeThere(newPos) && CouldPlaceItThere(newPos));
                }
                else
                    Nope = !CouldPlaceItThere(newPos);

            } while (Nope); 

            positions.Add(newPos);
        }

    }

    private bool CouldPlaceItThere(Vector3 newPos)
    {

        // Loop through all positions where we already want to place something
        for (int i = 0; i < positions.Count; i++)
            if (Vector3.Distance(positions[i], newPos) < minDistance) // ... and check if the new point maybe is to close
                return false;
        return true;
    }

    private bool PerlinThinksItShouldBeThere(Vector3 newPos)
    {
        newPos = transform.position;
        // Basically how fast perlin changes his mind when you ask him for nearby Points
        float frequency = 8;

        // Lets ask him what he thinks of the current position
        float howSurePerlinIsThatItShouldBeThere = Mathf.PerlinNoise(newPos.x / x * frequency, newPos.z / z * frequency);

        if (Random.value <= howSurePerlinIsThatItShouldBeThere)
            return true;
        else
            return false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.1f);
        Gizmos.DrawCube(transform.position, new Vector3(x, 0, z));
    }
}
