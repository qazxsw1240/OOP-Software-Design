using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MovieBooking.Applications.States
{
    public class ListableApplicationState(
        IoProcessor ioProcessor,
        List<IListableApplicationState> states) : IApplicationState
    {
        public async Task<IApplicationState?> HandleAsync(
            ApplicationStateStack stack,
            CancellationToken cancellationToken = default)
        {
            int index = -1;
            try
            {
                while (index < 0)
                {
                    await ioProcessor.WriteLinesAsync(
                        states
                            .Select(action => action.Name)
                            .Append("Quit")
                            .Index()
                            .Select(pair => $"{pair.Index + 1}. {pair.Item}"),
                        cancellationToken);
                    try
                    {
                        string value = await ioProcessor.ReadLineAsync("Input the number: ", cancellationToken);
                        index = int.Parse(value) - 1;
                        if (index < 0 || index > states.Count)
                        {
                            await ioProcessor.WriteLineAsync("Input invalid number. Try again.", cancellationToken);
                            index = -1;
                        }
                    }
                    catch (FormatException)
                    {
                        await ioProcessor.WriteLineAsync("Input invalid number. Try again.", cancellationToken);
                        index = -1;
                    }
                }
                if (index == states.Count)
                {
                    return null;
                }
                return await states
                    .ElementAt(index)
                    .HandleAsync(stack, cancellationToken);
            }
            catch (IOException)
            {
                await ioProcessor.WriteLineAsync(cancellationToken);
                return stack.Pop();
            }
        }
    }
}
