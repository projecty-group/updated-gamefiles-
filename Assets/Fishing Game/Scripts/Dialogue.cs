 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;


public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent; //reference to text in dialoguebox
    public string[] lines =  new string[3]; //to store the lines of text that will be displayed
    public float textSpeed; //to indicate the speed of the text

    private int index; //to indicate where we are in the conversation

    /*
    // specify the path to the text file
    string filePath = "Assets/TextFiles/DialogueText.txt";

    // read the text file
    string text = File.ReadAllText(filePath);

    // split the text into paragraphs using a delimiter (in this case, "\n\n")
    public string[] lines = text.Split(new string[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries);

    // print each paragraph of text to the console
    for (int i = 0; i < lines.Length; i++)
    {
        Debug.Log(lines[i]);
    }*/

    // Start is called before the first frame update
    void Start()
    {   
        lines[0] = "Ensure not to catch fishes that are too small so they can grow and reproduce to make sure there will be plenty of fish to catch in the future. This affects the marine biodiversity. Marine biodiversity refers to the many different fishes in the world's oceans and seas.";
        lines[1] = "Fishing for endangered species affects marine biodiversity because the extinction of a fish species would lead to there not being enough fishes for fishermen to catch for food. The yellowfin grouper is an example of an endangered fish. Try to catch less of them.";
        lines[2] = "The maximum amount of fishes to be caught (40 fishes) has been reached. Avoiding overfishing is important because there may be less fish available in the sea, which can lead to the overpopulation of plankton which is the main food for fish. Too much plankton contaminates the sea preventing fishing and swimming."; 
        textComponent.text = string.Empty;
        StartDialogue();
         
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)){

            if(textComponent.text ==  lines[index]){

                NextLine();
            }
            else 
                StopAllCoroutines();
                textComponent.text = lines[index];
        }
        
    }

    void StartDialogue(){

        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine(){

        //Type each character 1 by 1
        foreach (char c in lines[index].ToCharArray()){ //takes the string and breaks it down into a character array

            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine(){

        if(index < lines.Length - 1){

            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
            gameObject.SetActive(false);
    }
}
