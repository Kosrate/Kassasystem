using System;
using System.Collections.Generic;

namespace Kassasystem
{
    public class Produkt
    {
        public string Namn { get; private set; }
        public int Pris { get; private set; }
        public int Id { get; private set; }

        public Produkt(string namn, int pris, int id)
        {
            Namn = namn;
            Pris = pris;
            Id = id;
        }

        public Produkt(string line)
        {
            string[] words = line.Split(',');
            Id = Convert.ToInt32(words[0]);
            Namn = words[1];
            Pris = Convert.ToInt32(words[2]);
        }

        public string getProduktLine()
        {
            return $"{Id}, {Namn}, {Pris}";
        }
    }
}