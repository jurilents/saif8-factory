using UnityEngine;

namespace Utilities.Logging
{
    public interface IGameLogger
    {
        void AddMessage(LogMessage message, GameObject console);
    }
}