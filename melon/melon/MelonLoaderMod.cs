using MelonLoader;
using SLZ.VRMK;
using BoneLib;
using UnityEngine;
using System;
using Il2CppSystem;

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
        public void SwapAvatar(Avatar newAvatar)
        {
            Avatar newavatar = newAvatar;
        }
        public override void OnUpdate()
        {
            // string avatarName won't get send in as it isn't part of ML, you need to get it yourself
            // (probably though the getcurrentavatar method)
            Avatar avatar = Player.GetCurrentAvatar();
            if (BoneLib.HelperMethods.GetCleanObjectName(avatar.name) == "char_tall")
            {
                LoggerInstance.Msg(avatar.name);
                Player.rigManager.SwapAvatar(newAvatar);


            }
        }

    }

}
