/*
 * Created by Ranorex
 * User: wonda05
 * Date: 8/5/2016
 * Time: 11:10 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Sage50.Types
{
	/// <summary>
	/// Description of Person.
	/// </summary>
	public class Person : LedgerData
    {
        public Person()
        {
        }

        public Person(string name, String nameEdit, bool inactive)
        : base(name, nameEdit, inactive)
        {
        }

        public ADDRESS Address = new ADDRESS();
        public string memo { get; set; }
        public string toDoDate { get; set; }
        public Nullable<bool> displayCheckBox { get; set; }
        public string department { get; set; }

        public override void FillRandom()
        {
            base.FillRandom();
            this.Address.FillRandom();
        }
    }
}
