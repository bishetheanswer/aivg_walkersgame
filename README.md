# Walkers Game - Artificial Intelligence in Videogames 2019/20
This repository contains the base implementation to be used by all teams of the Walkers Game from the "Artificial Intelligence in Videogames" subject (Computer Science degree in the UCLM, Albacete)

All changes done will be detailed in the following file.

## Week 1 - Group 2

* Updated the version to Unity 2019 3.2f.
* Moved the camera to have a cenital view.
* Updated the colors to be less damaging to the eyes.
* Added a global variable to the main script (invisibleWalls) to decide if the zone is surrounded by invisible walls or not.
* Added a death plane below the playzone that destroys any walker or player that touches it.
* Added a timer that ticks down (the match duration can be selected from the options in the general script). Once the clock reaches 0, a winner will be selected (the player with the best score, if there is a tie then the player with the most health, and if there is a tie no winner is declared)
* Ensured that if a team leader reaches the death plane, the game ends.
