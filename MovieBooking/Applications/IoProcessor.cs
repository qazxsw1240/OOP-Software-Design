using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace MovieBooking.Applications
{
    public class IoProcessor(TextReader reader, TextWriter writer)
    {
        public async Task<string> ReadLineAsync(
            string? query = null,
            CancellationToken cancellationToken = default)
        {
            if (query is not null)
            {
                await writer.WriteAsync(query.AsMemory(), cancellationToken);
            }
            return await reader.ReadLineAsync(cancellationToken)
                ?? throw new IOException();
        }

        public async Task WriteLineAsync(CancellationToken cancellationToken = default)
        {
            await writer.WriteLineAsync(ReadOnlyMemory<char>.Empty, cancellationToken);
        }

        public async Task WriteLineAsync(
            string line,
            CancellationToken cancellationToken = default)
        {
            await writer.WriteLineAsync(line.AsMemory(), cancellationToken);
        }

        public async Task WriteLinesAsync(
            IEnumerable<string> lines,
            CancellationToken cancellationToken = default)
        {
            foreach (string line in lines)
            {
                await writer.WriteLineAsync(line.AsMemory(), cancellationToken);
            }
        }
    }
}
