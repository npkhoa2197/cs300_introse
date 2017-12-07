using System;

namespace AssemblyCSharp
{
	public class orderItem
	{
		public string additionalRequirements;
		public string key;
		public bool finished;
		public string meal;
		public bool paid;
		public double price;
		public long quantity;
		public long tableNumber;

		public orderItem ()
		{
			meal = additionalRequirements = "";
			price = 0;
			tableNumber = 0;
			quantity = 0;
			paid = finished = false;
		}

		public orderItem (string key, string meal, string additionalRequirements, long quantity, long tableNumber, double price, bool finished, bool paid) {
            this.key = key;
            this.meal = meal;
            this.additionalRequirements = additionalRequirements;
            this.quantity = quantity;
            this.tableNumber = tableNumber;
            this.price = price;
            this.finished = finished;
            this.paid = paid;
        }
	}
}
