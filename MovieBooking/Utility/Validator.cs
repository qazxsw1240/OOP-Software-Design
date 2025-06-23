using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieBooking.Utility
{
    public class Validator<T>(IEnumerable<Predicate<T>> predicates)
    {
        private readonly List<ValidatorStep> _steps = [.. predicates.Select(CreateStep)];

        public Validator() : this([]) {}

        public Validator<T> With(Predicate<T> predicate)
        {
            _steps.Add(CreateStep(predicate));
            return this;
        }

        public Validator<T> With(Predicate<T> predicate, Func<T, Exception> exceptionGenerator)
        {
            _steps.Add(new(predicate, exceptionGenerator));
            return this;
        }

        public bool Test(T value)
        {
            return _steps.Count == 0 || _steps.All(step => step.Predicate(value));
        }

        public void Check(T value)
        {
            if (_steps.Count == 0)
            {
                return;
            }
            foreach ((Predicate<T> predicate, Func<T, Exception> exceptionGenerator) in _steps)
            {
                if (!predicate(value))
                {
                    throw exceptionGenerator(value);
                }
            }
        }

        private static ValidatorStep CreateStep(Predicate<T> predicate)
        {
            return new(predicate, _ => new ArgumentException("Cannot pass all predicates."));
        }

        private record struct ValidatorStep(
            Predicate<T> Predicate,
            Func<T, Exception> ExceptionGenerator);
    }
}
