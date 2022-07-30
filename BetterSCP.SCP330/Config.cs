// -----------------------------------------------------------------------
// <copyright file="Config.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.ComponentModel;
using Mistaken.Updater.Config;

namespace Mistaken.BetterSCP.SCP330
{
    /// <inheritdoc/>
    public class Config : IAutoUpdatableConfig
    {
        /// <inheritdoc/>
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether debug should be displayed.
        /// </summary>
        [Description("If true then debug will be displayed")]
        public bool VerbouseOutput { get; set; }

        /// <summary>
        /// Gets or sets chance of getting Pink Candy on Fine setting.
        /// </summary>
        [Description("Chance to get Pink Candy on Fine setting")]
        public int FinePinkChance { get; set; } = 10;

        /// <summary>
        /// Gets or sets chance of getting Painkillers when making Pink Candy on Fine setting.
        /// </summary>
        [Description("Chance to get Painkillers when making Pink Candy on Fine setting")]
        public int FinePinkPillsChance { get; set; } = 50;

        /// <summary>
        /// Gets or sets chance of not getting any Candy when upgrading on Very Fine setting.
        /// </summary>
        [Description("Chance to not get any Candy when upgrading on Very Fine setting")]
        public int VeryFineDestroyChance { get; set; } = 25;

        /// <summary>
        /// Gets or sets chance of getting Pink Candy on Very Fine setting.
        /// </summary>
        [Description("Chance to get Pink Candy on Very Fine setting")]
        public int VeryFinePinkChance { get; set; } = 10;

        /// <summary>
        /// Gets or sets chance of getting Painkillers when making Pink Candy on Very Fine setting.
        /// </summary>
        [Description("Chance to get Painkillers when making Pink Candy on Very Fine setting")]
        public int VeryFinePinkPillsChance { get; set; } = 30;

        /// <inheritdoc/>
        [Description("Auto Update Settings")]
        public System.Collections.Generic.Dictionary<string, string> AutoUpdateConfig { get; set; }
    }
}
