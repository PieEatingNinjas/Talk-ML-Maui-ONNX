namespace AillBeBack;

public partial class LoanResultPage : ContentPage, IQueryAttributable
{
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var score = float.Parse(query["score"] as string);
		ResultLabel.Text = $"{score:P2} chance of your loan being approved";
    }

	public LoanResultPage()
	{
		InitializeComponent();
	}

	private async void OnBackButtonClicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("..");
	}
}