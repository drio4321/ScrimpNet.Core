using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ScrimpNet.Cryptography
{
    /// <summary>
    /// Defines a unit of meta data used in the library
    /// </summary>
    [DataContract(Namespace=CoreConfig.WcfNamespace)]
    [Serializable]
    public class CryptoProperty : IComparable, IComparable<CryptoProperty>
    {
        /// <summary>
        /// A string constant that specifies what kind of property this is (e.g. name, effective date, etc).  Use (and extend)
        /// one of the Crypto.PropertyTypes contants or any implementation defined values.  NOTE: This is the 'Key' of the logical key-value pair
        /// but is named 'Name' to avoid naming confusion with the idea of a 'key' within the crypto library.
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// A description of the data type contained in the property value.  This will give implementations hints for deserializing
        /// value to the appropriate native type.  Use (and extend) one of the Crypto.PropertyValueTypes.
        /// </summary>
        [DataMember]
        public string ValueType { get; set; }

        /// <summary>
        /// Value to assoicate with this property (e.g Name='Mack', UsageCount='6').  This is the 'Value' of the logical key-value pair.
        /// </summary>
        [DataMember]
        public string Value { get; set; }

        /// <summary>
        /// Comparer based on contents of PropertyType (case sensitive)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>0 if this and <paramref name="obj"/>contain the same case sensitive PropertyType values</returns>
        public int CompareTo(object obj)
        {
            return string.Compare(this.Name, ((CryptoProperty)obj).Name, false);
        }

        /// <summary>
        /// Comparer based on contents of PropertyType (case sensitive)
        /// </summary>
        /// <param name="other"></param>
        /// <returns>0 if this and <paramref name="other"/>contain the same case sensitive PropertyType values</returns>
        public int CompareTo(CryptoProperty other)
        {
            return CompareTo(other);
        }

        /// <summary>
        /// Compares to ensure both properties and values are case sensitive the same
        /// </summary>
        /// <param name="other">Property to comape with this instance</param>
        /// <returns>0 if this and <paramref name="other"/>contain the same case sensitive PropertyType values and values</returns>
        public int Same(CryptoProperty other)
        {
            int retVal = string.Compare(this.Name, other.Name, false);
            if (retVal != 0) return retVal;
            return string.Compare(this.Value, other.Value, false);
        }
    }

}
