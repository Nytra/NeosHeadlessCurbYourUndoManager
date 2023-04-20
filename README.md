# HeadlessCurbYourUndoManager

A [NeosModLoader](https://github.com/zkxs/NeosModLoader) mod for [Neos VR](https://neos.com/) Headless Client that deletes any extra UndoManager components that got accidentally spawned in the world. Those extra components normally break Undo functionality until you manually respawn, but with this mod it will be done automatically.

It must be installed on the Neos Headless Client which is hosting the session for it to work.

After it deletes the UndoManager component it will respawn all users, which will make Undo work again. This *should* only ever happen once after the very first user joins the session.

## Installation
1. Install [NeosModLoader](https://github.com/zkxs/NeosModLoader) for the Headless Client.
1. Place [
HeadlessCurbYourUndoManager.dll ](https://github.com/Nytra/NeosHeadlessCurbYourUndoManager/releases/latest/download/HeadlessCurbYourUndoManager.dll) into your `nml_mods` folder. This folder should be at `C:\Program Files (x86)\Steam\steamapps\common\NeosVR\HeadlessClient\nml_mods\` for a default install. You can create it if it's missing, or if you launch the game once with NeosModLoader installed it will create the folder for you.
1. Start the game. If you want to verify that the mod is working you can check your Neos logs.
