using Aritiafel.Artifacts.Calculator;
using Aritiafel.Artifacts.TinaValidator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace TinvaValidatorTest
{
    public partial class PartUnitTest
    {
        [TestMethod]
        public void CharsToBooleanUnitTest()
        {
            FakeVariableLinker fvl = new FakeVariableLinker();

            CharsToBooleanPart ctbp = new CharsToBooleanPart();
            Assert.IsTrue(ctbp.Validate("trUe".ToObjectList(), 0, fvl) == 4);
            Assert.IsTrue(ctbp.Validate("faLSe".ToObjectList()) == 5);
            Assert.IsTrue(ctbp.Validate("faLse458".ToObjectList()) == 5);
            Assert.IsTrue(ctbp.Validate("faLde".ToObjectList()) == -1);
            Assert.IsTrue(ctbp.Validate(new List<ObjectConst> { new LongConst(3) }) == -1);
            Assert.IsTrue(ctbp.Validate(ctbp.Random()) != -1);
            Assert.IsTrue(ctbp.Validate(ctbp.Random()) != -1);
            TestContext.WriteLine(ctbp.Random().ForEachToString());
            TestContext.WriteLine(ctbp.Random().ForEachToString());
            ctbp = new CharsToBooleanPart(true);
            Assert.IsTrue(ctbp.Validate("TRUe".ToObjectList()) == 4);
            Assert.IsTrue(ctbp.Validate("FalSE".ToObjectList()) == -1);
            Assert.IsTrue(ctbp.Validate(ctbp.Random()) != -1);
            Assert.IsTrue(ctbp.Validate(ctbp.Random()) != -1);
            TestContext.WriteLine(ctbp.Random().ForEachToString());
            TestContext.WriteLine(ctbp.Random().ForEachToString());

            ctbp = new CharsToBooleanPart(false);
            Assert.IsTrue(ctbp.Validate("TRUe".ToObjectList()) == -1);
            Assert.IsTrue(ctbp.Validate("dfalSE".ToObjectList()) == -1);
            Assert.IsTrue(ctbp.Validate("FalSepo".ToObjectList()) == 5);
            Assert.IsTrue(ctbp.Validate(ctbp.Random()) != -1);
            Assert.IsTrue(ctbp.Validate(ctbp.Random()) != -1);
            TestContext.WriteLine(ctbp.Random().ForEachToString());
            TestContext.WriteLine(ctbp.Random().ForEachToString());
        }


        [TestMethod]
        public void CharsToIntegerPartUnitTest()
        {
            CharsToIntegerPart ctip = new CharsToIntegerPart();
            Assert.IsTrue(ctip.Validate("13654835".ToObjectList()) == 8);
            Assert.IsTrue(ctip.Validate("135".ToObjectList()) == 3);
            Assert.IsTrue(ctip.Validate("136d563".ToObjectList()) == 3);
            Assert.IsTrue(ctip.Validate("-1755535dd".ToObjectList()) == 8);
            Assert.IsTrue(ctip.Validate("56.2365".ToObjectList()) == 2);
            Assert.IsTrue(ctip.Validate(new List<ObjectConst> { new LongConst(7) }) == -1);
            Assert.IsTrue(ctip.Validate("7315678649889876587".ToObjectList()) == 19);
            Assert.IsTrue(ctip.Validate("1897315678649889879324".ToObjectList()) == -1);
            Assert.IsTrue(ctip.Validate("-7315678649889876587".ToObjectList()) == 20);
            Assert.IsTrue(ctip.Validate("-9988671899889876587".ToObjectList()) == -1);
            Assert.IsTrue(ctip.Validate(ctip.Random()) != -1);
            Assert.IsTrue(ctip.Validate(ctip.Random()) != -1);
            TestContext.WriteLine(ctip.Random().ForEachToString());
            TestContext.WriteLine(ctip.Random().ForEachToString());
            TestContext.WriteLine(ctip.Random().ForEachToString());
            ctip = new CharsToIntegerPart(203587631978);
            Assert.IsTrue(ctip.Validate("203587631978drd".ToObjectList()) == 12);
            Assert.IsTrue(ctip.Validate("203587631979".ToObjectList()) == -1);
            Assert.IsTrue(ctip.Validate("203587631977".ToObjectList()) == -1);
            Assert.IsTrue(ctip.Validate(ctip.Random()) != -1);
            Assert.IsTrue(ctip.Validate(ctip.Random()) != -1);
            TestContext.WriteLine(ctip.Random().ForEachToString());
            TestContext.WriteLine(ctip.Random().ForEachToString());
            ctip = new CharsToIntegerPart(-25549, 5678913);
            Assert.IsTrue(ctip.Validate("0658d".ToObjectList()) == 4);
            Assert.IsTrue(ctip.Validate("-12253".ToObjectList()) == 6);
            Assert.IsTrue(ctip.Validate("-37253".ToObjectList()) == -1);
            Assert.IsTrue(ctip.Validate(ctip.Random()) != -1);
            Assert.IsTrue(ctip.Validate(ctip.Random()) != -1);
            TestContext.WriteLine(ctip.Random().ForEachToString());
            TestContext.WriteLine(ctip.Random().ForEachToString());
            TestContext.WriteLine(ctip.Random().ForEachToString());
            ctip = new CharsToIntegerPart(6895663, -57661);
            Assert.ThrowsException<ArgumentException>(() => ctip.Random());
        }

        [TestMethod]
        public void CharsToDoublePartUnitTest()
        {
            CharsToDoublePart ctdp = new CharsToDoublePart();
            Assert.IsTrue(ctdp.Validate("16.545935r".ToObjectList()) == 9);
            Assert.IsTrue(ctdp.Validate("0135.21.58s".ToObjectList()) == 7);
            Assert.IsTrue(ctdp.Validate("136d5.63".ToObjectList()) == 3);
            Assert.IsTrue(ctdp.Validate("-17.88535dd".ToObjectList()) == 9);
            Assert.IsTrue(ctdp.Validate("ssr1.2365".ToObjectList()) == -1);
            Assert.IsTrue(ctdp.Validate(new List<ObjectConst> { new DoubleConst(15) }) == -1);
            Assert.IsTrue(ctdp.Validate(".2365".ToObjectList()) == 5);
            string s = string.Concat("0.", new string('5', 400));
            Assert.IsTrue(ctdp.Validate(s.ToObjectList()) == 330);
            s = string.Concat("-0.", new string('7', 300));
            Assert.IsTrue(ctdp.Validate(s.ToObjectList()) == 303);
            Assert.IsTrue(ctdp.Validate(ctdp.Random()) != -1);
            Assert.IsTrue(ctdp.Validate(ctdp.Random()) != -1);
            TestContext.WriteLine(ctdp.Random().ForEachToString());
            TestContext.WriteLine(ctdp.Random().ForEachToString());
            TestContext.WriteLine(ctdp.Random().ForEachToString());
            ctdp = new CharsToDoublePart(811.568478);
            Assert.IsTrue(ctdp.Validate("811.568478.324".ToObjectList()) == 10);
            Assert.IsTrue(ctdp.Validate("811.568479".ToObjectList()) == -1);
            Assert.IsTrue(ctdp.Validate("811.568477".ToObjectList()) == -1);
            Assert.IsTrue(ctdp.Validate(ctdp.Random()) != -1);
            Assert.IsTrue(ctdp.Validate(ctdp.Random()) != -1);
            TestContext.WriteLine(ctdp.Random().ForEachToString());
            TestContext.WriteLine(ctdp.Random().ForEachToString());
            ctdp = new CharsToDoublePart(-788.49, 96633.12);
            Assert.IsTrue(ctdp.Validate("001264d".ToObjectList()) == 6);
            Assert.IsTrue(ctdp.Validate("96633.15".ToObjectList()) == -1);
            Assert.IsTrue(ctdp.Validate("-788.50".ToObjectList()) == -1);
            Assert.IsTrue(ctdp.Validate(ctdp.Random()) != -1);
            Assert.IsTrue(ctdp.Validate(ctdp.Random()) != -1);
            TestContext.WriteLine(ctdp.Random().ForEachToString());
            TestContext.WriteLine(ctdp.Random().ForEachToString());
            TestContext.WriteLine(ctdp.Random().ForEachToString());

            ctdp = new CharsToDoublePart(81.3, -726.5);
            Assert.ThrowsException<ArgumentException>(() => ctdp.Random());

            ctdp = new CharsToDoublePart();
            Assert.IsTrue(ctdp.Validate("2.3001810487634357E-21".ToObjectList()) == 22);
            Assert.IsTrue(ctdp.Validate("2.3001810487635857E+125".ToObjectList()) == 23);
            Assert.IsTrue(ctdp.Validate("2.3009818107634357E+3".ToObjectList()) == 21);
            Assert.IsTrue(ctdp.Validate("-6.3009818507634357E-35".ToObjectList()) == 23);
            Assert.IsTrue(ctdp.Validate("8.28037438864823E-50".ToObjectList()) == 20);
            Assert.IsTrue(ctdp.Validate("NaN".ToObjectList()) == -1);

            for (int i = 0; i < 5000; i++)
                if (ctdp.Random().ForEachToString().Contains("NaN"))
                    TestContext.WriteLine($"{i}:NaN");
        }
    }
}
