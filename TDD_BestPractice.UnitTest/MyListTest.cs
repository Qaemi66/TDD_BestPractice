using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TDD_Sample.UnitTest
{
    [TestClass]
    public class MyListTest
    {
        [TestMethod]
        public void GetListOfIntItems_CountOfResultMustBeEqualCountParameter()
        {
            var myList = new MyList();
            var result = myList.GetListOfIntItems(5);
            Assert.AreEqual(5, result.Count);
        }
    }
}
