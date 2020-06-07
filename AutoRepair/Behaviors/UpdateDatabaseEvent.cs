using System;

namespace AutoRepair.Behaviors
{
    public static class UpdateDatabaseEvent
    {
        public static event Action DatabaseUpdated;

        public static void OnDatabaseUpdated()
        {
            DatabaseUpdated?.Invoke();
        }
    }
}