using System.Collections.Generic;
using System.Linq;

namespace MovieBooking.Applications.States
{
    public class ApplicationStateStack
    {
        private readonly List<IApplicationState> _states = [];

        public void Push(IApplicationState state)
        {
            _states.Add(state);
        }

        public IApplicationState? Pop()
        {
            IApplicationState? top = _states.LastOrDefault();
            if (top is not null)
            {
                _states.RemoveAt(_states.Count - 1);
            }
            return top;
        }

        public IApplicationState? Peek()
        {
            return _states.LastOrDefault();
        }
    }
}
