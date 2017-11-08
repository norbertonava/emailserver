using System;

namespace Merculia.UI
{
	/// <summary>
	/// Summary description for IWSerialiser.
	/// </summary>
	public interface IWSerializer
	{
		/// <summary>
		/// Checks if must serialize spcified property.
		/// </summary>
		/// <param name="propertyName"></param>
		/// <returns></returns>
		bool ShouldSerialize(string propertyName);
	}
}
