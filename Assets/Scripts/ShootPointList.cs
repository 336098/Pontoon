using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPointList : MonoBehaviour
{
    public List<GameObject> objectList = new List<GameObject>();

    private void Start()
    {
        //Loop through each shoot point and disable it from being shown in game
        for (int i = 0; i < objectList.Count; i++)
        {
            objectList[i].GetComponent<Renderer>().enabled = false;
        }
    }

    public List<GameObject> GetObjectList()
    {
        return objectList;
    }
}
