using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ScrimpNet.Serialization.TNet
{
    public class TNetString
    {
        #region Members
        private int m_size = 0;
        private object m_data = null;
        private char? m_type = null;      //, # } ] ! ~ ^
        private string m_toString = null;
        #endregion
        #region Properties

        /// <summary>
        /// Size of just the data portion of the TNet string
        /// </summary>
        public int Length
        {
            get
            {
                return this.m_size;
            }
            internal set
            {
                this.m_size = value;
            }
        }

        /// <summary>
        /// Size of TNet string include length, colon ':', and ending delimiter        
        /// </summary>
        public int FullSize
        {
            get;
            private set;
        }

        public object Data
        {
            get
            {
                return this.m_data;
            }
            private set
            {
                m_data = value;
            }
        }
        public char Type
        {
            get
            {
                return this.m_type.Value;
            }
        }

        #endregion
        public TNetString()
        {

        }
        public static TNetString Parse(string input)
        {

            TNetString retval = null;


            //get length
            string[] parts = input.Split(':');
            int size = System.Convert.ToInt32(parts[0]);
            if (size == 0)
            {
                var ts = new TNetString();
                ts.m_type = parts[1][0];
                switch (ts.m_type.Value)
                {
                    case TNetDelimiter.String:
                        ts.Data = string.Empty;
                        break;
                    case TNetDelimiter.List:
                        ts.Data = new List<object>();
                        break;
                    case TNetDelimiter.Dictionary:
                        ts.Data = new Dictionary<string, object>();
                        break;
                }
                ts.ToString(); //the side effect of this call is that is sets Length, and FullSize properties.  Possible performance improvement location, if use-case is proven
                return ts;
            }

            char type = input.Substring(size + parts[0].Length + 1, 1)[0];

            //get body
            string body = input.Substring(parts[0].Length + 1, size);
            int fullSizeOfTNetString = parts[0].Length + 1 + 1+body.Length;

            switch (type)
            {
                case TNetDelimiter.String:
                    retval = new TNetString(body);
                    break;
                case TNetDelimiter.Integer:
                    retval = new TNetString(System.Convert.ToInt32(body));
                    break;
                case TNetDelimiter.Float:
                    retval = new TNetString(System.Convert.ToDouble(body));
                    break;
                case TNetDelimiter.Boolean:
                    retval = new TNetString(body == "true" ? true : false);
                    break;
                case TNetDelimiter.DateTime:
                    retval = new TNetString(System.Convert.ToDateTime(body));
                    break;
                case TNetDelimiter.Guid:
                    retval = new TNetString(new Guid(body));
                    break;
                case TNetDelimiter.Null:
                    retval = new TNetString(null);
                    break;
                case TNetDelimiter.List:
                    IList<object> retvalList = new List<object>();

                    int length = size;
                    int bodyPosition = 0;
                    while (bodyPosition < length)
                    {
                        string temp = body.Substring(bodyPosition);
                        TNetString subNet = TNetString.Parse(temp);
                        bodyPosition += subNet.FullSize;
                        retvalList.Add(subNet.Data);
                    }
                    retval = new TNetString(retvalList);
                    
                    
                    break;
                case TNetDelimiter.Dictionary:
                    Dictionary<string, object> retvalDictionary = new Dictionary<string, object>();

                    length = size;
                    bodyPosition = 0;
                    while (bodyPosition < length)
                    {
                        TNetString subNet = null;
                        string temp = null;
                        string key = null;
                        object val = null;

                        temp = body.Substring(bodyPosition);
                        subNet = TNetString.Parse(temp);
                        key = subNet.Data.ToString();
                        bodyPosition += subNet.FullSize;

                        temp = body.Substring(bodyPosition);
                        subNet = TNetString.Parse(temp);
                        val = subNet.Data;
                        bodyPosition += subNet.FullSize;

                        retvalDictionary.Add(key, val);
                    }
                    retval = new TNetString(retvalDictionary);
                    break;
                default:
                    throw ExceptionFactory.New<InvalidOperationException>("Field type delimiter of '{0}' in body '{1} is currently not supported", type, body);
            }

            retval.Length = body.Length;
            retval.FullSize =  fullSizeOfTNetString;

            //retval.ToString(); //the side effect of this call is that is sets Length, and FullSize properties.  Possible performance improvement location, if use-case is proven
            return retval;
        }

        public TNetString(object data)
        {
            this.m_data = data;
            if (this.Data == null)
                this.m_type = TNetDelimiter.Null;
            else if (this.Data is string)
                this.m_type = TNetDelimiter.String;
            else if (this.Data is long || this.Data is int || this.Data is short)
                this.m_type = TNetDelimiter.Integer;
            else if (this.Data is float || this.Data is double || this.Data is Single)
                this.m_type = TNetDelimiter.Float;
            else if (this.Data is bool)
                this.m_type = TNetDelimiter.Boolean;
            else if (this.Data is IList<object>)
                this.m_type = TNetDelimiter.List;
            else if (this.Data is IDictionary)
                this.m_type = TNetDelimiter.Dictionary;
            else if (this.Data is DateTime)
                this.m_type = TNetDelimiter.DateTime;
            else if (this.Data is Guid)
                this.m_type = TNetDelimiter.Guid;
            else if (this.Data is IList)
                this.m_type = TNetDelimiter.List;
            else
            {
                throw ExceptionFactory.New<ArgumentException>("Unable to identify type '{0}' as a TNet type", data.GetType().FullName);
            }

            this.ToString(); //this side effect of this is that is sets Length, and FullSize properties.  Possible performance improvement location, if use-case is proven
        }

        public string ToString()
        {
            if (this.Data == null)  // all null fields always return same token '0:~'
            {
                var nullString = "0:~";
                m_size = 0;
                FullSize = 3;
                return nullString;
            }

            //if (this.m_toString == null)
            //{
            StringBuilder sbValue = new StringBuilder();
            switch (this.Type)
            {
                case TNetDelimiter.Integer:
                case TNetDelimiter.Guid:
                case TNetDelimiter.Float:
                case TNetDelimiter.String:
                    sbValue.Append(this.Data.ToString());
                    break;
                case TNetDelimiter.Boolean:
                    sbValue.Append(this.Data.ToString().ToLower()); //TNet standard is to have true/false in lower case
                    break;
                case TNetDelimiter.DateTime:
                    sbValue.Append(System.Convert.ToDateTime(this.Data).ToString("yyyy-MM-ddTHH:mm:ss.fff"));  //all dates are exported in standard format
                    break;
                case TNetDelimiter.List:
                    IList l = (IList)Data;
                    if (l != null)
                    {
                        for (int x = 0; x < l.Count; x++)
                        {
                            TNetString v = new TNetString(l[x]);
                            sbValue.Append(v.ToString());
                        }
                    }
                    break;
                case TNetDelimiter.Dictionary:
                    IDictionary d = (IDictionary)this.Data;
                    if (d != null)
                    {
                        foreach (var key in d.Keys)
                        {
                            TNetString tnetKey = new TNetString(key);
                            TNetString tnetValue = new TNetString(d[key]);

                            sbValue.Append(tnetKey.ToString());
                            sbValue.Append(tnetValue.ToString());
                        }
                    }
                    break;
                default:
                    throw ExceptionFactory.New<InvalidOperationException>("Unable to convert value of TNetType '{0}' to string", this.Type);
            }

            //finalize formatting
            string data = sbValue.ToString();
            this.m_size = data.Length;
            this.m_toString = String.Format("{0}:{1}{2}"
                   , this.Length.ToString()
                   , data
                   , (this.Data != null) ? this.Type : '~'
                   );
            this.FullSize = m_toString.Length;
            
            return this.m_toString;
        }
    }
}
