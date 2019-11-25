using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Favorite List", menuName = "Kuupu/Favotite List", order = 1)]
public class FavoriteList : ScriptableObject
{
    [SerializeField] public List<Object> Favorites = new List<Object>();

    public void RemoveObj(Object objToRemove)
    {
        Favorites.Remove(objToRemove);
    }
}
