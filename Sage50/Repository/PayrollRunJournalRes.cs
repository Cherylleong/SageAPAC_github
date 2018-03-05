﻿///////////////////////////////////////////////////////////////////////////////
//
// This file was automatically generated by RANOREX.
// DO NOT MODIFY THIS FILE! It is regenerated by the designer.
// All your modifications will be lost!
// http://www.ranorex.com
//
///////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Repository;
using Ranorex.Core.Testing;

namespace Sage50.Repository
{
#pragma warning disable 0436 //(CS0436) The type 'type' in 'assembly' conflicts with the imported type 'type2' in 'assembly'. Using the type defined in 'assembly'.
    /// <summary>
    /// The class representing the PayrollRunJournalRes element repository.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCode("Ranorex", "8.0")]
    [RepositoryFolder("7e8e081c-1b2f-4cb1-b3d2-8a0726587aa1")]
    public partial class PayrollRunJournalRes : RepoGenBaseFolder
    {
        static PayrollRunJournalRes instance = new PayrollRunJournalRes();
        PayrollRunJournalResFolders.PayrollRunJournalAppFolder _payrollrunjournal;

        /// <summary>
        /// Gets the singleton class instance representing the PayrollRunJournalRes element repository.
        /// </summary>
        [RepositoryFolder("7e8e081c-1b2f-4cb1-b3d2-8a0726587aa1")]
        public static PayrollRunJournalRes Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// Repository class constructor.
        /// </summary>
        public PayrollRunJournalRes() 
            : base("PayrollRunJournalRes", "/", null, 0, false, "7e8e081c-1b2f-4cb1-b3d2-8a0726587aa1", ".\\RepositoryImages\\PayrollRunJournalRes7e8e081c.rximgres")
        {
            _payrollrunjournal = new PayrollRunJournalResFolders.PayrollRunJournalAppFolder(this);
        }

#region Variables

#endregion

        /// <summary>
        /// The Self item info.
        /// </summary>
        [RepositoryItemInfo("7e8e081c-1b2f-4cb1-b3d2-8a0726587aa1")]
        public virtual RepoItemInfo SelfInfo
        {
            get
            {
                return _selfInfo;
            }
        }

        /// <summary>
        /// The PayrollRunJournal folder.
        /// </summary>
        [RepositoryFolder("98c4fdf7-7947-457e-99c5-564e927b8d6b")]
        public virtual PayrollRunJournalResFolders.PayrollRunJournalAppFolder PayrollRunJournal
        {
            get { return _payrollrunjournal; }
        }
    }

    /// <summary>
    /// Inner folder classes.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCode("Ranorex", "8.0")]
    public partial class PayrollRunJournalResFolders
    {
        /// <summary>
        /// The PayrollRunJournalAppFolder folder.
        /// </summary>
        [RepositoryFolder("98c4fdf7-7947-457e-99c5-564e927b8d6b")]
        public partial class PayrollRunJournalAppFolder : RepoGenBaseFolder
        {
            RepoItemInfo _paidfromInfo;
            RepoItemInfo _payperiodfrequencyInfo;
            RepoItemInfo _chequenumberInfo;
            RepoItemInfo _directdepositnumberInfo;
            RepoItemInfo _chequedateInfo;
            RepoItemInfo _periodenddateInfo;
            RepoItemInfo _postInfo;
            RepoItemInfo _chequetableInfo;

