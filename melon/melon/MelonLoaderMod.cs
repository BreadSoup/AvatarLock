using MelonLoader;
using SLZ.VRMK;
using BoneLib;
using UnityEngine;
using System;
using System.Collections.Generic;
using Il2CppSystem;
using SLZ.Rig;
using SLZ.Marrow.Warehouse;
using SLZ.Bonelab;
using Cysharp;



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

    public class Melon : MelonMod
    {

        public override void OnUpdate()
        {

            Avatar avatar = Player.GetCurrentAvatar();

            string Short = "fa534c5a83ee4ec6bd641fec424c4142.Avatar.CharFurv4GB";

            var barcode = BoneLib.Player.rigManager._avatarCrate.Barcode.ID;

            if (barcode != Short)
            {
                Player.rigManager.SwapAvatarCrate(Short);
                LoggerInstance.Msg(barcode);

            }
            if (BoneLib.HelperMethods.GetCleanObjectName(avatar.name) == "char_strong")
            {
                LoggerInstance.Msg(barcode);
            }
        }

        private void PlayerAvatarSwitched(Avatar newAvatar)
        {

        }
    }
}
