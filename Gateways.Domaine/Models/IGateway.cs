using System.Collections.Generic;

namespace Gateways.Domaine
{
    public interface IGateway
    {
        int Id { get; set; }
        string SerialNumber { get; set; }
        string Name { get; set; }
        string IPAddress { get; set; }
        ICollection<PeripheralDevice> PeripheralDevices { get; set; }
    }
}