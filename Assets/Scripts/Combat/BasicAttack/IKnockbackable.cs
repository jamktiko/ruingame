using System.Collections;
using UnityEngine;
public interface IKnockbackable
{
    void HandleKnockBack(Vector3 target, float force);
    IEnumerator KnockbackReset();
}
