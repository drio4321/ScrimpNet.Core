/**
/// ScrimpNet.Core Library
/// Copyright © 2005-2012
///
/// This module is Copyright © 2005-2012 Steve Powell
/// All rights reserved.
///
/// This library is free software; you can redistribute it and/or
/// modify it under the terms of the Microsoft Public License (Ms-PL)
/// 
/// This library is distributed in the hope that it will be
/// useful, but WITHOUT ANY WARRANTY; without even the implied
/// warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR
/// PURPOSE.  See theMicrosoft Public License (Ms-PL) License for more
/// details.
///
/// You should have received a copy of the Microsoft Public License (Ms-PL)
/// License along with this library; if not you may 
/// find it here: http://www.opensource.org/licenses/ms-pl.html
///
/// Steve Powell, spowell@scrimpnet.com
**/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Security.Cryptography;

namespace ScrimpNet.Cryptography
{
    /// <summary>
    /// Key suitable for generating hash values.  NOTE:  Use HashKeySalted for hashing with salt values
    /// </summary>
    public class HashEncoder:IDisposable 
    {
        CryptoKey _key;
        /// <summary>
        /// Crypto.HashModesSimple.SHA256
        /// </summary>
        private static string _defaultHashProvider = Crypto.HashModesSimple.SHA256; //EXTENSION Change this for larger or smaller default hash values
        /// <summary>
        /// Crypto.HashModesSalted.HMACSHA256
        /// </summary>
        private static string _defaultSaltedHashProvider = Crypto.HashModesSalted.HMACSHA256;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="key">Hydrated key containing any required hashing parameters</param>
        public HashEncoder(CryptoKey key)
        {
            _key = key;
        }

        /// <summary>
        /// Explicit constructor
        /// </summary>
        /// <param name="hashMode">Any one of the Crypto.HashModes constants (simple or salted)</param>
        /// <param name="salt">Salt to use in hash.  If non-null, forces salted hash mode.  If null, uses simple hash mode</param>
        public HashEncoder(string hashMode, byte[] salt)
        {
            CryptoKey key = new CryptoKey();
            if (salt != null)
            {
                key.Properties.Add(new CryptoProperty()
                    {
                        Name = Crypto.PropertyNames.HashSaltedMode,
                        ValueType = Crypto.PropertyValueTypes.String,
                        Value = hashMode
                    }
                );
                key.Segments.Add(Crypto.SegmentNames.HashSalt, salt);
            }
            else
            {
                key.Properties.Add(new CryptoProperty()
                {
                    Name = Crypto.PropertyNames.HashSimpleMode,
                    ValueType = Crypto.PropertyValueTypes.String,
                    Value = hashMode
                }
                );
            }
        }

        /// <summary>
        /// Returns a reference to a new instance of the underlying .Net algorithm based on key provided during class instantiation
        /// </summary>
        public HashAlgorithm NativeAlgorithm
        {
            get
            {

                if (_key.Segments.ContainsKey(Crypto.SegmentNames.HashSalt) == true)
                {
                    return findSaltedProvider(_key.Properties[Crypto.PropertyNames.HashSaltedMode].Value, _key.Segments[Crypto.SegmentNames.HashSalt]);
                }
                else
                {
                    return findSimpleHashPovider(_key.Properties[Crypto.PropertyNames.HashSimpleMode].Value);
                }
            }
        }

        /// <summary>
        /// Create a hash of a series of bytes
        /// </summary>
        /// <param name="plainBytes">Data to be hashed</param>
        /// <returns>Hashed value</returns>
        public byte[] Hash(byte[] plainBytes)
        {
            using (HashAlgorithm hasher = NativeAlgorithm)
            {
                return hasher.ComputeHash(plainBytes);
            }
        }


        private KeyedHashAlgorithm findSaltedProvider(string hashProviderConstant, byte[] salt)
        {
            switch (hashProviderConstant)
            {
                case Crypto.HashModesSalted.HMACMD5:
                    return new HMACMD5(salt);
                case Crypto.HashModesSalted.HMACSHA1:
                    return new HMACSHA1(salt);
                case Crypto.HashModesSalted.HMACSHA256:
                    return new HMACSHA256(salt);
                case Crypto.HashModesSalted.HMACSHA384:
                    return new HMACSHA384(salt);
                case Crypto.HashModesSalted.HMACSHA512:
                    return new HMACSHA512(salt);
                case Crypto.HashModesSalted.HMACRIPEMD160:
                    return new HMACRIPEMD160(salt);
                case Crypto.HashModesSalted.MACTrippleDES:
                    return new MACTripleDES(salt);
            }
            throw ExceptionFactory.New<InvalidOperationException>("Unable to find .net keyed hash provider for '{0}'", hashProviderConstant);
        }

