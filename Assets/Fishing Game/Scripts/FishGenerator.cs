using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject[] fishPrefabs; //array of diffrent fish sprite which different properties.
    public float sizeMin; //maximum size of fish
    public float sizeMax; //minimum size of fish
    private int randomIndex; //random index for the array of fish prefabs
    //public int numberOfFish = 10; //random value for the number of fish
    private GameObject spawnedFish; //gameobject for the spawned fish prefab
    public bool gamePlaying; //gamePlaying variable from the manager class
    public FCP_Manager fcpmanager; //instance of the FCP_Manager class
    private int randomSide;

  

    [SerializeField]
    private Transform fishPOI1, fishPOI2;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnFishes());
    }

    void Update(){
        
         gamePlaying = fcpmanager.gamePlaying;

    }
     
    IEnumerator SpawnFishes(){
         
         
         while(gamePlaying){
                    
                yield return new WaitForSeconds(Random.Range(1,3)); //time between which fishes are spawn

                randomIndex = Random.Range(0, fishPrefabs.Length); //selects a random fish sprite from the array of prefabs
                randomSide = Random.Range(0, 1); //spawns the fish either on the left or the right
                spawnedFish = Instantiate(fishPrefabs[randomIndex]); //creates an instance of the fish

                if(randomSide == 0){ //fishPOI1

                        spawnedFish.transform.position = fishPOI1.position;
                        spawnedFish.GetComponent<FCP_Fish>().size = Random.Range(sizeMin,sizeMax);
                        spawnedFish.GetComponent<FCP_Fish>().swimSpeed = Random.Range(2,7);
                }
                else{ //fishPOI2
                        
                        spawnedFish.transform.position = fishPOI2.position;
                        spawnedFish.GetComponent<FCP_Fish>().size = Random.Range(sizeMin,sizeMax);
                        spawnedFish.GetComponent<FCP_Fish>().swimSpeed = Random.Range(2,7);

                }
              
                gamePlaying = fcpmanager.gamePlaying;
        } //while loop
    }

   
}
