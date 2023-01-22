using System;
using System.Linq;
using HarmonyLib;
using InventorySystem.Items.Pickups;
using InventorySystem.Items.Usables.Scp330;
using Mistaken.API.Utilities;
using PluginAPI.Core;
using Scp914;
using UnityEngine;

namespace Mistaken.BetterSCP.SCP330;

[HarmonyPatch(typeof(Scp914Upgrader), nameof(Scp914Upgrader.ProcessPickup))]
internal static class Scp914UpgraderPatch
{
    public static readonly CandyKindID[] CandyList = new[]
    {
        CandyKindID.Yellow,
        CandyKindID.Green,
        CandyKindID.Red,
        CandyKindID.Purple,
        CandyKindID.Blue,
        CandyKindID.Rainbow,
        CandyKindID.Pink,
    };

    public static bool Prefix(ItemPickupBase pickup, Vector3 moveVector, Scp914KnobSetting setting)
    {
        if (pickup is not Scp330Pickup p)
            return true;

        try
        {
            if (setting == Scp914KnobSetting.Coarse || setting == Scp914KnobSetting.Rough)
            {
                p.DestroySelf();
                return false;
            }

            var outputPos = pickup.transform.position + moveVector;

            foreach (var candy in p.StoredCandies.ToArray())
            {
                switch (setting)
                {
                    case Scp914KnobSetting.OneToOne:
                        {
                            p.StoredCandies.Remove(candy);
                            p.StoredCandies.Add(Scp330Candies.GetRandom());
                            break;
                        }

                    case Scp914KnobSetting.Fine:
                        {
                            int index = CandyList.IndexOf(candy) + 1;
                            index %= CandyList.Length;

                            if (CandyList[index] == CandyKindID.Pink)
                            {
                                switch (UnityEngine.Random.Range(1, 101))
                                {
                                    case int x when x <= Plugin.Instance.Config.FinePinkChance:
                                        p.StoredCandies.Add(CandyList[index]);
                                        break;

                                    case int x when x <= Plugin.Instance.Config.FinePinkPillsChance + Plugin.Instance.Config.FinePinkChance:
                                        Item.CreatePickup(ItemType.Painkillers, outputPos);
                                        break;
                                }

                                p.StoredCandies.Remove(candy);
                                break;
                            }

                            p.StoredCandies.Remove(candy);
                            p.StoredCandies.Add(CandyList[index]);
                            break;
                        }

                    case Scp914KnobSetting.VeryFine:
                        {
                            if (UnityEngine.Random.Range(1, 101) <= Plugin.Instance.Config.VeryFineDestroyChance)
                            {
                                p.StoredCandies.Remove(candy);
                                break;
                            }

                            int index = CandyList.IndexOf(candy) + 2;
                            index %= CandyList.Length;

                            if (CandyList[index] == CandyKindID.Pink)
                            {
                                switch (UnityEngine.Random.Range(1, 101))
                                {
                                    case int x when x <= Plugin.Instance.Config.VeryFinePinkChance:
                                        p.StoredCandies.Add(CandyList[index]);
                                        break;
                                    case int x when x <= Plugin.Instance.Config.VeryFinePinkPillsChance + Plugin.Instance.Config.VeryFinePinkChance:
                                        Item.CreatePickup(ItemType.Painkillers, outputPos);
                                        break;
                                }

                                p.StoredCandies.Remove(candy);
                                break;
                            }

                            p.StoredCandies.Remove(candy);
                            p.StoredCandies.Add(CandyList[index]);
                            break;
                        }

                    default:
                        p.StoredCandies.Remove(candy);
                        break;
                }
            }

            if (p.StoredCandies.Count == 0)
                p.DestroySelf();
            else
            {
                p.NetworkExposedCandy = p.StoredCandies.Count == 1 ? p.StoredCandies.First() : CandyKindID.None;
                p.transform.position += moveVector;
                p.RefreshPositionAndRotation();
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
        }

        return false;
    }
}
