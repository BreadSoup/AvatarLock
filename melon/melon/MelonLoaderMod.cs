using MelonLoader;
using SLZ.VRMK;
using BoneLib;
using HarmonyLib;
using SLZ.Rig;
using UnityEngine;
using System;
using System.Collections.Generic;
using Il2CppSystem;
using SLZ.Marrow.Warehouse;
using SLZ.Bonelab;
using Cysharp;
using UnhollowerBaseLib;
using SLZ.Interaction;
using BoneLib.BoneMenu;
using BoneLib.BoneMenu.Elements;





namespace melon
{
    public static class BuildInfo
    {
        public const string Name = "melon"; // Name of the Mod.  (MUST BE SET)
        public const string Author = null; // Author of the Mod.  (Set as null if none)
        public const string Company = null; // Company that made the Mod.  (Set as null if none)
        public const string Version = "1.0.0"; // Version of the Mod.  (MUST BE SET)
        public const string DownloadLink = null; // Download Link for the Mod.  (Set as null if none)
    }
    public static class AvatarExtensions
    {
        public static bool IsEmptyRig(this Avatar avatar)
        {
            return avatar.name == "[RealHeptaRig (Marrow1)]";
        }
    }
    public class MelonLoaderMod : MelonMod
    {




        [HarmonyPatch(typeof(RigManager), nameof(RigManager.SwapAvatar))]

        public static class RigManagerPatch
        {
            public static bool Prefix() => false;

        }

        public class Melon : MelonMod
        {
            [System.Obsolete]
            public override void OnLevelWasLoaded(int level)
            {
                LoggerInstance.Msg("Hiiiiiiiiiiii");
            }
            public override void OnInitializeMelon()
            {
                LoggerInstance.Msg("Hello!");
                Hooking.OnSwitchAvatarPrefix += (avatar) =>
                    {
                        if (avatar != null)
                        {
                            if (avatar != null)
                            {
                                Avatar CurrentAvatar = Player.GetCurrentAvatar();

                                string Short = "fa534c5a83ee4ec6bd641fec424c4142.Avatar.CharFurv4GB";

                                var barcode = BoneLib.Player.rigManager._avatarCrate.Barcode.ID;

                                if (barcode != Short)
                                {
                                    LoggerInstance.Msg("Switcheroo");
                                    Player.rigManager.SwapAvatarCrate(Short);
                                    LoggerInstance.Msg(barcode);

                                }
                            }
                        };
                    };
            }
        }
    }

}





