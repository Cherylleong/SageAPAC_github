/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/2/2016
 * Time: 15:46
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Sage50.Types
{
	/// <summary>
	/// Description of COMPANY_SETTINGS.
	/// </summary>
	public class COMPANY_SETTINGS
	{
		public COMPANY_SETTINGS()
		{
		}
		
		public SYSTEM SystemSettings = new SYSTEM();
		public BACKUP BackupSettings = new BACKUP();
		public FEATURES FeatureSettings = new FEATURES();
		public CREDIT_CARD_SETTINGS CreditCardSettings = new CREDIT_CARD_SETTINGS();
		public List<TAX> TaxSettings = new List<TAX>();
		public List<TAX_CODE> TaxCodes = new List<TAX_CODE>();
		public CURRENCY CurrencySettings = new CURRENCY();
		public FORMS FormSettings = new FORMS();
		public EMAIL EmailSettings = new EMAIL();
		public DATE_FORMAT DateSettings = new DATE_FORMAT();
		public TRACK_SHIPMENTS ShipperSettings = new TRACK_SHIPMENTS();
		public string LogoLocation { get; set; }
		public string additionalInformationDate { get; set; }
		public string additionalInformationField { get; set; }
	}
}
