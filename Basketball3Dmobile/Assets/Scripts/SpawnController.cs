using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public Transform[] Positions;
    public GameObject Object;
    
    public Transform Location;

    public bool ToSpawn = true;

    void Update()
    {
        Location = Positions[Random.Range(0, Positions.Length)];

        if (ToSpawn == true)
        {
            Instantiate(Object, Location);
            Ball.GetComponent<Rigidbody>().isKinematic = false;
            ToSpawn = false;

            StartCoroutine(ToSpawnTrue());
             StartCoroutine(destroy());
        }
    }
    IEnumerator ToSpawnTrue()
    {
        yield return new WaitForSeconds(5f);
        ToSpawn = true;
    }
    IEnumerator destroy()
    {
        yield return new WaitForSeconds(1f);
        Destroy(Ball);
    }
    public GameObject Ball;
}
