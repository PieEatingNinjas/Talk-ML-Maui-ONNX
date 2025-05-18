using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ML.Data;

namespace AillBeBack.Features.MedicalCosts;

public class InputModel
{
    [LoadColumn(0)]
    [ColumnName(@"Age")]
    public int Age { get; set; }

    [LoadColumn(1)]
    [ColumnName(@"Sex")]
    public string Sex { get; set; } = string.Empty;

    [LoadColumn(2)]
    [ColumnName(@"Bmi")]
    public float Bmi { get; set; }

    [LoadColumn(3)]
    [ColumnName(@"Children")]
    public int Children { get; set; }

    [LoadColumn(4)]
    [ColumnName(@"Smoker")]
    public bool Smoker { get; set; }

    [LoadColumn(5)]
    [ColumnName(@"Region")]
    public string Region { get; set; } = string.Empty;

    [LoadColumn(6)]
    [ColumnName(@"MedicalCost")]
    public float MedicalCost { get; set; }
}

public class OutputModel
{
    [ColumnName("Score")]
    public float MedicalCost { get; set; }
}