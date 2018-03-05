/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/6/2016
 * Time: 13:51
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Sage50.Types
{
	/// <summary>
	/// Description of PRICE_LIST.
	/// </summary>
	public class PRICE_LIST
	{
		public PRICE_LIST()
		{
		}
		
		public PRICE_LIST(string description, Nullable<bool> ActiveStatus)
        {
            this.description = description;
            this.ActiveStatus = ActiveStatus;
        
        }

		public string description { get; set; }
		public Nullable<bool> ActiveStatus { get; set; }
	}
}
