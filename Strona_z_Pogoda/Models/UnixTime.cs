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
            string[] DzieńTygodnia = { "Czwartek", "Piątek", "Sobota", "Niedziela", "Poniedziałek", "Wtorek", "Sroda" };


            int IlośćDni = ((int)UTCTime / 86400);
            int WybranyDzień = 0;
            string Dzień = null;
            for (int i = 0; i < IlośćDni; i++)
            {
                if (WybranyDzień == 7)
                {
                    WybranyDzień = 0;
                }
                else
                {
                    Dzień = DzieńTygodnia[WybranyDzień];
                    WybranyDzień++;
                }
            }

            return Dzień;
        }



        public string Aktualna_Godzina(double UTCTime,bool rano)
        {
            int Teraz = DzisiajUnixTime();
            int obliczona_Godzina = (int)(UTCTime - Teraz);
            if (obliczona_Godzina < 0)
            {
                obliczona_Godzina = -obliczona_Godzina;
            }



            int sekunda = 0, minuta = 0, godzina = 0;

            for (int i = 0; i < obliczona_Godzina; i++)
            {
                sekunda++;
                if (sekunda == 60)
                {
                    sekunda = 0;
                    minuta++;
                    if (minuta == 60)
                    {
                        minuta = 0;
                        godzina++;
                        if (godzina == 24)
                        {
                            godzina = 0;
                        }
                    }
                }
            }

            int sekunda_1 = 0, minuta_1 = 0, godzina_1 = 0;

            for (int i = 0; i < Teraz; i++)
            {
                sekunda_1++;
                if (sekunda_1 == 60)
                {
                    minuta_1++;
                    sekunda_1 = 0;
                    if (minuta_1 == 60)
                    {
                        minuta_1 = 0;
                        godzina_1++;
                        if (godzina_1 == 24)
                        {
                            godzina_1 = 0;
                        }
                    }
                }
            }




            if (rano)
            {
                godzina = godzina_1 - godzina;
                if (godzina < 0) { godzina = -godzina; }
                minuta = minuta_1 - minuta;
                if (minuta < 0) { minuta = -minuta; }
            }
            else
            {
                godzina = godzina_1 + godzina;
                if (godzina < 0) { godzina = -godzina; }
                minuta = minuta_1 + minuta;
                if (minuta < 0) { minuta = -minuta; }
            }

            string GodzinaString = godzina.ToString() + ":" + minuta.ToString();



            return GodzinaString;
        }


    }
}