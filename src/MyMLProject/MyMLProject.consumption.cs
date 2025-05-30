// This file was auto-generated by ML.NET Model Builder.
using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
namespace MyMLProject.ConsoleApp
{
    public partial class MyMLProject
    {
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

        private static string MLNetModelPath = Path.GetFullPath("MyMLProject.mlnet");

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
}
