/*
 * Created by Ranorex
 * User: wonda05
 * Date: 8/5/2016
 * Time: 11:07 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Sage50.Types
{
	/// <summary>
	/// Description of Import.
	/// </summary>
	public class IMPORT
	{
		public IMPORT ()
		{}

		public GL_ACCOUNT myAccount = new GL_ACCOUNT();
		public string itemNumber { get; set; }
		public string myItemNumber { get; set; }
	}
}
