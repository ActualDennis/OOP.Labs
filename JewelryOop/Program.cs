using System;
using System.Collections.Generic;

namespace JewelryOop {
    class Program {
        static void Main(string[] args)
        {
            var myList = new List<Jewelry>()
            {
                new Jewelry("Good golden ring", new List<Material>
                {
                    new PremiumMaterial("Gold", 60,  15),
                    new Material("Copper", 4, 1),
                    new PremiumGemstone("Sapphire", 300, 2, Color.Blue, 1),
                    new PremiumGemstone("Diamond", 1000, 0.3, Color.Opaque, 1.3)
                }),
                new Bijouterie("Good-looking bijouterie", new List<Material>
                {
                    new Material("Steel", 2,  15),
                    new Gemstone("Blue Glass gemstone", 10, 5, Color.Blue)
                }, 2.5)

            };

            foreach(var item in myList)
            {
                Console.WriteLine();
                Console.WriteLine($"{item.Name} : ");
                Console.WriteLine("Materials information: ");

                Console.WriteLine($"Real price is : {item.GetRealPrice()} Look-like price : {item.GetPrice()}");

                foreach(var material in item.Materials)
                {
                    Console.WriteLine(material.GetDescription());
                }
            }

            Console.ReadLine();
        }
    }
}
