using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerTest
{
    
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator PlayerTestWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        GameObject player = new GameObject();
        player.AddComponent<Mover>();
        
        yield return new WaitForSeconds(1);
      
        Assert.AreEqual(player.GetComponent<Mover>().gravity, -9.81f);


    }
}
