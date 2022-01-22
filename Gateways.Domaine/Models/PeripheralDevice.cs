using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gateways.Domaine
{
    public class PeripheralDevice//: IPeripheralDevice
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Vendor { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

        public DeviceStatus Status { get; set; } 

        [Required]
        public virtual int GatewayId { get; set; }

        public virtual Gateway Gateway { get; set; }
    }

    public enum DeviceStatus { Offline, Online }
}
