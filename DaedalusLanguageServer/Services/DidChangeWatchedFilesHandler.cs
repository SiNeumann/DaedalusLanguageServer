using OmniSharp.Extensions.Embedded.MediatR;
using OmniSharp.Extensions.LanguageServer.Protocol.Client.Capabilities;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;
using OmniSharp.Extensions.LanguageServer.Protocol.Server;
using System.Threading;
using System.Threading.Tasks;

namespace DaedalusLanguageServer.Services
{
    public class DidChangeWatchedFilesHandler : IDidChangeWatchedFilesHandler
    {
        public object GetRegistrationOptions() => null;

        public Task<Unit> Handle(DidChangeWatchedFilesParams request, CancellationToken cancellationToken) => Unit.Task;

        public void SetCapability(DidChangeWatchedFilesCapability capability) => capability.DynamicRegistration = false;
    }
}
