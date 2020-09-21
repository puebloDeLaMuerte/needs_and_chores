

namespace YBC.Audix.InnerVoice
{
	public interface IInnerVoiceDataAdapter
	{
		/// <summary>
		/// get the String-Data associated with this selecorID and selectorDepth.
		/// </summary>
		/// <param name="selectorID">The data-item to be querried</param>
		/// <param name="selectorDepth">The depth of the querry. [0] asking for the first item, [n] asking for nth sub-criteria of this selection.</param>
		/// <returns>the data-value as a String</returns>
		string	getStringForSelectorID( int selectorID, int selectorDepth);

		/// <summary>
		/// get the Float-Data associated with this selecorID and selectorDepth.
		/// </summary>
		/// <param name="selectorID">The data-item to be querried</param>
		/// <param name="selectorDepth">The depth of the querry. [0] asking for the first item, [n] asking for nth sub-criteria of this selection.</param>
		/// <returns>the data-value as a float</returns>
		float getFloatForSelectorID( int selectorID, int selectorDepth);

		/// <summary>
		/// get the Int-Data associated with this selecorID and selectorDepth.
		/// </summary>
		/// <param name="selectorID">The data-item to be querried</param>
		/// <param name="selectorDepth">The depth of the querry. [0] asking for the first item, [n] asking for nth sub-criteria of this selection.</param>
		/// <returns>the data-value as an Int</returns>
		int getIntForSelectorID( int selectorID, int selectorDepth);

		/// <summary>
		/// An Array of all Data-Items that can possibliy be querried through the adapter. Main purpose is for initialization of the SoundCollection.
		/// </summary>
		/// <returns>All (selectorID, selectorName) pairs that are available for querries through this adapter.</returns>
		//TODO	implement a precalculated list to save time on each call...
		(int, string)[] getAllSelectorIDs();
	}
}