using Strona_z_Pogoda.Models;
using System;
using System.Web.Mvc;
using System.Web.WebPages;
using weatherInfo;

namespace Strona_z_Pogoda.Controllers
{
    
    public class HomeController : Controller
    {
        static double publicLat ;
        static double publicLon ;
        static String Nazwa_Miasta;

        public ActionResult Index()
        {

            #region Weather

            #region Current
            string Miasto = "Limanowa";

            if (!Request["Wybrane_miasto"].IsEmpty())
            {
                Miasto = Request["Wybrane_miasto"];
            }




            weatherInfo.WeatherInfo.root checkWeather = new WeatherInfo.root();
            weather weather = new weather();

            checkWeather = weather.DownloadWeather(Miasto);



            bool sprawdz = true;
            if (checkWeather != null)
            {
                sprawdz = true;
                // lastweather.SaveWeather(checkWeather);
                ViewBag.weatherName = checkWeather.name;
                ViewBag.weatherCountry = checkWeather.sys.country;
                ViewBag.weatherTemp = checkWeather.main.temp.ToString();
                ViewBag.weatherfeelsLike = checkWeather.main.feels_like.ToString();
                ViewBag.weatherTempMax = checkWeather.main.temp_max.ToString();
                ViewBag.weatherTempMin = checkWeather.main.temp_min.ToString();
                ViewBag.weatherPressure = checkWeather.main.pressure.ToString();
                ViewBag.weatherHumidity = checkWeather.main.humidity.ToString();
                ViewBag.weatherWindSpeed = checkWeather.wind.speed.ToString();


            }
            else
            {
                sprawdz = false;
                ViewBag.error = "Brak miasta " + Miasto + " w bazie danych";
            }

            #endregion

            #region Historia

            weatherInfo.HistoriWeatherInfo.root yeasterdayweater = new weatherInfo.HistoriWeatherInfo.root();
            PastWeather pastWeather = new PastWeather();

            UnixTime wczorajTime = new UnixTime();
            int time = wczorajTime.WczorajUnixTime();

            int dtNow = 15;

            if (sprawdz == true)
            {
                yeasterdayweater = pastWeather.DownloandLastWeather(checkWeather.coord.lat, checkWeather.coord.lon, time);
                //dtNow = pastWeather.ActualTime(yeasterdayweater);

            }
            if (yeasterdayweater.hourly != null && dtNow != -1)
            {

                ViewBag.yeasterdayWeaterTemp = yeasterdayweater.hourly[dtNow].temp;
                if (checkWeather.main.temp > yeasterdayweater.hourly[dtNow].temp)
                {
                    int one = (int)((checkWeather.main.temp - yeasterdayweater.hourly[dtNow].temp) * 100);
                    double two = (double)one / 100;

                    ViewBag.yeasterdayDifferenceTempInt = two;
                    ViewBag.yeasterdayDifferenceTempString = "cieplej ";
                }
                else if (checkWeather.main.temp < yeasterdayweater.hourly[dtNow].temp)
                {
                    int one = (int)((yeasterdayweater.hourly[dtNow].temp - checkWeather.main.temp) * 100);
                    double two = (double)one / 100;

                    ViewBag.yeasterdayDifferenceTempInt = two;
                    ViewBag.yeasterdayDifferenceTempString = "zimniej ";
                }

                ViewBag.yeasterdayWeaterfeelslike = yeasterdayweater.hourly[dtNow].feels_like;
                if (checkWeather.main.feels_like > yeasterdayweater.hourly[dtNow].feels_like)
                {
                    int one = (int)((checkWeather.main.feels_like - yeasterdayweater.hourly[dtNow].feels_like) * 100);
                    double two = (double)one / 100;

                    ViewBag.yeasterdayDifferencefeelslikeInt = two;
                    ViewBag.yeasterdayDifferencefeelslikeString = "cieplej ";
                }
                else if (checkWeather.main.feels_like < yeasterdayweater.hourly[dtNow].feels_like)
                {
                    int one = (int)((yeasterdayweater.hourly[dtNow].feels_like - checkWeather.main.feels_like) * 100);
                    double two = (double)one / 100;

                    ViewBag.yeasterdayDifferencefeelslikeint = two;
                    ViewBag.yeasterdayDifferencefeelslikeString = "zimniej ";
                }

                ViewBag.yeasterdayWeaterPressure = yeasterdayweater.hourly[dtNow].pressure;
                if (checkWeather.main.pressure > yeasterdayweater.hourly[dtNow].pressure)
                {
                    ViewBag.yeasterdayDifferencePressureInt = checkWeather.main.pressure - yeasterdayweater.hourly[dtNow].pressure;
                    ViewBag.yeasterdayDifferencePressureString = "wyższe";

                }
                else if (checkWeather.main.pressure < yeasterdayweater.hourly[dtNow].pressure)
                {
                    ViewBag.yeasterdayDifferencePressureInt = yeasterdayweater.hourly[dtNow].pressure - checkWeather.main.pressure;
                    ViewBag.yeasterdayDifferencePressureString = "niższe";
                }

                ViewBag.yeasterdayWeaterHumidity = yeasterdayweater.hourly[dtNow].humidity;
                if (checkWeather.main.humidity > yeasterdayweater.hourly[dtNow].humidity)
                {
                    ViewBag.yeasterdayDifferenceHumidityInt = checkWeather.main.humidity - yeasterdayweater.hourly[dtNow].humidity;
                    ViewBag.yeasterdayDifferenceHumidityString = "wyższe";

                }
                else if (checkWeather.main.humidity < yeasterdayweater.hourly[dtNow].humidity)
                {
                    ViewBag.yeasterdayDifferenceHumidityInt = yeasterdayweater.hourly[dtNow].humidity - checkWeather.main.humidity;
                    ViewBag.yeasterdayDifferenceHumidityString = "niższe";
                }

                ViewBag.yeasterdayWeaterWeaterWindSpeed = yeasterdayweater.hourly[dtNow].wind_speed;
                if (checkWeather.wind.speed > yeasterdayweater.hourly[dtNow].wind_speed)
                {
                    ViewBag.yeasterdayDifferenceWindSpeedInt = checkWeather.wind.speed - yeasterdayweater.hourly[dtNow].wind_speed;
                    ViewBag.yeasterdayDifferenceWindSpeedString = "mocniejszy";

                }
                else if (checkWeather.wind.speed < yeasterdayweater.hourly[dtNow].wind_speed)
                {
                    ViewBag.yeasterdayDifferenceWindSpeedInt = yeasterdayweater.hourly[dtNow].wind_speed - checkWeather.wind.speed;
                    ViewBag.yeasterdayDifferenceWindSpeedString = "słabszy";
                }


            }
            else
            {

                ViewBag.lastError = "Nie udało się pobrać danych wczorajszego dnia";

            }
            #endregion

            #region Prognoza


            FutureWeatherInfo.root futureWeather = new FutureWeatherInfo.root();
            FutureWeather dowloand = new FutureWeather();

            UnixTime unixTime = new UnixTime();



            if (sprawdz == true)
            {
                futureWeather = dowloand.DownloandFutureWeather(checkWeather.coord.lat, checkWeather.coord.lon);
                Nazwa_Miasta = checkWeather.name;
                publicLat = checkWeather.coord.lat;
                publicLon = checkWeather.coord.lon;
            }
            if (futureWeather.daily != null)
            {
                ViewBag.futureWeatherTemp = futureWeather.daily[0].temp.day;
                if (checkWeather.main.temp > futureWeather.daily[0].temp.day)
                {
                    int one = (int)((checkWeather.main.temp - futureWeather.daily[0].temp.day) * 100);
                    double two = (double)one / 100;

                    ViewBag.futureDifferenceTempInt = two;
                    ViewBag.futureDifferenceTempString = "zimniej ";
                }
                else if (checkWeather.main.temp < futureWeather.daily[0].temp.day)
                {
                    int one = (int)((futureWeather.daily[0].temp.day - checkWeather.main.temp) * 100);
                    double two = (double)one / 100;

                    ViewBag.futureDifferenceTempInt = two;
                    ViewBag.futureDifferenceTempString = "cieplej ";
                }

                ViewBag.futureWeatherTempMax = futureWeather.daily[0].temp.max;
                if (checkWeather.main.temp_max > futureWeather.daily[0].temp.max)
                {
                    int one = (int)((checkWeather.main.temp_max - futureWeather.daily[0].temp.max) * 100);
                    double two = (double)one / 100;

                    ViewBag.futureDifferenceTempMaxInt = two;
                    ViewBag.futureDifferenceTempMaxString = "zimniej";
                }
                else if (checkWeather.main.temp_max < futureWeather.daily[0].temp.max)
                {
                    int one = (int)((futureWeather.daily[0].temp.max - checkWeather.main.temp_max) * 100);
                    double two = (double)one / 100;

                    ViewBag.futureDifferenceTempMaxInt = two;
                    ViewBag.futureDifferenceTempMaxString = "cieplej";

                }

                ViewBag.futureWeatherTempMin = futureWeather.daily[0].temp.min;
                if (checkWeather.main.temp_min > futureWeather.daily[0].temp.min)
                {
                    int one = (int)((checkWeather.main.temp_min - futureWeather.daily[0].temp.min) * 100);
                    double two = (double)one / 100;

                    ViewBag.futureDifferenceTempMinInt = two;
                    ViewBag.futureDifferenceTempMinString = "zimniej ";
                }
                else if (checkWeather.main.temp_min > futureWeather.daily[0].temp.min)
                {
                    int one = (int)((futureWeather.daily[0].temp.min - checkWeather.main.temp_min) * 100);
                    double two = (double)one / 100;

                    ViewBag.futureDifferenceTempMinInt = two;
                    ViewBag.futureDifferenceTempMinString = "cieplej ";
                }

                ViewBag.futureWeatherFeelsLike = futureWeather.daily[0].feels_Like.day;
                if (checkWeather.main.feels_like > futureWeather.daily[0].feels_Like.day)
                {
                    int one = (int)((checkWeather.main.feels_like - futureWeather.daily[0].feels_Like.day) * 100);
                    double two = (double)one / 100;

                    ViewBag.futureDifferenceFeelsLikeInt = two;
                    ViewBag.futureDifferenceFeelsLikeString = "zimniej ";

                }
                else if (checkWeather.main.feels_like < futureWeather.daily[0].feels_Like.day)
                {
                    int one = (int)((futureWeather.daily[0].feels_Like.day - checkWeather.main.feels_like) * 100);
                    double two = (double)one / 100;

                    ViewBag.futureDifferenceFeelsLikeInt = two;
                    ViewBag.futureDifferenceFeelsLikeString = "cieplej ";
                }

                ViewBag.futureWeatherPressure = futureWeather.daily[0].pressure;
                if (checkWeather.main.pressure > futureWeather.daily[0].pressure)
                {
                    int one = (int)((checkWeather.main.pressure - futureWeather.daily[0].pressure) * 100);
                    double two = (double)one / 100;

                    ViewBag.futureDifferencePressureInt = two;
                    ViewBag.futureDifferencePressureString = "niższe ";

                }
                else if (checkWeather.main.pressure < futureWeather.daily[0].pressure)
                {
                    int one = (int)((futureWeather.daily[0].pressure - checkWeather.main.pressure) * 100);
                    double two = (double)one / 100;

                    ViewBag.futureDifferencePressureInt = two;
                    ViewBag.futureDifferencePressureString = "wyższe ";
                }

                ViewBag.futureWeatherHumidity = futureWeather.daily[0].humidity;
                if (checkWeather.main.humidity > futureWeather.daily[0].humidity)
                {
                    int one = (int)((checkWeather.main.humidity - futureWeather.daily[0].humidity) * 100);
                    double two = (double)one / 100;

                    ViewBag.futureDifferenceHumidityInt = two;
                    ViewBag.futureDifferenceHumidityString = "niższe ";

                }
                else if (checkWeather.main.humidity < futureWeather.daily[0].humidity)
                {
                    int one = (int)((futureWeather.daily[0].humidity - checkWeather.main.humidity) * 100);
                    double two = (double)one / 100;

                    ViewBag.futureDifferenceHumidityInt = two;
                    ViewBag.futureDifferenceHumidityString = "wyższe ";
                }

                ViewBag.futureWeatherWindSpeed = futureWeather.daily[0].wind_speed;
                if (checkWeather.wind.speed > futureWeather.daily[0].wind_speed)
                {
                    int one = (int)((checkWeather.wind.speed - futureWeather.daily[0].wind_speed) * 100);
                    double two = (double)one / 100;

                    ViewBag.futureDifferenceWindSpeedInt = two;
                    ViewBag.futureDifferenceWindSpeedString = "słabszy ";

                }
                else if (checkWeather.wind.speed < futureWeather.daily[0].wind_speed)
                {
                    int one = (int)((futureWeather.daily[0].wind_speed - checkWeather.wind.speed) * 100);
                    double two = (double)one / 100;

                    ViewBag.futureDifferenceWindSpeedInt = two;
                    ViewBag.futureDifferenceWindSpeedString = "mocniejszy ";
                }


            }//szegóły jutrzejszego dnia
            else
            {
                ViewBag.FutureError = "Nie udało się pobrać prognozowanej pogody";
            }
            if (futureWeather.daily != null)
            {
                ViewBag.weatherLon = checkWeather.coord.lon;
                ViewBag.weatherLat = checkWeather.coord.lat;

                string dzień = unixTime.AktualnyDzieńString(futureWeather.daily[0].dt);


                ViewBag.futureWeatherOneDay = dzień;
                ViewBag.futureWeatherOneTemp = futureWeather.daily[0].temp.day;
                ViewBag.futureWeatherOnePop = futureWeather.daily[0].pop * 100;

                dzień = unixTime.AktualnyDzieńString(futureWeather.daily[1].dt);
                ViewBag.futureWeatherTwoDay = dzień;
                ViewBag.futureWeatherTwoTemp = futureWeather.daily[1].temp.day;
                ViewBag.futureWeatherTwoPop = futureWeather.daily[1].pop * 100;

                dzień = unixTime.AktualnyDzieńString(futureWeather.daily[2].dt);
                ViewBag.futureWeatherThreeDay = dzień;
                ViewBag.futureWeatherThreeTemp = futureWeather.daily[2].temp.day;
                ViewBag.futureWeatherThreePop = futureWeather.daily[2].pop * 100;

                dzień = unixTime.AktualnyDzieńString(futureWeather.daily[3].dt);
                ViewBag.futureWeatherFourDay = dzień;
                ViewBag.futureWeatherFourTemp = futureWeather.daily[3].temp.day;
                ViewBag.futureWeatherFourPop = futureWeather.daily[3].pop * 100;

                dzień = unixTime.AktualnyDzieńString(futureWeather.daily[4].dt);
                ViewBag.futureWeatherFiveDay = dzień;
                ViewBag.futureWeatherFiveTemp = futureWeather.daily[4].temp.day;
                ViewBag.futureWeatherFivePop = futureWeather.daily[4].pop * 100;

                dzień = unixTime.AktualnyDzieńString(futureWeather.daily[5].dt);
                ViewBag.futureWeatherSixDay = dzień;
                ViewBag.futureWeatherSixTemp = futureWeather.daily[5].temp.day;
                ViewBag.futureWeatherSixPop = futureWeather.daily[5].pop * 100;


                dzień = unixTime.AktualnyDzieńString(futureWeather.daily[6].dt);
                ViewBag.futureWeatherSevenDay = dzień;
                ViewBag.futureWeatherSevenTemp = futureWeather.daily[6].temp.day;
                ViewBag.futureWeatherSevenPop = futureWeather.daily[6].pop * 100;

                dzień = unixTime.AktualnyDzieńString(futureWeather.daily[7].dt);
                ViewBag.futureWeatherEightDay = dzień;
                ViewBag.futureWeatherEightTemp = futureWeather.daily[7].temp.day;
                ViewBag.futureWeatherEightPop = futureWeather.daily[7].pop * 100;
            } //Szybka probnoza na 8 dni

            #endregion

            #endregion


            return View();
        }

