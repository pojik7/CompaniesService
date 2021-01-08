using PipServices3.Commons.Convert;
using PipServices3.Commons.Validate;

namespace Companies.Data.Version1
{
    public class CompanyV1Schema: ObjectSchema
    {
        public CompanyV1Schema()
        {
            this.WithRequiredProperty("bank_code", TypeCode.String);
            this.WithRequiredProperty("acc_code", TypeCode.String);
            this.WithRequiredProperty("state_code", TypeCode.String);
            this.WithRequiredProperty("iban", TypeCode.String);
            this.WithRequiredProperty("name", TypeCode.String);
            this.WithRequiredProperty("contract_date", TypeCode.DateTime);
            this.WithRequiredProperty("contract_no", TypeCode.String);
            this.WithRequiredProperty("employee_id", TypeCode.Integer);
        }

    }
}
