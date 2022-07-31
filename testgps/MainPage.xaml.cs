using testgps.Services;

namespace testgps;

public partial class MainPage : ContentPage
{
	int count = 0;
	int time = 0;
	Location location;
	LocationService service;
	public MainPage()
	{
		InitializeComponent();
	}

	private async void OnSendBtnClicked(object sender, EventArgs e)
	{
        var periodicTimer = new PeriodicTimer(TimeSpan.FromSeconds(time));
        while (await periodicTimer.WaitForNextTickAsync())
        {
            // Place function in here..
            service.SendLocation();
        }
        //count++;

        //if (count == 1)
        //	CounterBtn.Text = $"Clicked {count} time";
        //else
        //	CounterBtn.Text = $"Clicked {count} times";

        //SemanticScreenReader.Announce(CounterBtn.Text);
    }

	public async void OnSetTimeIntervalClicked(object sender, EventArgs e)
	{
		time = (int)TimeSlider.Value;

		location = await service.GetCurrentLocation();
		Position.Text = $"({location.Longitude}, {location.Latitude})";
		SemanticScreenReader.Announce(Position.Text);
    }

    private async void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
	{
		time = (int)e.NewValue;

		Interval.Text = $"{(int)e.NewValue} Seconds";
		SemanticScreenReader.Announce(Interval.Text);

        location = await service.GetCurrentLocation();
        Position.Text = $"({location.Longitude}, {location.Latitude})";
        SemanticScreenReader.Announce(Position.Text);
    }

}

