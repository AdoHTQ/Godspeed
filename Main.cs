﻿using System;
using HarmonyLib;
using PluginConfig.API;
using BepInEx;
using UnityEngine;
using UObj = UnityEngine.Object;
using BepInEx.Logging;
using MonoMod.RuntimeDetour;
using SettingsMenu.Components.Pages;
using System.Runtime.CompilerServices;
using PluginConfiguratorComponents;
using PluginConfig.API.Fields;

namespace Godspeed
{
    internal class PluginInfo
    {
        public const string Name = "Godspeed";
        public const string GUID = "adohtq.ultrakill.godspeed";
        public const string Version = "1.0.0";
    }

    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    [BepInDependency("com.eternalUnion.pluginConfigurator")]
    class Godspeed : BaseUnityPlugin
    {
        private PluginConfigurator config;

        public static FloatField speedThreshold;
        public static FloatField leniency;
        public static EnumField<Punishments> punishment;

        public static NewMovement player;

        public enum Punishments
        {
            None,
            Death,
            Damage,
            Maurice
        }

        public void Awake()
        {
            config = PluginConfigurator.Create("Godspeed", "godspeed.settings");

            speedThreshold = new FloatField(config.rootPanel, "Speed Threshold", "godspeed.settings.speedThreshold", 20f)
            {
                minimumValue = 0f,
                maximumValue = 100f
            };
            
            leniency = new FloatField(config.rootPanel, "Leniency Seconds", "godspeed.settings.leniency", 1f)
            {
                minimumValue = 0f,
                maximumValue = 60f
            };

            punishment = new EnumField<Punishments>(config.rootPanel, "Thy Punishment", "godspeed.settings.punishment", Punishments.Death);
        }

        public void Start()
        {
            Harmony harmony = new Harmony(PluginInfo.GUID);
            harmony.PatchAll(typeof(Movement_Patch));
            //harmony.PatchAll(typeof(BossHealthBarTemplate_Patch));
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
            Godspeed.player = __instance;
            __instance.gameObject.AddComponent<SpeedThreshold>(); // Add the SpeedThreshold component to the NewMovement instance
        }
    }

    //[HarmonyPatch(typeof(BossHealthBarTemplate))]
    //public static class BossHealthBarTemplate_Patch
    //{
    //    [HarmonyPostfix]
    //    [HarmonyPatch(typeof(BossHealthBarTemplate), nameof(BossHealthBarTemplate.Initialize))]
    //    public static void patch_Initialize(BossHealthBarTemplate __instance, object[] __args, float __introCharge)
    //    {
    //        BossHealthBar? bossBar = __args[0] as BossHealthBar;
    //        if (bossBar == null) return;
    //        if (bossBar.source is CustomBossBar) __introCharge = bossBar.source.Health;
    //    }
    //}
}
