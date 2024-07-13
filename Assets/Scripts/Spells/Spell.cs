using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/TestSpell")]
public class Spell : ScriptableObject
{
    public IEnumerator Activate()
    {
        var timer = 0f;
        while (timer < 10)
        {
            Debug.Log(timer);
            timer += Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator SetDirection()
    {
        yield return null;
    }
}
