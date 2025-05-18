namespace AillBeBack;

public partial class MedicalCostsResultPage : ContentPage, IQueryAttributable
{
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var cost = float.Parse(query["cost"] as string);
		ResultLabel.Text = $"$ {cost:F2}";
    }

	public MedicalCostsResultPage()
	{
		InitializeComponent();
	}

	private async void OnBackButtonClicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("..");
	}
}