using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomObjectSpawner : MonoBehaviour
{
 
   public GameObject[] myObjects;
   public float spawnRate = 1f;
   

    // Update is called once per frame
    void Start()
    {
        StartCoroutine(spawnRandomObjects());
    }

    IEnumerator spawnRandomObjects()
    {
    
      while(true)
      {
       yield return new WaitForSeconds(Random.Range(0.5f, 1.5f) * spawnRate);
        int randomIndex = Random.Range(0, myObjects.Length);
        Vector3 randomSpawnPosition = new Vector3(Random.Range(-10, 11), 5, Random.Range(-10, 11));

        Instantiate(myObjects[randomIndex], randomSpawnPosition, Quaternion.identity);
      } 
    }
}
