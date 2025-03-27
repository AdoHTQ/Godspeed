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
        public const string GUID = "adohtq.uk.gspd";
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
            
        }

        public void Update()
        {

        }

        [HarmonyPatch]
        public static class Speedometer_Patch
        {
            [HarmonyPrefix]
            [HarmonyPatch(typeof(Speedometer), "OnEnable")]
            public static void patch_OnEnable(Speedometer __instance)
            {
                Logger logger = new Logger(__instance);
                Logger.LogInfo("Speedometer OnEnable");
            }
        }
    }
}
