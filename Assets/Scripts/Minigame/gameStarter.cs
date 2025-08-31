using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class gameStarter : MonoBehaviour
{
    [Header("Refrences")]
    public basicMovement player;
    public GameObject gameManager;
    public GameObject gameUI;

    public inputReader inputReader;
    [Space]

    public LayerMask Table;
    private bool isOnCooldown = false;

    public float cdDuration;



    private void OnCollisionEnter(Collision collision)
    {
        //prevent collision looping for testing:
        if (isOnCooldown) return;

        if (((1 << collision.gameObject.layer) & Table) != 0)
        {
            //Table Information:
            tableData table = collision.gameObject.GetComponent<tableData>();

            if (table != null)
            {
                inputReader.stringAmount = table.SeatedClients;
                inputReader.CreatePattern();
            }
            else
            {
                Debug.Log("There are NO CLIENTS IN THIS TABLE");
            }

            //disable the player
            player.enabled = !player.enabled;


            //enable the gameManager
            gameManager.SetActive(!gameManager.activeSelf);


            //enable the gameUI
            gameUI.SetActive(!gameUI.activeSelf);


            if (gameManager.activeSelf == false) 
            {
                StartCoroutine(MinigameCooldown(cdDuration));
            }
        }
    }

    private IEnumerator MinigameCooldown(float duration)
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(duration);
        isOnCooldown = false;
    }

}
