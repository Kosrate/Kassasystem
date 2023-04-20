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
        public int Total { get; set; }
        public int ProduktId { get; set; }
 

        public KvittoRad(string produktNamn, int validatedInputItem2, int produktPris, int produktId)
        {
            ProduktNamn = ProduktNamn;
            Antal = Antal;
            Pris = ProduktId;
            Summa = ProduktId * Antal;
            Total = Total;
            ProduktId = produktId;
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



















