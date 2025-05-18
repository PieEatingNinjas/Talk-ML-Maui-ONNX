using System;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace MedicalCostModelTrainer;

public class ModelTrainer
{
    private readonly string _dataPath;
    private readonly string _modelOutputPath;

    private readonly MLContext _mlContext;

    public ModelTrainer(string dataPath, string modelOutputPath)
    {
        _dataPath = dataPath;
        _modelOutputPath = modelOutputPath;
        _mlContext = new MLContext();
    }

    public void TrainModel()
    {
        var data = LoadData();
        var (trainData, testData) = SplitData(data);
        var model = BuildAndTrainModel(trainData);
        EvaluateModel(model, testData);
        SaveModel(model, trainData.Schema);
    
    }
    
    private IDataView LoadData()
        => _mlContext.Data.LoadFromTextFile<InputModel>(_dataPath, separatorChar: ',', hasHeader: true);


    private (IDataView trainData, IDataView testData) SplitData(IDataView data)
    {
        var trainTestSplit = _mlContext.Data.TrainTestSplit(data, testFraction: 0.2);
        return (trainTestSplit.TrainSet, trainTestSplit.TestSet);
    }
    private ITransformer BuildAndTrainModel(IDataView trainData)
    {
        var pipeline = _mlContext.Transforms.Categorical.OneHotEncoding(
            [
                new InputOutputColumnPair("RegionEncoded", "Region"),
                new InputOutputColumnPair("SexEncoded", "Sex"),
                new InputOutputColumnPair("SmokerEncoded", "Smoker")
            ])
            .Append(_mlContext.Transforms.Conversion.ConvertType("ChildrenFloat", "Children", DataKind.Single))
            .Append(_mlContext.Transforms.Conversion.ConvertType("AgeFloat", "Age", DataKind.Single))
            .Append(_mlContext.Transforms.Concatenate("Features", "RegionEncoded", "SexEncoded", "SmokerEncoded", "ChildrenFloat", "AgeFloat", "Bmi"))
            .Append(_mlContext.Regression.Trainers.Sdca(featureColumnName: "Features", labelColumnName: "MedicalCost", maximumNumberOfIterations: 100));
        
        var model = pipeline.Fit(trainData);
        return model;
    }

    
    private void EvaluateModel(ITransformer model, IDataView testData)
    {
        var predictions = model.Transform(testData);
        var metrics = _mlContext.Regression.Evaluate(predictions, labelColumnName: "MedicalCost");

        Console.WriteLine($"MeanAbsError: {metrics.MeanAbsoluteError}"); 
        Console.WriteLine($"MeanSquaredError: {metrics.MeanSquaredError}");
        Console.WriteLine($"R-squared: {metrics.RSquared}");
        Console.WriteLine($"RMSE: {metrics.RootMeanSquaredError}");
    }
    
    private void SaveModel(ITransformer model, DataViewSchema schema)
        => _mlContext.Model.Save(model, schema, _modelOutputPath);
}
