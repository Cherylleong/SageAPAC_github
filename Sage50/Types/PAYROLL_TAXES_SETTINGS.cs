/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/6/2016
 * Time: 11:32
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Sage50.Types
{
	/// <summary>
	/// Description of PAYROLL_TAXES_SETTINGS.
	/// </summary>
	public class PAYROLL_TAXES_SETTINGS
	{
		public PAYROLL_TAXES_SETTINGS()
		{
		}
		
		public string eiFactor { get; set; }					// Taxes page CDN
		public string wcbRate { get; set; }					// Taxes page CDN
		public string ehtFactor { get; set; }					// Taxes page CDN
		public string qhsfFactor { get; set; }				// Taxes page CDN
		public string sdiRate { get; set; }		//US
		public string sutaRate { get; set; }  	//US
		public string futaRate { get; set; }  	//US
	}
}
