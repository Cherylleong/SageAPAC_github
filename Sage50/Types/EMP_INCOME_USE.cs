/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/6/2016
 * Time: 13:26
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Sage50.Types
{
	/// <summary>
	/// Description of EMP_INCOME_USE.
	/// </summary>
	public class EMP_INCOME_USE
	{
		public EMP_INCOME_USE()
		{
		}
		
		public Nullable<bool> Use { get; set; }
		public string income { get; set; }
		public string unit { get; set; }
		public string amountPerUnit { get; set; }
		public string hoursPerPeriod { get; set; }
		public string piecesPerPeriod { get; set; }
		public string historicalAmount { get; set; }
		public int iDataFileLine { get; set; }//for use only with old datafiles
	}
}
