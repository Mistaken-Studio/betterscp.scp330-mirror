using HarmonyLib;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;

namespace Mistaken.BetterSCP.SCP330;

internal sealed class Plugin
{
    public static Plugin Instance { get; private set; }

    [PluginConfig]
    public Config Config;

    private static readonly Harmony _harmony = new("mistaken.betterscp.scp330.patch");

    [PluginPriority(LoadPriority.Medium)]
    [PluginEntryPoint("BetterSCP-SCP330", "1.0.0", "Plugin that allows upgrading SCP-330", "Mistaken Devs")]
    private void Load()
    {
        Instance = this;
        _harmony.PatchAll();
    }

    [PluginUnload]
    private void Unload()
        => _harmony.UnpatchAll();
}
