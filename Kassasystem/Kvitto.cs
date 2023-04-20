using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.CompilerServices;

namespace Kassasystem
   
{
    public class kvitto
    {
        private double Total { get; set; }
        private DateTime kvittoDateTime { get; set; }
        public List<KvittoRad> KvittoRad { get; set; }
        private double Rabbat { get; set; }
        private double TotalAntal { get; set; }

        public kvitto()
        {
            kvittoDateTime = DateTime.Now;
            Total = 0;
            KvittoRad = new List<KvittoRad>();
            Rabbat = 0;
            TotalAntal = 0;
        }

        public void addKvittoRad(KvittoRad kvittoRad)
        {
            int index = kvittoRadExists(kvittoRad.ProduktId);
            if (index == -1)
            {
                KvittoRad.Add(kvittoRad);
            }
            else
            {
                KvittoRad[index].Antal += kvittoRad.Antal;
                KvittoRad[index].updateSumma();
            }

            TotalAntal = KvittoRad.Sum(Produkt => Produkt.Summa);
        }

        public void decreaseAntal(int ProduktId)
        {
            int index = kvittoRadExists(ProduktId);
            if (index != -1)
            {
                KvittoRad[index].Antal -= 1;
                KvittoRad[index].updateSumma();
                TotalAntal = KvittoRad.Sum(Produkt =>  Produkt.Summa);
            }
        }

        public void print()
        {
            printDate();
            printKvittoRad();
            printRabbat();
            printTotal();
        }

        public void save()
        {
            string timestamp = DateTime.Today.ToString("MM/dd/yyyy");
            var outFile = $"../../../kvitto{timestamp}.txt";
            using (StreamWriter sw = new StreamWriter(outFile, true))
            {
                sw.WriteLine("*****");

                sw.WriteLine(kvittoDateTime.ToString());
                sw.WriteLine(Total);
                sw.WriteLine(TotalAntal);
                sw.WriteLine(Rabbat);
                foreach (var KvittoRad in KvittoRad)
                {
                    string kvittoRadLineline = $"{KvittoRad.ProduktId}, {KvittoRad.ProduktNamn}, {KvittoRad.Antal}, {KvittoRad.Summa}";
                    sw.WriteLine( kvittoRadLineline );
                }
                sw.WriteLine("*****");
            }
        }

        public void printKvittoRad()
        {
            foreach (var KvittoRad in KvittoRad)
            {
                KvittoRad.printRow();
            }
        }

        private int kvittoRadExists(int produktID)
        {
            return KvittoRad.FindIndex(Produkt => Produkt.ProduktId == produktID);
        }

        private void checkRabbat()
        {
            if (TotalAntal >= 1001 && TotalAntal <= 2000)
            {
                Rabbat = Math.Round((TotalAntal * 0.02), 2);
                Total = TotalAntal - Rabbat;
            }
            else if (TotalAntal > 2000)
            {
                Rabbat = Math.Round((TotalAntal * 0.03), 2);
                Total = TotalAntal - Rabbat;
            }
        }

        private void printRabbat()
        {
            checkRabbat();
            if (Rabbat > 0)
            {
                Console.WriteLine($"TotalAntal: {TotalAntal}kr");
                Console.WriteLine($"Rabbat: -{Rabbat}kr");
            }
            else
            {
                Total = TotalAntal;
            }
        }

        private void printDate()
        {
            Console.WriteLine($"KVITTO {kvittoDateTime}");
        }


        private void printTotal()
        {
            Console.WriteLine($"Total: {Total}kr ");
        }
        //public IEnumerable KvittoRad { get; set; }
    }
}
