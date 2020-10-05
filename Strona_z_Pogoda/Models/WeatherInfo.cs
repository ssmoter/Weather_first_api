using Newtonsoft.Json;
using Strona_z_Pogoda.Models;
using System;
using System.Collections.Generic;
using System.Net;

namespace weatherInfo
{
    class WeatherInfo
    {
        public class coord
        {
            public double lon { get; set; }
            public double lat { get; set; }
        }
        public class weather
        {
            public int id { get; set; }
            public string main { get; set; }
            public string description { get; set; }

        }
        public class main
        {
            public double temp { get; set; }
            public double feels_like { get; set; }
            public double temp_min { get; set; }
            public double temp_max { get; set; }
            public double pressure { get; set; }
            public double humidity { get; set; }
        }
        public class wind
        {
            public double speed { get; set; }
            public double deg { get; set; }

        }
        public class sys
        {
            public string country { get; set; }

        }
        public class rain
        {
            public double h1 { get; set; }
            public double h3 { get; set; }
        }
        public class snow
        {
            public double h1 { get; set; }
            public double h3 { get; set; }
        }

        public class root
        {
            public string name { get; set; }
            public sys sys { get; set; }
            public double dt { get; set; }
            public wind wind { get; set; }
            public main main { get; set; }
            public List<weather> weatherList { get; set; }
            public coord coord { get; set; }
            public rain rain { get; set; }
            public snow snow { get; set; }
        }
    }

    class HistoriWeatherInfo
    {
        public class sys
        {
            public double lat { get; set; }
            public double lon { get; set; }
            public string timezone { get; set; }
        }
        public class current
        {
            public double dt { get; set; }
            public double sunrise { get; set; }
            public double sunset { get; set; }
            public double temp { get; set; }
            public double feels_like { get; set; }
            public double pressure { get; set; }
            public double humidity { get; set; }
            public double dev_point { get; set; }
            public double clouds { get; set; }
            public double visibility { get; set; }
            public double wind_speed { get; set; }
            public double wind_deg { get; set; }
            public class Weather
            {
                public double id { get; set; }
                public string main { get; set; }
                public string description { get; set; }
            }

        }
        public class hourly
        {
            public double dt { get; set; }
            public double temp { get; set; }
            public double feels_like { get; set; }
            public double pressure { get; set; }
            public double humidity { get; set; }
            public double dev_point { get; set; }
            public double wind_speed { get; set; }
            public double wind_deg { get; set; }
            public class Weather
            {
                public double id { get; set; }
                public string main { get; set; }
                public string description { get; set; }
            }
        }
        public class Weather
        {
            public double id { get; set; }
            public string main { get; set; }
            public string description { get; set; }
        }
        public class root
        {
            public sys sys { get; set; }
            public current current { get; set; }
            public hourly[] hourly { get; set; }
        }

    }

    class FutureWeatherInfo
    {
        public class sys
        {
            public double lat { get; set; }
            public double lon { get; set; }
            public double timezone { get; set; }
            public double timezone_offset { get; set; }
        }
        public class current
        {
            public double dt { get; set; }
            public double sunrice { get; set; }
            public double sunset { get; set; }
            public double temp { get; set; }
            public double feels_like { get; set; }
            public double pressure { get; set; }
            public double humidity { get; set; }
            public double dew_point { get; set; }
            public double uvi { get; set; }
            public double clouds { get; set; }
            public double visibility { get; set; }
            public double wind_speed { get; set; }
            public double wind_deg { get; set; }
            public Rain rain { get; set; }
            public Snow snow { get; set; }


        }
        public class minutely
        {
            public double dt { get; set; }
            public double precipitation { get; set; }
        }
        public class hourly
        {
            public double dt { get; set; }
            public double temp { get; set; }
            public double feels_like { get; set; }
            public double pressure { get; set; }
            public double humidity { get; set; }
            public double clouds { get; set; }
            public double visibility { get; set; }
            public double wind_speed { get; set; }
            public double wind_deg { get; set; }
            public double pop { get; set; }
            public Rain rain { get; set; }
            public Snow snow { get; set; }

        }
        public class daily
        {
            public double dt { get; set; }
            public double sunrise { get; set; }
            public double sunset { get; set; }
            public Temp temp { get; set; }
            public Feels_like feels_Like { get; set; }
            public double pressure { get; set; }
            public double humidity { get; set; }
            public double dew_point { get; set; }
            public double wind_speed { get; set; }
            public double wind_gust { get; set; }
            public double wind_deg { get; set; }
            public double clouds { get; set; }
            public double uvi { get; set; }
            public double visibility { get; set; }
            public double pop { get; set; }
            public double rain { get; set; }
            public double snow { get; set; }
        }



        public class Rain
        {
            public double h1 { get; set; }
        }
        public class Snow
        {
            public double h1 { get; set; }
        }
        public class Temp
        {
            public double morn { get; set; }
            public double day { get; set; }
            public double eve { get; set; }
            public double night { get; set; }
            public double min { get; set; }
            public double max { get; set; }
        }
        public class Feels_like
        {
            public double morn { get; set; }
            public double day { get; set; }
            public double eve { get; set; }
            public double night { get; set; }
        }
        public class Weather
        {
            public double id { get; set; }
            public string main { get; set; }
            public string description { get; set; }

        }


