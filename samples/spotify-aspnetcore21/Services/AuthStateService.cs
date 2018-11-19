using System;
using System.Collections.Concurrent;

namespace SpotifyVue.Services
{
    public class AuthStateService
    {   
        /// A dictionary of state, userHash for verification of authorization callback
        /// IRL this would be Redis, Table Storage or Cosmos DB
        private readonly ConcurrentDictionary<string, string> _states = new ConcurrentDictionary<string, string>();

        /// Creates and stores a new state value GUID
        public string NewState(string userId) 
        {
            // Store the state
            string state = Guid.NewGuid().ToString("N");
            _states.TryAdd(state, userId);            
            return state;
        }

        /// throws an exception if state is not found in dictionary, userId does not match
        public void ValidateState(string state, string userId)
        {
            string value;
            if (!_states.TryGetValue(state, out value)) throw new InvalidOperationException("Invalid State value");
            if (value != userId) throw new InvalidOperationException("Invalid State value");
        }
    }
}