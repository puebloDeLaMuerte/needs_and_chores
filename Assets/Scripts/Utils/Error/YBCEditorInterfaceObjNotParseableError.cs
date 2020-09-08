using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;

namespace YBC.Utils.Error
{
	public class YBCEditorInterfaceObjNotParseableError : YBCErrorMessage
	{
		public YBCEditorInterfaceObjNotParseableError()
		{
			errorText = "YBC.NotParseableError - An Object couldn't be parsed to an Interface. A public field has not been woringly assigned in the editor. Things will break!";
		}
	}
}