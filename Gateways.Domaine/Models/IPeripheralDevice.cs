using System;

namespace Gateways.Domaine
{
    public interface IPeripheralDevice
    {
        int Id { get; set; }
        string Vendor { get; set; }
        DateTime Created { get; set; }
        DeviceStatus Status { get; set; }
        int GatewayId { get; set; }
        Gateway Gateway { get; set; }
    }
}