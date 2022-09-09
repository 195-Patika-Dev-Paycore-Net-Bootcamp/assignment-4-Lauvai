using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using VeliErdemCoskun_Hafta3.Models;


namespace VeliErdemCoskun_Hafta3.Mappings
{
    //Container object is mapped to the database in the constructor method.
    public class ContainerMap:ClassMapping<Container>
    {
        public ContainerMap()
        {
            Id(x => x.Id, x =>
            {
                x.Type(NHibernateUtil.Int32);
                x.Column("id");
                x.UnsavedValue(0);
                x.Generator(Generators.Increment);

            });

            Property(b => b.ContainerName, x =>
            {
                x.Length(50);
                x.Column("containerName");
                x.Type(NHibernateUtil.String);
                x.NotNullable(false);
            });
            Property(b => b.Latitude, x =>
            {
                x.Length(14);
                x.Column("latitude");
                x.Type(NHibernateUtil.Double);
                x.NotNullable(false);
            });
            Property(b => b.Longitude, x =>
            {
                x.Length(10);
                x.Column("longitude");
                x.Type(NHibernateUtil.Double);
                x.NotNullable(false);
            });

            Property(b => b.VehicleId, x =>
            {
                x.Length(14);
                x.Column("vehicleId");
                x.Type(NHibernateUtil.Int32);
                x.NotNullable(false);
            });
            Table("container");
        }
    }
}
