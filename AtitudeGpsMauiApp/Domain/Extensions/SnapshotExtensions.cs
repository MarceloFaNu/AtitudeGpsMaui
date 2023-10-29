using AtitudeGpsMauiApp.Core.Models;

namespace AtitudeGpsMauiApp.Domain.Extensions
{
    public static class SnapshotExtensions
    {
        public static void ShiftUpSnapshotArrayItems(this Snapshot[] items)
        {
            if (items.Length < 2) return;

            for (int i = 1; i < items.Length; i++)
            {
                items[i - 1] = items[i];
            }

            items[items.Length - 1] = null;
        }
    }
}
