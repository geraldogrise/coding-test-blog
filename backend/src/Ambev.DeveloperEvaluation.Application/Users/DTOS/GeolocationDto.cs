using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Users.DTOS
{
    /// <summary>
    /// Represents the geolocation details of an address.
    /// </summary>
    public class GeolocationDto
    {
        public string Lat { get; set; } = string.Empty;
        public string Long { get; set; } = string.Empty;
    }
}
