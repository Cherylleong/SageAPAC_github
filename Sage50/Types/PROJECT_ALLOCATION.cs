/*
 * Created by Ranorex
 * User: wonga01
 * Date: 8/30/2016
 * Time: 14:56
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Sage50.Types
{
	/// <summary>
	/// Description of PROJECT_ALLOCATION.
	/// </summary>
	public class PROJECT_ALLOCATION
	{
		public PROJECT_ALLOCATION()
		{
		}
		
		public PROJECT Project = new PROJECT();

        public string Percent { get; set;}

        public string Amount { get; set; }
	}
}
