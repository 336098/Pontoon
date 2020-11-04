using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPointList : MonoBehaviour
{
    public List<GameObject> objectList;

    public GameObject shootPoint1;
    public GameObject shootPoint2;

    // Start is called before the first frame update
    void Start()
    {
        objectList.Add(shootPoint1);
        objectList.Add(shootPoint2);
    }

    public List<GameObject> GetObjectList()
    {
        return objectList;
    }
}
