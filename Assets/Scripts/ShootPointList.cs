using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPointList : MonoBehaviour
{
    public List<GameObject> objectList = new List<GameObject>();

   

    public List<GameObject> GetObjectList()
    {
        return objectList;
    }
}
