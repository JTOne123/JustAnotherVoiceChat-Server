﻿/*
 * File: VoicePositionTask.cs
 * Date: 1.2.2018,
 *
 * MIT License
 *
 * Copyright (c) 2018 AlternateVoice
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AlternateVoice.Server.Wrapper.Interfaces;

namespace AlternateVoice.Server.Wrapper.Elements.Tasks
{
    internal partial class VoicePositionTask : IVoicePositionTask
    {
        private readonly IVoiceServer _voiceServer;
        private readonly IVoicePositionTaskServer _voicePositionTaskServer;

        public CancellationTokenSource TokenSource { get; }

        public VoicePositionTask(IVoiceServer voiceServer, IVoicePositionTaskServer voicePositionTaskServer) : this(voiceServer, voicePositionTaskServer, new CancellationTokenSource())
        {
        }

        public VoicePositionTask(IVoiceServer voiceServer, IVoicePositionTaskServer voicePositionTaskServer, CancellationTokenSource cancellationTokenSource)
        {
            _voiceServer = voiceServer;
            _voicePositionTaskServer = voicePositionTaskServer;

            TokenSource = cancellationTokenSource;
        }

        public void CancelVoiceTask()
        {
            TokenSource.Cancel();
            Task.Delay(1000);
        }

        public Task RunVoiceTask()
        {
            return Task.Run(async () => await UpdatePlayerPositionsAndDirections(), TokenSource.Token);
        }

        private async Task UpdatePlayerPositionsAndDirections()
        {
            while (!TokenSource.Token.IsCancellationRequested)
            {
                var clients = _voiceServer.GetClients<IVoiceClient>().ToArray();

                foreach (var listenerClient in clients)
                {
                    TrySetListenerDirection(listenerClient);

                    foreach (var foreignClient in clients)
                    {
                        TokenSource.Token.ThrowIfCancellationRequested();

                        if (listenerClient.Equals(foreignClient))
                        {
                            continue;
                        }

                        if(TryMuteForeignClientForListener(listenerClient, foreignClient))
                        {
                            continue;
                        }

                        TrySetForeignClientPositonForListener(listenerClient, foreignClient);
                    }
                }

                await Task.Delay(300);
            }
        }

        public void Dispose()
        {
            TokenSource.Dispose();
        }
    }
}