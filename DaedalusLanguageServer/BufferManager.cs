using System.Collections.Concurrent;

namespace DaedalusLanguageServer
{
    public class BufferManager
    {
        private ConcurrentDictionary<string, BufferedDocument> _buffers = new ConcurrentDictionary<string, BufferedDocument>();

        public void UpdateBuffer(string documentPath, char[] buffer)
        {
            _buffers.AddOrUpdate(documentPath,
                (_, b) => new BufferedDocument(b),
                (_, d, b) => { d.UpdateDocument(b); return d; },
                buffer);
        }

        public BufferedDocument GetBuffer(string documentPath)
        {
            return _buffers.TryGetValue(documentPath, out var buffer) ? buffer : null;
        }
    }
}


