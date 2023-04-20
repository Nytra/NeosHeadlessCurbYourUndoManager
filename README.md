# HeadlessCurbYourUndoManager

A [NeosModLoader](https://github.com/zkxs/NeosModLoader) mod for [Neos VR](https://neos.com/) Headless Client that stops the headless from spawning more than one UndoManager in the world, which normally breaks Undo for everyone, but with this mod it will be fixed!

It must be installed on the Neos Headless Client which is hosting the session for it to work.

It will delete the extra UndoManager component and then respawn all users, which will fix undo. This will only happen when the very first user joins the session.

## Installation
1. Install [NeosModLoader](https://github.com/zkxs/NeosModLoader) for the Headless Client.
1. Place [
HeadlessCurbYourUndoManager.dll ](https://github.com/Nytra/NeosHeadlessCurbYourUndoManager/releases/latest/download/HeadlessCurbYourUndoManager.dll) into your `nml_mods` folder. This folder should be at `C:\Program Files (x86)\Steam\steamapps\common\NeosVR\HeadlessClient\nml_mods\` for a default install. You can create it if it's missing, or if you launch the game once with NeosModLoader installed it will create the folder for you.
1. Start the game. If you want to verify that the mod is working you can check your Neos logs.
