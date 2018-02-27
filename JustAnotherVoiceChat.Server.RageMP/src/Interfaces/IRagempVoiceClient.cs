﻿using GTANetworkAPI;
using JustAnotherVoiceChat.Server.Wrapper.Interfaces;

namespace JustAnotherVoiceChat.Server.RageMP.Interfaces
{
    public interface IRagempVoiceClient : IVoiceClient
    {
        
        Client Player { get; }
        
        bool SetRelativeSpeakerPosition(Client speaker, Vector3 position);
        bool ResetRelativeSpeakerPosition(Client speaker);

        bool MuteSpeaker(Client speaker, bool muted);
        bool IsSpeakerMuted(Client speaker);
        
    }
}