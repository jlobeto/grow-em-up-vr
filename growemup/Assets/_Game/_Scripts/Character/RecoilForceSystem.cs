using UnityEngine;

public class RecoilForceSystem : MonoBehaviour
{
    [SerializeField] private bool isRightHand;
    [SerializeField] private Rigidbody attachTransformRB;

    public bool IsRightHand => isRightHand;
    
    public void Recoil(float force)
    {
        attachTransformRB.AddForce( -transform.forward * force, ForceMode.Impulse);
        attachTransformRB.AddTorque( -transform.right * force * 5f, ForceMode.Impulse);
    }
}
