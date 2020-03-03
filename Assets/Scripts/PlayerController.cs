using UnityEngine;
using UnityEngine.UI;
using System.Collections;


/*  ***********************
 Script para el manejo de los Lideres (Players)
 Autor : Miguel Angel Fernandez Graciani 2020-01-01
 Control de cambios :
 	Modificacion : Autor: - Fecha : - Comentarios
 Observaciones :
 Entradas:
 	No hay entradas
Salidas :
	No hay salidas
********************* */
public class PlayerController : MonoBehaviour {

	public bool NPC;  // NPC = true, player is NPC - false, who plays is a physical user
	public GameObject General; // For profit objects

	private Rigidbody rb;   // We need a rigid body to use dynamics

	// To define the period between random changes
	private int numLatenciaCambios = 100;
	private int estadoLatenciaCambios = 0;

	private float moveHorizontal = 0.0f;  // X component of the applied force
	private float moveVertical = 0.0f;    // Z component of the applied force



	// This function is only executed when the game object is activated
	void Start ()
	{
		rb = GetComponent<Rigidbody>();
//		NPC = false;
		numLatenciaCambios = 100;
		estadoLatenciaCambios = 0;
	}


	// This function is executed every time you have to perform physical calculations
	void FixedUpdate ()
	{
		estadoLatenciaCambios = estadoLatenciaCambios + 1; //  To calculate when we perform random force modification
		Vector3 movement;  // Direction vector of applied force

		if (NPC) // If it is NPC we perform a random movement
		{
			movement = NPC_leader_mov();
		} 
		else // If it is a physical user we take the instructions of the keyboard
		{
			// We take the information from the keyboard
			if (this.CompareTag ("Team A Player"))  // Player of team A
			{
				moveHorizontal = Input.GetAxis ("Horizontal");  // We use the arrows keys
				moveVertical = Input.GetAxis ("Vertical");
			}
			if (this.CompareTag ("Team B Player"))  // Player of team B
			{
				moveHorizontal = Input.GetAxis ("Horizontal2"); // PENDIENTE modificar para que use otrs teclas
				moveVertical = Input.GetAxis ("Vertical2");
			}

			Vector3 movement_usr = new Vector3 (moveHorizontal, 0.0f, moveVertical);  // Direction vector of applied force
			movement = movement_usr;
		}

		rb.AddForce (movement * General.GetComponent<GeneralScript>().force_leaders);  // We apply force with the corresponding module
	}

	// To eliminate the other objects with which it collides, when they are gains
	// Only the score can be updated here
	void OnTriggerEnter(Collider other) 
	{
		// Check that we have touched a reward
		if (other.gameObject.CompareTag("Ganancia"))
		{
			// Mark the reward as earned
			other.gameObject.SetActive(false);
			General.GetComponent<GeneralScript>().activeProfits = General.GetComponent<GeneralScript>().activeProfits - 1;
			
			// Check the team to update the score
			if (gameObject.CompareTag("Team A Player"))
			{
				General.GetComponent<GeneralScript>().countGanancia_A = General.GetComponent<GeneralScript>().countGanancia_A + 1;
			}
			else if (gameObject.CompareTag("Team B Player"))
			{
				General.GetComponent<GeneralScript>().countGanancia_B = General.GetComponent<GeneralScript>().countGanancia_B + 1;
			}
		}
	}

	// Used to check collisions, in order to compute health
	void OnCollisionEnter(Collision collision)
	{
		// The logic will depend on the current team
		if (gameObject.CompareTag("Team A Player"))
		{
			// Check if we have collided with the opposite team and update the score
			if (collision.gameObject.CompareTag("Team B Walker"))
			{
				General.GetComponent<GeneralScript>().countLife_A = General.GetComponent<GeneralScript>().countLife_A - 1;
			}
		}
		else if (gameObject.CompareTag("Team B Player"))
		{
			// Check if we have collided with the opposite team and update the score
			if (collision.gameObject.CompareTag("Team A Walker"))
			{
				General.GetComponent<GeneralScript>().countLife_B = General.GetComponent<GeneralScript>().countLife_B - 1;
			}
		}
	}

	// Function that calculates the direction of the force that moves the leader (player) when it is an NPC
	/* **********************************************************
	 * 
	 * 					YOU CAN CHANGE THIS
	 * 
	 * You can change this function to modify the behavior of the NPC leader
	 * ********************************************************** */
	Vector3 NPC_leader_mov() 
	{
		// We only modify the force every "numLatenciaCambios" cycles
		if (estadoLatenciaCambios > numLatenciaCambios) {
			moveHorizontal = Random.Range (-1.0f, 1.0f);
			moveVertical = Random.Range (-1.0f, 1.0f);
			estadoLatenciaCambios = 0;
		}

		Vector3 movement_NPC = new Vector3 (moveHorizontal, 0.0f, moveVertical);  // Direction vector of applied force

		return movement_NPC;
	}
}