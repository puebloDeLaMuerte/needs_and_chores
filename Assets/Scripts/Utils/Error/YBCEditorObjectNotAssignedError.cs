using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;

namespace YBC.Utils.Error
{
	public class YBCEditorNotAssignedError : YBCErrorMessage
	{
		public YBCEditorNotAssignedError()
		{
			errorText = "YBC.NotAssignedError - A public field has not been assigned in the editor.Things will break!";
		}
	}
}