        private HashAlgorithm findSimpleHashPovider(string hashProviderConstant)
        {
            switch (hashProviderConstant)
            {
                case Crypto.HashModesSimple.MD5:
                    return new MD5CryptoServiceProvider();
                case Crypto.HashModesSimple.SHA1:
                    return new SHA1Managed();
                case Crypto.HashModesSimple.SHA256:
                    return new SHA256Managed();
                case Crypto.HashModesSimple.SHA384:
                    return new SHA384Managed();
                case Crypto.HashModesSimple.SHA512:
                    return new SHA512Managed();
                case Crypto.HashModesSimple.RIPEMD160:
                    return new RIPEMD160Managed();
            }
            throw ExceptionFactory.New<InvalidOperationException>("Unable to find .Net hash provider for '{0}'", hashProviderConstant);
        }

        /// <summary>
        /// Create a key suitable for a hashing using a specific algorithm
        /// </summary>
        /// <param name="hashMode">One of the Crypto.HashModesSimple constants</param>
        /// <returns>New hash key</returns>
        public static CryptoKey CreateKey(string hashMode)
        {
            var key = new CryptoKey();
            key.Properties.Add(new CryptoProperty()
            {
                Name = Crypto.PropertyNames.HashSimpleMode,
                Value = hashMode,
                ValueType = Crypto.PropertyValueTypes.String
            });

            return key;
        }

        /// <summary>
        /// Create a key using default (SHA256) hash provider.  NOTE: Default can be changed for smaller or larger hash values
        /// </summary>
        /// <returns>New hash key</returns>
        public static CryptoKey CreateKey()
        {
            return CreateKey(_defaultHashProvider);
        }

        /// <summary>
        /// Create a key suitable for salted hash operations.  Uses default algorigthm (HMACSHA256) and random salt
        /// </summary>
        /// <returns></returns>
        public static CryptoKey CreateSaltedKey()
        {
            return CreateSaltedKey(_defaultSaltedHashProvider);
        }

        /// <summary>
        /// Create a key using a specific Crypto.HashModesSalted and specific salt.
        /// </summary>
        /// <param name="hashMode">One of the Crypto.HashModesSalted constants</param>
        /// <param name="salt">Non-null salt value to use in hashing operations</param>
        /// <returns>Newly created key</returns>
        public static CryptoKey CreateSaltedKey(string hashMode, byte[] salt)
        {
            var key = new CryptoKey();
            key.Properties.Add(new CryptoProperty()
            {
                Name = Crypto.PropertyNames.HashSaltedMode,
                Value = hashMode,
                ValueType = Crypto.PropertyValueTypes.String
            });
            key.Segments.Add(Crypto.SegmentNames.HashSalt, salt);
            return key;
        }

        /// <summary>
        /// Create a key using a specific Crypto.HashModesSalted and a randomly generated salt
        /// </summary>
        /// <param name="hashMode">One of the Crypto.HashModesSalted constants</param>
        /// <returns>Newly created key</returns>
        public static CryptoKey CreateSaltedKey(string hashMode)
        {
            int[] validSizes = new int[] { 8, 16, 24 };
            Random rdm = new Random();
            int actualSize = validSizes[rdm.Next(0, validSizes.Length)]; //MACTripleDES requires one of three salt sizes so apply same constraint to each of the keyed hash algorithms
            return CreateSaltedKey(hashMode, CryptoUtils.Generate.RandomBytes(actualSize, actualSize));
        }

        /// <summary>
        /// Create a key using a default hash mode (HMACSHA256) and specified salt
        /// </summary>
        /// <param name="salt">Non-null salt value to use in hashing operations</param>
        /// <returns>Newly created key</returns>
        public static CryptoKey CreateSaltedKey(byte[] salt)
        {
            return CreateSaltedKey(_defaultSaltedHashProvider, salt);
        }

        /// <summary>
        /// Non-op.  Placeholder for future expansion
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }
        /// <summary>
        /// Non-op.  Placeholder for future expansion
        /// </summary>
        /// <param name="isDisposing"></param>
        public void Dispose(bool isDisposing)
        {
            _key = null;
            if (isDisposing)
            {
             
            }
        }
        /// <summary>
        /// 
        /// </summary>
        ~HashEncoder()
        {
            Dispose(false);
        }
    }
}
