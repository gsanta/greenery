using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Test1
{
    // A Test behaves as an ordinary method
    [Test]
    public void Test1SimplePasses()
    {
        Assert.AreEqual(1, 1);
    }
}
