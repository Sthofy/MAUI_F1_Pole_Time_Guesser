﻿using PoleTimeGuesser.Model;
using PoleTimeGuesser.Services;
using PoleTimeGuesser.View;

namespace PoleTimeGuesser.ViewModel
{
    public partial class DriverStandingsViewModel : BaseViewModel
    {
        IF1DataGetterService _f1DataGetterService;
        public ObservableCollection<DriverStandingsModel> driverStandingsModels { get; } = new();

        public DriverStandingsViewModel(IF1DataGetterService f1DataGetterService)
        {
            Title = "Driver Standings";
            _f1DataGetterService = f1DataGetterService;
        }

        [RelayCommand]
        async Task GetDriverStandigsAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var driversStandigs = await _f1DataGetterService.GetDriverStandings();
                if (driverStandingsModels.Count != 0)
                    driverStandingsModels.Clear();

                foreach (var item in driversStandigs)
                {
                    driverStandingsModels.Add(item);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        async Task GoToDriverDetailsAsync(DriverStandingsModel driverstandings)
        {
            if (driverstandings is null)
                return;

            await Shell.Current.GoToAsync($"{nameof(DriverDetailsView)}", true,
                new Dictionary<string, object>
                {
                    { "Driver" , driverstandings.Driver }
                });
        }
    }
}