// -----------------------------------------------------------------------
// <copyright file="SCP330Handler.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features.Items;
using InventorySystem.Items.Usables.Scp330;
using Mistaken.API.Diagnostics;
using Scp914;

namespace Mistaken.BetterSCP.SCP330
{
    internal class SCP330Handler : Module
    {
        public SCP330Handler(PluginHandler plugin)
            : base(plugin)
        {
        }

        public override string Name => "SCP330Handler";

        public override void OnEnable()
        {
            Exiled.Events.Handlers.Scp914.UpgradingItem += this.Scp914_UpgradingItem;
        }

        public override void OnDisable()
        {
            Exiled.Events.Handlers.Scp914.UpgradingItem -= this.Scp914_UpgradingItem;
        }

        private readonly List<CandyKindID> candyList = new List<CandyKindID>()
        {
            CandyKindID.Yellow,
            CandyKindID.Green,
            CandyKindID.Red,
            CandyKindID.Purple,
            CandyKindID.Blue,
            CandyKindID.Rainbow,
            CandyKindID.Pink,
        };

        private void Scp914_UpgradingItem(Exiled.Events.EventArgs.UpgradingItemEventArgs ev)
        {
            if (!(ev.Item.Base is Scp330Pickup scp330))
            {
                return;
            }

            switch (ev.KnobSetting)
            {
                case Scp914KnobSetting.Rough:
                case Scp914KnobSetting.Coarse:
                    ev.Item.Destroy();
                    break;
                case Scp914KnobSetting.OneToOne:
                    {
                        if (UnityEngine.Random.Range(0, 100) <= 35)
                        {
                            ev.Item.Destroy();
                            break;
                        }

                        List<CandyKindID> candies = Enum.GetValues(typeof(CandyKindID)).ToArray<CandyKindID>().ToList();
                        candies.ShuffleList();
                        scp330.ExposedCandy = candies.First(x => x != CandyKindID.Pink && x != CandyKindID.None);
                        break;
                    }

                case Scp914KnobSetting.Fine:
                    {
                        if (UnityEngine.Random.Range(0, 100) <= 35)
                        {
                            ev.Item.Destroy();
                            break;
                        }

                        int index = this.candyList.IndexOf(scp330.ExposedCandy) + 1;
                        int random = UnityEngine.Random.Range(0, 100);
                        if (this.candyList[index] == CandyKindID.Rainbow)
                        {
                            if (random >= 10)
                            {
                                scp330.ExposedCandy = this.candyList[index];
                            }
                            else if (random <= 11 && random >= 50)
                            {
                                ev.Item.Destroy();
                                Item pain = new Item(ItemType.Painkillers);
                                pain.Spawn(ev.OutputPosition);
                            }
                            else if (random <= 51)
                            {
                                ev.Item.Destroy();
                            }

                            break;
                        }

                        scp330.ExposedCandy = this.candyList[index];
                        break;
                    }

                case Scp914KnobSetting.VeryFine:
                    {
                        if (UnityEngine.Random.Range(0, 100) <= 50)
                        {
                            ev.Item.Destroy();
                            break;
                        }

                        int index = this.candyList.IndexOf(scp330.ExposedCandy) + 1;
                        int random = UnityEngine.Random.Range(0, 100);
                        if (this.candyList[index] == CandyKindID.Rainbow)
                        {
                            if (random >= 5)
                            {
                                scp330.ExposedCandy = this.candyList[index];
                            }
                            else if (random <= 6 && random >= 35)
                            {
                                ev.Item.Destroy();
                                Item pain = new Item(ItemType.Painkillers);
                                pain.Spawn(ev.OutputPosition);
                            }
                            else if (random <= 36)
                            {
                                ev.Item.Destroy();
                            }

                            break;
                        }

                        scp330.ExposedCandy = this.candyList[index];
                        break;
                    }
            }
        }
    }
}
