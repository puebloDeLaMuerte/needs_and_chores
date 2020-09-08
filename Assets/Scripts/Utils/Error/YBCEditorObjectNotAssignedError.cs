using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;

namespace YBC.Utils.Error
{
	public class YBCEditorInterfaceObjNotParseable : YBCErrorMessage
	{
		public YBCEditorInterfaceObjNotParseable()
		{
			errorText = "YBC.NotAssignedError - A public field has not been assigned in the editor.Things will break!";
		}
	}
}