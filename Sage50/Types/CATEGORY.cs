/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/6/2016
 * Time: 14:00
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Sage50.Types
{
	/// <summary>
	/// Description of CATEGORY.
	/// </summary>
	public class CATEGORY
	{
		public CATEGORY()
		{
		}
		
		public List<ITEM> items = new List<ITEM>();
		public string name { get; set; }
	}
}
