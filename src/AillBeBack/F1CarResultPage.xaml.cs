namespace AillBeBack;

public partial class F1CarResultPage : ContentPage, IQueryAttributable
{
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var img = query["img"] as string;
        var label = query["label"] as string;
        var score = float.Parse(query["score"] as string);
		ResultLabel.Text = $"Looks like this is {label} ({score:P2} sure)";
		input.Source = img;
    }

	public F1CarResultPage()
	{
		InitializeComponent();
	}

	private async void OnBackButtonClicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("..");
	}
}