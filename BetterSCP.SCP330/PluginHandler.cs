// -----------------------------------------------------------------------
// <copyright file="PluginHandler.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using Exiled.API.Enums;
using Exiled.API.Features;
using HarmonyLib;

namespace Mistaken.BetterSCP.SCP330
{
    /// <inheritdoc/>
    public class PluginHandler : Plugin<Config>
    {
        /// <inheritdoc/>
        public override string Author => "Mistaken Devs";

        /// <inheritdoc/>
        public override string Name => "BetterSCP-SCP330";

        /// <inheritdoc/>
        public override string Prefix => "MSCP330";

        /// <inheritdoc/>
        public override PluginPriority Priority => PluginPriority.Medium;

        /// <inheritdoc/>
        public override Version RequiredExiledVersion => new Version(5, 0, 0);

        /// <inheritdoc/>
        public override void OnEnabled()
        {
            Instance = this;
            harmony = new Harmony("mistaken.betterscp.scp330.patch");
            harmony.PatchAll();

            base.OnEnabled();
        }

        /// <inheritdoc/>
        public override void OnDisabled()
        {
            harmony.UnpatchAll();

            base.OnDisabled();
        }

        internal static PluginHandler Instance { get; private set; }

        private static Harmony harmony;
    }
}
