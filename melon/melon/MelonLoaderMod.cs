using MelonLoader;
using SLZ.VRMK;
using BoneLib;
using HarmonyLib;
using SLZ.Rig;
using UnityEngine;
using BoneLib.BoneMenu;
using BoneLib.BoneMenu.Elements;
using System;
using System.Collections.Generic;
using SLZ.Marrow.Warehouse;
using SLZ.Bonelab;
using Cysharp;
using UnhollowerBaseLib;
using SLZ.Interaction;
using Cpp2ILInjected;





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



    public class Melon : MelonMod
    {
        public override void OnInitializeMelon()
        {
            Melon.MelonPrefs();
            Melon.CreateBoneMenu();
        }

        public static void MelonPrefs() //might be broken I havent testested it yet
        {
            Melon.MelonPrefCategory = MelonPreferences.CreateCategory("Avatar Locker");
            Melon.MelonPrefEnabled = Melon.MelonPrefCategory.CreateEntry<bool>("IsEnabled", true, null, null, false, false, null, null);
            Melon.IsEnabled = Melon.MelonPrefEnabled.Value;
            Melon._previousIsEnabled = Melon.IsEnabled;
            Melon.CurrentBarcode = Melon.MelonPrefCategory.CreateEntry<string>("Currently locked avatar", null, null, null, false, false, null, null);
            Melon.CurrentName = Melon.MelonPrefCategory.CreateEntry<string>("Current name of locked avatar", null, null, null, false, false, null, null);

        }

        public static void CreateBoneMenu()
        {
            var category = MenuManager.CreateCategory("Avatar Locker", Color.cyan);
            category.CreateBoolElement("Mod Toggle", Color.yellow, Melon.IsEnabled, new Action<bool>(Melon.OnSetEnabled));
            FunctionElement AvatarValue = null;


            category.CreateFunctionElement("Switch lock to current avatar", Color.white, delegate ()
                {
                    Melon.CurrentBarcode.Value = Player.rigManager._avatarCrate.Barcode.ID;
                    MelonLogger.Msg(Melon.CurrentBarcode.Value);
                    Melon.CurrentName.Value = Player.GetCurrentAvatar().name;
                    AvatarValue.SetName(Melon.CurrentName.Value);


                });
            AvatarValue = category.CreateFunctionElement("Current avtar lock name", Color.white, delegate ()
            {

            });



        }

        public static void OnSetEnabled(bool value)
        {
            Melon.IsEnabled = value;
            Melon.MelonPrefEnabled.Value = value;
            Melon.MelonPrefCategory.SaveToFile(false);
        }

        public override void OnPreferencesLoaded()
        {
      
            Melon.IsEnabled = Melon.MelonPrefEnabled.Value;
           
        }

            [HarmonyPatch(typeof(RigManager), nameof(RigManager.SwapAvatar))]

        public static class RigManagerPatch
        {
            public static bool Prefix() => false;
        }

 
        public override void OnUpdate()
        {
            if (Melon.IsEnabled)
            {
                Avatar CurrentAvatar = Player.GetCurrentAvatar();

                var barcode = BoneLib.Player.rigManager._avatarCrate.Barcode.ID;
                if (barcode != Melon.CurrentBarcode.Value)
                {
                    LoggerInstance.Msg("Switcheroo");
                    Player.rigManager.SwapAvatarCrate(Melon.CurrentBarcode.Value);
                    LoggerInstance.Msg(barcode);

                }
            }



        }
        public static MelonPreferences_Category MelonPrefCategory { get; private set; }
        public static BoolElement BoneMenuEnabledElement { get; private set; }
        public static MenuCategory BoneMenuCategory { get; private set; }
        public static MelonPreferences_Entry<bool> MelonPrefEnabled { get; private set; }
        public static MelonPreferences_Entry<string> CurrentBarcode { get; private set; }
        public static MelonPreferences_Entry<string> CurrentName { get; private set; }
        public static bool IsEnabled { get; private set; }
        private static bool _previousIsEnabled;




    }


}







