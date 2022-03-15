// -----------------------------------------------------------------------
// <copyright file="SCP330Handler.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Mistaken.API.Diagnostics;

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

        }

        public override void OnDisable()
        {

        }
    }
}
