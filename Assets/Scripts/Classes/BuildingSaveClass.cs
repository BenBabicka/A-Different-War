using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSaveClass
{
    
        public string buildingName;

        public int amount = 0;
        public List<Vector3> buildingPositions;
        public List<Vector3> buildingRotations;

        public List<float> buildProgress;

    public List<bool> hasPlaced;
}
