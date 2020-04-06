using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    private List<Enemy> myCollidingObjects = new List<Enemy>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy e = collision.GetComponent<Enemy>();
        if(e != null && !myCollidingObjects.Contains(e))
        {
            myCollidingObjects.Add(e);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Enemy e = collision.GetComponent<Enemy>();
        if (e != null && myCollidingObjects.Contains(e))
        {
            myCollidingObjects.Remove(e);
        }
    }

    public List<Enemy> GetCollidingObjects()
    {
        return myCollidingObjects;
    }
}
