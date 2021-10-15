namespace OmegaWarhead
{
    using OmegaWarhead.Events;
    using Exiled.API.Enums;
    using Exiled.API.Features;
    using System;
    using Warhead = Exiled.Events.Handlers.Warhead;
    using Server = Exiled.Events.Handlers.Server;
    using MEC;

    public class Plugin : Plugin<Config>
    {
        public static Plugin Singleton;
        private WarheadHandler handler;
        public bool OmegaPhase = false;
        public override string Author { get; } = "Cegla";
        public override string Name { get; } = "OmegaWarhead";
        public override string Prefix { get; } = "OmegaWarhead";
        public override Version RequiredExiledVersion { get; } = new Version(3, 0, 0);
        public override Version Version { get; } = new Version(1, 0, 0);
        public override PluginPriority Priority { get; } = PluginPriority.Medium;

        public override void OnEnabled()
        {
            Singleton = this;
            RegisterEvents();
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            Singleton = null;
            UnregisterEvents();
            base.OnDisabled();
        }
        public void RegisterEvents()
        {
            handler = new WarheadHandler();
            Warhead.Detonated += handler.OnDetonating;
            Server.WaitingForPlayers += handler.OnWaitingForPlayers;
        }
        public void UnregisterEvents()
        {
            Warhead.Detonated -= handler.OnDetonating;
            Server.WaitingForPlayers -= handler.OnWaitingForPlayers;
            handler = null;
        }
    }

}