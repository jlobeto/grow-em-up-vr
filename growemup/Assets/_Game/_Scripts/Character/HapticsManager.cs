using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HapticsManager : MonoBehaviour
{
    [SerializeField] private ActionBasedController rightXRController;
    [SerializeField] private ActionBasedController leftXRController;

    public void DoHaptic(bool isRightController, float force, float duration = .1f)
    {
        bool result;
        
        if (isRightController)
        {
            result = rightXRController.SendHapticImpulse(force, duration);
        }
        else
        {
            result = leftXRController.SendHapticImpulse(force, duration);
        }
        
        Debug.Log(result);
    }
}
