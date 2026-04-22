using CMS.Data;
using CMS.Models;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Geometries;

namespace CMS.Controllers.Api;

[Route("api/parkingzone")]
[ApiController]
public class ParkingZoneApiController(AppDbContext db) : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        var zone = db.ParkingZone.ToList();
        return Ok(new
        {
            type = "FeatureCollection",
            features = zone.Select(z => new
            {
                type = "Feature",
                id = z.Id,
                properties = new
                {
                    id = z.Id,
                    naziv = z.Naziv,
                    tip = (int)z.Tip,
                    aktivna = z.Aktivna,
                    kreiranaNa = z.KreiranaNa
                },
                geometry = z.Geometrija
            })
        });
    }

    public record CreateZonaRequest(string Naziv, TipZone Tip, Geometry Geometrija);

    [HttpPost]
    public IActionResult Post([FromBody] CreateZonaRequest req)
    {
        req.Geometrija.SRID = 4326;
        var zona = new ParkingZona
        {
            Naziv = req.Naziv,
            Tip = req.Tip,
            Geometrija = req.Geometrija,
            Aktivna = true,
            KreiranaNa = DateTime.UtcNow
        };
        db.ParkingZone.Add(zona);
        db.SaveChanges();
        return Ok(new { zona.Id });
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var zona = db.ParkingZone.Find(id);
        if (zona == null) return NotFound();
        db.ParkingZone.Remove(zona);
        db.SaveChanges();
        return Ok();
    }

    [HttpPatch("{id}/toggle")]
    public IActionResult Toggle(int id)
    {
        var zona = db.ParkingZone.Find(id);
        if (zona == null) return NotFound();
        zona.Aktivna = !zona.Aktivna;
        db.SaveChanges();
        return Ok(new { zona.Aktivna });
    }
}
