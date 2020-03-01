using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*  ***********************
 Script for handling general functions
 Autor : Miguel Angel Fernandez Graciani 2020-01-01
 Control de cambios :
 	Modificacion : Autor: - Fecha : - Comentarios
 Observaciones :
 Entradas:
 	No hay entradas
Salidas :
	No hay salidas
********************* */
public class GeneralScript : MonoBehaviour {

	/// ////////////////////////////
	/// GAME PARAMETERS
	// -	NumTeams = 2; Number of teams
	// -	NumWalkers = 5; Number of walkers in each team
	// -	MassWalker = (Not used); Mass of the walkers
	// -	MaxSpeedWalkers = (Not used);  Maximum speed of the walkers
	// -	MaxForceWalkers = (Not used);  Maximum force applied to walkers (module)
	// -	MassLeader = (Not used); Mass of the leaders
	// -	MaxSpeedLeader = (Not used);  Maximum speed of the leaders
	// -	MaxForceLeader = (Not used);  Maximum force applied to leaders (module)
	// -	MassProfit = (Not used);  Mass of a profit object

	public int countGanancia_A;  // Score points earner
	public int countGanancia_B;  // Score points earner
	public int countLife_A;  // Counter of remaining lives
	public int countLife_B;  // Counter of remaining lives
	public int IniNumberLivesLeader;  // Initial number of lives of the leader
	public int TargetGain;  // Target gain
	public float force_leaders; // Parameter we use to control the modulus of force applied to players
	public float force_walkers; // Parameter we use to control the modulus of force applied to players
//	public long maxPlayTime; // Maximum play time
//	public long iniPlayTime; // Time when the game started
//	public long gameClock; // Elapsed game time


	// Game board parameters
	// public GameObject GameBoard; // For profit objects
	// 


	// User interface texts (canvas)
	public Text countText_A;  // Content of team marker text A
	public Text countText_B;  // Content of team marker text B
	public Text winText;  // End of game text content
	private string winnerTex;  // End of game text content

	// To control Profits
	public GameObject Ganancia; // For profit objects 
								// It will be necessary to relate this object from the unity interface by dragging it to 
								//the corresponding variable in the inspector window corresponding to the object script
	public int numProfits; // Number of profits that must be on the board
	public int activeProfits;  // Number of profits that exist in board must be on the board
	public int createdProfits;  // Total number of profits created during the game

	// ADDED BY TEAM 2

	public bool invisibleWallsActive; // Whether to create or not the invisible walls.
	public GameObject invisibleWalls; // Container for the invisible walls

	public int playTime; // The maximum play time that the match can last (in seconds)
	private float countDown; // Time remaining in the match (in seconds)
	public Text countDownTextContainer; // Text displaying the time remaining
	private string countDownText; // Actual string to display on the counter

	// Use this for initialization
	void Start () {
		IniNumberLivesLeader = 5;
		TargetGain = 4;
		force_leaders = 10.0f;
		force_walkers = 10.0f;

		/* ***
		iniPlayTime = currentTime;
		maxPlayTime 100; // Maximum play time
		gameClock = 0; // Elapsed game time
		** */

		countGanancia_A = 0;  // Score points earner
		countGanancia_B = 0;  // Score points earner
		countLife_A = IniNumberLivesLeader;  // Counter of remaining lives
		countLife_B = IniNumberLivesLeader;  // Counter of remaining lives

		countText_A.text = "Team A Score";  // Content of team marker text A
		countText_B.text = "Team B Score";  // Content of team marker text B
		winText.text = "";
		winnerTex = "None";

		activeProfits = 0;
		createdProfits = 0;
		numProfits = 3;

		// TEAM 2

		// Set an internal clock to count the time that has passed.
		countDown = (float)playTime;

		// If invisibleWallsActive is not true, then the invisible walls must be disabled
		// (if they are inactive, their colliders will not be active)
		if (!invisibleWallsActive)
		{
			invisibleWalls.SetActive(false);
		}
	}

	// This function is executed at every step (frames) of the game.
	void Update ()
	{   
		// End of game conditions
		// The TargetGain value is reached	
		if (countGanancia_A >= TargetGain) 
		{
			winnerTex = "Team A";
			SetTextAndEnd ();
		}
		if (countGanancia_B >= TargetGain) 
		{
			winnerTex = "Team B";
			SetTextAndEnd ();
		}
			// A leader runs out of lives
		if (countLife_A <= 0) 
		{
			winnerTex = "Team B";
			SetTextAndEnd ();
		}
		if (countLife_B <= 0) 
		{
			winnerTex = "Team A";
			SetTextAndEnd ();
		}

		// We update the scores
		SetCountText ();

		// Profits control. We generate the profits that have been destroyed
		while (activeProfits < numProfits) 
		{
			creaGanancia ();
		}

		// Update the timer and check the time that has passed
		countDown -= Time.deltaTime;
		if (countDown <= 0.0f)
		{
			// Counter is below or equal to 0: time is over

			// Check who won: first, check for points
			if (countGanancia_A > countGanancia_B)
			{
				winnerTex = "Team A";
			}
			else if (countGanancia_B > countGanancia_A)
			{
				winnerTex = "Team B";
			}
			else
			{
				// A tie has happened: tie break will be decided by health remaining
				if (countLife_A > countLife_B)
				{
					winnerTex = "Team A";
				}
				else if (countLife_B > countLife_A)
				{
					winnerTex = "Team B";
				}
				else
				{
					// A tie has happened in score and health: simply declare the game as a tie
					winnerTex = "Tie!";
				}
			}

			// Display the final results and end the game
			SetTextAndEnd();

			
		}
		else
		{
			// Counter above 0: the game is still in play
			// We use a ceiling to ensure that the countdown is rounded up to the closest number (f. ex. we're interested in displaying 1 if 0.3 secs are remaining)
			countDownText = Mathf.CeilToInt(countDown).ToString() + "s";
			countDownTextContainer.text = countDownText;
		}
	}

	// To create profits
	void creaGanancia() {
		GameObject otherGanancia = Instantiate(Ganancia); // We generate a new game object as an instance of the corresponding prefad
		GameObject objetPikUps = GameObject.Find ("PikUps"); // We obtain the object of game "PikUps", and then turn object "otherGanancia" into a descendant of this
			// 
		float x_coordinate = Random.Range (-45.0f, 45.0f);
		float z_coordinate = Random.Range (-45.0f, 45.0f);
		Vector3 ProfitLocation  = new Vector3 (x_coordinate, 3.0f, z_coordinate);  // random profit position
		otherGanancia.transform.position = ProfitLocation;

		// otherGanancia.transform.SetParent(objetPikUps);  // Becomes a descendant of Profits

		activeProfits = activeProfits + 1;
		createdProfits = createdProfits +1;
	}

	// We update the scores
	void SetCountText ()
	{
		countText_A.text = "Puntos: " + countGanancia_A.ToString ()+" - lifes : " + countLife_A.ToString ();
		countText_B.text = "Puntos: " + countGanancia_B.ToString ()+" - lifes : " + countLife_B.ToString ();
		// Incluir gameClock
	}

	// The game is over
	void SetTextAndEnd ()
	{
		// If there is a tie, it takes priority
		if (winnerTex.Equals("Tie!"))
		{
			winText.text = winnerTex;
		}
		else
		{
			winText.text = "The winner is : " + winnerTex + "\n Team A : " + countText_A.text + "\n Team B : " + countText_B.text;
		}

		// Hide the timer (since it's no longer relevant)
		countDownTextContainer.gameObject.SetActive(false);

		Time.timeScale = 0;  // Paramos el juego al llegar al final
	}
}
