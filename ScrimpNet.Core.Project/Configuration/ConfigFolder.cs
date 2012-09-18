using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ScrimpNet.Configuration
{
	[DataContract]
	[Serializable]
	public class ConfigFolder:ConfigNode
	{
		public ConfigFolder():base()
		{
		}
		public ConfigFolder(string folderName, string folderDescription = null):this()
		{		
			Name = folderName;
			Description = folderDescription;
		}
	}
}
