using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketballController : MonoBehaviour {

 
    // Start is called before the first frame update
   public GameObject[] myObjects;
   public float spawnRate = 1f;
   

   // player controller
    public Transform PosOverHead;
    public Transform Arms;
    public float MoveSpeed = 10f;

  private bool IsBallInHands = true;
    //private bool IsBallFlying = false;
   // private float T = 0;

  private  GameObject newObject;

 public GameObject[] pickupableObjects;
 bool carryingObject = false;




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
        Vector3 randomSpawnPosition = new Vector3(Random.Range(-11, 12), 5, Random.Range(-11, 12));

         newObject = Instantiate(myObjects[randomIndex], randomSpawnPosition, Quaternion.identity);

        newObject.transform.SetParent(transform);
      } 
    }
    

    // Update is called once per frame
   

  void Update()
 {

      // walking
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        transform.position += direction * MoveSpeed * Time.deltaTime;
        transform.LookAt(transform.position + direction);

       // ball in hands
        

 }

 /*void OnTriggerEnter(Collider other)
{ 
    if (other.CompareTag("PickUp"))
    {
        // Do something with the object, such as adding it to the player's inventory
        // ...

        // Remove the object from the scene
        Destroy(other.gameObject);

        // Spawn a new random object at a random spawn point
       
    }
  }
*/




}



    
       










