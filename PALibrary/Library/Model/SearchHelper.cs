using System.Collections.Generic;
using System.Data;

namespace PALibrary.Library.Model
{
    public class SearchHelper
    {
        private List<string> conditions;
        private List<IDbDataParameter> parameters;
        private decimal amount;

        public List<string> Conditions
        {
            get { return conditions; }
            set { conditions = value; }
        }

        public List<IDbDataParameter> Parameters
        {
            get { return parameters; }
            set { parameters = value; }
        }

        public decimal Amount
        {
            get { return amount; }
            set { amount = value; }
        }
    }
}
