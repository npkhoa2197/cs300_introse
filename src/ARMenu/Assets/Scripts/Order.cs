using System;

namespace AssemblyCSharp
{
	public class Order
	{
		public string additionalRequirements;
		public bool finished;
		public string meal;
		public bool paid;
		public double price;
		public long quantity;
		public long tableNumber;

		public Order ()
		{
			meal = additionalRequirements = "";
			price = 0;
			tableNumber = 0;
			quantity = 0;
			paid = finished = false;
		}

		public Order (string additionalRequirements , bool finished, string meal, bool paid, double price, 
			long quantity, long tableNumber) {

			this.meal = meal;
			this.price = price;
			this.quantity = quantity;
			this.tableNumber = tableNumber;
			this.paid = paid;
			this.finished = finished;
			this.additionalRequirements = additionalRequirements;
		}
	}
}

