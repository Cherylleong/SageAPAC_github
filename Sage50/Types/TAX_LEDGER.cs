/*
 * Created by Ranorex
 * User: wonda05
 * Date: 8/5/2016
 * Time: 12:10 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Sage50.Types
{
	/// <summary>
	/// Description of TAX_LEDGER.
	/// </summary>
	public class TAX_LEDGER
	{
        public TAX_LEDGER()
        {}

		public TAX tax = new TAX();
		public string taxExempt { get; set; }
		public string taxID { get; set; }
	}
}
