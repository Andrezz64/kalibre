using System;
using System.Collections.Generic;

namespace kalibre_api;

public partial class Despesa
{
    public int DespesaId { get; set; }

    public decimal Valor { get; set; }

    public DateTime Data { get; set; }
}
