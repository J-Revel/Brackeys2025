using UnityEngine;

public class Cookie : MonoBehaviour
{
    private void OnDestroy()
    {
        CookieDestructionHandler.instance.on_destruction(transform.position, transform.lossyScale);
    }
}
