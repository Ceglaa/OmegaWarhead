namespace OmegaWarhead
{
    using Exiled.API.Enums;
    using Exiled.API.Features;
    using OmegaWarhead.Events;
    using System;
    using Server = Exiled.Events.Handlers.Server;
    using Warhead = Exiled.Events.Handlers.Warhead;

    public class Plugin : Plugin<Config>
    {
        private WarheadHandler handler;
        public override string Author { get; } = "Cegla";
        public override string Name { get; } = "OmegaWarhead";
        public override string Prefix { get; } = "OmegaWarhead";
        public override Version RequiredExiledVersion { get; } = new Version(4, 2, 1);
        public override Version Version { get; } = new Version(1, 0, 1);
        public override PluginPriority Priority { get; } = PluginPriority.Medium;

        public static Plugin Singleton { get; internal set; }
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
            Server.RoundEnded += handler.OnRoundEnded;
        }
        public void UnregisterEvents()
        {
            Warhead.Detonated -= handler.OnDetonating;
            Server.WaitingForPlayers -= handler.OnWaitingForPlayers;
            Server.RoundEnded -= handler.OnRoundEnded;
            handler = null;
        }
    }

}