using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project6_ApiWeather.Context;
using Project6_ApiWeather.Entities;

namespace Project6_ApiWeather.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeathersController : ControllerBase
    {
        WeatherContext context=new WeatherContext();

        [HttpGet]
        public IActionResult WeatherCityList()
        {
            var values=context.Cities.ToList();
            return Ok(values);
        }
        [HttpPost]
        public IActionResult CreateWeatherCity(City city)
        {
            context.Cities.Add(city);
            context.SaveChanges();
            return Ok("Şehir Eklendi");
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteWeatherCity(int id)
        {
           var values=context.Cities.Find(id);
            context.Remove(values);
            context.SaveChanges();
            return Ok("Şehir Silindi");
        }
        [HttpPut]
        public IActionResult UpdateWeatherCity(City city)
        {
            var values = context.Cities.Find(city.CityId);
            values.CityId = city.CityId;
            values.CityName = city.CityName;
            values.Country=city.Country;
            values.Detail = city.Detail;
            context.Update(values);
            context.SaveChanges();
            return Ok("Şehir Güncellendi");
        }
        [HttpGet("GetBtIdWeatherCity")]
        public IActionResult GetBtIdWeatherCity(int id)
        {
            var values = context.Cities.Find(id);
            return Ok(values);
        }
        [HttpGet("TotalCityCount")]
        public IActionResult TotalCityCount()
        {
            var values= context.Cities.Count();
            return Ok(values);
        }
        [HttpGet("MaxTempCityName")]
        public IActionResult CityTempMax()
        {
            var values = context.Cities.OrderBy(x => x.Temp).Select(y => y.CityName).FirstOrDefault();
           // var values = context.Cities.Select(x => x.Temp).Max();
            return Ok(values);
        }
        [HttpGet("MinTempCityName")]
        public IActionResult CityTempMin()
        {
            var values = context.Cities.OrderByDescending(x => x.Temp).Select(y => y.CityName).FirstOrDefault();
            //var values = context.Cities.Select(x => x.Temp).Min();
            return Ok(values);
        }
    }
}
