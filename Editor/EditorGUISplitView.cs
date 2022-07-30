using System;
using UnityEditor;
using UnityEngine;

namespace DeepFreeze.Packages.UIExtensions.Editor
{
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

			EditorGUIUtility.AddCursorRect(resizeHandleRect,
				_splitDirection == Direction.Horizontal ? MouseCursor.ResizeHorizontal : MouseCursor.ResizeVertical);

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
}