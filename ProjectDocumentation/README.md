RUIN GAME

*Are marked as done

Sprint - 2 - 16.2.2021


### Skill

	TARGETING
		CLOSEST TARGET
		AREA AROUND PLAYER (CAN BE GIVEN A PREFERRED RADIUS)
		ALL ENEMIES IN RANGE
		ALL RIGIDBODIES IN RANGE
			SELF?
	SKILL EXECUTION
		GAIN TARGET FROM SKILLTARGETER
		EXECUTE SKILL ON THE TARGET(s) CHOSEN

### PLAYER CONTROLS

		*PLAYER MANAGER
			STATUS
	STATES?
	*DIFFERENT MOVEMENT SYSTEM (Rigidbody variant)
	
### ANIMATION CANCELLING

	*Attack animation startup frame / Cancellable with sprint
	*Attack animation return frame / Cancellable with sprint

	Skill animation startup frame / NOT Cancellable
	Skill animation return frame / Cancellable with sprint or attack
	
	*Movement / Cancellable
	
	Jump / NOT Cancellable
	

### ATTACK

	*MAKE TRANSITION BETWEEN ATTACKING AND MOVING SMOOTHER
	*ATTACKING SHOULD STOP MOMENTUM
	
		
	ADD RANGED ATTACK
		*MAKE ATTACK FIND NEAREST TARGET FOR AUTOAIM
	
	
### ENEMY
	
	ADD ENEMY AI BEHAVIOURS FOR DIFFERENT TYPES OF ENEMIES
	
### ROOM

	SELECT ROOM TYPE / PREFAB
	
	*INSTANTIATE ROOM
		*ENTRY, EXIT
		*SPAWNER
			*SPAWNING LOCATIONS
		ALL OBJECTS
		
	REMOVE ROOM FROM LIST OF POSSIBLE ROOMS
	
	*SPAWN PLAYER IN -> MOVE PLAYER TO STARTING POSITION
	*WAIT UNTIL PLAYER IS LOADED
	*START ENCOUNTER
		*CHECK IF ENEMIES ARE DEAD WHEN PLAYER TRIES TO EXIT ROOM
		
	*OnRoomExit, DESTROY PREVIOUS ROOM
	CLEANUP
		
		

### SPAWNER
	
	*GET ROOM SPAWNER LOCATIONS
		SPAWNER LOCATION AS ENEMY COORDINATOR?
	*SET ENEMIES TO EACH SPAWNER LOCATION
		ENEMY TYPES?
	*START SPAWNING AFTER AN INITIAL DELAY
	*SPAWN ENEMIES IN INTERVALS UNTIL ALL ENEMIES ARE SPAWNED
	

### MAIN MENU

	*PLAY GAME
		GET A CHOICE OF SKILL 
			SKILL GETS STORED INTO LIST OF SKILLS TO ADD TO PLAYER
		REPEAT
		REPEAT
		REPEAT
		
### GAMEMANAGER SETUP
	
		* CREATE PLAYER 
			BASED ON SELECTIONS ( WEAPON, SKILLS )
		* SETUP CAMERA TO FOLLOW PLAYER
		* SETUP PLAYER REFERENCES
		SETUP UI
			TRACK PLAYER MANAGER REFERENCE
		*START ROOM MANAGER ROUTINE
	*STOP ROOM MANAGER IF PLAYER DIES, RETURN TO MAIN MENU
	
