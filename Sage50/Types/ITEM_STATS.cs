/*
 * Created by Ranorex
 * User: wonga01
 * Date: 8/30/2016
 * Time: 13:55
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Sage50.Types
{
	/// <summary>
	/// Description of ITEM_STATS.
	/// </summary>
	public class ITEM_STATS
	{
		public ITEM_STATS()
		{
		}
		
		public string forLocation { get; set; }
		public string ytdNoOfTransactions { get; set; }
		public string ytdUnitsSold { get; set; }
		public string ytdAmountSold { get; set; }
		public string ytdCostOfGoodsSold { get; set; }
		public string lastYearNoOfTransactions { get; set; }
		public string lastYearUnitsSold { get; set; }
		public string lastYearAmountSold { get; set; }
		public string lastYearCostOfGoodsSold { get; set; }
		public string dateOfLastSale { get; set; }
	}
}
