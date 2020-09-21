using YBC.Utils.Error;

namespace YBC.Audix.InnerVoice
{
	interface IEditorInterfaceReceiver
	{
		/// <summary>
		/// Check if a linked GameObject holds any script that implements a certain interface. If so, assign it to the private variable of Type IInterfaceInQuestion. If not, throw an Error
		/// </summary>
		/// <returns>true if an Interface is found, false if not</returns>
		bool ValidateAndAssignAdapter();
	}

}