using Amiibopedia.Helpers;
using Amiibopedia.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Amiibopedia.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private ObservableCollection<Amiibo> _amiibos;
        public ObservableCollection<Character> Characters { get; set; }
        public ICommand SearchCommand { get; set; }

        public ObservableCollection<Amiibo> Amiibos
        {
            get => _amiibos;
            set
            {
                _amiibos = value;
                OnPropertyChanged();
            }
        }
        public MainPageViewModel()
        {
            SearchCommand =
                new Command(async (param) =>
                {
                    IsBusy = true;

                    var character = param as Character;
                    if (character != null)
                    {
                        string url = $"https://amiiboapi.com/api/amiibo/?character={character.name}";
                        var service =
                        new HttpHelper<Amiibos>();
                        var amiibos =
                        await service.GetRestServiceDataAsync(url);
                        Amiibos = new ObservableCollection<Amiibo>(amiibos.amiibo);

                    }

                    IsBusy = false;
                });
        }

        public async Task LoadCharacters()
        {
            IsBusy = true;

            var url = "https://amiiboapi.com/api/character/";
            var service =
                new HttpHelper<Characters>();
            var characters = await service.GetRestServiceDataAsync(url);
            Characters = new ObservableCollection<Character>(characters.amiibo);

            IsBusy = false;
        }

    }
}