        public ActionResult FutureWeather()
        {
            #region WybórDnia
            string NumberDayString = null;
            if (!Request["0"].IsEmpty())
            {
                NumberDayString = Request["0"];
            }


            int NumberDay = Convert.ToInt32(NumberDayString);
            #endregion

            FutureWeatherInfo.root futureWeather = new FutureWeatherInfo.root();
            FutureWeather dowloand = new FutureWeather();
         
                futureWeather = dowloand.DownloandFutureWeather(publicLat, publicLon);
            ViewBag.test = publicLat + " " + publicLon;
            
            UnixTime unixTime = new UnixTime();


            if (futureWeather.daily != null)
            {
                ViewBag.NazwaMiasta = Nazwa_Miasta;


                ViewBag.Dzieńtygodnia = unixTime.AktualnyDzieńString(futureWeather.daily[NumberDay].dt);
                ViewBag.futureWeatherDt = futureWeather.daily[NumberDay].dt;
                ViewBag.futureWeatherSunrise = unixTime.Aktualna_Godzina(futureWeather.daily[NumberDay].sunrise,true);
                ViewBag.futureWeatherSunset = unixTime.Aktualna_Godzina(futureWeather.daily[NumberDay].sunset,false);
                ViewBag.futureWeatherTempMorn = futureWeather.daily[NumberDay].temp.morn;
                ViewBag.futureWeatherTempDay = futureWeather.daily[NumberDay].temp.day;
                ViewBag.futureWeatherTempEve = futureWeather.daily[NumberDay].temp.eve;
                ViewBag.futureWeatherTempNight = futureWeather.daily[NumberDay].temp.night;
                ViewBag.futureWeatherTempMin = futureWeather.daily[NumberDay].temp.min;
                ViewBag.futureWeatherTempMax = futureWeather.daily[NumberDay].temp.max;
                ViewBag.futureWeatherFeelsLikeMorn = futureWeather.daily[NumberDay].feels_Like.morn;
                ViewBag.futureWeatherFeelsLikeDay = futureWeather.daily[NumberDay].feels_Like.day;
                ViewBag.futureWeatherFeelsLikeEve = futureWeather.daily[NumberDay].feels_Like.eve;
                ViewBag.futureWeatherFeelsLikeNight = futureWeather.daily[NumberDay].feels_Like.night;
                ViewBag.futureWeatherPressure = futureWeather.daily[NumberDay].pressure;
                ViewBag.futureWeatherHumidity = futureWeather.daily[NumberDay].humidity;
                ViewBag.futureWeatherDewPoint = futureWeather.daily[NumberDay].dew_point;
                ViewBag.futureWeatherWindSpeed = futureWeather.daily[NumberDay].wind_speed;
                ViewBag.futureWeatherWindGust = futureWeather.daily[NumberDay].wind_gust;
                ViewBag.futureWeatherWindDeg = futureWeather.daily[NumberDay].wind_deg;
                ViewBag.futureWeatherClouds = futureWeather.daily[NumberDay].clouds;
                ViewBag.futureWeatherUvi = futureWeather.daily[NumberDay].uvi;
                ViewBag.futureWeatherVisibility = futureWeather.daily[NumberDay].visibility;
                ViewBag.futureWeatherPop = futureWeather.daily[NumberDay].pop * 100;
                ViewBag.futureWeatherRain = futureWeather.daily[NumberDay].rain;
                ViewBag.futureWeatherSnow = futureWeather.daily[NumberDay].snow;


            } //szegóły wybranego dnia
            else
            {
                ViewBag.error = "Nie udało się pobrać danych";
            }


            return View();
        }



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


    }
}