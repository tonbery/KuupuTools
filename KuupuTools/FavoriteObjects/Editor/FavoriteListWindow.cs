using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class FavoriteListWindow : EditorWindow
{
    FavoriteList favoriteList;
    Vector2 _scroll;

    [MenuItem("Window/Kuupu/FavoriteList")]
    static void Init()
    {
        FavoriteListWindow window = (FavoriteListWindow)EditorWindow.GetWindow(typeof(FavoriteListWindow));
        window.Show();
        var v2 = window.minSize;
        v2.y = 20f;
        window.minSize = v2;
        window.favoriteList = Resources.Load<FavoriteList>("FavoriteList");
    }
        
    [MenuItem("Assets/Add Favotite %#g", priority = 500)]
    private static void AddFavorite()
    {
        FavoriteList list = Resources.Load<FavoriteList>("FavoriteList");
        if (list.Favorites.Contains( Selection.activeObject ))
            list.Favorites.Remove( Selection.activeObject );
        else
            list.Favorites.Add( Selection.activeObject );
        EditorUtility.SetDirty(list);
        FavoriteListWindow.Init();
    }

    void OnGUI()
    {
        var width = 200f;
        var rectPart1 = width* 0.15f;
        var rectPart2 = width * 0.2f;
        var rectPart3 = width * 0.1f;     

        if (favoriteList == null)
        {
            FavoriteListWindow window = (FavoriteListWindow)EditorWindow.GetWindow(typeof(FavoriteListWindow));
            window.favoriteList = Resources.Load<FavoriteList>("FavoriteList");
        }
        if (favoriteList.Favorites == null) favoriteList.Favorites = new List<Object>();

        _scroll = GUILayout.BeginScrollView( _scroll, false, false, GUILayout.Width( position.width ) );

        var serObj = new SerializedObject( favoriteList );
        var arr = serObj.FindProperty( "Favorites" );

        GUILayout.BeginHorizontal();
        for (int i = 0; i < favoriteList.Favorites.Count; )
        {
            var item = favoriteList.Favorites[i];

            if (GUILayout.Button("S", GUILayout.MinWidth(rectPart1), GUILayout.MaxWidth(rectPart1)))
            {
                Selection.activeObject = item;
            }

            EditorGUI.BeginDisabledGroup( true );
            EditorGUILayout.ObjectField( arr.GetArrayElementAtIndex( i ), GUIContent.none, GUILayout.MinWidth( width * 0.6f ), GUILayout.MaxWidth(width * 0.6f) );
            EditorGUI.EndDisabledGroup();

            if (GUILayout.Button("X", GUILayout.MinWidth(rectPart3), GUILayout.MaxWidth(rectPart3)))
            {
                favoriteList.RemoveObj(item);
            }  else
            {
                GUILayout.Space( 25f );
                i++;
            }
        }
        GUILayout.EndHorizontal();
        GUILayout.EndScrollView();
    }
}
