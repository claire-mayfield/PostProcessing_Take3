using UnityEngine;
using System.Collections;

public class ReactiveTarget : MonoBehaviour
{
    [SerializeField] float _defeatAnimTime = 1.5f;

    public void ReactToHit()
    {
        WanderingAI navBehavior = GetComponent<WanderingAI>();

        if (navBehavior != null)
            navBehavior.IsAlive = false;

        StartCoroutine(DefeatAnim());
      
    }

    private IEnumerator DefeatAnim()
    {
        float elapsedTime = 0.0f;

        Quaternion initRotation = transform.rotation;
        Quaternion endRotation = transform.rotation * Quaternion.Euler(-75.0f, 0.0f, 0.0f);

        while (elapsedTime < _defeatAnimTime)
        {
            // Interpolate between A and B based on Alpha:
            // A = initRotation
            // B = endRotation
            // Alpha = timer

            transform.rotation = Quaternion.Lerp(
                initRotation, endRotation, elapsedTime / _defeatAnimTime);

            elapsedTime += Time.deltaTime;

            yield return null; // skip frame
        }

        // Make sure we make it to the destination

        transform.rotation = endRotation;

        yield return new WaitForSeconds(1.0f);

        Destroy(gameObject);

    }
}
