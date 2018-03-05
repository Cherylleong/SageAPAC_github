/*
 * Created by Ranorex
 * User: wonga01
 * Date: 8/30/2016
 * Time: 14:01
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Sage50.Types
{
	/// <summary>
	/// Description of ITEM_BUILD_ITEMS.
	/// </summary>
	public class ITEM_BUILD_ITEMS
	{
		public ITEM_BUILD_ITEMS()
		{
		}

		public ITEM item = new ITEM();
		public string quantity { get; set; }
	}
}
