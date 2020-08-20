using System;

namespace YBC.Utils
{
	public class YouBeRandom
	{
		private Random r;

		public YouBeRandom()
		{
			r = new Random();
		}


		/// <summary>
		/// Roll a Dice and see if you hit me.
		/// </summary>
		/// <param name="chance">the cance of a hit between 0f and 1f</param>
		/// <returns>randomized bool for hit or no hit</returns>
		public bool HitMe( float chance )
		{

			if ( r.NextDouble() < chance )
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Roll a random integer.
		/// </summary>
		/// <param name="from">between this</param>
		/// <param name="to">and this</param>
		/// <returns>random int</returns>
		public int Roll( int from, int to )
		{
			return r.Next(from, to);
		}


		/// <summary>
		/// Roll a random integer between one and specified max.
		/// </summary>
		/// <param name="to">maximum</param>
		/// <returns>random int</returns>
		public int RollOne( int to )
		{
			return r.Next( 1, to );
		}


		/// <summary>
		/// Roll a random integer between zero and specified max.
		/// </summary>
		/// <param name="to">and this</param>
		/// <returns>random int</returns>
		public int RollZero( int to )
		{
			if( to <=0 )
			{
				return 0;
			} else
			{
				return r.Next( 0, to );
			}
		}

	}
}