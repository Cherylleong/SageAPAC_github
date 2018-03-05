/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/6/2016
 * Time: 10:50
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Sage50.Types
{
	/// <summary>
	/// Description of TRACK_SHIPMENTS.
	/// </summary>
	public class TRACK_SHIPMENTS
	{
		public TRACK_SHIPMENTS()
		{
		}
		
		public List<SHIP_SERVICES> ShipServices = new List<SHIP_SERVICES>();
		public Nullable<bool> TrackShipments { get; set; }
	}
}
