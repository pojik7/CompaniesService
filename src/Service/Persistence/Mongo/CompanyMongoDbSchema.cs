using MongoDB.Bson.Serialization.Attributes;
using PipServices3.Commons.Data;
using System;

namespace Companies.Persistence.Mongo
{
    [BsonIgnoreExtraElements]
    [BsonNoId]
    public class CompanyMongoDbSchema : IStringIdentifiable
    {
        [BsonElement("id")]
        public string Id { get; set; }

        [BsonElement("bank_code")]
        public string BankCode { get; set; }

        [BsonElement("acc_code")]
        public string AccCode { get; set; }

        [BsonElement("state_code")]
        public string StateCode { get; set; }

        [BsonElement("iban")]
        public string IBAN { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("contract_date")]
        public DateTime ContractDate { get; set; }

        [BsonElement("contract_no")]
        public string ContractNo { get; set; }

        [BsonElement("employee_id")]
        public int EmployeeId { get; set; }
    }
}
