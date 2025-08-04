using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cache 
{
    private static Dictionary<Collider2D, Cat> cat = new Dictionary<Collider2D, Cat>();

    public static Cat GetCat(Collider2D collider)
    {
        if (!cat.ContainsKey(collider))
        {
            cat.Add(collider, collider.GetComponent<Cat>());
        }

        return cat[collider];
    }
}
