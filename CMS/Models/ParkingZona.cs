using NetTopologySuite.Geometries;

namespace CMS.Models;

public class ParkingZona
{
    public int Id { get; set; }
    public string Naziv { get; set; } = string.Empty;
    public TipZone Tip { get; set; }
    public Geometry Geometrija { get; set; } = null!;
    public bool Aktivna { get; set; } = true;
    public DateTime KreiranaNa { get; set; } = DateTime.UtcNow;
}
