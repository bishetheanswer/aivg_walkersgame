using UnityEngine;
using System.Collections;
// using static ObjetoGlobal;

/*  ***********************
Script for handling walkers in random mode
 Autor : Miguel Angel Fernandez Graciani 2020-01-01
 Control de cambios :
 	Modificacion : Autor: - Fecha : - Comentarios
 Observaciones :
 Entradas:
 	No hay entradas
Salidas :
	No hay salidas
********************* */
public class RandonMovement : MonoBehaviour {

	public GameObject General; // To access its properties
							// It will be necessary to relate this object from the unity interface by dragging it to 
							//the corresponding variable in the inspector window corresponding to the object script

	// We declare the variables for the coordinates
	private float moveHorizontal = 0.0f;
	private float moveVertical = 0.0f;
	//To handle the period between changes
	private int numLatenciaCambios = 100;
	private int estadoLatenciaCambios = 0;

	private Rigidbody rb; // We need a rigid body to execute the force on it

	void Start ()
	{
		rb = GetComponent<Rigidbody>();  // We take the rigitbody of the object that runs the script
	}

	void Update ()
	{	// We control the period between changes. It can be done in multiple ways
		if (estadoLatenciaCambios > numLatenciaCambios) {
			moveHorizontal = Random.Range(-1.0f, 1.0f);
			moveVertical = Random.Range(-1.0f, 1.0f);
			estadoLatenciaCambios = 0;
		}
		estadoLatenciaCambios = estadoLatenciaCambios +1;
	}

	// Used to apply the forces
	void FixedUpdate()
	{
		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical); // Vercor of force direction

		rb.AddForce(movement * General.GetComponent<GeneralScript>().force_walkers);  // We apply force with the corresponding module
	}
}
