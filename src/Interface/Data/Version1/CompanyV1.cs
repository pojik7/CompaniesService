using System;
using System.Runtime.Serialization;
using PipServices3.Commons.Data;

namespace Companies.Data.Version1
{
    [DataContract]
    public class CompanyV1 : IStringIdentifiable
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "bank_code")]
        public string BankCode { get; set; }

        [DataMember(Name = "acc_code")]
        public string AccCode { get; set; }

        [DataMember(Name = "state_code")]
        public string StateCode { get; set; }

        [DataMember(Name = "iban")]
        public string IBAN { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "contract_date")]
        public DateTime ContractDate { get; set; }

        [DataMember(Name = "contract_no")]
        public string ContractNo { get; set; }

        [DataMember(Name = "employee_id")]
        public int EmployeeId { get; set; }
    }
}
