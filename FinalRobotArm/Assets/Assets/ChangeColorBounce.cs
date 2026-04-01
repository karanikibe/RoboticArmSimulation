using UnityEngine;

public class ChangeColorOnBounce : MonoBehaviour
{
    private Renderer sphereRenderer;

    void Start()
    {
        // Get the Renderer component of the sphere
        sphereRenderer = GetComponent<Renderer>();
    }

    // This function is called when the object collides with something
    void OnCollisionEnter(Collision collision)
    {
        // Change to a random color
        sphereRenderer.material.color = new Color(Random.value, Random.value, Random.value);
    }
}
