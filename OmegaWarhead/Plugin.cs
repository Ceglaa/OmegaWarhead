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
        private Handler _handler;
        
        public override string Author => "Cegla";

        public override string Name => "OmegaWarhead";

        public override string Prefix => "OmegaWarhead";

        public override Version RequiredExiledVersion { get; } = new(5, 0, 0);
        
        public override Version Version { get; } = new(1, 0, 2);
        
        public override PluginPriority Priority => PluginPriority.Medium;

        public static Plugin Singleton { get; private set; }
        
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

        private void RegisterEvents()
        {
            _handler = new Handler();
            Warhead.Detonated += _handler.OnDetonating;
            Server.WaitingForPlayers += _handler.OnWaitingForPlayers;
            Server.RoundEnded += _handler.OnRoundEnded;
        }

        private void UnregisterEvents()
        {
            Warhead.Detonated -= _handler.OnDetonating;
            Server.WaitingForPlayers -= _handler.OnWaitingForPlayers;
            Server.RoundEnded -= _handler.OnRoundEnded;
            _handler = null;
        }
    }

}