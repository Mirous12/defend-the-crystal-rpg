using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int Damage = 1;

    private void OnTriggerEnter2D( Collider2D collision )
    {
        var enemy = collision.gameObject.GetComponent<Enemy>();

        if( enemy != null )
        {
            enemy.SendDamage(Damage);
        }
    }
}
