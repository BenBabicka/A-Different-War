using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {
    /*
    public static void SavePlayer(PlayerSave player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        if (!Directory.Exists(Application.persistentDataPath + "/saves"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves");
        }
        string path = Application.persistentDataPath + "/saves/"+ player.GetComponent<PlayerSave>().ID +".save";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer(PlayerSave player)
    {
        string path = Application.persistentDataPath + "/saves/" + player.GetComponent<PlayerSave>().ID + ".save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;
        } else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }*/

}
