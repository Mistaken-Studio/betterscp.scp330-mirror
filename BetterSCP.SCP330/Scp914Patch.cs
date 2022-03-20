// -----------------------------------------------------------------------
// <copyright file="Scp914Patch.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using HarmonyLib;
using InventorySystem.Items.Pickups;
using InventorySystem.Items.Usables.Scp330;
using Mirror;
using Scp914;
using UnityEngine;

namespace Mistaken.BetterSCP.SCP330
{
    /// <inheritdoc/>
    [HarmonyPatch(typeof(Scp914Upgrader), nameof(Scp914Upgrader.ProcessPickup))]
    public class Scp914Patch
    {
        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public static bool Prefix(ItemPickupBase pickup, Vector3 moveVector, Scp914KnobSetting setting)
        {
            if (!(pickup is Scp330Pickup p))
                return true;

            var newPos = pickup.transform.position + moveVector;
            pickup.DestroySelf();

            Item pickup2 = new Item(ItemType.SCP330);
            Scp330Pickup candy = pickup2.Base.PickupDropModel as Scp330Pickup;

            switch (setting)
            {
                case Scp914KnobSetting.OneToOne:
                    {
                        if (UnityEngine.Random.Range(0, 100) <= 35)
                        {
                            break;
                        }

                        List<CandyKindID> candies = Enum.GetValues(typeof(CandyKindID)).ToArray<CandyKindID>().ToList();
                        candies.ShuffleList();
                        candy.ExposedCandy = candies.First(x => x != CandyKindID.Pink && x != CandyKindID.None);
                        break;
                    }

                case Scp914KnobSetting.Fine:
                    {
                        if (UnityEngine.Random.Range(0, 100) <= 35)
                        {
                            break;
                        }

                        int index = CandyList.IndexOf(p.NetworkExposedCandy) + 1;
                        int random = UnityEngine.Random.Range(0, 100);
                        if (CandyList[index] == CandyKindID.Rainbow)
                        {
                            if (random >= 10)
                            {
                                candy.ExposedCandy = CandyList[index];
                                pickup2.Spawn(newPos);
                            }
                            else if (random <= 11 && random >= 50)
                            {
                                Pickup pain = new Item(ItemType.Painkillers).Spawn(newPos);
                            }

                            return false;
                        }

                        p.ExposedCandy = CandyList[index];
                        break;
                    }

                case Scp914KnobSetting.VeryFine:
                    {
                        if (UnityEngine.Random.Range(0, 100) <= 50)
                        {
                            break;
                        }

                        int index = CandyList.IndexOf(p.NetworkExposedCandy) + 1;
                        int random = UnityEngine.Random.Range(0, 100);
                        if (CandyList[index] == CandyKindID.Rainbow)
                        {
                            if (random >= 5)
                            {
                                candy.ExposedCandy = CandyList[index];
                                pickup2.Spawn(newPos);
                            }
                            else if (random <= 6 && random >= 35)
                            {
                                Pickup pain = new Item(ItemType.Painkillers).Spawn(newPos);
                            }

                            return false;
                        }

                        p.ExposedCandy = CandyList[index];
                        break;
                    }
            }

            pickup2.Spawn(newPos);
            return false;
        }
    }
}
