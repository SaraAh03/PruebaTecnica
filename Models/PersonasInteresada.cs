using System;
using System.Collections.Generic;

namespace PruebaTecnica.Models;

public partial class PersonasInteresada
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public DateOnly? FechaNacimiento { get; set; }

    public int? ProyectoId { get; set; }

    public virtual Proyecto? Proyecto { get; set; }
}
