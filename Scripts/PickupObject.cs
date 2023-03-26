using UnityEngine;

public class PickupObject : MonoBehaviour
{
    public string pickupMessage = "Picked up an object";
    private PickupTextController pickupTextController;

    void Start()
    {
        GameObject pickupTextControllerObject = GameObject.FindWithTag("PickupTextController");
        if (pickupTextControllerObject != null)
        {
            pickupTextController = pickupTextControllerObject.GetComponent<PickupTextController>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (pickupTextController != null)
            {
                pickupTextController.ShowPickupText(pickupMessage);
            }

            // Add code to handle the object pick up (e.g., add to inventory, destroy object, etc.)
        }
    }
}
