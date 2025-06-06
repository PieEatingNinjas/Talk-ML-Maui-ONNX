using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AillBeBack.Helpers;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace AillBeBack.Features.Loan;

public static class LoanPredictionEngine
{
    static string MLNetModelPath = string.Empty;
    static bool IsInitialized = false;

    public static async Task Init(string modelPath)
    {
        if (IsInitialized)
            return;

        if (DeviceInfo.Current.Platform == DevicePlatform.Android)
        {
            var path = await FileSystemHelper.CopyResourceFileTo(modelPath, Path.GetFileName(modelPath));
            MLNetModelPath = path;
        }
        else
        {
            MLNetModelPath = modelPath;
        }

        IsInitialized = true;
    }  

    public static readonly Lazy<PredictionEngine<ModelInput, ModelOutput>> PredictEngine = new Lazy<PredictionEngine<ModelInput, ModelOutput>>(() => CreatePredictEngine(), true);

    private static PredictionEngine<ModelInput, ModelOutput> CreatePredictEngine()
    {
        var mlContext = new MLContext();
        ITransformer mlModel = mlContext.Model.Load(MLNetModelPath, out var _);
        return mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);
    }
    /// <summary>
    /// Use this method to predict scores for all possible labels.
    /// </summary>
    /// <param name="input">model input.</param>
    /// <returns><seealso cref=" ModelOutput"/></returns>
    public static IOrderedEnumerable<KeyValuePair<string, float>> PredictAllLabels(ModelInput input)
    {
        var predEngine = PredictEngine.Value;
        var result = predEngine.Predict(input);
        return GetSortedScoresWithLabels(result);
    }
    /// <summary>
    /// Map the unlabeled result score array to the predicted label names.
    /// </summary>
    /// <param name="result">Prediction to get the labeled scores from.</param>
    /// <returns>Ordered list of label and score.</returns>
    /// <exception cref="Exception"></exception>
    public static IOrderedEnumerable<KeyValuePair<string, float>> GetSortedScoresWithLabels(ModelOutput result)
    {
        var unlabeledScores = result.Score;
        var labelNames = GetLabels(result);
        Dictionary<string, float> labledScores = new Dictionary<string, float>();
        for (int i = 0; i < labelNames.Count(); i++)
        {
            // Map the names to the predicted result score array
            var labelName = labelNames.ElementAt(i);
            labledScores.Add(labelName.ToString(), unlabeledScores[i]);
        }
        return labledScores.OrderByDescending(c => c.Value);
    }
    /// <summary>
    /// Get the ordered label names.
    /// </summary>
    /// <param name="result">Predicted result to get the labels from.</param>
    /// <returns>List of labels.</returns>
    /// <exception cref="Exception"></exception>
    private static IEnumerable<string> GetLabels(ModelOutput result)
    {
        var schema = PredictEngine.Value.OutputSchema;
        var labelColumn = schema.GetColumnOrNull("loan_status");
        if (labelColumn == null)
        {
            throw new Exception("loan_status column not found. Make sure the name searched for matches the name in the schema.");
        }
        // Key values contains an ordered array of the possible labels. This allows us to map the results to the correct label value.
        var keyNames = new VBuffer<float>();
        labelColumn.Value.GetKeyValues(ref keyNames);
        return keyNames.DenseValues().Select(x => x.ToString());
    }
    /// <summary>
    /// Use this method to predict on <see cref="ModelInput"/>.
    /// </summary>
    /// <param name="input">model input.</param>
    /// <returns><seealso cref=" ModelOutput"/></returns>
    public static ModelOutput Predict(ModelInput input)
    {
        var predEngine = PredictEngine.Value;
        return predEngine.Predict(input);
    }
}        
