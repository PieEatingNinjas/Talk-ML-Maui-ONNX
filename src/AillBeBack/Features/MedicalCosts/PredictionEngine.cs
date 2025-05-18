using Microsoft.ML;
using AillBeBack.Helpers;

namespace AillBeBack.Features.MedicalCosts;

public static class MedicalCostPredictionEngine
{
    static string ModelPath = string.Empty;
    static bool IsInitialized = false;

    public static async Task Init(string modelPath)
    {
        if (IsInitialized)
            return;

        if (DeviceInfo.Current.Platform == DevicePlatform.Android)
        {
            var path = await FileSystemHelper.CopyResourceFileTo(modelPath, 
                Path.GetFileName(modelPath));
            ModelPath = path;
        }
        else
        {
            ModelPath = modelPath;
        }

        IsInitialized = true;
    }

    private static readonly Lazy<PredictionEngine<InputModel, OutputModel>> PredictEngine =
        new (() => CreatePredictEngine());

    private static PredictionEngine<InputModel, OutputModel> CreatePredictEngine()
    {
        var mlContext = new MLContext();
        ITransformer mlModel = mlContext.Model.Load(ModelPath, out var _);
        return mlContext.Model.CreatePredictionEngine<InputModel, OutputModel>(mlModel);
    }

    public static OutputModel Predict(InputModel input)
        => PredictEngine.Value.Predict(input);

}        
