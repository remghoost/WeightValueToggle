# WeightValueToggle

A mod for Caves of Qud to toggle the display of weight or value in chests / trade windows using the F7 key.

--- 

<h2>Installation</h2>

0. **BACK UP YOUR SAVE FILE.**
1. [Download WeightValueToggle.zip from the releases on the right.](https://github.com/remghoost/WeightValueToggle/releases/tag/0.0.1)
2. Extract the mod files in modding directory.  
Usually `%USERPROFILE%\AppData\LocalLow\Freehold Games\CavesOfQud\Mods`
3. Make sure you have `Allow scripting mods` enabled in the Options (under Mods).
4. Enable mod in the modding menu.


<h2>Possible bugs</h2>

- Cursor gets stuck on the top bar of the trade window.  
Just mouse over the screen. Usually a restart fixes this bug all together.

- If you run into anything else, please submit an issue.


<h2>Other</h2>

**This mod is pretty messy.**  
I'm hooking into the `Qud.UI` library and listening for the hotkey on every UI update. This can (and probably will) cause *some* amount of lag, but yeah.

The modding documentation for implementing in-game hotkeys did not work for me (I tried for a few days). Not sure if an update changed something or I'm just daft. Couldn't find another mod that used hotkeys either. But I know how to harmony patch, so I did it that way. lmao.

**If someone wants to make a pull request to make it an in-game hotkey, please do.**

---


<h2>Contributions</h2>

**Pull requests welcomed.**  
- `git clone` this repo
- Generate your `Mods.csproj` from the `Modding Toolkit` menu on the left of the title screen.
- Place that into this repo.


<h2>todo</h2>

- Not use Harmony patches. lmao.
- Make mod look pretty and upload to Steam Workshop.
- Force the trade/chest UI window to refresh on hotkey press.
- Allow custom hotkey assignment in-game.


<h2>Shoutouts</h2>

- Mistral-Large-Instruct-2407
- Cline + VSCODE  

Fun fact, did you know that Mistral has a **free** Mistral-Large API?  
And you can use it with Cline to edit code directly in VSCode?  
**We live in the freaking future.**
