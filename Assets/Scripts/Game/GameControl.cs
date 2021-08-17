using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameControl : MonoBehaviour
{

    [SerializeField]
    public GameObject drawing;
    public string drawName;

   
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/drawing.obj");

        UserDraw data = new UserDraw();
        data.drawing = drawing;

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/drawing.obj"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/drawing.obj", FileMode.Open);
            UserDraw data = (UserDraw)bf.Deserialize(file);
            file.Close();

            drawing = data.drawing;
        }
    }

}

[System.Serializable]
class UserDraw
{
    public GameObject drawing;
}