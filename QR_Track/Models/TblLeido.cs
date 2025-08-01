using System;
using System.Collections.Generic;

namespace QR_Track.Models;

public partial class TblLeido
{
    public int Id { get; set; }

    public int IdQr { get; set; }

    public DateTime DtLeido { get; set; }
}
