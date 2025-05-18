using Microsoft.ML.Data;

namespace AillBeBack.Features.Loan;

/// <summary>
/// model input class for MyMLProject.
/// </summary>
#region model input class
public class ModelInput
{
    [LoadColumn(1)]
    [ColumnName(@"no_of_dependents")]
    public float No_of_dependents { get; set; }

    [LoadColumn(2)]
    [ColumnName(@"education")]
    public string Education { get; set; }

    [LoadColumn(3)]
    [ColumnName(@"self_employed")]
    public bool Self_employed { get; set; }
        
    [LoadColumn(4)]
    [ColumnName(@"income_annum")]
    public float Income_annum { get; set; }
        
    [LoadColumn(5)]
    [ColumnName(@"loan_amount")]
    public float Loan_amount { get; set; }
        
    [LoadColumn(6)]
    [ColumnName(@"loan_term")]
    public float Loan_term { get; set; }
        
    [LoadColumn(7)]
    [ColumnName(@"cibil_score")]
    public float Cibil_score { get; set; }
        
    [LoadColumn(8)]
    [ColumnName(@"residential_assets_value")]
    public float Residential_assets_value { get; set; }
        
    [LoadColumn(9)]
    [ColumnName(@"commercial_assets_value")]
    public float Commercial_assets_value { get; set; }
        
    [LoadColumn(10)]
    [ColumnName(@"luxury_assets_value")]
    public float Luxury_assets_value { get; set; }
        
    [LoadColumn(11)]
    [ColumnName(@"bank_asset_value")]
    public float Bank_asset_value { get; set; }
        
    [LoadColumn(12)]
    [ColumnName(@"loan_status")]
    public float Loan_status { get; set; }
        
}

#endregion

/// <summary>
/// model output class for MyMLProject.
/// </summary>
#region model output class
public class ModelOutput
{
    [ColumnName(@"no_of_dependents")]
    public float No_of_dependents { get; set; }
        
    [ColumnName(@"education")]
    public float[] Education { get; set; }
        
    [ColumnName(@"self_employed")]
    public float[] Self_employed { get; set; }
        
    [ColumnName(@"income_annum")]
    public float Income_annum { get; set; }
        
    [ColumnName(@"loan_amount")]
    public float Loan_amount { get; set; }
        
    [ColumnName(@"loan_term")]
    public float Loan_term { get; set; }
        
    [ColumnName(@"cibil_score")]
    public float Cibil_score { get; set; }
        
    [ColumnName(@"residential_assets_value")]
    public float Residential_assets_value { get; set; }
        
    [ColumnName(@"commercial_assets_value")]
    public float Commercial_assets_value { get; set; }
        
    [ColumnName(@"luxury_assets_value")]
    public float Luxury_assets_value { get; set; }
        
    [ColumnName(@"bank_asset_value")]
    public float Bank_asset_value { get; set; }
        
    [ColumnName(@"loan_status")]
    public uint Loan_status { get; set; }
        
    [ColumnName(@"Features")]
    public float[] Features { get; set; }
        
    [ColumnName(@"PredictedLabel")]
    public float PredictedLabel { get; set; }
        
    [ColumnName(@"Score")]
    public float[] Score { get; set; }
        
}

#endregion