using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inputReader : MonoBehaviour
{

    public string[] possiblePhrases;
    public List<string> StringPattern { get { return stringPattern; } }
    private List<string> stringPattern;

    [Header("# Of Clients At The Table")]
    public int stringAmount;


    //string index
    public int CurrentIndex { get { return currentIndex; } }
    private int currentIndex = 0;

    //character index:
    public int CurrentCharacterIndex { get { return currentCharacterIndex; } }
    private int currentCharacterIndex = 0;


    public float score; 
    public float waitTime; //float used for the amount of seconds of delay when putting the wrong input.

    private bool canInput = true; //bool toggle when an input is messed up working alongside the waitTime.

    public overflowBar meter;

    void Start()
    {
        stringPattern = new List<string>();

        CreatePattern();
    }


    void Update()
    {
        if (canInput)
        {
            CheckInput();
        }
    }


    void CheckInput()
    {
        if (currentIndex >= stringPattern.Count) return;

        string currentPhrase = stringPattern[currentIndex];

        if (Input.anyKeyDown)
        {
            foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(key))
                {
                    string keyPressed = key.ToString().ToLower();

                    char requiredKey = currentPhrase[currentCharacterIndex];
                    string requiredKeyString = requiredKey.ToString().ToLower();


                    //If the user inputes the correct keycode:
                    if (keyPressed == requiredKeyString)
                    {
                        score++;
                        Debug.Log("Correct Key Entered!!");
                        currentCharacterIndex++;


                        //After the user iterates through the current string:
                        if (currentCharacterIndex >= currentPhrase.Length)
                        {
                            //Reset
                            currentCharacterIndex = 0;
                            currentIndex++;


                            //After all phrases are used Reset Phrase List:
                            if (currentIndex >= stringPattern.Count)
                            {
                                meter.OnPatternCompleted();
                                currentIndex = 0;
                                CreatePattern();
                            }
                        }
                    }
                    else
                    {
                        Debug.Log("WRONG INPUT KEY!! FAILED!!!");
                        StartCoroutine(DisableInputs());
                    }

                    break;
                }
            }
        }
    }

    IEnumerator DisableInputs()
    {
        canInput = false;
        yield return new WaitForSeconds(waitTime);
        canInput = true;

        
        currentIndex = 0;
        currentCharacterIndex = 0;
        CreatePattern();
       
    }


    void CreatePattern()
    {
        stringPattern.Clear();

        //CREATES A FOR LOOP IN WHICH BASED ON THE PATTERN LENGTH, CHOOSES A RANDOM KEYCODE FROM POSSIBLE INPUTS, THEN ADDS IT TO THE ARRAY OF THE INPUT PATTERN, REPEATS UNTIL PATTERN LENGTH IS REACHED.
        for (int i = 0; i < stringAmount; i++)
        {
            string randomString = possiblePhrases[Random.Range(0, possiblePhrases.Length)];
            stringPattern.Add(randomString);
        }
    }
    

}
