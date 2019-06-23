using OmniSharp.Extensions.LanguageServer.Protocol.Client.Capabilities;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;
using OmniSharp.Extensions.LanguageServer.Protocol.Server;
using System.Threading;
using System.Threading.Tasks;

namespace DaedalusLanguageServer.Services
{
    public class GoToDefinitionHandler : IDefinitionHandler
    {
        public TextDocumentRegistrationOptions GetRegistrationOptions() => DaedalusDefaults.RegistrationOptions;

        public Task<LocationOrLocationLinks> Handle(DefinitionParams request, CancellationToken cancellationToken)
        {
            return Task.FromResult<LocationOrLocationLinks>(null);
        }

        public void SetCapability(DefinitionCapability capability) { }
    }
}
