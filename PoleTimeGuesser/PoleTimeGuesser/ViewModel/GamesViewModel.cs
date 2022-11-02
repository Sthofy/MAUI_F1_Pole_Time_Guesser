using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoleTimeGuesser.ViewModel
{
    public partial class GamesViewModel : ObservableObject
    {
        
        [RelayCommand]
        async Task ShowQuiz()
        {
            await Shell.Current.GoToAsync(nameof(QuizView));
            //string action = await Shell.Current.DisplayActionSheet("Ki nyerte a 2022-es világbajnokságot", "Cancel", null, "Lewis Hamilton", "Max Verstapppen", "George Russel");
            //Debug.WriteLine("Action: " + action);
        }

    }
}
