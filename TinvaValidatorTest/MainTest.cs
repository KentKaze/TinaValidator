using Aritiafel.Artifacts.TinaValidator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace TinvaValidatorTest
{
    [TestClass]
    public class MainTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void TestMethod1()
        {
            ValidateLogic th = new ValidateLogic();
            Status st1 = new Status();
            th.InitalStatus = st1;
            Sequence se = new Sequence();
            th.Choices.Add(se);
            Status st = new Status();
            TestContext.WriteLine(se.ToString());

            int a = 5;
            string s = "ssds";
            th.Choices.Add(s.ToUnitSet("a"));
        }
    }
}
