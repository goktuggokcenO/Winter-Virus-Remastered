using Unity.IntegerTime;
using UnityEngine;

public class AdManager : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(true);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                Destroy(gameObject);
            }
        }
    }
}
