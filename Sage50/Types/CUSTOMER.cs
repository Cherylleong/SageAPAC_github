/*
 * Created by Ranorex
 * User: wonda05
 * Date: 8/5/2016
 * Time: 10:48 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Sage50.Types
{
	/// <summary>
	/// Description of Customer.
	/// </summary>
	public class CUSTOMER : APARPerson
	{
		public CUSTOMER ()
		{}

        public CUSTOMER(string name, string nameEdit, bool inactive)
            : base(name, nameEdit, inactive)
        {

        }

		public ADDRESS ShipToAddress = new ADDRESS();
		public GL_ACCOUNT revenueAccount = new GL_ACCOUNT();
		
		public HISTORICAL_INVOICE historicalInvoice = new HISTORICAL_INVOICE();
		public HISTORICAL_PAYMENT historicalPayment = new HISTORICAL_PAYMENT();
		
		public Nullable<bool> internalCheckBox { get; set; }
		public string dateOfLastSale { get; set; }
		public string customerSince { get; set; }
		public string salesPerson { get; set; }			// new for 2008
		public Nullable<bool> defaultShipToAddressCheckbox { get; set; }
		public Nullable<bool> produceStatementsForThisCustCheckbox { get; set; }
		public string formsForThisCustomer { get; set; }
		public string priceList { get; set; }
		public string usuallyShipItemFrom { get; set; }
		public string standardDiscount { get; set; }
		public string ytdSales { get; set; }
		public string lySales { get; set; }
		public string creditLimit { get; set; }
		public string foreignYtdSales { get; set; }
		public string foreignLySales { get; set; }
		public string foreignCreditLimit { get; set; }
        public Nullable<bool> custHasPADCheckbox { get; set; }
	}
}
