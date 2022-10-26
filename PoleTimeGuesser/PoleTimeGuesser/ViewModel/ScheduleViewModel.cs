using PoleTimeGuesser.Model;

namespace PoleTimeGuesser.ViewModel
{
    public partial class ScheduleViewModel : BaseViewModel
    {
        [ObservableProperty]
        bool isRefresing;

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

        [RelayCommand]
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
                IsRefresing = false;
            }
        }

        [RelayCommand]
        async Task GoToCircuitDetailsAsync(ScheduleModel schedule)
        {
            if (schedule is null)
                return;

            await Shell.Current.GoToAsync($"{nameof(CircuitDetailsView)}", true,
                new Dictionary<string, object>
                {
                    { "Circuit" , schedule },
                });
        }
    }
}
