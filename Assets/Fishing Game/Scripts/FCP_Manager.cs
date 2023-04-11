using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class FCP_Manager : MonoBehaviour
{
    public static FCP_Manager instance;                                 // Our singleton instance
    private float startTime, elapsedTime;
    public TextMeshProUGUI timeCounter;  //reference to timecounter text gameobject
    public TextMeshProUGUI fishCounter;  //reference to fishcount text gameobject
    public GameObject hudContainer, gameOverPanel, endGamePanel;
    TimeSpan timePlaying; //allows for a readable format of time
    public bool gamePlaying {get; private set; } //true when the player is playing the game and false when the game has ended.
    public float timeLimit = 180f; // Set the time limit to 3 minutes (180 seconds)
    
    [HideInInspector]
    public FCP_Player fcpPlayer;

    public int fishCount = 0;                                           // The amount of fish coins we have
    public int maxFishCount;                                       //the total amount of fishes we are allowed to catch.

    [Header("SFX Settings")]
    public AudioClip waterHitSFX;
    public AudioClip reelingInSFX;

    [Header("Fisher Settings")]   
    public float reelSpeed = 8.0f;                                      // The speed at which we reel 
    public float reelAttackPower = 0.12f;                               // The step attack power for us reeling in our lure
    public float reelFishAttackPower = 0.1f;                            // The attack power we have against fish, this is how much power we have to defeat them in reeling

    [HideInInspector]
    public Rigidbody2D fishingLure;                                     // Our lure's rigidbody
    [HideInInspector]
    public Transform castFromLocation;                                  // The location we cast from off of the fishing rod

    public bool isCatchingFishInBattle = true;                          // Used to determine if we're actively catching a fish in a battle or not

    [HideInInspector]
    public Transform fishingRod;                                        // Our fishing rod. This can be extended to be something we unlock.

    [Header("UI Related")]
    public Text fishCountText;
    //public Text areaName;

    [Header("UI Popup")]
    public GameObject uiPopupFishLost;                                  //This is the window that pops up when a fish is lost
    public GameObject uiPopupFishCaught;                                //This is the window that pops up when a fish is caught

    public GameObject uiFishBattlePopupWindow;                          // This is the window that pops up for Fish Catching
    public Slider uiFishBattlePopupSlider;                              // This is the battle slider. The object that slides left / right when you battle a fish

    [Header("Current Caught Fish Settings")]
    public FCP_Fish currentFish;                                        // The current fish we have caught on the lure

    [Header("Particle Effects")]
    public ParticleSystem coinExplosionLootEffect;                      // The particle effect that occurs when we catch a fish


    private void Awake()
    {
        // Manage our singleton
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        // Find our important objects and alert if we do not find them
        fishingLure = GameObject.FindGameObjectWithTag("Lure").GetComponent<Rigidbody2D>();
        if (fishingLure == null) Debug.LogError("Cannot find lure, please assign the prefab.");

        castFromLocation = GameObject.FindGameObjectWithTag("CastFromSpot").transform;
        if (castFromLocation == null) Debug.LogError("Cannot find cast from location.");

        fishingRod = GameObject.FindGameObjectWithTag("FishingRod").transform;
        if (fishingRod == null) Debug.LogError("Cannot find the fishing rod.");

        fishCountText = GameObject.FindGameObjectWithTag("FishCountText UI").GetComponent<Text>();
        if (fishCountText == null) Debug.LogError("Cannot find the fish text.");

        coinExplosionLootEffect = GameObject.FindGameObjectWithTag("CoinExplosionFX").GetComponent<ParticleSystem>();
        if (coinExplosionLootEffect == null) Debug.LogError("Cannot find the coin loot effect.");

        // Get our player
        fcpPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<FCP_Player>();

    }

    // Start is called before the first frame update
    void Start()
    {   

        
        maxFishCount = UnityEngine.Random.Range(1, 14); //generates a random number for the number of fishes to be caught.
        fishCounter.text = "Catch: " + maxFishCount.ToString() + " fish";
        fishCount = 0;
        fishCountText.text = fishCount.ToString();  //initialize the number of fishes caught to zero.
        PlayerPrefs.SetInt("PlayerCoinCount", fishCount);
        PlayerPrefs.Save();
        gamePlaying = false;
        timeCounter.text = "Time: 00:00";

        BeginGame();

        /*
        // Look up the area name text, this comes from PlayerPrefs. If not found, default to "Lake"
        if(PlayerPrefs.HasKey("FCP_CurrentArea"))
        {
            areaName.text = PlayerPrefs.GetString("FCP_CurrentArea");
        }
        else
        {
            // Default name if we cannot find one saved
            areaName.text = "Lake";
        } */
    }

    private void BeginGame(){

        gamePlaying = true;
        startTime = Time.time; //starts at zero when the game starts and as the game progress, increments based on the time since the game started

    }

    // Update is called once per frame
    void Update()
    {
        if(gamePlaying){

        elapsedTime = Time.time - startTime; //amount of time that has passed since the game started
        timePlaying = TimeSpan.FromSeconds(elapsedTime); //converts the timspan variable in a time format for the player to understand

        string timePlayingStr = "Time: " + timePlaying.ToString("mm':'ss"); //creates a string with the time format
        timeCounter.text = timePlayingStr; //sets the timetext object to the timer to start the counting
        
        // Update our values
        fishCount = PlayerPrefs.GetInt("PlayerCoinCount");
        fishCountText.text = fishCount.ToString();

        if(Time.time >= timeLimit){

              GameOver();
        }

        // If we do not have a fish, disable the battle popup
        if (currentFish == null)
            uiFishBattlePopupWindow.SetActive(false);

        }
    }

    public void CaughtFish(FCP_Fish fish)
    {
        if (currentFish == null)
        {
            if (!fish.recentlyCaught)
            {
                // Open the fish battle
                uiFishBattlePopupWindow.SetActive(true);

                // Flag that we've been caught
                fish.recentlyCaught = true;

                // Disable physics on the fishing lure
                FCP_Manager.instance.fishingLure.simulated = false;
                isCatchingFishInBattle = true;

                currentFish = fish;
                currentFish.isCaught = true;
                currentFish.GetComponent<SpriteRenderer>().flipY = true;
                
                // Reparent the fish to the lure to easily bring them back to the boat
                fish.transform.SetParent(fishingLure.transform);
                fish.transform.localPosition = Vector3.zero; 

                // Start our battle
                StartCoroutine(fishBattle());
            }
        }
    }

    public void LostFish(FCP_Fish fish)
    {
        Debug.Log("Lost the fish!");

        // Close the battle window
        uiFishBattlePopupWindow.SetActive(false);

        ShowPopup(uiPopupFishLost);
        
        fish.isCaught = false;
        fish.transform.parent = null;
        fish.GetComponent<SpriteRenderer>().flipY = false;

        currentFish = null;
        
        FCP_Manager.instance.fishingLure.simulated = true;

        isCatchingFishInBattle = false;

        fish.RecentlyCaughtCooldown();

        // Swim away to a new POI
        fish.FindPOI();
    }

    IEnumerator fishBattle()
    {
        // If we're actively battling a fish
        while (isCatchingFishInBattle)
        {
            // Wait our break away timer for the current fish
            yield return new WaitForSeconds(currentFish.breakAwayTimer);
            
            Debug.Log("Fish battling!");

            // And it's not null
            if (currentFish == null)
                isCatchingFishInBattle = false;

            // Do some "damage" to the fish, aka, try to catch it
            currentFish.fishLife -= currentFish.breakAwayPower;

            // Check to see if we drained the fish enough to catch it
            if(currentFish.fishLife <= 0)
            {
                LostFish(currentFish);
            }    
            else if(currentFish.fishLife >= 1)
            {
                WonFishBattle(currentFish);
            }

        }
    }

    /// <summary>
    /// Called when we win and catch a fish
    /// </summary>
    /// <param name="fish"></param>
    public void WonFishBattle(FCP_Fish fish)
    {
        isCatchingFishInBattle = false;

        // Close the battle window
        uiFishBattlePopupWindow.SetActive(false);
    }

    /// <summary>
    /// Manage our scoring of the fish
    /// </summary>
    /// <param name="fish"></param>
    public void ScoreFish(FCP_Fish fish)
    {
        // Each fish has a value associated to the prefab
        fishCount += fish.coinValue;

        if(fishCount >= maxFishCount){ //checks to see if the player obtained the number of fish to be caught

            EndGame();
        }

        // Write to our player prefs the amount of coins they have earned from this fish
        PlayerPrefs.SetInt("PlayerCoinCount", fishCount);
        PlayerPrefs.Save();

        Destroy(currentFish.gameObject);
        currentFish = null;

        coinExplosionLootEffect.Play();

        ShowPopup(uiPopupFishCaught);
    }

    private void EndGame(){ //screen that shows once the player catches the required number of fishes.

        gamePlaying = false;
        Invoke("ShowGameOverScreen", 1.25f);

    }

    private void GameOver(){ //screen that shows when the time runs out before the player catches the given amount of fishes.
 
        gamePlaying = false;
        Invoke("ShowEndGameScreen", 1.25f);

    }

    private void ShowGameOverScreen(){

        gameOverPanel.SetActive(true);
        hudContainer.SetActive(false);
        string timePlayingStr = "Time: " + timePlaying.ToString("mm':'ss");
        gameOverPanel.transform.Find("FinalTimeText").GetComponent<TextMeshProUGUI>().text = timePlayingStr; //finds the finaltime text object and sets up the timer to show in the text gameobject
        gameOverPanel.transform.Find("RestartButton").GetComponent<Button>().Select(); //selects the button when the restart button is pressed.
    }

    private void ShowEndGameScreen(){

        endGamePanel.SetActive(true);
        hudContainer.SetActive(false);
        string timePlayingStr = "Time: " + timePlaying.ToString("mm':'ss");
        endGamePanel.transform.Find("FinalTimeText").GetComponent<TextMeshProUGUI>().text = timePlayingStr; //finds the finaltime text object and sets up the timer to show in the text gameobject
        endGamePanel.transform.Find("TryAgain").GetComponent<Button>().Select(); //selects the button when the restart button is pressed.
    }

    private void ShowPopup(GameObject popupWindow)
    {
        StartCoroutine(ShowPopupTimer(popupWindow));
    }

    IEnumerator ShowPopupTimer(GameObject popupWindow)
    {
        popupWindow.SetActive(true);

        yield return new WaitForSeconds(2.0f);

        popupWindow.SetActive(false);
    }
    /*
    // This is used to go back to the main map, this is a button
    public void UI_GoBackMap()
    {
        //SceneManager.LoadScene("TitleScreen");
        SceneManager.LoadScene("Map");
    }*/

    // This is used to go back to the main menu, this is a button
    public void UI_GoBackMenu()
    {
        SceneManager.LoadScene("TitleScreen");
        //SceneManager.LoadScene("Map");
    }

    public void OnButtonLoadLevel(string levelToLoad){

        SceneManager.LoadScene(levelToLoad);
    }
}
