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

            var newPos = pickup.transform.position + moveVector;

            pickup.DestroySelf();

            Scp330 bag = new Scp330(ItemType.SCP330);

            switch (setting)
            {
                case Scp914KnobSetting.OneToOne:
                    {
                        if (UnityEngine.Random.Range(0, 100) <= 35)
                            break;

                        var candyType = Scp330Candies.GetRandom();
                        Scp330Pickup obj = (Scp330Pickup)GameObject.Instantiate(bag.Base.PickupDropModel, newPos, Quaternion.identity);
                        obj.NetworkExposedCandy = candyType;
                        NetworkServer.Spawn(obj.gameObject);
                        obj.InfoReceived(PickupSyncInfo.None, bag.Base.PickupDropModel.NetworkInfo);

                        break;
                    }

                case Scp914KnobSetting.Fine:
                    {
                        if (UnityEngine.Random.Range(0, 100) <= 35)
                            break;

                        int index = CandyList.IndexOf(p.NetworkExposedCandy) + 1;
                        int random = UnityEngine.Random.Range(0, 100);
                        if (CandyList[index] == CandyKindID.Rainbow)
                        {
                            if (random >= 10)
                            {
                                Scp330Pickup obj = (Scp330Pickup)GameObject.Instantiate(bag.Base.PickupDropModel, newPos, Quaternion.identity);
                                obj.NetworkExposedCandy = CandyList[index];
                                NetworkServer.Spawn(obj.gameObject);
                                obj.InfoReceived(PickupSyncInfo.None, bag.Base.PickupDropModel.NetworkInfo);
                            }
                            else if (random <= 11 && random >= 50)
                                new Item(ItemType.Painkillers).Spawn(newPos);

                            return false;
                        }

                        Scp330Pickup candyPickup = (Scp330Pickup)GameObject.Instantiate(bag.Base.PickupDropModel, newPos, Quaternion.identity);
                        candyPickup.NetworkExposedCandy = CandyList[index];
                        NetworkServer.Spawn(candyPickup.gameObject);
                        candyPickup.InfoReceived(PickupSyncInfo.None, bag.Base.PickupDropModel.NetworkInfo);
                        break;
                    }

                case Scp914KnobSetting.VeryFine:
                    {
                        if (UnityEngine.Random.Range(0, 100) <= 50)
                            break;

                        int index = CandyList.IndexOf(p.NetworkExposedCandy) + 1;
                        int random = UnityEngine.Random.Range(0, 100);
                        if (CandyList[index] == CandyKindID.Rainbow)
                        {
                            if (random >= 5)
                            {
                                Scp330Pickup obj = (Scp330Pickup)GameObject.Instantiate(bag.Base.PickupDropModel, newPos, Quaternion.identity);
                                obj.NetworkExposedCandy = CandyList[index];
                                NetworkServer.Spawn(obj.gameObject);
                                obj.InfoReceived(PickupSyncInfo.None, bag.Base.PickupDropModel.NetworkInfo);
                            }
                            else if (random <= 6 && random >= 35)
                                new Item(ItemType.Painkillers).Spawn(newPos);

                            return false;
                        }

                        Scp330Pickup candyPickup = (Scp330Pickup)GameObject.Instantiate(bag.Base.PickupDropModel, newPos, Quaternion.identity);
                        candyPickup.NetworkExposedCandy = CandyList[index];
                        NetworkServer.Spawn(candyPickup.gameObject);
                        candyPickup.InfoReceived(PickupSyncInfo.None, bag.Base.PickupDropModel.NetworkInfo);
                        break;
                    }
            }

            return false;
        }
    }
}
