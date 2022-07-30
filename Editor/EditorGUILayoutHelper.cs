using System;
using UnityEditor;
using UnityEngine;

namespace DeepFreeze.Packages.UIExtensions.Editor
{
	/// <summary>
	/// Creates a vertical block for Unity Editor extensions. To use this follow the example below.
	/// using (new VerticalBlock())
	/// {
	///		your code here...
	/// }
	/// </summary>
	public class VerticalBlock : IDisposable
	{
		public VerticalBlock(params GUILayoutOption[] options)
		{
			GUILayout.BeginVertical(options);
		}
		public VerticalBlock(GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayout.BeginVertical(style, options);
		}
		public void Dispose()
		{
			GUILayout.EndVertical();
		}
	}

	/// <summary>
	/// Creates a scrollview block for Unity Editor extensions. To use this follow the example below.
	/// using (new ScrollviewBlock(ref someVector2ForScrollPos))
	/// {
	///		your code here...
	/// }
	/// </summary>
	public class ScrollviewBlock : IDisposable
	{
		public ScrollviewBlock(ref Vector2 scrollPos, params GUILayoutOption[] options)
		{
			scrollPos = GUILayout.BeginScrollView(scrollPos, options);
		}
		public void Dispose()
		{
			GUILayout.EndScrollView();
		}
	}

	/// <summary>
	/// Creates a horizontal block for Unity Editor extensions. To use this follow the example below.
	/// using (new HorizontalBlock())
	/// {
	///		your code here...
	/// }
	/// </summary>
	///
	public class HorizontalBlock : IDisposable
	{
		public HorizontalBlock(params GUILayoutOption[] options)
		{
			GUILayout.BeginHorizontal(options);
		}
		public HorizontalBlock(GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayout.BeginHorizontal(style, options);
		}
		public void Dispose()
		{
			GUILayout.EndHorizontal();
		}
	}
	
	public class LabeledBlock : IDisposable
	{
		public LabeledBlock(string label, params GUILayoutOption[] options)
		{
			GUILayout.BeginVertical("box");
			GUILayout.BeginHorizontal(EditorStyles.helpBox);
			GUILayout.Label(label, EditorStyles.boldLabel);
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
		}
		
		public LabeledBlock(string label, Color headerColor, params GUILayoutOption[] options)
		{
			GUILayout.BeginVertical("box");
			var oldColor = GUI.backgroundColor;
			GUI.backgroundColor = headerColor;
			GUILayout.BeginHorizontal(EditorStyles.helpBox);
			GUILayout.Label(label, EditorStyles.boldLabel);
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
			GUI.backgroundColor = oldColor;
		}
		
		public void Dispose()
		{
			GUILayout.EndVertical();
		}
	}

	public class ColoredBlock : IDisposable
	{
		private Color oldColor;
		
		public ColoredBlock(Color color)
		{
			oldColor = GUI.color;
			GUI.color = color;
		}
		public void Dispose()
		{
			GUI.color = oldColor;
		}
	}
	
	public class ColoredBackgroundBlock : IDisposable
	{
		private Color oldColor;
		
		public ColoredBackgroundBlock(Color color)
		{
			oldColor = GUI.backgroundColor;
			GUI.backgroundColor = color;
		}
		public void Dispose()
		{
			GUI.backgroundColor = oldColor;
		}
	}
	
	public class ColoredContentBlock : IDisposable
	{
		private Color oldColor;
		
		public ColoredContentBlock(Color color)
		{
			oldColor = GUI.contentColor;
			GUI.contentColor = color;
		}
		public void Dispose()
		{
			GUI.contentColor = oldColor;
		}
	}

	public class EnabledBlock : IDisposable
	{
		private bool _state;
		
		public EnabledBlock(bool enabled)
		{
			_state = enabled;
			GUI.enabled = _state;
		}

		public void Dispose()
		{
			GUI.enabled = !_state;
		}
	}

	public partial class EditorGUILayoutHelper
	{
		public static bool ToggleableButton(bool enabled, string label, params GUILayoutOption[] options)
		{
			GUI.enabled = enabled;
			if (GUILayout.Button(label, options))
			{
				GUI.enabled = true;
				return true;
			}
			GUI.enabled = true;
			return false;
		}

		public static bool ToggleableButton(bool enabled, string label, GUIStyle style, params GUILayoutOption[] options)
		{
			GUI.enabled = enabled;
			if (GUILayout.Button(label, style, options))
			{
				GUI.enabled = true;
				return true;
			}
			GUI.enabled = true;
			return false;
		}

		/// <summary>
		/// Creates a folder path textfield with a browse button.
		/// </summary>
		public static string FolderLabel(string name, float labelWidth, string path)
		{
			EditorGUILayout.BeginHorizontal();
			var filepath = EditorGUILayout.TextField(name, path);
			if (GUILayout.Button("Browse", GUILayout.MaxWidth(60)))
			{
				GUI.FocusControl(null);
				filepath = EditorUtility.SaveFolderPanel(name, path, "Folder");
			}
			EditorGUILayout.EndHorizontal();
			return filepath;
		}
		
		
		/// <summary>
		/// Creates a folder path textfield with a browse button.
		/// </summary>
		public static string FolderLabel(GUIContent label, string name, float labelWidth, string path)
		{
			EditorGUILayout.BeginHorizontal();
			var filepath = EditorGUILayout.TextField(label, path);
			if (GUILayout.Button("Browse", GUILayout.MaxWidth(60)))
			{
				GUI.FocusControl(null);
				filepath = EditorUtility.SaveFolderPanel(name, path, "Folder");
			}
			EditorGUILayout.EndHorizontal();
			return filepath;
		}

		public static int RadioButtonField(string label, int currentSelection, string[] options)
		{
			using (new HorizontalBlock())
			{
				if (!string.IsNullOrEmpty(label))
				{
					GUILayout.Label(label);
				}
				for (var i = 0; i < options.Length; i++)
				{
					GUI.enabled = currentSelection != i;
					if (GUILayout.Button(options[i]))
					{
						return i;
					}
					GUI.enabled = true;
				}
			}

			return currentSelection;
		}
		
		public static int RadioButtonField(string label, int currentSelection, string[] options, GUIStyle style)
		{
			using (new HorizontalBlock())
			{
				if (!string.IsNullOrEmpty(label))
				{
					GUILayout.Label(label);
				}
				for (var i = 0; i < options.Length; i++)
				{
					GUI.enabled = currentSelection != i;
					if (GUILayout.Button(options[i], style))
					{
						return i;
					}
					GUI.enabled = true;
				}
			}

			return currentSelection;
		}

		public static void CenteredMessage(string message, params GUILayoutOption[] options)
		{
			using(new VerticalBlock())
			{
				GUILayout.FlexibleSpace();
				using(new HorizontalBlock(options))
				{
                    GUILayout.FlexibleSpace();
					GUILayout.Label(message, EditorStyles.boldLabel);
                    GUILayout.FlexibleSpace();
				}
                GUILayout.FlexibleSpace();
			}
		}

        /// <summary>
        /// Creates the UI implementation for a search bar
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static string SearchField(string searchString, params GUILayoutOption[] options)
        {
            using (new HorizontalBlock())
            {
                searchString = GUILayout.TextField(searchString, "ToolbarSeachTextField", options);
                if (GUILayout.Button("", "ToolbarSeachCancelButton"))
                {
                    searchString = "";
                    GUI.FocusControl(null);
                }
            }

            return searchString;
        }
	}
}
