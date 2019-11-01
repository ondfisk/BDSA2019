using BDSA2019.Lecture08.App.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BDSA2019.Lecture08.App.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly HttpClient _client;

        private double _dkk;
        public double DKK
        {
            get => _dkk; set
            {
                if (_dkk != value)
                {
                    _dkk = value; OnPropertyChanged();
                }
            }
        }

        private double _usd;
        public double USD
        {
            get => _usd; set
            {
                if (_usd != value)
                {
                    _usd = value; OnPropertyChanged();
                }
            }
        }

        private double _gbp;
        public double GBP
        {
            get => _gbp; set
            {
                if (_gbp != value)
                {
                    _gbp = value; OnPropertyChanged();
                }
            }
        }

        private double _eur;
        public double EUR
        {
            get => _eur; set
            {
                if (_eur != value)
                {
                    _eur = value; OnPropertyChanged();
                }
            }
        }

        public MainWindowViewModel()
        {
            _client = new HttpClient();
        }

        public MainWindowViewModel(HttpClient client)
        {
            _client = client;
        }

        public ICommand Calculate => new RelayCommand(async _ => await CalculateRates());

        private async Task CalculateRates()
        {
            USD = await GetRateAsync("DKK", "USD") * DKK;
            GBP = await GetRateAsync("DKK", "GBP") * DKK;
            EUR = await GetRateAsync("DKK", "EUR") * DKK;
        }

        private async Task<double> GetRateAsync(string from, string to)
        {
            Thread.Sleep(TimeSpan.FromSeconds(2));

            var url = $"http://currency-api.appspot.com/api/{from}/{to}.json";

            var data = await _client.GetStringAsync(url);
            var json = JsonConvert.DeserializeObject<ExchangeRate>(data);

            return json.Rate;
        }
    }
}
