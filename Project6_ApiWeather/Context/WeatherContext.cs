﻿using Microsoft.EntityFrameworkCore;
using Project6_ApiWeather.Entities;

namespace Project6_ApiWeather.Context
{
    public class WeatherContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=LAPTOP-U78U9MAM\\SQLEXPRESS;initial catalog=Db6Project20;integrated Security=true");
        }
        public DbSet<City> Cities { get; set; }
    }
}
