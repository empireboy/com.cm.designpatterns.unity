using UnityEngine;

namespace CM.Patterns.Command
{
	/// <summary>
	/// Represents a reference to a Command as a ScriptableObject.
	/// </summary>
	public abstract class CommandSO : ScriptableObject
	{
		/// <summary>
		/// Gets the Command that this ScriptableObject represents.
		/// </summary>
		/// <returns>The Command that this ScriptableObject represents.</returns>
		public abstract Command GetCommand();
	}
}