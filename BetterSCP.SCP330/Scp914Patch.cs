// -----------------------------------------------------------------------
// <copyright file="Scp914Patch.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using HarmonyLib;
using InventorySystem.Items.Pickups;
using InventorySystem.Items.Usables.Scp330;
using Scp914;
using UnityEngine;

namespace Mistaken.BetterSCP.SCP330
{
    [HarmonyPatch(typeof(Scp914Upgrader), nameof(Scp914Upgrader.ProcessPickup))]
    internal class Scp914Patch
    {
        public static readonly List<CandyKindID> CandyList = new List<CandyKindID>()
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
            if (!(pickup is Scp330Pickup p))
                return true;

            try
            {
                if (setting == Scp914KnobSetting.Coarse || setting == Scp914KnobSetting.Rough)
                {
                    p.DestroySelf();
                    return false;
                }

                var newPos = pickup.transform.position + moveVector;
                foreach (var candy in p.StoredCandies.ToArray())
                {
                    switch (setting)
                    {
                        case Scp914KnobSetting.OneToOne:
                            {
                                var candyType = Scp330Candies.GetRandom();
                                p.StoredCandies.Remove(candy);
                                p.StoredCandies.Add(candyType);
                                break;
                            }

                        case Scp914KnobSetting.Fine:
                            {
                                int index = CandyList.IndexOf(candy) + 1;
                                index %= CandyList.Count;
                                int random = Random.Range(0, 100);
                                if (CandyList[index] == CandyKindID.Pink)
                                {
                                    if (random < PluginHandler.Instance.Config.FinePinkChance)
                                        p.StoredCandies.Add(CandyList[index]);
                                    else if (random >= PluginHandler.Instance.Config.FinePinkChance && random < PluginHandler.Instance.Config.FinePinkChance + PluginHandler.Instance.Config.FinePinkPillsChance)
                                        new Item(ItemType.Painkillers).Spawn(newPos);

                                    p.StoredCandies.Remove(candy);
                                    break;
                                }

                                p.StoredCandies.Remove(candy);
                                p.StoredCandies.Add(CandyList[index]);
                                break;
                            }

                        case Scp914KnobSetting.VeryFine:
                            {
                                if (Random.Range(0, 100) < PluginHandler.Instance.Config.VeryFineDestroyChance)
                                {
                                    p.StoredCandies.Remove(candy);
                                    break;
                                }

                                int index = CandyList.IndexOf(candy) + 2;
                                index %= CandyList.Count;
                                int random = Random.Range(0, 100);
                                if (CandyList[index] == CandyKindID.Pink)
                                {
                                    if (random < PluginHandler.Instance.Config.VeryFinePinkChance)
                                        p.StoredCandies.Add(CandyList[index]);
                                    else if (random >= PluginHandler.Instance.Config.VeryFinePinkChance && random < PluginHandler.Instance.Config.VeryFinePinkChance + PluginHandler.Instance.Config.VeryFinePinkPillsChance)
                                        new Item(ItemType.Painkillers).Spawn(newPos);

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

                return false;
            }
            catch (System.Exception ex)
            {
                Log.Error(ex);
                return false;
            }
        }
    }
}
