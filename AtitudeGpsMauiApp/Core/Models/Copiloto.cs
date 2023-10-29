using AtitudeGpsMauiApp.Domain.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtitudeGpsMauiApp.Core.Models
{
    public class Copiloto : LogModelBase
    {
        public AtitudeEnum Atitude { get; set; }
    }
}
