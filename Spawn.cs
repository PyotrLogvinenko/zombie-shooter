using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public Transform[] array = new Transform[5];
    int random;
    public GameObject generate;
    public Transform Target;
    // Use this for initialization
    void Start()
    {
        random = Random.Range(0, array.Length);
        Spawner();
    }
    void Spawner()
    {
        GameObject clone = Instantiate(generate, array[random].transform.position, array[random].transform.rotation) as GameObject;
        Vector3 origin = clone.transform.position;
        Vector3 target = Target.position;

    }
    // Update is called once per frame
    void Update()
    {

    }
}