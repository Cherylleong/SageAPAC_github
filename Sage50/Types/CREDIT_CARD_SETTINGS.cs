/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/2/2016
 * Time: 16:16
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Sage50.Types
{
	/// <summary>
	/// Description of CREDIT_CARD_SETTINGS.
	/// </summary>
	public class CREDIT_CARD_SETTINGS
	{
		public CREDIT_CARD_SETTINGS()
		{
		}
		
		public List<CREDIT_CARD_USED> CardsUsed = new List<CREDIT_CARD_USED>();
		public List<CREDIT_CARD_ACCEPTED> CardsAccepted = new List<CREDIT_CARD_ACCEPTED>();
		public string MerchantID { get; set; }
		public string MerchantKey { get; set; }
	}
}
