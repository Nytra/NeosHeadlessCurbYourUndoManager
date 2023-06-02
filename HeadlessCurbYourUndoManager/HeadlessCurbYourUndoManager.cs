using FrooxEngine;
using FrooxEngine.Undo;
using HarmonyLib;
using NeosModLoader;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HeadlessCurbYourUndoManager
{
	public class HeadlessCurbYourUndoManager : NeosMod
	{
		public override string Name => "HeadlessCurbYourUndoManager";
		public override string Author => "Nytra";
		public override string Version => "1.0.3";
		public override string Link => "https://github.com/Nytra/NeosHeadlessCurbYourUndoManager";

		private static Dictionary<World, bool> worldDontDestroyMap = new Dictionary<World, bool>();

		public override void OnEngineInit()
		{
			Harmony harmony = new Harmony($"owo.{Author}.{Name}");
			Type neosHeadless = AccessTools.TypeByName("NeosHeadless.Program");
			if (neosHeadless != null)
			{
				Debug("Patching.");
				harmony.PatchAll();
			}
		}

		[HarmonyPatch(typeof(UndoManager), "OnAttach")]
		class HeadlessCurbYourUndoManagerPatch
		{
			public static bool Prefix(UndoManager __instance)
			{
				Debug($"UndoManager attached in world {__instance.World.Name}!");

				if (!worldDontDestroyMap.ContainsKey(__instance.World))
				{
					worldDontDestroyMap.Add(__instance.World, false);
					__instance.World.RootSlot.ChildAdded += (slot, child) =>
					{
						if (worldDontDestroyMap[child.World] == false)
						{
							child.RunInUpdates(0, () =>
							{
								if (child.Name == "Undo Manager")
								{
									Debug("Found the extra Undo Manager slot!");
									var undoManager = child.GetComponent<UndoManager>();
									if (undoManager != null && child.World.RootSlot.GetComponentsInChildren<UndoManager>().Count >= 2)
									{
										Debug("Found the extra UndoManager component! Destroying it!");

										// The slot is marked as protected, so it cannot be destroyed, so here I am just renaming it to something else
										child.Name = "Destroyed Undo Manager";
										undoManager.Destroy();

										Debug("Respawning all users.");
										foreach (User u in child.World.FindUsers((User u2) => u2.ReferenceID != child.LocalUser.ReferenceID))
										{
											u.Root.Slot.Destroy();
										}

										// disable the mod in this world because the destroy code already executed
										worldDontDestroyMap[child.World] = true;

										Debug($"Mod has been disabled in world {child.World.Name}.");
									}
								}
							});
						}
					};

					__instance.World.UserSpawn += (user) =>
					{
						if (worldDontDestroyMap[user.World] == false)
						{
							// user spawn disables the mod in this world
							worldDontDestroyMap[user.World] = true;
							Debug($"User spawned. Mod has been disabled in world {user.World.Name}.");
						}
					};

					__instance.World.RunInSeconds(5, () => 
					{
						if (worldDontDestroyMap[__instance.World] == false)
						{
							// disable the mod in this world after 5 seconds
							worldDontDestroyMap[__instance.World] = true;
							Debug($"Timer elapsed. Mod has been disabled in world {__instance.World.Name}.");
						}
					});
				}

				return true;
			}
		}
	}
}