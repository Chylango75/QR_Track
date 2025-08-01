using System;
using System.Collections.Generic;

namespace QR_Track.Models;

public partial class TblQr
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public int? IdPersona { get; set; }
}
