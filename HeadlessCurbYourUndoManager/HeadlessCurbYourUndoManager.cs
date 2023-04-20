using FrooxEngine;
using FrooxEngine.Undo;
using HarmonyLib;
using NeosModLoader;
using System;
using System.Linq;

namespace HeadlessCurbYourUndoManager
{
    public class HeadlessCurbYourUndoManager : NeosMod
    {
        public override string Name => "HeadlessCurbYourUndoManager";
        public override string Author => "Nytra";
        public override string Version => "1.0.1";
        public override string Link => "https://github.com/Nytra/NeosHeadlessCurbYourUndoManager";

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

                __instance.World.RootSlot.ChildAdded += (slot, child) =>
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
                                foreach (User u in __instance.World.FindUsers((User u2) => u2.ReferenceID != __instance.LocalUser.ReferenceID))
                                {
                                    u.Root.Slot.Destroy();
                                }
                            }
                        }
                    });
                };

                return true;
            }
        }
    }
}