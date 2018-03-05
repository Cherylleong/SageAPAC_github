/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/6/2016
 * Time: 13:34
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Sage50.Types
{
	/// <summary>
	/// Description of EMP_DIRECT_DEP_DATA.
	/// </summary>
	public class EMP_DIRECT_DEP_DATA
	{
		public EMP_DIRECT_DEP_DATA()
		{
		}
		
		public string bankNumber { get; set; }
		public string transitNumber { get; set; }
		public string accountNumber { get; set; }
		public string amount { get; set; }
		public string percentage { get; set; }
		public Nullable<bool> ActiveStatus { get; set; }
	}
}
