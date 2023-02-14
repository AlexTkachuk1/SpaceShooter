using UnityEngine;

public class TripleShot : MonoBehaviour
{
    void Update()
    {
        GameObject child = gameObject.transform.GetChild(0).gameObject;
        
        if (child == null) Destroy(gameObject);
    }
}
