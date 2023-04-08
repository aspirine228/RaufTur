using UnityEngine;

public class StarScript : MonoBehaviour
{
    public GameObject star;

    void Start()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = star.GetComponent<SpriteRenderer>().sprite;
    }
}
