using AillBeBack.Features.MedicalCosts;

namespace AillBeBack;

public partial class MedicalCosts : ContentPage
{
	public MedicalCosts()
	{
		InitializeComponent();
	}

	private async void SubmitButton_Clicked(object sender, EventArgs e)
	{
		var modelInput = new InputModel
		{
			Age = int.Parse(AgeEntry.Text),
			Sex = SexPicker.SelectedItem?.ToString() ?? string.Empty,
			Bmi = float.Parse(BMIEntry.Text),
			Children = int.Parse(ChildrenEntry.Text),
			Smoker = SmokerSwitch.IsToggled,
			Region = RegionPicker.SelectedItem?.ToString() ?? string.Empty,
		};

		await MedicalCostPredictionEngine.Init("MedicalCosts/MedicalCostModel.zip");
		var result = MedicalCostPredictionEngine.Predict(modelInput);
		

		await Shell.Current.GoToAsync($"costsresult?cost={result.MedicalCost}");
	}
}