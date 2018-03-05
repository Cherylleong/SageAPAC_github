/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/6/2016
 * Time: 13:23
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Sage50.Types
{
	/// <summary>
	/// Description of EMP_TAX_HIST.
	/// </summary>
	public class EMP_TAX_HIST
	{
		public EMP_TAX_HIST()
		{
		}
		
		public string Tax { get; set; }
		public string Amount { get; set; }
		public int iDataFileLine { get; set; }//for use only with old datafiles
	}
}
