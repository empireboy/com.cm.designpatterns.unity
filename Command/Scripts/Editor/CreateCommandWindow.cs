using CM.SO;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace CM.Patterns.Command
{
	/// <summary>
	/// An editor window for command creation.
	/// This window automizes the task to create a command.
	/// </summary>
	public class CreateCommandWindow : EditorWindow
	{
		private StringSO _commandPathSO;
		private string _commandName;

		/// <summary>
		/// Opens the editor window.
		/// </summary>
		[MenuItem("Window/CreateCommandWindow")]
		public static void ShowWindow()
		{
			GetWindow(typeof(CreateCommandWindow), false, "Create Command Window");
		}

		private void Awake()
		{
			_commandPathSO = Resources.Load<StringSO>("Command/CommandPathSO");
		}

		private void OnGUI()
		{
			EditorGUILayout.LabelField("Create Your Command", EditorStyles.boldLabel);

			_commandName = EditorGUILayout.TextField("Command Name", _commandName);

			// Create New Command Button

			EditorGUILayout.BeginHorizontal();

			EditorGUILayout.PrefixLabel(" ");

			if (GUILayout.Button("Create New Command") && _commandPathSO)
				CreateCommand(_commandName, _commandPathSO.value);

			EditorGUILayout.EndHorizontal();

			// Create ScriptableObject from Command Button

			EditorGUILayout.BeginHorizontal();

			EditorGUILayout.PrefixLabel(" ");

			if (GUILayout.Button("Create ScriptableObject from Command") && _commandPathSO)
				CreateCommandSO(_commandName, _commandPathSO.value);

			EditorGUILayout.EndHorizontal();

			// References

			EditorGUILayout.Space();
			EditorGUILayout.LabelField("References", EditorStyles.boldLabel);

			_commandPathSO = EditorGUILayout.ObjectField("Command Path", _commandPathSO, typeof(StringSO), true) as StringSO;
		}

		private void CreateCommandSO(string commandName, string path)
		{
			path += "/" + commandName + "SO.cs";

			if (File.Exists(path))
				return;

			using (StreamWriter outfile = new StreamWriter(path))
			{
				outfile.WriteLine("using CM.Patterns.Command;");
				outfile.WriteLine("using UnityEngine;");
				outfile.WriteLine(" ");
				outfile.WriteLine("[CreateAssetMenu(fileName = " + "\"" + commandName + "\"" + ", menuName = " + "\"" + "CM/Commands/" + commandName + "\"" + ")]");
				outfile.WriteLine("public class " + commandName + "SO : CommandSO");
				outfile.WriteLine("{");
				outfile.WriteLine("\tpublic override Command GetCommand()");
				outfile.WriteLine("\t{");
				outfile.WriteLine("\t\treturn new " + commandName + "();");
				outfile.WriteLine("\t}");
				outfile.WriteLine("}");
			}

			AssetDatabase.Refresh();
		}

		private void CreateCommand(string commandName, string path)
		{
			path += "/" + commandName + ".cs";

			if (File.Exists(path))
				return;

			using (StreamWriter outfile = new StreamWriter(path))
			{
				outfile.WriteLine("using CM.Patterns.Command;");
				outfile.WriteLine(" ");
				outfile.WriteLine("public class " + commandName + " : Command");
				outfile.WriteLine("{");
				outfile.WriteLine("\tpublic override void Execute()");
				outfile.WriteLine("\t{");
				outfile.WriteLine("\t\tthrow new System.NotImplementedException();");
				outfile.WriteLine("\t}");
				outfile.WriteLine(" ");
				outfile.WriteLine("\tpublic override void Undo()");
				outfile.WriteLine("\t{");
				outfile.WriteLine("\t\tthrow new System.NotImplementedException();");
				outfile.WriteLine("\t}");
				outfile.WriteLine("}");
			}

			AssetDatabase.Refresh();
		}
	}
}