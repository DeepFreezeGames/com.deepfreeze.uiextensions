
# EditorGUIExtensions - WIP
A UPM package to make building Editor GUI elements in Unity easier. It contains a series of disposable to help clean up your editor code.

To use this package, add this line to your manifest.json file inside the Packages folder of your Unity project.
"com.deepfreeze.editorguiextensions": "https://github.com/DeepFreezeGames/EditorGUIExtensions.git",

# Components
The EditorGUILayoutHelper class contains various helper methods to make building Editor UI elements quicker and cleaner than the normal way.

## Vertical Block
Examples:

	//Example 1
    using (new VerticalBlock())  
    {  
        //Your code here
    }

	//Example 2
    using (new VerticalBlock(EditorStyles.helpBox, GUILayout.Width(60f)))  
    {  
	    //Your code here
    }

The vertical block is a IDisposable class that will create a vertically-aligned box. You can pass in any GUILayoutOption parameters like you normally would.

## Horizontal Block
Examples:

	//Example 1
    using (new HorizontalBlock())  
    {  
        //Your code here
    }

	//Example 2
    using (new HorizontalBlock(EditorStyles.helpBox, GUILayout.Width(60f), GUILayout.Height(60f)))  
    {  
	    //Your code here
    }

The horizontal block is also a IDisposable class that will create a horizontally-aligned box. You can pass in any GUILayoutOption parameters like you normally would.
