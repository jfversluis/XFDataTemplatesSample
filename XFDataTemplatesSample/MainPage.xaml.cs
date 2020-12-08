using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace XFDataTemplatesSample
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<Monkey> Monkeys { get; } = new ObservableCollection<Monkey>();

        private readonly HttpClient _httpClient = new HttpClient();

        public MainPage()
        {
            InitializeComponent();

            BindingContext = this;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (!Monkeys.Any())
            {
                var monkeyJson = await _httpClient.GetStringAsync("https://montemagno.com/monkeys.json");

                var monkeys = JsonConvert.DeserializeObject<List<Monkey>>(monkeyJson);

                foreach(var m in monkeys)
                {
                    Monkeys.Add(m);
                }
            }
        }
    }
}