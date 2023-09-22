# NoWinningAllowedGame

This was just a dumb game based off a paradoxical question I saw on some meme with a similar format. To get my friend to play it, I marketed it as a Horror game. I also added a red herring "wager" system. The wager is not real. It gets replaced with the path to system32.
Having the deletion of system32 on the line and a "Attempts Remaining" counter was my way of adding pressure. Of course, I am not actually deleting system32 from anyone's PC. I do end up running a console command found in the InducePanic() function within the GameLogic.cs script


The console command that is executed is as follows:
Process.Start("cmd", "/k tree C:\\");

This is executed when the game closes, or when the attempts run out (the game will force close as well). If you dont know what tree does.. you're on github and you dont know? just google it, its 4:42am and I barely have the energy to type this out.

I am surprised I managed to program this whole thing in in a little under 3 hours. It's been a couple years since I properly work with C# or Unity! I'm mostly an Unreal or Ventuz dev so woohoo!
