using System.ComponentModel;

namespace Mistaken.BetterSCP.SCP330
{
    public sealed class Config
    {
        [Description("If true then debug will be displayed")]
        public bool Debug { get; set; }

        [Description("Chance to get Pink Candy on Fine setting")]
        public int FinePinkChance { get; set; } = 10;

        [Description("Chance to get Painkillers when making Pink Candy on Fine setting")]
        public int FinePinkPillsChance { get; set; } = 50;

        [Description("Chance to not get any Candy when upgrading on Very Fine setting")]
        public int VeryFineDestroyChance { get; set; } = 25;

        [Description("Chance to get Pink Candy on Very Fine setting")]
        public int VeryFinePinkChance { get; set; } = 10;

        [Description("Chance to get Painkillers when making Pink Candy on Very Fine setting")]
        public int VeryFinePinkPillsChance { get; set; } = 30;
    }
}
