using Nitzz.Utility;
using UnityEngine;

public class CollectItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManage.instance._scrCollect.allCollect.Remove(gameObject);
            Destroy(gameObject);
            AudioManager.instance.PlaySFX("collect");
            GameManage.instance.CollectItem();
        }
    }
}
