using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class rotationTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void Yrotation()
    {
        Assert.AreEqual(new Vector3(0, 1, 0), Spinner.rotation);
    }

 
}
