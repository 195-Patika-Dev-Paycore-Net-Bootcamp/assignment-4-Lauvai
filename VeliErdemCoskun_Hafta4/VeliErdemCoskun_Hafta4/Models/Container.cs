namespace VeliErdemCoskun_Hafta3.Models
{
    //Model class for container properties.

    public class Container
    {

        public virtual long Id { get; set; }
        public virtual string ContainerName { get; set; }
        public virtual double Latitude { get; set; }
        public virtual double Longitude { get; set; }
        public virtual long VehicleId { get; set; }
    }
}
