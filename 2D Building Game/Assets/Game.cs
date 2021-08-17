using UnityEngine;
using System.Collections;

[System.Serializable]//Important! Every custom class that needs to be serialized has to be marked like this!
public class Game
{ //don't need ": Monobehaviour" because we are not attaching it to a game object

    public string savegameName;//used as the file name when saving as well as for loading a specific savegame
    public string testString;//just a test variable of data we want to keep

    void Start()
    {
        
    }

}