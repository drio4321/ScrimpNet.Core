using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrimpNet.Serialization.TNet
{
    /// <summary>
    /// Convenience class containing TNet field delimiters
    /// </summary>
	public class TNetDelimiter
	{
        /// <summary>
        /// '+'
        /// </summary>
        public const char Guid = '+';

		/// <summary>
		/// ,
		/// </summary>
		public const char String = ',';
		/// <summary>
		/// #
		/// </summary>
		public const char Integer = '#';

        /// <summary>
        /// '^'
        /// </summary>
		public const char Float = '^';

        /// <summary>
        /// '!'
        /// </summary>
		public const char Boolean = '!';

        /// <summary>
        /// '}'
        /// </summary>
		public const char Dictionary = '}';

        /// <summary>
        /// ']'
        /// </summary>
		public const char List = ']';

        /// <summary>
        /// '*'
        /// </summary>
		public const char Binary = '*';

        /// <summary>
        /// '@'
        /// </summary>
		public const char DateTime = '@';

        /// <summary>
        /// '~'
        /// </summary>
		public const char Null = '~';
	}


}
