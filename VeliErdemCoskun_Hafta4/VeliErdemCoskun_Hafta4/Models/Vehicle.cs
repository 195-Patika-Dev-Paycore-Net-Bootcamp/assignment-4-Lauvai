using System.Numerics;
using System.Security.Cryptography.X509Certificates;

//Model class for vehicle properties.

namespace VeliErdemCoskun_Hafta3.Models
{
    public class Vehicle
    {
        public virtual int Id { get; set; }
        public virtual string VehicleName { get; set; }
        public virtual string VehiclePlate { get; set; }
    }
}
