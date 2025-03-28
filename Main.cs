using System;
using HarmonyLib;
using BepInEx;
using UnityEngine;
using UObj = UnityEngine.Object;
using BepInEx.Logging;
using MonoMod.RuntimeDetour;
using SettingsMenu.Components.Pages;
using System.Runtime.CompilerServices;

namespace Godspeed
{
    internal class PluginInfo
    {
        public const string Name = "Godspeed";
        public const string GUID = "adohtq.ultrakill.godspeed";
        public const string Version = "1.0.0";
    }

    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    class Godspeed : BaseUnityPlugin
    {
        public void Awake()
        {
            Logger.LogInfo("godspeed loaded");
        }

        public void Start()
        {
            Harmony harmony = new Harmony(PluginInfo.GUID);
            harmony.PatchAll(typeof(Movement_Patch));
        }

        public void Update()
        {

        }
    }

    [HarmonyPatch(typeof(NewMovement))]
    public static class Movement_Patch
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(NewMovement), nameof(NewMovement.Start))]
        public static void patch_Update(NewMovement __instance)
        {
            __instance.gameObject.AddComponent<SpeedThreshold>(); // Add the SpeedThreshold component to the NewMovement instance
        }
    }
}
