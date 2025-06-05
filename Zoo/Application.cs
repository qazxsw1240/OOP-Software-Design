using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Zoo
{
    public class Application(
        TextWriter textWriter,
        TextReader textReader,
        List<IApplicationAction> actions)
    {
        private readonly AnimalCollection _animalCollection = new();
        private readonly IoProcessor _ioProcessor = new(
            textWriter,
            textReader,
            new DefaultApplicationInputResultProvider());

        public void Start()
        {
            foreach (ApplicationInputResult inputResult in TakeInput(
                () => _ioProcessor.ReadValue(),
                PrintMenu))
            {
                IApplicationAction? action = actions
                    .FirstOrDefault(action =>
                        action.Key.Equals(inputResult, StringComparison.CurrentCultureIgnoreCase));
                if (action is null)
                {
                    NotifyUnknownInput();
                    continue;
                }
                action.Execute(_ioProcessor, _animalCollection);
            }
        }

        private IEnumerable<ApplicationInputResult> TakeInput(
            Func<ApplicationInputResult> inputResultGenerator,
            Action? beforeTakeInput = null)
        {
            bool canTakeInput = true;
            while (canTakeInput)
            {
                if (beforeTakeInput is not null)
                {
                    beforeTakeInput();
                }
                ApplicationInputResult inputResult = inputResultGenerator();
                canTakeInput = !inputResult.IsRequestTermination();
                if (canTakeInput)
                {
                    yield return inputResult;
                }
            }
        }

        private void PrintMenu()
        {
            _ioProcessor.WriteLines(actions
                .Select(action => $"{action.Key.ToUpper()}: {action.Description}")
                .Append("Q: 종료"));
        }

        private void NotifyUnknownInput()
        {
            _ioProcessor.WriteLines("해당하는 명령어를 찾을 수 없습니다.");
        }

        private record DefaultApplicationInputResult(string Value) : ApplicationInputResult(Value)
        {
            public override bool IsRequestTermination()
            {
                return Value.Equals("q", StringComparison.CurrentCultureIgnoreCase);
            }
        }

        private class DefaultApplicationInputResultProvider : IApplicationInputResultProvider
        {
            public ApplicationInputResult Provide(string value)
            {
                return new DefaultApplicationInputResult(value);
            }
        }
    }

    public abstract record ApplicationInputResult(string Value)
    {
        public abstract bool IsRequestTermination();

        public static implicit operator string(ApplicationInputResult inputResult)
        {
            return inputResult.Value;
        }
    }

    public interface IApplicationInputResultProvider
    {
        public ApplicationInputResult Provide(string value);
    }

    public class IoProcessor(
        TextWriter writer,
        TextReader reader,
        IApplicationInputResultProvider inputResultProvider)
    {
        public ApplicationInputResult ReadValue(string? query = null)
        {
            if (query is not null)
            {
                writer.Write(query);
            }
            string value = reader.ReadLine()
                ?? throw new ApplicationException("입력을 받을 수 없습니다.");
            return inputResultProvider.Provide(value);
        }

        public void WriteLines(params string[] lines)
        {
            foreach (string line in lines)
            {
                writer.WriteLine(line);
            }
        }

        public void WriteLines(IEnumerable<string> lines)
        {
            foreach (string line in lines)
            {
                writer.WriteLine(line);
            }
        }
    }
}
