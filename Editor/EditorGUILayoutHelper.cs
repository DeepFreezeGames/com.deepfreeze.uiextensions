using System;
using UnityEditor;
using UnityEngine;

namespace EditorGUIExtensions.Editor
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
	
	public class EditorGUISplitView : IDisposable
	{
		public enum Direction 
		{
			Horizontal,
			Vertical
		}

		private readonly Direction _splitDirection;
		private float _splitNormalizedPosition;
		private bool _resize;
		public Vector2 ScrollPosition;
		private Rect _availableRect;
		
		public EditorGUISplitView(Direction splitDirection) 
		{
			_splitNormalizedPosition = 0.5f;
			this._splitDirection = splitDirection;
			BeginSplitView();
		}

		public void BeginSplitView() 
		{
			var tempRect = _splitDirection == Direction.Horizontal 
				? EditorGUILayout.BeginHorizontal (GUILayout.ExpandWidth(true)) 
				: EditorGUILayout.BeginVertical (GUILayout.ExpandHeight(true));
			
			if (tempRect.width > 0.0f) 
			{
				_availableRect = tempRect;
			}

			ScrollPosition = GUILayout.BeginScrollView(ScrollPosition, _splitDirection == Direction.Horizontal 
				? GUILayout.Width(_availableRect.width * _splitNormalizedPosition) 
				: GUILayout.Height(_availableRect.height * _splitNormalizedPosition));
		}

		public void Split() 
		{
			GUILayout.EndScrollView();
			ResizeSplitFirstView ();
		}

		public void EndSplitView() 
		{
			if(_splitDirection == Direction.Horizontal)
			{
				EditorGUILayout.EndHorizontal();
			}
			else
			{
				EditorGUILayout.EndVertical();
			}
		}

		private void ResizeSplitFirstView()
		{
			var resizeHandleRect = _splitDirection == Direction.Horizontal 
				? new Rect (_availableRect.width * _splitNormalizedPosition, _availableRect.y, 2f, _availableRect.height) 
				: new Rect (_availableRect.x,_availableRect.height * _splitNormalizedPosition, _availableRect.width, 2f);

			GUI.DrawTexture(resizeHandleRect,EditorGUIUtility.whiteTexture);

			if(_splitDirection == Direction.Horizontal)
				EditorGUIUtility.AddCursorRect(resizeHandleRect,MouseCursor.ResizeHorizontal);
			else
				EditorGUIUtility.AddCursorRect(resizeHandleRect,MouseCursor.ResizeVertical);

			if( Event.current.type == EventType.MouseDown && resizeHandleRect.Contains(Event.current.mousePosition))
			{
				_resize = true;
			}
			
			if(_resize)
			{
				if(_splitDirection == Direction.Horizontal)
				{
					_splitNormalizedPosition = Event.current.mousePosition.x / _availableRect.width;
				}
				else
				{
					_splitNormalizedPosition = Event.current.mousePosition.y / _availableRect.height;
				}
			}
			
			if(Event.current.type == EventType.MouseUp)
			{
				_resize = false;
			}        
		}

		public void Dispose()
		{
			EndSplitView();
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

	public class EditorGUILayoutHelper
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