        public class root
        {
            public sys sys { get; set; }
            public current current { get; set; }
            public minutely[] minutely { get; set; }
            public hourly[] hourly { get; set; }
            public daily[] daily { get; set; }
        }
    }





    class weather
    {

        const string APPID = "298c12510c52c152e4e52fa57d4b1a1c";

        public weatherInfo.WeatherInfo.root DownloadWeather(string city)
        {
            try
            {

                var client = new WebClient();
                string url = string.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}&units=metric", city, APPID);
                var json = client.DownloadString(url);
                var wynik = JsonConvert.DeserializeObject<weatherInfo.WeatherInfo.root>(json);
                return wynik;
            }
            catch (System.Exception)
            {
                return null;
            }

        }



        #region save
        //public void SaveWeather(weatherInfo.WeatherInfo.root _WeatherInfo)
        //{
        //    FileStream file = new FileStream("C:\\Users\\" + Environment.UserName + "\\Desktop\\Lastweather.txt"
        //    , FileMode.Append, FileAccess.Write);




        //    try
        //    {
        //        StreamWriter writer = new StreamWriter(file);


        //        writer.WriteLine(DateTime.Now.ToString("yyyy-MM-dd"));

        //        writer.WriteLine(_WeatherInfo.name);
        //        writer.WriteLine(_WeatherInfo.sys.country);
        //        writer.WriteLine(_WeatherInfo.main.temp);
        //        writer.WriteLine(_WeatherInfo.main.feels_like);
        //        writer.WriteLine(_WeatherInfo.main.temp_max);
        //        writer.WriteLine(_WeatherInfo.main.temp_min);
        //        writer.WriteLine(_WeatherInfo.main.pressure);
        //        writer.WriteLine(_WeatherInfo.main.humidity);
        //        writer.WriteLine(_WeatherInfo.wind.speed);

        //        writer.Close();
        //    }
        //    catch (Exception)
        //    {
        //        StreamWriter writer = new StreamWriter(file);
        //        writer.WriteLine("Nie udało się zapisać danych\n\n\n\n\n\n\n\n\n");
        //        writer.Close();
        //    }
        //}

        //public weatherInfo.WeatherInfo.root ReadWeather(string DATA, string city)
        //{
        //    FileStream file = new FileStream("C:\\Users\\" + Environment.UserName + "\\Desktop\\Lastweather.txt"
        //   , FileMode.Open, FileAccess.Read);

        //    weatherInfo.WeatherInfo.root readweather = new weatherInfo.WeatherInfo.root();



        //    try
        //    {
        //        StreamReader read = new StreamReader(file);
        //        while (!read.EndOfStream)
        //        {

        //            if (read.ReadLine() == DATA)
        //            {
        //                while (!read.EndOfStream)
        //                {
        //                    if (read.ReadLine() == city)
        //                    {
        //                        readweather.name = city;
        //                        readweather.sys.country = read.ReadLine();
        //                        readweather.main.temp = Convert.ToDouble(read.ReadLine());
        //                        readweather.main.feels_like = Convert.ToDouble(read.ReadLine());
        //                        readweather.main.temp_max = Convert.ToDouble(read.ReadLine());
        //                        readweather.main.temp_min = Convert.ToDouble(read.ReadLine());
        //                        readweather.main.pressure = Convert.ToDouble(read.ReadLine());
        //                        readweather.main.humidity = Convert.ToDouble(read.ReadLine());
        //                        readweather.wind.speed = Convert.ToDouble(read.ReadLine());
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        readweather = null;
        //    }

        //    return readweather;
        //}
        #endregion



    }

    class PastWeather
    {
        const string APPID_2 = "0505ec9a1af0f6ad16c1ddabdac4c95e";

        public weatherInfo.HistoriWeatherInfo.root DownloandLastWeather(double lat, double lon, int time)
        {

            try
            {
                var client = new WebClient();
                string url = string.Format("https://api.openweathermap.org/data/2.5/onecall/timemachine?lat={0}&lon={1}&dt={2}&appid={3}&units=metric"
                  , lat, lon, time, APPID_2);
                var json = client.DownloadString(url);
                var wynik = JsonConvert.DeserializeObject<weatherInfo.HistoriWeatherInfo.root>(json);

                return wynik;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int ActualTime(HistoriWeatherInfo.root historiWeatherInfo)
        {
            UnixTime unixTime = new UnixTime();
            int teraz = unixTime.DzisiajUnixTime();
            teraz = teraz / 100;
            teraz = teraz * 100;


            int n = -1;

            for (int i = 0; i < historiWeatherInfo.hourly.Length; i++)
            {
                n++;
                if (historiWeatherInfo.hourly[i].dt == teraz)
                {

                    break;
                }

            }


            return n;
        }



    }

    class FutureWeather
    {
        const string APPID_2 = "0505ec9a1af0f6ad16c1ddabdac4c95e";

        public weatherInfo.FutureWeatherInfo.root DownloandFutureWeather(double lat, double lon)
        {

            try
            {
                var client = new WebClient();
                string url = string.Format("https://api.openweathermap.org/data/2.5/onecall?lat={0}&lon={1}&exclude=&appid={2}&units=metric"
                  , lat, lon, APPID_2);
                var json = client.DownloadString(url);
                var wynik = JsonConvert.DeserializeObject<weatherInfo.FutureWeatherInfo.root>(json);
                return wynik;
            }
            catch (Exception)
            {
                return null;
            }
        }



    }

}
