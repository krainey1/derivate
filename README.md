# Derivate
Derivate is a 2D Dungeon Crawler (demo) made with Unity. Currently, it shows the proof of concept for the game - where players navigate through this dungeon and battle enemies by solving the derivative questions the enemies present before they drain your health. Further concept information/notes can be found in the now updated game design document in the repository.

# Use
Currently, one must obtain an OpenAI API key and set it up in a .env before starting the api using:

flask --run api run where the api is served using flasks development server (so I did not put this to production/deploy) 

Then the game can be run in the unity editor or a build. (OpenAI is used to generate player questions, mainly as an experiment -> would likely be a different system in the future)

# Where things are
/api - contains the .py api file and .gitignore (no api keys to the repository...)

/assets - contains all of the assets used for the build (usually in folders depending on what it is... i.e enemy is all the enemy assets/prefab, music, etc.), scenes, scripts for the game in a single folder, some settings, etc. 

/ProjectSettings - has all of the overarching project settings

/Packages - the package lock 

Note: This was a project for CSE 489 Educational Games 

