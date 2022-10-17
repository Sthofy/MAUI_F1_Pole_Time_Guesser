using PoleTimeGuesser.Model;

namespace PoleTimeGuesser.ViewModel
{
    public class ScheduleViewModel : BaseViewModel
    {
        public Task Init { get; }

        public ObservableCollection<ScheduleModel> scheduleModels { get; } = new();
        F1DataGetterService f1DataGetterService = new F1DataGetterService();

        public ScheduleViewModel()
        {
            Title = "Schedule";
            Init = Initailize();
        }

        private async Task Initailize()
        {
            await GetScheduleAsync();
        }

        private async Task GetScheduleAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var schedules = await f1DataGetterService.GetSchedule();
                if (scheduleModels.Count != 0)
                    scheduleModels.Clear();

                foreach (var item in schedules)
                {
                    scheduleModels.Add(item);
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
        private async void ShowCircuitPopupAsync()
        {
            await Shell.Current.Navigation.PushModalAsync(new CircuitPopup());
        }

    }
}
