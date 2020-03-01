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

    // All logic will be launched whenever something enters the collider (we want to delete the walkers WHEN they touch the trigger)
    private void OnTriggerEnter(Collider other)
    {
        // Disable the other game object
        other.gameObject.SetActive(false);
    }

}
