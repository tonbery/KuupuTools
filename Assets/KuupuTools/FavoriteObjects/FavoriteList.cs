using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FavoriteList : ScriptableObject
{
    [SerializeField] public List<Object> Favorites = new List<Object>();

    public void RemoveObj(Object objToRemove)
    {
        Favorites.Remove(objToRemove);
    }
}
