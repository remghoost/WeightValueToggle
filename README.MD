# WeightValueToggle

A mod to toggle the display of weight or value in chests / trade windows using the F7 key.

--- 

## Installation
0. **BACK UP YOUR SAVE FILE.**
1. Download from the releases on the right.
2. Place the mod files in modding directory.  
Usually `%USERPROFILE%\AppData\LocalLow\Freehold Games\CavesOfQud\Mods`
3. Make sure you have `Allow scripting mods` enabled in the Options (under Mods).

---

## Possible bugs
- Cursor gets stuck on the top bar of the trade window.  
Just mouse over the screen. Usually a restart fixes this bug all together.

## Other
**This mod is pretty messy.**  
I'm hooking into the `Qud.UI` library and listening for the hotkey on every UI update. This can (and probably will) cause *some* amount of lag, but yeah.

The modding documentation for implementing in-game hotkeys did not work for me (I tried for a few days). Not sure if an update changed something or I'm just daft. Couldn't find another mod that used hotkeys either. But I know how to harmony patch, so I did it that way. lmao.

**If someone wants to make a pull request to make it an in-game hotkey, please do.**

---

## Contributions
**Pull requests welcomed.**  
- `git clone` this repo
- Generate your `Mods.csproj` from the `Modding Toolkit` menu on the left of the title screen.
- Place that into this repo.

## todo
- Not use Harmony patches. lmao.
- Make mod look pretty and upload to Steam Workshop.
- Force the trade/chest UI window to refresh on hotkey press.

### Shoutouts
- Mistral-Large-Instruct-2407
- Cline + VSCODE  

Fun fact, did you know that Mistral has a **free** Mistral-Large API?  
And you can use it with Cline to edit code directly in VSCode?  
**We live in the freaking future.**
