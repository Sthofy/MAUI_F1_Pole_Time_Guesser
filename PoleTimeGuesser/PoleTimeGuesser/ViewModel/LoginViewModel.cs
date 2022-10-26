using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static JetBrains.Annotations.Async;

namespace PoleTimeGuesser.ViewModel
{
    public partial class LoginViewModel : BaseViewModel
    {
        [ObservableProperty]
        string username;

        [ObservableProperty]
        string password;

        public LoginViewModel()
        {
            Username = "Sthofy";
            Password = "12345.";
        }

        [RelayCommand]
        async Task Login()
        {
            //await Shell.Current.GoToAsync($"{nameof(CircuitDetailsView)}", true,
            //    new Dictionary<string, object>
            //    {
            //        { "Circuit" , schedule },
            //    });
            await Shell.Current.GoToAsync("///main");
        }
    }
}
