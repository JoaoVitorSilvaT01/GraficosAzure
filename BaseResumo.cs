using System;
using System.Collections.Generic;

namespace AzureOrbis;

public partial class BaseResumo
{
    public DateOnly? DtBase { get; set; }

    public string? Periodo { get; set; }

    public string? Modalidade { get; set; }

    public string? Instalacao { get; set; }

    public double? Quota { get; set; }

    public string? PostoHorario { get; set; }

    public double QtdConsumo { get; set; }

    public double? QtdGeracao { get; set; }

    public double? QtdCompensacao { get; set; }

    public double? QtdSaldoAnt { get; set; }

    public double? QtdTransferencia { get; set; }

    public double? QtdRecebimento { get; set; }

    public double? QtdSaldoAtual { get; set; }

    public double? QtdSaldoExpirar { get; set; }

    public double? QtdProxSaldoExpirar { get; set; }

    public string? DtProxSaldoExpirar { get; set; }

    public string? Usina { get; set; }

    public string? Empresa { get; set; }

    public string? ArquivoXml { get; set; }

    public string? PeriodoUso { get; set; }
}
