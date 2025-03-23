using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_13WeatherApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://open-weather13.p.rapidapi.com/city/istanbul/EN"),
                Headers =
    {
        { "x-rapidapi-key", "b3b03a45b9mshacb9cac15e76222p13e450jsnffcf2c2c3996" },
        { "x-rapidapi-host", "open-weather13.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(body);
                var fahrenehit = json["main"]["feels_like"].ToString();
                var windSpeed = json["wind"]["speed"].ToString();
                var humidity = json["main"]["humidity"].ToString();
                lblFahrenheit.Text = fahrenehit;
                lblNem.Text = humidity;
                lblRüzgar.Text = windSpeed;
                var celsius = (double.Parse(fahrenehit) - 32);
                double celsiusvalue = celsius / 1.8;
                lblcelsius.Text = celsiusvalue.ToString("0.00");
                lblcity.Text = json["name"].ToString();
                lblcity1.Text = json["name"].ToString();
                lblCountry1.Text = json["sys"]["country"].ToString();
                lblCountry.Text = json["sys"]["country"].ToString();
                var weatherStatus = json["weather"][0]["main"].ToString();

                string imagePath = @"C:\Users\FATIH\MyUdemy20Projects\Project_13WeatherApp\images";
                string imageFile = "";

                switch (weatherStatus)
                {
                    case "Clouds":
                        imageFile = "cloud.png";
                        break;
                    case "Rainy":
                        imageFile = "rainy.png";
                        break;
                    case "Sun":
                        imageFile = "sun.png";
                        break;
                    case "Snow":
                        imageFile = "snow.png";
                        break;
                    case "CloudwithSun":
                        imageFile = "cloudwithsun.png";
                        break;

                }
                string fullImagePath = Path.Combine(imagePath, imageFile);
                pictureBox1.Image=Image.FromFile(fullImagePath);
            }
        }
    }
}
