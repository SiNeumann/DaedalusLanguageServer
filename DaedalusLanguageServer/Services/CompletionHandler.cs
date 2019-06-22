using OmniSharp.Extensions.LanguageServer.Protocol.Models;
using OmniSharp.Extensions.LanguageServer.Protocol.Server;
using System.Threading;
using System.Threading.Tasks;

namespace DaedalusLanguageServer.Services
{
    public class CompletionHandler : ICompletionResolveHandler
    {
        public bool CanResolve(CompletionItem value)
        {
            return false;
        }

        public Task<CompletionItem> Handle(CompletionItem request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new CompletionItem());
        }
    }
}
