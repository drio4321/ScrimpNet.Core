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
    /// <summary>
    /// Marks codes that is a unqiue extension of code that might be attributable to another source
    /// </summary>
    [AttributeUsage( AttributeTargets.All,AllowMultiple=true,Inherited=true)]
    public sealed class CitationExclusionAttribute : System.Attribute
    {
    }
}
