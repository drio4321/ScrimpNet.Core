using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScrimpNet.ServiceModel;

namespace ScrimpNet
{
	public class ServiceParameter
	{
		public ServiceParameter(){}

		public ServiceParameter(string key, object value)
		{
			Key = key;
			Value = value;

		}

		public string Key;
		public object Value { get; set; }
	}
	public interface IServiceLocator
	{
		IServiceLocator Register<I>(WcfEndPoint endPoint);
		I GetInstance<I>(params ServiceParameter[] constructorParameters);
		IServiceLocator Register<IFromInterface, TToConcrete>() where TToConcrete : class,IFromInterface;
		void Empty();
		T InternalContainer<T>() where T:class;
		bool ErrorOnMissingRegistration { get; set; }
	}
}
