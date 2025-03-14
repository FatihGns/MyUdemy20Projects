


#region Menü_Başlangıcı
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Channels;

Console.ForegroundColor = ConsoleColor.Red;//Console Yazı rengi kırmızı yap
Console.BackgroundColor = ConsoleColor.Yellow;//Console Arka Plan rengi sarı yap
Console.Clear();// Konsolu temizle ve yeni renkleri uygula


Console.WriteLine("Api Consume İşlemine Hoş Geldiniz");
Console.WriteLine();
Console.WriteLine("### Yapmak İstediğiniz İşlemi Seçiniz ###");

Console.WriteLine("1-Şehir Listesini Getirin");
Console.WriteLine("2-Şehir ve Hava Durumu Listesini Getirin");
Console.WriteLine("3-Yeni Şehir Ekleme");
Console.WriteLine("4-Şehir Silme İşlemi");
Console.WriteLine("5-Şehir Güncelleme İşlemi");
Console.WriteLine("6-ID'Ye Göre Şehir Getirme");
Console.WriteLine();
#endregion



string number;

Console.WriteLine("Tercihiniz: ");
number = Console.ReadLine();
Console.WriteLine();

if (number == "1")
{
    string url = "https://localhost:7202/api/Weathers";
    using (HttpClient client = new HttpClient())
    {
        HttpResponseMessage response = await client.GetAsync(url);
        string responseBody = await response.Content.ReadAsStringAsync();//içeriği string formatta oku
        JArray jArray = JArray.Parse(responseBody);//responseBodyİ jSON formata Çevirdi
        foreach (var item in jArray)
        {
            string cityName = item["cityName"].ToString();
            Console.WriteLine($"Şehir: {cityName}");
        }

    }
}
if (number == "2")
{
    string url = "https://localhost:7202/api/Weathers";
    using (HttpClient client = new HttpClient())
    {
        HttpResponseMessage response = await client.GetAsync(url);
        string responseBody = await response.Content.ReadAsStringAsync();
        JArray jArray = JArray.Parse(responseBody);
        foreach (var item in jArray)
        {
            string cityName = item["cityName"].ToString();
            string temp = item["temp"].ToString();
            string country = item["country"].ToString();
            Console.WriteLine(cityName + "-" + country + "-->" + temp + "\u00B0C");
            Console.WriteLine("---------------------------------------------------");
        }

    }
}
if (number == "3")
{
    Console.WriteLine("### Yeni Veri Girişi");
    Console.WriteLine();
    string cityName, country, detail;
    decimal temp;

    Console.WriteLine("Şehir Adı: ");
    cityName = Console.ReadLine();

    Console.WriteLine("Ülke Adı: ");
    country = Console.ReadLine();

    Console.WriteLine("Hava Durumu Detayı: ");
    detail = Console.ReadLine();

    Console.WriteLine("Sıcaklık: ");
    temp = decimal.Parse(Console.ReadLine());

    string url = "https://localhost:7202/api/Weathers";
    var newWeatherCity = new
    {
        cityName = cityName,
        Country = country,
        Detail = detail,
        Temp = temp
    };
    using (HttpClient client = new HttpClient())
    {
        string json = JsonConvert.SerializeObject(newWeatherCity);
        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsync(url, content);
        response.EnsureSuccessStatusCode();
    }

}
if (number == "4")
{
    string url = "https://localhost:7202/api/Weathers/";
    Console.Write("Silmek İstediğiniz Id Değeri");
    int id = int.Parse(Console.ReadLine());

    using (HttpClient client = new HttpClient())
    {
        HttpResponseMessage response = await client.DeleteAsync(url + id);
        response.EnsureSuccessStatusCode();
    }
    Console.Write("Şehir Silindi");
}
if (number == "5")
{

    Console.WriteLine("### Veri Güncelleme");
    Console.WriteLine();
    string cityName, country, detail;
    decimal temp;
    int cityId;

    Console.WriteLine("Şehir Id: ");
    cityId = int.Parse(Console.ReadLine());

    Console.WriteLine("Şehir Adı: ");
    cityName = Console.ReadLine();

    Console.WriteLine("Ülke Adı: ");
    country = Console.ReadLine();

    Console.WriteLine("Hava Durumu Detayı: ");
    detail = Console.ReadLine();

    Console.WriteLine("Sıcaklık: ");
    temp = decimal.Parse(Console.ReadLine());
    string url = "https://localhost:7202/api/Weathers";
    var upodatedWeatherCity = new
    {
        CityId = cityId,
        CityName = cityName,
        Country = country,
        Detail = detail,
        Temp = temp
    };
    using (HttpClient client = new HttpClient())
    {
        string json = JsonConvert.SerializeObject(upodatedWeatherCity);
        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PutAsync(url, content);
        response.EnsureSuccessStatusCode();
    }



}
if (number == "6")
{
    string url = "https://localhost:7202/api/Weathers/GetBtIdWeatherCity?id=";
    Console.WriteLine("Bilgilerini Getirmek İstediğiniz Id Değeri:");
    int id = int.Parse(Console.ReadLine());
    Console.WriteLine();
    using (HttpClient client = new HttpClient())
    {
        HttpResponseMessage response = await client.GetAsync(url + id);
        response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        JObject weatherCityObject = JObject.Parse(responseBody);
 
        string cityName = weatherCityObject["cityName"].ToString();
        string country = weatherCityObject["country"].ToString();
        string detail = weatherCityObject["detail"].ToString();
        decimal temp = decimal.Parse(weatherCityObject["temp"].ToString());
        Console.WriteLine("Girmiş olduğunuz Id değerine ait bilgiler");
        Console.WriteLine();
        Console.WriteLine("Şehir: " + cityName + "Ülke:"  + country + "Detay" + detail + "Sıcaklık:" + temp);
    }
}
Console.Read();