using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.IO;

namespace Kassasystem
{
    class Program
    {
        public static void writeProdukts()
        {
            var outFile = "../../../produkts.txt";
            Produkt produkt = new Produkt("Kexchoklad", 10, 100 );
            Produkt produkt1 = new Produkt("Mjölk", 2, 200);
            Produkt produkt2 = new Produkt("Banan", 6, 300);
            Produkt produkt3 = new Produkt("Gurka", 7, 400);
            
            StreamWriter sw = new StreamWriter(outFile);
            {
                sw.WriteLine(produkt.getProduktLine());
                sw.WriteLine(produkt1.getProduktLine());
                sw.WriteLine(produkt2.getProduktLine());
                sw.WriteLine(produkt3.getProduktLine());
            }
        }

        public static List<Produkt> readProdukts()
        {
            var inFile = "../../../produkt.txt";
            var produkts = new List<Produkt>();

            using (StreamReader sr = File.OpenText(inFile))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var produkt = new Produkt(line);
                    produkts.Add(produkt);
                }
            }
            return produkts;
        }

        public static int searchProdukts(int produktid, List<Produkt> produkts)
        {
            int position = -1;

            for (int i = 0; i < produkts.Count; i++)
            {
                if (produktid == produkts[i].Id)
                {
                    position = i;
                }
            }
            return position;
        }

        public static string checkInputType(string input)
        {
            string inputType = "";
            var inputList = input.Split(',');
            if ((inputList.Count() == 1) && (input.ToUpper() == ("BETALA")))
            {
                inputType = "BETALA";
            }
            else if ((inputList.Count() == 2) && (inputList[0].ToUpper() == "BACKA"))
            {
                inputType = "BACKA";
            }
            else if (inputList.Count() == 2)
            {
                inputType = "FORTSÄTT";
            }
            else
            {
                inputType = "FEL";
            }
            return inputType;
        }

        public static int checkReturnInput(string input)
        {
            int produktID;
            var inputList = input.Split(',');
            bool result = int.TryParse(inputList[1], out produktID);
            if (result)
            {
                produktID = -1;
            }
            return produktID;
        }

        public static Tuple<int, int> checkContinueInput(string input)
        {
            var inputList = input.Split(',');
            int produktID;
            int antal;

            bool resultProduktID = int.TryParse(inputList[0], out produktID);
            bool resultTotal = int.TryParse(inputList[1], out antal);

            if ((resultProduktID == true) && (resultTotal == true))
            {
                return Tuple.Create(produktID, antal);
            }
            else
            {
                return Tuple.Create(-1, -1);
            }
        }

        public static void run(List<Produkt> produkts)
        {
            var kvitto = new kvitto();
            while (true)
            {
                int produktID = -1;
                Produkt produkt;

                Console.WriteLine("\n");

                Console.WriteLine("BETALA");
                Console.WriteLine("BACKA <PRODUKTID>");
                Console.WriteLine("<Produktid> <Antal>");

                string input = Console.ReadLine();

                var inputType = checkInputType(input);

                if (inputType == "FEL")
                {
                    Console.WriteLine("Fel kommando");
                }
                else if (inputType == "BETALA")
                {
                    if (kvitto.KvittoRad.Count() !=0)
                    {
                        kvitto.save();
                    }
                    else
                    {
                        Console.WriteLine("Inget kvitto har sparats. Välkommen åter");
                    }

                    break;
                }
                else if (inputType == "BACKA")
                {
                    if (kvitto.KvittoRad.Count() != 0)
                    {
                        produktID = checkReturnInput(input);

                        if (produktID == -1)
                        {
                            Console.WriteLine("Fel kommando");
                        }
                        else
                        {
                            produktID = checkReturnInput(input);
                            produkt = produkts.Find(p => p.Id == produktID);
                            if (produkt != null)
                            {
                                kvitto.decreaseAntal(produktID);
                                kvitto.print();
                            }
                            else
                            {
                                Console.WriteLine("Produkten finns ej!");
                            }
                        }
                    }
                }
                else
                {
                    var validatedInput = checkContinueInput(input);
                    if (validatedInput.Item1 == -1)
                    {
                        Console.WriteLine("Fel kommando");
                    }
                    else
                    {
                        produkt = produkts.Find(p => p.Id == validatedInput.Item1);
                        if (produkt != null)
                        {
                            var KvittoRad = new KvittoRad(produkt.Namn, validatedInput.Item2, produkt.Pris, produktID, total:, antal:);
                            kvitto.addKvittoRad(KvittoRad);
                            kvitto.print();
                        }
                        else
                        {
                            Console.WriteLine("Produkten finns ej!");
                        }
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            var produkts = readProdukts();
            while (true)
            {
                Console.WriteLine("KASSA");
                Console.WriteLine("1. Ny kund");
                Console.WriteLine("2. Admin");
                Console.WriteLine("0. Avsluta");
                var input = Console.ReadLine();
                if (input == "1")
                {
                    run(produkts);
                }
                else if(input == "2")
                {
                    //Ändra produkter
                }
                else if (input == "0")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Fel inmatning, försök igen!");
                }
            }
        }

    }
}