            /// <summary>
            /// Creates a new PayrollRunJournal  folder.
            /// </summary>
            public PayrollRunJournalAppFolder(RepoGenBaseFolder parentFolder) :
                    base("PayrollRunJournal", "/form[@controlname='PayrollCheckRunFormCdn']", parentFolder, 30000, null, true, "98c4fdf7-7947-457e-99c5-564e927b8d6b", "")
            {
                _paidfromInfo = new RepoItemInfo(this, "PaidFrom", "combobox[@controlname='m_paidFromAccount']", 30000, null, "c7536b30-be5e-45e6-94ef-56ad6c56a918");
                _payperiodfrequencyInfo = new RepoItemInfo(this, "PayPeriodFrequency", "combobox[@controlname='m_payPeriodComboBox']", 30000, null, "ed8fff1c-399f-4604-881c-7931b9d99db6");
                _chequenumberInfo = new RepoItemInfo(this, "ChequeNumber", "text[@controlname='m_startChqNumTextBox']/text[@accessiblerole='Text']", 30000, null, "296358d3-2934-4b64-bc81-0a3d7c604058");
                _directdepositnumberInfo = new RepoItemInfo(this, "DirectDepositNumber", "?/?/text[@accessiblename~'^Starting\\ Direct\\ Deposit\\ N']", 30000, null, "6f882549-27cc-4ef9-9845-d97c48706a53");
                _chequedateInfo = new RepoItemInfo(this, "ChequeDate", "container[@controlname='m_datePicker']/?/?/text[@accessiblerole='Text']", 30000, null, "caacde39-b6c4-4020-834d-e9c7783bdb21");
                _periodenddateInfo = new RepoItemInfo(this, "PeriodEndDate", "container[@controlname='m_periodEndDatePickerText']/?/?/text[@accessiblerole='Text']", 30000, null, "334abc32-75dc-4d5e-a146-e6f693cfa11c");
                _postInfo = new RepoItemInfo(this, "Post", "button[@controlname='m_postButton']", 30000, null, "f1cb2887-90ad-4a8f-a07b-a4f544edb7d9");
                _chequetableInfo = new RepoItemInfo(this, "ChequeTable", "table[@controlname='m_gridPayrollCheckRunEmp']", 30000, null, "979dbe0a-a69e-44d8-be22-4ccda11243b9");
            }

            /// <summary>
            /// The Self item.
            /// </summary>
            [RepositoryItem("98c4fdf7-7947-457e-99c5-564e927b8d6b")]
            public virtual Ranorex.Form Self
            {
                get
                {
                    return _selfInfo.CreateAdapter<Ranorex.Form>(true);
                }
            }

            /// <summary>
            /// The Self item info.
            /// </summary>
            [RepositoryItemInfo("98c4fdf7-7947-457e-99c5-564e927b8d6b")]
            public virtual RepoItemInfo SelfInfo
            {
                get
                {
                    return _selfInfo;
                }
            }

            /// <summary>
            /// The PaidFrom item.
            /// </summary>
            [RepositoryItem("c7536b30-be5e-45e6-94ef-56ad6c56a918")]
            public virtual Ranorex.ComboBox PaidFrom
            {
                get
                {
                    return _paidfromInfo.CreateAdapter<Ranorex.ComboBox>(true);
                }
            }

            /// <summary>
            /// The PaidFrom item info.
            /// </summary>
            [RepositoryItemInfo("c7536b30-be5e-45e6-94ef-56ad6c56a918")]
            public virtual RepoItemInfo PaidFromInfo
            {
                get
                {
                    return _paidfromInfo;
                }
            }

            /// <summary>
            /// The PayPeriodFrequency item.
            /// </summary>
            [RepositoryItem("ed8fff1c-399f-4604-881c-7931b9d99db6")]
            public virtual Ranorex.ComboBox PayPeriodFrequency
            {
                get
                {
                    return _payperiodfrequencyInfo.CreateAdapter<Ranorex.ComboBox>(true);
                }
            }

            /// <summary>
            /// The PayPeriodFrequency item info.
            /// </summary>
            [RepositoryItemInfo("ed8fff1c-399f-4604-881c-7931b9d99db6")]
            public virtual RepoItemInfo PayPeriodFrequencyInfo
            {
                get
                {
                    return _payperiodfrequencyInfo;
                }
            }

