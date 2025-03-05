using UnityEngine;

public abstract class PickUp : MonoBehaviour
{
    public GameObject[] PickUpFeedbacks;

    public LayerMask TargetLayerMask;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!((TargetLayerMask.value & (1 << collision.gameObject.layer)) > 0))
            return;

        PickedUp(collision);

        foreach (var feedback in PickUpFeedbacks)
        {
            GameObject.Instantiate(feedback, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }


    protected virtual void PickedUp(Collider2D collision)
    {

    }
}
