using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Newtonsoft.Json;
namespace Common
{
    public class Databases
    {
        public Databases()
        {
            if (string.IsNullOrEmpty(ConnectionString))
            {
                _ConnectionString = GetConnectionString();
                if (_ConnectionString != "")
                    DataCache.SetCache("ConnectionString", _ConnectionString);
            }
        }
        private string GetConnectionString()
        {
            string Str = string.Empty;
            
            return Str;
        }

        private string _ConnectionString;
        public string ConnectionString
        {
            set
            {
                _ConnectionString = value;
            }
            get
            {
                if (DataCache.GetCache("ConnectionString") != null)
                {
                    _ConnectionString = DataCache.GetCache("ConnectionString").ToString();
                }
                else
                {
                    _ConnectionString = GetConnectionString();
                }
                return _ConnectionString;
            }
        }       
    }
}
