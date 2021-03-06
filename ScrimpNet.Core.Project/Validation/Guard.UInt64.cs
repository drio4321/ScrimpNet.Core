/**
/// ScrimpNet.Core Library
/// Copyright � 2005-2012
///
/// This module is Copyright � 2005-2012 Steve Powell
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
using System.Text;

namespace ScrimpNet
{
    public static partial class Guard
    {
        ///// <summary>
        ///// Verifies two UInt64s are equal
        ///// </summary>
        ///// <param name="testValue">First value for comparison</param>
        ///// <param name="expectedValue">Second value for comparison</param>
        //public static Boolean Equal(UInt64 testValue, UInt64 expectedValue)
        //{
        //    return Equal<UInt64>(testValue, expectedValue,  null, null, null, null);
        //}
        ///// <summary>
        ///// Verifies two UInt64s are equal
        ///// </summary>
        ///// <param name="testValue">First value for comparison</param>
        ///// <param name="expectedValue">Second value for comparison</param>
        ///// <param name="objectName">Name of object to Validate</param>
        //public static Boolean Equal(UInt64 testValue, UInt64 expectedValue, string objectName)
        //{
        //    return Equal<UInt64>(testValue, expectedValue, objectName, null, null, null);
        //}
        ///// <summary>
        ///// Verifies two UInt64s are equal
        ///// </summary>
        ///// <param name="testValue">First value for comparison</param>
        ///// <param name="expectedValue">Second value for comparison</param>
        ///// <param name="objectName">Name of object to Validate</param>
        ///// <param name="applicationName">Name of application containing object being Validated</param>
        //public static Boolean Equal(UInt64 testValue, UInt64 expectedValue, string objectName, string applicationName)
        //{
        //    return Equal<UInt64>(testValue, expectedValue, objectName, applicationName, null, null);
        //}

        ///// <summary>
        ///// Verifies two UInt64s are equal
        ///// </summary>
        ///// <param name="testValue">First value for comparison</param>
        ///// <param name="expectedValue">Second value for comparison</param>
        ///// <param name="objectName">Name of object to Validate</param>
        ///// <param name="applicationName">Name of application containing object being Validated</param>
        ///// <param name="message">Message to include in any exceptions.  May contain .Net standard {} token templates</param>
        ///// <param name="args">Arguments to pass to message</param>
        //public static Boolean Equal(UInt64 testValue, UInt64 expectedValue, string objectName, string applicationName, string message, params object[] args)
        //{
        //    return Equal<UInt64>(testValue, expectedValue, objectName, applicationName, message, args);
        //}

        ///// <summary>
        ///// Verifies two UInt64 values are not equal
        ///// </summary>
        ///// <param name="testValue">First value for comparison</param>
        ///// <param name="expectedValue">Second value for comparison</param>
        //public static Boolean AreNotEqual(UInt64 testValue, UInt64 expectedValue)
        //{
        //    return AreNotEqual(testValue, expectedValue, null, null, null, null);
        //}

        ///// <summary>
        ///// Verifies two UInt64 values are not equal
        ///// </summary>
        ///// <param name="testValue">First value for comparison</param>
        ///// <param name="expectedValue">Second value for comparison</param>
        ///// <param name="objectName">Name of object to Validate</param>
        ///// <param name="applicationName">Name of application containing object being Validated</param>
        //public static Boolean AreNotEqual(UInt64 testValue, UInt64 expectedValue, string objectName, string applicationName)
        //{
        //    return AreNotEqual(testValue, expectedValue, objectName, applicationName, null, null);
        //}

        ///// <summary>
        ///// Verifies two UInt64 values are not equal
        ///// </summary>
        ///// <param name="testValue">First value for comparison</param>
        ///// <param name="expectedValue">Second value for comparison</param>
        ///// <param name="objectName">Name of object to Validate</param>
        ///// <param name="applicationName">Name of application containing object being Validated</param>
        ///// <param name="message">Message to include in any exceptions.  May contain .Net standard {} token templates</param>
        ///// <param name="args">Arguments to pass to message</param>
        //public static Boolean AreNotEqual(UInt64 testValue, UInt64 expectedValue, string objectName, string applicationName, string message, params object[] args)
        //{
        //    return AreNotEqual(testValue, expectedValue, objectName, applicationName, message, args);
        //}

        ///// <summary>
        ///// Verifies that a UInt64 is greater than a specified threshhold
        ///// </summary>
        ///// <param name="testValue">Value being tested</param>
        ///// <param name="limitValue">Threshhold that comparison is made against</param>
        //public static Boolean GreaterThan(UInt64 testValue, UInt64 limitValue)
        //{
        //    return GreaterThan(testValue, limitValue, null, null, null, null);
        //}

        ///// <summary>
        ///// Verifies that a UInt64 is greater than a specified threshhold
        ///// </summary>
        ///// <param name="testValue">Value being tested</param>
        ///// <param name="limitValue">Threshhold that comparison is made against</param>
        ///// <param name="objectName">Name of object to Validate</param>
        //public static Boolean GreaterThan(UInt64 testValue, UInt64 limitValue, string objectName)
        //{
        //    return GreaterThan(testValue, limitValue, objectName, null, null, null);
        //}

        ///// <summary>
        ///// Verifies that a UInt64 is greater than a specified threshhold
        ///// </summary>
        ///// <param name="testValue">Value being tested</param>
        ///// <param name="limitValue">Threshhold that comparison is made against</param>
        ///// <param name="objectName">Name of object to Validate</param>
        ///// <param name="applicationName">Name of application containing object being Validated</param>
        //public static Boolean GreaterThan(UInt64 testValue, UInt64 limitValue, string objectName, string applicationName)
        //{
        //    return GreaterThan(testValue, limitValue, objectName, applicationName, null, null);
        //}

        ///// <summary>
        ///// Verifies that a UInt64 <paramref name="testValue"/> is greater than a specified threshhold
        ///// </summary>
        ///// <param name="testValue">Value being tested</param>
        ///// <param name="limitValue">Threshhold that comparison is made against</param>
        ///// <param name="objectName">Name of object to Validate</param>
        ///// <param name="applicationName">Name of application containing object being Validated</param>
        ///// <param name="message">Message to include in any exceptions.  May contain .Net standard {} token templates</param>
        ///// <param name="args">Arguments to pass to message</param>
        //public static Boolean GreaterThan(UInt64 testValue, UInt64 limitValue, string objectName, string applicationName, string message, params object[] args)
        //{
        //    return GreaterThan<UInt64>(testValue, limitValue, objectName, applicationName, message, args);
        //}

        ///// <summary>
        ///// Verifies that a UInt64 is less than a specified threshhold
        ///// </summary>
        ///// <param name="testValue">Value being tested</param>
        ///// <param name="limitValue">Threshhold that comparison is made against</param>
        //public static Boolean LessThan(UInt64 testValue, UInt64 limitValue)
        //{
        //    return LessThan(testValue, limitValue, null, null, null, null);
        //}

        ///// <summary>
        ///// Verifies that a UInt64 is less than a specified threshhold
        ///// </summary>
        ///// <param name="testValue">Value being tested</param>
        ///// <param name="limitValue">Threshhold that comparison is made against</param>
        ///// <param name="objectName">Name of object to Validate</param>
        //public static Boolean LessThan(UInt64 testValue, UInt64 limitValue, string objectName)
        //{
        //    return LessThan(testValue, limitValue, objectName, null, null, null);
        //}

        ///// <summary>
        ///// Verifies that a UInt64 is less than a specified threshhold
        ///// </summary>
        ///// <param name="testValue">Value being tested</param>
        ///// <param name="limitValue">Threshhold that comparison is made against</param>
        ///// <param name="objectName">Name of object to Validate</param>
        ///// <param name="applicationName">Name of application containing object being Validated</param>
        //public static Boolean LessThan(UInt64 testValue, UInt64 limitValue, string objectName, string applicationName)
        //{
        //    return LessThan(testValue, limitValue, objectName, applicationName, null, null);
        //}

        ///// <summary>
        ///// Verifies that a UInt64 is less than a specified threshhold
        ///// </summary>
        ///// <param name="testValue">Value being tested</param>
        ///// <param name="limitValue">Threshhold that comparison is made against</param>
        ///// <param name="objectName">Name of object to Validate</param>
        ///// <param name="applicationName">Name of application containing object being Validated</param>
        ///// <param name="message">Message to include in any exceptions.  May contain .Net standard {} token templates</param>
        ///// <param name="args">Arguments to pass to message</param>
        //public static Boolean LessThan(UInt64 testValue, UInt64 limitValue, string objectName, string applicationName, string message, params object[] args)
        //{
        //    return LessThan<UInt64>(testValue, limitValue, objectName, applicationName, message, args);
        //}

        ///// <summary>
        ///// Compare a UInt64 to see if it falls between two values
        ///// </summary>
        ///// <param name="testValue">Value to be checked</param>
        ///// <param name="lowerLimit">Lower limit of range</param>
        ///// <param name="upperLimit">Upper limit of range</param>
        ///// <param name="objectName">Name of object to Validate</param>
        //public static Boolean Between(UInt64 testValue, UInt64 lowerLimit, UInt64 upperLimit)
        //{
        //    return Between(testValue, lowerLimit, upperLimit, null, null, null, null);
        //}

        ///// <summary>
        ///// Compare a UInt64 to see if it falls between two values
        ///// </summary>
        ///// <param name="testValue">Value to be checked</param>
        ///// <param name="lowerLimit">Lower limit of range</param>
        ///// <param name="upperLimit">Upper limit of range</param>
        ///// <param name="objectName">Name of object to Validate</param>
        ///// <param name="applicationName">Name of application containing object being Validated</param>
        //public static Boolean Between(UInt64 testValue, UInt64 lowerLimit, UInt64 upperLimit, string objectName)
        //{
        //    return Between(testValue, lowerLimit, upperLimit, objectName, null, null, null);
        //}

        ///// <summary>
        ///// Compare a UInt64 to see if it falls between two values
        ///// </summary>
        ///// <param name="testValue">Value to be checked</param>
        ///// <param name="lowerLimit">Lower limit of range</param>
        ///// <param name="upperLimit">Upper limit of range</param>
        ///// <param name="objectName">Name of object to Validate</param>
        ///// <param name="applicationName">Name of application containing object being Validated</param>
        //public static Boolean Between(UInt64 testValue, UInt64 lowerLimit, UInt64 upperLimit,  string objectName, string applicationName)
        //{
        //    return Between(testValue, lowerLimit, upperLimit, objectName, applicationName, null, null);
        //}

        ///// <summary>
        ///// Compare a UInt64 to see if it falls between two values
        ///// </summary>
        ///// <param name="testValue">Value to be checked</param>
        ///// <param name="lowerLimit">Lower limit of range</param>
        ///// <param name="upperLimit">Upper limit of range</param>
        ///// <param name="objectName">Name of object to Validate</param>
        ///// <param name="applicationName">Name of application containing object being Validated</param>
        ///// <param name="message">Message to include in any exceptions.  May contain .Net standard {} token templates</param>
        ///// <param name="args">Arguments to pass to message</param>
        //public static Boolean Between(UInt64 testValue, UInt64 lowerLimit, UInt64 upperLimit,  string objectName, string applicationName, string message, params object[] args)
        //{
        //    return Between(testValue, lowerLimit, upperLimit, objectName, applicationName, message, args);
        //}

        ///// <summary>
        ///// Compare a UInt64 to see if it falls outside a range of values
        ///// </summary>
        ///// <param name="testValue">Value to be checked</param>
        ///// <param name="lowerLimit">Lower limit of range</param>
        ///// <param name="upperLimit">Upper limit of range</param>
        //public static Boolean NotBetween(UInt64 testValue, UInt64 lowerLimit, UInt64 upperLimit)
        //{
        //    return NotBetween(testValue, lowerLimit, upperLimit, null, null, null, null);
        //}

        ///// <summary>
        ///// Compare a UInt64 to see if it falls outside a range of values
        ///// </summary>
        ///// <param name="testValue">Value to be checked</param>
        ///// <param name="lowerLimit">Lower limit of range</param>
        ///// <param name="upperLimit">Upper limit of range</param>
        ///// <param name="objectName">Name of object to Validate</param>
        //public static Boolean NotBetween(UInt64 testValue, UInt64 lowerLimit, UInt64 upperLimit, string objectName)
        //{
        //    return NotBetween(testValue, lowerLimit, upperLimit, objectName, null, null, null);
        //}

        ///// <summary>
        ///// Compare a UInt64 to see if it falls outside a range of values
        ///// </summary>
        ///// <param name="testValue">Value to be checked</param>
        ///// <param name="lowerLimit">Lower limit of range</param>
        ///// <param name="upperLimit">Upper limit of range</param>
        ///// <param name="objectName">Name of object to Validate</param>
        ///// <param name="applicationName">Name of application containing object being Validated</param>
        //public static Boolean NotBetween(UInt64 testValue, UInt64 lowerLimit, UInt64 upperLimit, string objectName, string applicationName)
        //{
        //    return NotBetween(testValue, lowerLimit, upperLimit, objectName, applicationName, null, null);
        //}

        ///// <summary>
        ///// Compare a UInt64 to see if it falls outside a range of values
        ///// </summary>
        ///// <param name="testValue">Value to be checked</param>
        ///// <param name="lowerLimit">Lower limit of range</param>
        ///// <param name="upperLimit">Upper limit of range</param>
        ///// <param name="objectName">Name of object to Validate</param>
        ///// <param name="applicationName">Name of application containing object being Validated</param>
        ///// <param name="message">Message to include in any exceptions.  May contain .Net standard {} token templates</param>
        ///// <param name="args">Arguments to pass to message</param>
        //public static Boolean NotBetween(UInt64 testValue, UInt64 lowerLimit, UInt64 upperLimit, string objectName, string applicationName, string message, params object[] args)
        //{
        //    return NotBetween<UInt64>(testValue, lowerLimit, upperLimit, objectName, applicationName, message, args);
        //}
    } // Is_UInt64
}
