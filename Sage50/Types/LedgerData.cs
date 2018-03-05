/*
 * Created by Ranorex
 * User: wonda05
 * Date: 8/5/2016
 * Time: 10:40 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Sage50.Shared;

namespace Sage50.Types
{
	/// <summary>
	/// Description of LedgerData.
	/// </summary>
	public class LedgerData : BaseType
    {
        public LedgerData()
        {
        }

        public LedgerData(string name, String nameEdit, bool inactive)
        {
            this.name = name;
            this.nameEdit = nameEdit;
            this.inactiveCheckBox = inactive;
        }

		public string name { get; set; }
        public string nameEdit { get; set; }
        public Nullable<bool> inactiveCheckBox { get; set; }
        public string additional1 { get; set; }
        public string additional2 { get; set; }
        public string additional3 { get; set; }
        public string additional4 { get; set; }
        public string additional5 { get; set; }
        public Nullable<bool> addCheckBox1 { get; set; }
        public Nullable<bool> addCheckBox2 { get; set; }
        public Nullable<bool> addCheckBox3 { get; set; }
        public Nullable<bool> addCheckBox4 { get; set; }
        public Nullable<bool> addCheckBox5 { get; set; }

        public virtual void FillRandom()
        {
            this.name = StringFunctions.RandStr("A(6)");
        }
    }
}
