/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/6/2016
 * Time: 13:29
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Sage50.Types
{
	/// <summary>
	/// Description of EMP_ENTITLE.
	/// </summary>
	public class EMP_ENTITLE
	{
		public EMP_ENTITLE()
		{
		}
		
		public EMP_ENTITLE_DATA[] EntitlementRows = new EMP_ENTITLE_DATA[5] { new EMP_ENTITLE_DATA(), new EMP_ENTITLE_DATA(), new EMP_ENTITLE_DATA(), new EMP_ENTITLE_DATA(), new EMP_ENTITLE_DATA()};
		public string hrsInWorkDay { get; set; }
	}
}
