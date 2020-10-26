using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectsPool
{
	private GameObject obj;
	private Transform parent;
	private List<GameObject> objects;

	public void Initialize (GameObject prefab, int amount, Transform p) 
    {		
		obj = prefab;
		parent = p;
		objects = new List<GameObject>();
        for(int i = 0; i < amount; i++)
            AddObject();
	}

    private void AddObject() 
    {
		GameObject temp = GameObject.Instantiate(obj.gameObject, parent);
		temp.name = obj.name;
		//temp.transform.SetParent(parent);
		objects.Add(temp);
		temp.SetActive(false);
    }

	public GameObject GetObject () 
    {
	    foreach (var obj in objects)
	    {
		    if (!obj.gameObject.activeSelf)
			    return obj;
	    }
		GameObject g = objects[0];
		objects.RemoveAt(0);
		objects.Add(g);
		return objects[objects.Count - 1];
	}
}
