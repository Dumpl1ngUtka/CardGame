using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/TestSpell")]
public class Spell : ScriptableObject
{
    public void StartUse()
    {
        Debug.Log("Start");
    }

    public IEnumerator SetDirection()
    {
        var timer = 0f;
        while (timer < 10)
        {
            Debug.Log(timer);
            timer += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        Debug.Log(timer++);
    }
}
