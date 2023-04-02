using MelonLoader;
using SLZ.VRMK;
using BoneLib;
using HarmonyLib;
using SLZ.Rig;
using UnityEngine;
using BoneLib.BoneMenu;
using BoneLib.BoneMenu.Elements;
using System;


namespace melon
{

    public static class BuildInfo //Stuff that does stuff inside the log to tell you it was made by me
    {
        public const string Name = "Avatar Locker"; // Name of the Mod.  (MUST BE SET)
        public const string Author = "Bread Soup"; // Author of the Mod.  (Set as null if none)
        public const string Company = null; // Company that made the Mod.  (Set as null if none)
        public const string Version = "1.0.0"; // Version of the Mod.  (MUST BE SET)
        public const string DownloadLink = null; // Download Link for the Mod.  (Set as null if none)
    }



    [HarmonyPatch(typeof(RigManager), nameof(RigManager.SwapAvatar))]

    public static class RigManagerPatch
    {
        public static bool Prefix() => false;
    
    }

    
    public class Melon : MelonMod //I shouldnt of named this file melon but its a lot of work to change and I dont want to do that
    {

        public override void OnInitializeMelon()//Sets up melon prefs and bonemneu also on level loaded stuff
        {
            Melon.MelonPrefs();
            Melon.CreateBoneMenu();
            Melon.CreateCurrentAvatarLockNameThing();
            Hooking.OnLevelInitialized += (_) => { OnSceneAwake(); };


        }


        public static void MelonPrefs() //Creates stuff in the MelonPrefrances file so it saves for every load
        {
            Melon.MelonPrefCategory = MelonPreferences.CreateCategory("Avatar Locker");
            Melon.MelonPrefEnabled = Melon.MelonPrefCategory.CreateEntry<bool>("IsEnabled", true, null, null, false, false, null, null);
            Melon.IsEnabled = Melon.MelonPrefEnabled.Value;
            Melon.CurrentBarcode = Melon.MelonPrefCategory.CreateEntry<string>("Currently locked avatar", null, null, null, false, false, null, null);
            Melon.CurrentNameValue = Melon.MelonPrefCategory.CreateEntry<string>("Current name of locked avatar", null, null, null, false, false, null, null);

        }

        public static void CreateBoneMenu() //creates bonemenu
        {

            var category = MenuManager.CreateCategory("Avatar Locker", Color.cyan); //this just makes it so I dont have to add all of this to the begining of every element
            category.CreateBoolElement("Mod Toggle", Color.yellow, Melon.IsEnabled, new Action<bool>(Melon.OnSetEnabled)); //creates a ON/OFF switch for the mod but a if statement within the mod is needed to make it work


            category.CreateFunctionElement("Switch lock to current avatar", Color.white, delegate () //This gets the barcode of the avatar to switch and also sets a button to the name of the avatar
             {
                 Melon.CurrentBarcode.Value = Player.rigManager._avatarCrate.Barcode.ID; //This gets the barcode of the current avatar
                 Avatar avatar = Player.GetCurrentAvatar();
                 Melon.CurrentNameValue.Value = BoneLib.HelperMethods.GetCleanObjectName(avatar.name); //Gets a clean name of the avatar so its not too long within bonemneu
                 CurrentName.SetName(Melon.CurrentNameValue.Value); //Sets the name of the button 


             });


        }
        private static void OnSceneAwake() //Just so the button doesnt come up blank on first load 
        {
            CurrentName.SetName(Melon.CurrentNameValue.Value);
        }
        public static void CreateCurrentAvatarLockNameThing() //This creates a button the can be acsessed anywhere
        {
            CurrentName = MenuManager.CreateCategory("Avatar Locker", Color.cyan).CreateFunctionElement("No avatar lock found set one with the button above", Color.white, delegate ()
            {

            });
        }

        public static void OnSetEnabled(bool value) // Some extra stuff for the on enabled button
        {
            Melon.IsEnabled = value;
            Melon.MelonPrefEnabled.Value = value;
            Melon.MelonPrefCategory.SaveToFile(false);
        }

        public override void OnPreferencesLoaded() //Just sets IsEnabled to the value of the on off button for easy read I think
        {

            Melon.IsEnabled = Melon.MelonPrefEnabled.Value;

        }


        public override void OnUpdate() // On update is every tick
        {
            
            if (Melon.IsEnabled) //Checks if the mod is enabled or not
            {

                if (Player.rigManager != null)
                {
                    var barcode = BoneLib.Player.rigManager._avatarCrate.Barcode.ID; //var that gets the barcode so there is no overlap to make it easier to read 

                    if (barcode != Melon.CurrentBarcode.Value) //Checks if the current barcode is equel to the locked one
                    {
                        Player.rigManager.SwapAvatarCrate(Melon.CurrentBarcode.Value); //Swaps the avatar
                        
                    }

                }
            } 

        }
        //Bunch of extra stuff that is needed
        public static MelonPreferences_Category MelonPrefCategory { get; private set; }
        public static BoolElement BoneMenuEnabledElement { get; private set; }
        public static MenuCategory BoneMenuCategory { get; private set; }
        public static MelonPreferences_Entry<bool> MelonPrefEnabled { get; private set; }
        public static MelonPreferences_Entry<string> CurrentBarcode { get; private set; }
        public static bool IsEnabled { get; private set; }
        private static FunctionElement CurrentName;
        public static MelonPreferences_Entry<string> CurrentNameValue { get; private set; }


        //If you're readinging this for reference or something for whatever reason give your thanks to Adi#6627 and yellowyears#8953 and the rest of the bonelab discord, honestly would of given up a lonnnng time ago without them

       

    }


}






//Also if by chance you care enough about patching out avatar switching while the mod was on I was too stupid to figure it out but here is a few snipits that might help

/*
 * 
 * private bool Locked = false;
public static void Prefix(RigManager __instance, Avatar avatar)
{
    // Do anything
    if (No idea what to put in here)
    {

    }
}

        public override void OnUpdate() // On update is every tick
{

    if (Melon.IsEnabled) //Checks if the mod is enabled or not
    {
        if (Locked == true) return;
        if (Player.rigManager != null)
        {
            var barcode = BoneLib.Player.rigManager._avatarCrate.Barcode.ID; //var that gets the barcode so there is no overlap to make it easier to read 

            if (barcode != Melon.CurrentBarcode.Value) //Checks if the current barcode is equel to the locked one
            {
                Player.rigManager.SwapAvatarCrate(Melon.CurrentBarcode.Value); //Swaps the avatar
                Locked = true;
            }

        }
    } 


 */






