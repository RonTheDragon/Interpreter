using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Interpreter
{

    class Program
    {
        static void Main(string[] args)
        {
           // var domainLanguage = "order x10 '2L water bottles' from Tesco";
		   //var domainLanguage = "/give RonTheDragon 64 minecraft:Dirt";
		   //Console.WriteLine(Order.Parse(domainLanguage));
			Console.WriteLine("\n\tI created Minecraft Command! type:\n\t/give [player] [amount] minecraft:[item]\n");
			while (true)
			{
				Order order = Order.Parse(Console.ReadLine());
				if (order != null)
				{
					Console.WriteLine(order);
				}
				else
				{
					Console.WriteLine("Wrong command, please try again!");
				}
			}
			
		}
    }
	public class Order
	{
		/*
		const string optionalSpace = " ?";
		const string qty = "x(?<qty>\\d+)" + optionalSpace;
		const string product = "'(?<product>[\\w ]+)'" + optionalSpace;
		const string source = "from (?<source>\\w+)";
		const string orderCommand = "order" + optionalSpace + qty + product + source;
		*/
		const string optionalSpace = " ?";
		const string player = " (?<player>\\w+)" + optionalSpace;
		const string qty = "(?<qty>\\d+)" + optionalSpace;
		const string product = "minecraft:(?<product>[\\w ]+)";	
		const string orderCommand = "/give" + optionalSpace + player + qty + product;


		static Regex _regex = new Regex(orderCommand);

		Order(int qty, string product, string player)
		{
			Qty = qty;
			Product = product;
			Player = player;
		}

		public int Qty { get; }
		public string Product { get; }
		public string Player { get; }

		// Interpreter
		public static Order Parse(string command)
		{
			var match = _regex.Match(command);
			if (!match.Success)
			{
				return null;
			}

			var qty = int.Parse(match.Groups["qty"].Value);
			var product = match.Groups["product"].Value;
			var player = match.Groups["player"].Value;

			return new Order(qty, product, player);
		}
        public override string ToString()
        {
			return $"Given [{Product}] * {Qty} to {Player}";
        }
    }
}
