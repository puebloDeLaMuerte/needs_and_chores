using System;

namespace YBC.Neemotix
{
	public interface INeemotixAdapter
	{
		(float, NeemotionStatus) GetNeemotionStateByName( String neemotionName );
		(float, NeemotionStatus) GetNeemotionStateByID( int neemotionID );
		int[] GetAllNemotionIDs();
	}
}