using AillBeBack.Features.Loan;

namespace AillBeBack;

public partial class Loan : ContentPage
{
	public Loan()
	{
		InitializeComponent();
	}

	private async void SubmitButton_Clicked(object sender, EventArgs e)
	{
		var modelInput = new ModelInput
		{
			No_of_dependents = float.Parse(NoOfDependentsEntry.Text),
			Education = EducationPicker.SelectedItem.ToString(),
			Self_employed = SelfEmployedSwitch.IsToggled,
			Income_annum = float.Parse(IncomeAnnumEntry.Text),
			Loan_amount = float.Parse(LoanAmountEntry.Text),
			Loan_term = float.Parse(LoanTermEntry.Text),
			Cibil_score = float.Parse(CibilScoreEntry.Text),
			Residential_assets_value = float.Parse(ResidentialAssetsValueEntry.Text),
			Commercial_assets_value = float.Parse(CommercialAssetsValueEntry.Text),
			Luxury_assets_value = float.Parse(LuxuryAssetsValueEntry.Text),
			Bank_asset_value = float.Parse(BankAssetValueEntry.Text),
			Loan_status = 0 // This is the label column, set to 0 for prediction
		};

		await LoanPredictionEngine.Init("Loan/MyMLProject.mlnet");
		var result = LoanPredictionEngine.PredictAllLabels(modelInput);
		var scoreOfPositive = result.Single(a => a.Key == "1").Value;

		await Shell.Current.GoToAsync($"loanresult?score={scoreOfPositive}");

	}

	private void FillDefaultsButton_Clicked(object sender, EventArgs e)
	{
        NoOfDependentsEntry.Text = "2";
		EducationPicker.SelectedIndex = 0; // "Graduate"
		SelfEmployedSwitch.IsToggled = false;
		IncomeAnnumEntry.Text = "9600000";
		LoanAmountEntry.Text = "29900000";
		LoanTermEntry.Text = "12";
		CibilScoreEntry.Text = "778";
		ResidentialAssetsValueEntry.Text = "2400000";
		CommercialAssetsValueEntry.Text = "17600000";
		LuxuryAssetsValueEntry.Text = "22700000";
		BankAssetValueEntry.Text = "8000000";
	}
}