            /// <summary>
            /// The ChequeNumber item.
            /// </summary>
            [RepositoryItem("296358d3-2934-4b64-bc81-0a3d7c604058")]
            public virtual Ranorex.Text ChequeNumber
            {
                get
                {
                    return _chequenumberInfo.CreateAdapter<Ranorex.Text>(true);
                }
            }

            /// <summary>
            /// The ChequeNumber item info.
            /// </summary>
            [RepositoryItemInfo("296358d3-2934-4b64-bc81-0a3d7c604058")]
            public virtual RepoItemInfo ChequeNumberInfo
            {
                get
                {
                    return _chequenumberInfo;
                }
            }

            /// <summary>
            /// The DirectDepositNumber item.
            /// </summary>
            [RepositoryItem("6f882549-27cc-4ef9-9845-d97c48706a53")]
            public virtual Ranorex.Text DirectDepositNumber
            {
                get
                {
                    return _directdepositnumberInfo.CreateAdapter<Ranorex.Text>(true);
                }
            }

            /// <summary>
            /// The DirectDepositNumber item info.
            /// </summary>
            [RepositoryItemInfo("6f882549-27cc-4ef9-9845-d97c48706a53")]
            public virtual RepoItemInfo DirectDepositNumberInfo
            {
                get
                {
                    return _directdepositnumberInfo;
                }
            }

            /// <summary>
            /// The ChequeDate item.
            /// </summary>
            [RepositoryItem("caacde39-b6c4-4020-834d-e9c7783bdb21")]
            public virtual Ranorex.Text ChequeDate
            {
                get
                {
                    return _chequedateInfo.CreateAdapter<Ranorex.Text>(true);
                }
            }

            /// <summary>
            /// The ChequeDate item info.
            /// </summary>
            [RepositoryItemInfo("caacde39-b6c4-4020-834d-e9c7783bdb21")]
            public virtual RepoItemInfo ChequeDateInfo
            {
                get
                {
                    return _chequedateInfo;
                }
            }

            /// <summary>
            /// The PeriodEndDate item.
            /// </summary>
            [RepositoryItem("334abc32-75dc-4d5e-a146-e6f693cfa11c")]
            public virtual Ranorex.Text PeriodEndDate
            {
                get
                {
                    return _periodenddateInfo.CreateAdapter<Ranorex.Text>(true);
                }
            }

            /// <summary>
            /// The PeriodEndDate item info.
            /// </summary>
            [RepositoryItemInfo("334abc32-75dc-4d5e-a146-e6f693cfa11c")]
            public virtual RepoItemInfo PeriodEndDateInfo
            {
                get
                {
                    return _periodenddateInfo;
                }
            }

            /// <summary>
            /// The Post item.
            /// </summary>
            [RepositoryItem("f1cb2887-90ad-4a8f-a07b-a4f544edb7d9")]
            public virtual Ranorex.Button Post
            {
                get
                {
                    return _postInfo.CreateAdapter<Ranorex.Button>(true);
                }
            }

            /// <summary>
            /// The Post item info.
            /// </summary>
            [RepositoryItemInfo("f1cb2887-90ad-4a8f-a07b-a4f544edb7d9")]
            public virtual RepoItemInfo PostInfo
            {
                get
                {
                    return _postInfo;
                }
            }

            /// <summary>
            /// The ChequeTable item.
            /// </summary>
            [RepositoryItem("979dbe0a-a69e-44d8-be22-4ccda11243b9")]
            public virtual Ranorex.Table ChequeTable
            {
                get
                {
                    return _chequetableInfo.CreateAdapter<Ranorex.Table>(true);
                }
            }

            /// <summary>
            /// The ChequeTable item info.
            /// </summary>
            [RepositoryItemInfo("979dbe0a-a69e-44d8-be22-4ccda11243b9")]
            public virtual RepoItemInfo ChequeTableInfo
            {
                get
                {
                    return _chequetableInfo;
                }
            }
        }

    }
#pragma warning restore 0436
}