/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/6/2016
 * Time: 13:58
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Sage50.Types
{
	/// <summary>
	/// Description of LOCATION.
	/// </summary>
	public class LOCATION
	{
		public LOCATION()
		{
		}
				
		public string code { get; set; }
		public string description { get; set; }
		public Nullable<bool> ActiveStatus { get; set; }
	}
}
