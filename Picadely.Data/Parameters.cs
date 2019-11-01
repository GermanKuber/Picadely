using System;
using System.Collections.Generic;
using System.Data;

namespace Picadely.Services
{
    public class Parameters
    {
        private readonly List<Parameter> _parameters = new List<Parameter>();

        protected Parameters(List<Parameter> parameters)
        {
            _parameters = parameters;
        }
        public Parameters()
        {

        }
        public Parameters Add(string key, string value, SqlDbType type)
        {
            _parameters.Add(new Parameter(key, value, type));
            return new Parameters(_parameters); ;
        }
        public Parameters Add(string key, int value, SqlDbType type)
        {
            _parameters.Add(new Parameter(key, value.ToString(), type));
            return new Parameters(_parameters); ;
        }
        public Parameters Add(string key, DateTime value)
        {
            _parameters.Add(new Parameter(key, value.ToString(), SqlDbType.DateTime));
            return new Parameters(_parameters); ;
        }
        public Parameters Add(string key, Guid value)
        {
            _parameters.Add(new Parameter(key, value.ToString(), SqlDbType.UniqueIdentifier));
            return new Parameters(_parameters); ;
        }
        public Parameters Add(string key, DateTimeOffset value) =>
            Add(key, value.Date);

        public Parameters Add(string key, int value)
        {
            _parameters.Add(new Parameter(key, value.ToString(), SqlDbType.Int));
            return new Parameters(_parameters); ;
        }
        public Parameters Add(string key, bool value)
        {
            _parameters.Add(new Parameter(key, value.ToString(), SqlDbType.Bit));
            return new Parameters(_parameters); ;
        }
        public Parameters Add(string key, decimal value)
        {
            _parameters.Add(new Parameter(key, value.ToString(), SqlDbType.Decimal));
            return new Parameters(_parameters); ;
        }
        public Parameters Add(string key, string value)
        {
            _parameters.Add(new Parameter(key, value, SqlDbType.NVarChar));
            return new Parameters(_parameters); ;
        }
        public List<Parameter> Send() => _parameters;
    }
}
