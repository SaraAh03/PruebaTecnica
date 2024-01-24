using System;
using System.Collections.Generic;

namespace PruebaTecnica.Models;

public partial class Proyecto
{
    public int Id { get; set; }

    public string Codigo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string Constructora { get; set; } = null!;

    public string Contacto { get; set; } = null!;

    public virtual ICollection<PersonasInteresada> PersonasInteresada { get; set; } = new List<PersonasInteresada>();
}
