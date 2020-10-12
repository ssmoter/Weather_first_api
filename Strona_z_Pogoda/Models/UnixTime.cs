using Microsoft.Ajax.Utilities;
using System;

namespace Strona_z_Pogoda.Models
{
    public class UnixTime
    {
        public int DzisiajUnixTime()
        {
            int toDayTime = (int)DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            return toDayTime;
        }
        public int WczorajUnixTime()
        {
            int wczoraj = DzisiajUnixTime() - 86400;
            return wczoraj;
        }
        public int JutroUnixTime()
        {
            int jutro = DzisiajUnixTime() + 86400;
            return jutro;
        }

        public string AktualnyDzieńString(double UTCTime)
        {
   
          return DateTimeOffset.FromUnixTimeSeconds((long)UTCTime).DateTime.ToLongDateString();
        }


        public string Aktualna_Godzina(double UTCTime)
        {
            var Pelna_data = DateTimeOffset.FromUnixTimeSeconds((long)UTCTime).DateTime.ToLocalTime();
            int Tylko_godzina = Pelna_data.Hour;
            int tylko_minuta = Pelna_data.Minute;
            string pelna_godziana = Tylko_godzina + ":" + tylko_minuta;

            return pelna_godziana;
        }


    }
}