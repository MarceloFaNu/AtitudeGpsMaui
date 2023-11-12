using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtitudeGpsMauiApp.Domain.Extensions
{
    public static class LocationExtensions
    {
        public static void ShiftUpLocationArrayItems(this Location[] items)
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
