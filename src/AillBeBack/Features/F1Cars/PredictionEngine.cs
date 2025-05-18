using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms.Image;
using AillBeBack.Helpers;

namespace AillBeBack.Features.F1Cars;

public static class F1CarPredictionEngine
{
    static string ModelPath = string.Empty;
    static bool IsInitialized = false;
    static string[] Labels = [];
    
    public static async Task Init(string modelPath, string labelsPath)
    {
        if (IsInitialized)
            return;

        if (DeviceInfo.Current.Platform == DevicePlatform.Android)
        {
            var path = await FileSystemHelper.CopyResourceFileTo(modelPath, "model.onnx");
            ModelPath = path;

            var labelsPathCopy = await FileSystemHelper.CopyResourceFileTo(labelsPath, "labels");
            Labels = await File.ReadAllLinesAsync(labelsPathCopy);
        }
        else
        {
            ModelPath = modelPath;
            Labels = await File.ReadAllLinesAsync(labelsPath);
        }
    }

	private static readonly Lazy<PredictionEngine<InputModel, OutputModel>> PredictEngine =
        new(() => CreatePredictEngine());

    private static PredictionEngine<InputModel, OutputModel> CreatePredictEngine()
    {
        var context = new MLContext();
    
        var pipeline = context.Transforms.ResizeImages(
            inputColumnName: "Image",
            outputColumnName: "resized_image",
            imageWidth: 300,
            imageHeight: 300,
            resizing: ImageResizingEstimator.ResizingKind.Fill)
            .Append(context.Transforms.ExtractPixels(
                inputColumnName: "resized_image",
                outputColumnName: "data"))
            .Append(context.Transforms.ApplyOnnxModel(
                modelFile: ModelPath,
                inputColumnNames: ["data"],
                outputColumnNames: ["model_output"]));
        
        var model = pipeline.Fit(context.Data.LoadFromEnumerable(new List<InputModel>()));

		return context.Model.CreatePredictionEngine<InputModel, OutputModel>(model);
    }

    public static Dictionary<string, float> Predict(Stream input)
    {
        var img = MLImage.CreateFromStream(input);

        var prediction = PredictEngine.Value.Predict(new InputModel{ Image = img });

        if(prediction.ModelOutput is null)
            return [];

        return Labels.Select((label, index) => new { label, index })
            .ToDictionary(x => x.label, x => prediction.ModelOutput[x.index]);
    }
}        
