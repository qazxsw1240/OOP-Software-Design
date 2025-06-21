using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MovieBooking.Applications.States;

namespace MovieBooking.Applications
{
    public abstract class ActionListApplicationStateBase(IoProcessor ioProcessor) : IApplicationState
    {
        protected readonly IoProcessor _ioProcessor = ioProcessor;

        public async Task<IApplicationState?> HandleAsync(CancellationToken cancellationToken = default)
        {
            List<IApplicationStateAction<IApplicationState?>> actions = GetActions();
            int index = -1;
            while (index < 0)
            {
                await BeforeItemSelectAsync(cancellationToken);
                await _ioProcessor.WriteLinesAsync(
                    actions
                        .Select(action => action.Name)
                        .Append("Quit")
                        .Index()
                        .Select(pair => string.Format("{0}. {1}", pair.Index + 1, pair.Item)),
                    cancellationToken);
                try
                {
                    string value = await _ioProcessor.ReadLineAsync("Input the number: ", cancellationToken);
                    index = int.Parse(value) - 1;
                    if (index < 0 || index > actions.Count)
                    {
                        await _ioProcessor.WriteLineAsync("Input invalid number. Try again.", cancellationToken);
                        index = -1;
                    }
                }
                catch (FormatException)
                {
                    await _ioProcessor.WriteLineAsync("Input invalid number. Try again.", cancellationToken);
                    index = -1;
                }
            }
            await AfterItemSelectAsync(cancellationToken);
            if (index == actions.Count)
            {
                return null;
            }
            return await actions
                .ElementAt(index)
                .ExecuteAsync(cancellationToken);
        }

        protected abstract List<IApplicationStateAction<IApplicationState?>> GetActions();

        protected abstract Task BeforeItemSelectAsync(CancellationToken cancellationToken = default);

        protected abstract Task AfterItemSelectAsync(CancellationToken cancellationToken = default);
    }
}
