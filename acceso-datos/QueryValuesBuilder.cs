﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace acceso_datos
{
    public class QueryValuesBuilder
    {

        private List<string> values;

        public QueryValuesBuilder() {
            this.values = new List<string>();
        }

        public QueryValuesBuilder setIntValue(object value)
        {
            values.Add(value.ToString());

            return this;
        }

        public QueryValuesBuilder setStringValue(object value) { 
            values.Add($"'{value}'");

            return this;
        }

        public QueryValuesBuilder setDateValue(DateTime dateValue)
        {
            return this.setStringValue(dateValue.ToString("yyyy-MM-dd"));
        }

        public List<string> build() { return values; }

    }
}
