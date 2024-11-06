using System.Net;
using System.Text.Json;
using System.Xml.Linq;

namespace kursylista
{
    public partial class MainPage : ContentPage
    {
        public class Currency
        {
            public string? table { get; set; }
            public string? currency { get; set; }
            public string? code { get; set; }
            public IList<Rate> rates { get; set; }
        }
        //***************************
        //nazwa funkcji:          Currency
        //parametry wejściowe:    nie
        //wartość zwracana:       tablica,dane z tablicy
        //informacje:             odpowiada odpowiednim nazwom w pliku z którego sie pobiera dane
        //autor:                  Patryk Szykuła
        //*************************
        public class Rate
        {
            public string? no { get; set; }
            public string? effectiveDate { get; set; }
            public double? bid { get; set; }
            public double? ask { get; set; }
        }
        //***************************
        //nazwa funkcji:          Rate
        //parametry wejściowe:    nie
        //wartość zwracana:       dane z tablicy
        //informacje:             odpowiada odpowiednim nazwom w pliku z którego sie pobiera dane
        //autor:                  Patryk Szykuła
        //*************************
        public MainPage()
        {
            InitializeComponent();
            DateTime dzis = DateTime.Now;
            dpData1.MaximumDate = dzis;
        }
        //***************************
        //nazwa funkcji:          MainPage
        //parametry wejściowe:    wszystko
        //wartość zwracana:       data
        //informacje:             potrzebne
        //autor:                  xaml
        //*************************
        private void Bcurrency1(object sender, EventArgs e)
        {
            string data = dpData1.Date.ToString("yyyy-MM-dd");
            int index = wybierzwalute.SelectedIndex;
            string walut = "";
            if (index != -1)
            {
                walut = (string)wybierzwalute.ItemsSource[index];
            }
            string url1 = "https://api.nbp.pl/api/exchangerates/rates/c/" + walut + "/" + data + "/?format=json";
            string json;
            using (var webClient = new WebClient())
            {
                json = webClient.DownloadString(url1);
            }
            Currency c = JsonSerializer.Deserialize<Currency>(json);
            string s = $"nazwa waluty : {c.currency}\n";
            s += $"kod waluty {c.code} \n";
            s += $"Data : {c.rates[0].effectiveDate} \n";
            s += $"Cena skupu : {c.rates[0].bid} \n";
            s += $"Cena sprzedazy : {c.rates[0].ask} \n ";
            textCurrency1.Text = s;
        }
        //***************************
        //nazwa funkcji:          Bcurrency1
        //parametry wejściowe:    data, waluta, informacje ze strony internetowej api nbp,
        //wartość zwracana:       nazwa waluty, kod waluty,wybrana data, cena skupu, cena sprzedarzy
        //informacje:             cel tego programu
        //autor:                  Patryk Szykuła
        //*************************

    }
}

