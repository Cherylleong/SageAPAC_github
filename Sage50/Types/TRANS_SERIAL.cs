/*
 * Created by Ranorex
 * User: wonga01
 * Date: 8/30/2016
 * Time: 15:06
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Sage50.Types
{
	/// <summary>
	/// Description of TRANS_SERIAL.
	/// </summary>
	public class TRANS_SERIAL
	{
		public TRANS_SERIAL()
		{
		}
		
		public ITEM Item = new ITEM();
		public List <String> SerialNumbersToUse { get; set; }
	}
}
