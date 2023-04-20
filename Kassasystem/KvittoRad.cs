using System;
using System.Collections.Generic;

namespace Kassasystem
{

    public class KvittoRad
    {
        public string ProduktNamn { get; set; }
        public int Antal { get; set; }
        public int Pris { get; set; }
        public int Summa { get; set; }
        public int ProduktId { get; set; }
        public string PrisTyp { get; set; }

        public KvittoRad(string produktNamn, int produktPris, int produktId, int antal, string prisTyp)
        {
            ProduktNamn = produktNamn;
            Antal = antal;
            Pris = produktPris;
            Summa = produktPris * antal;
            ProduktId = produktId;
            PrisTyp = prisTyp;
        }
        
        public void printRow()
        {
            Console.WriteLine($"{ProduktNamn} {Antal} * {Pris}kr = {Summa}kr ");
        }

        public void updateSumma()
        {
            Summa = Pris * Antal;
        }
    }
}



















