using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MinigameUI : MonoBehaviour
{
    public TextMeshProUGUI currentInputText; //Text for button press.
    public TextMeshProUGUI upcomingInputText; //Text for preview button press.

    public inputReader game; //Pattern creater script refrence.
    public int previewLength; //Value that determines how many inputs you are able to see ahead of time.

    void Start()
    {
        //HAVING THEM START OFF AS EMPTY STRINGS INSTEAD OF BEING UNDELCARED.
        currentInputText.text = "";
        upcomingInputText.text = "";
    }

    void Update()
    {

        if (game.StringPattern.Count > 0 && game.CurrentIndex < game.StringPattern.Count)
        {
            string currentPhrase = game.StringPattern[game.CurrentIndex];

            // Get typed progress vs remaining
            int charIndex = game.CurrentCharacterIndex; // need public getter in inputReader
            charIndex = Mathf.Clamp(charIndex, 0, currentPhrase.Length);

            string typedPart = currentPhrase.Substring(0, charIndex);
            string remainingPart = currentPhrase.Substring(charIndex);

            // Highlight typed part in green
            currentInputText.text = $"<color=green>{typedPart}</color>{remainingPart}";

            UpdateInputs();
        }
        else
        {
            // Avoid leftover text when pattern resets
            currentInputText.text = "";
            upcomingInputText.text = "";
        }
    }

    void UpdateInputs()
    {
        upcomingInputText.text = "";

        //FOR LOOP STARTING AT 1 SINCE IF WE START AT 0 IT WILL ALSO COUNT THE CURRENT INPUT.
        for (int i = 1; i <= previewLength; i++)
        {
            int nextIndex = game.CurrentIndex + i;

            //SHOWS THE PREVIEW OF THE UPCOMING INPUTS
            if (nextIndex < game.StringPattern.Count)
            {
                upcomingInputText.text += game.StringPattern[nextIndex].ToString() + " ";
            }
            //IN THE CASE THAT THE PATTERN IS REACHING LESS THAN THE PREVIEW LENGTH
            else
            {
                upcomingInputText.text += " - ";

            }
        }
    }
}
