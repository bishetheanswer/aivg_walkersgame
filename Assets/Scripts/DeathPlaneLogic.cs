using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  ***********************
 Script for handling the death plane logic
 Author: Luna Jimenez Fernandez - Group 2 (01/03/2020)
 Change control :
 	Modificacion : Autor: - Fecha : - Comentarios
 Observaciones :
 Inputs:
 	No inputs
 Outputs:
	No outputs
********************* */

public class DeathPlaneLogic : MonoBehaviour
{
    // Reference to the general game logic controller itself
    private GeneralScript general;

    // On start, grab the reference to the general object.
    private void Start()
    {
        general = GameObject.Find("General").GetComponent<GeneralScript>();
    }

    // All logic will be launched whenever something enters the collider (we want to delete the walkers WHEN they touch the trigger)
    private void OnTriggerEnter(Collider other)
    {
        // If a player character has been destroyed, the game has to end directly.
        if(other.gameObject.CompareTag("Team A Player"))
        {
            general.countLife_A = 0;
        }
        else if (other.gameObject.CompareTag("Team B Player"))
        {
            general.countLife_B = 0;
        }

        // Disable the other game object
        other.gameObject.SetActive(false);
    }

}
