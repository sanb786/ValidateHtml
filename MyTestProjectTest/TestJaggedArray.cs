using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyTestProject;

namespace MyTestProjectTest
{
    [TestClass]
    public class TestJaggedArray
    {
        [TestMethod]
        public void TestFlatternedJaggedArray()
        {
            var testjArray = new FlatternJaggedArray();

            //[[1,2,[3]],4]
            var arr = new object[] { new object[] { 1, 2, new int[] { 3 } }, 4 };
            int[] expectedResult = { 1, 2, 3, 4 };
            testjArray.FlatternNested(arr);
            var actual = testjArray.Result;
            CollectionAssert.AreEqual(expectedResult, actual);
        }
    }
}
