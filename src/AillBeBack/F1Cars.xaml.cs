using AillBeBack.Features.F1Cars;

namespace AillBeBack;

public partial class F1Cars : ContentPage
{
	public List<string> Files = [
		"F1Cars/Testing/AT/1.jpg",
		"F1Cars/Testing/AT/2.jpg",
		"F1Cars/Testing/AT/3.jpg",
		"F1Cars/Testing/F/1.jpg",
		"F1Cars/Testing/F/2.jpg",
		"F1Cars/Testing/F/3.jpg",
		"F1Cars/Testing/McL/1.jpg",
		"F1Cars/Testing/McL/2.jpg",
		"F1Cars/Testing/McL/3.jpg",
		"F1Cars/Testing/Merc/1.jpg",
		"F1Cars/Testing/Merc/2.jpg",
		"F1Cars/Testing/Merc/3.jpg",
	];

	public F1Cars()
	{
		InitializeComponent();
		ImagePicker.ItemsSource = Files;
	}

	private async void SubmitButton_Clicked(object sender, EventArgs e)
	{
		await F1CarPredictionEngine.Init("F1Cars/model.onnx", "F1Cars/labels.txt");

		var img = ImagePicker.SelectedItem.ToString()!;
		if (img is not null)
		{
			var stream = await FileSystem.OpenAppPackageFileAsync(img);
			var result = F1CarPredictionEngine.Predict(stream);
			var prediction = result.MaxBy(x => x.Value);

			await Shell.Current.GoToAsync($"f1result?img={img}&label={prediction.Key}&score={prediction.Value}");
		}
		else
		{
			await DisplayAlert("Error", "Please select an image.", "OK");
		}
	}
}