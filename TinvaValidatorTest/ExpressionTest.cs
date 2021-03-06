using Aritiafel.Artifacts.Calculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;

namespace TinvaValidatorTest
{
    [TestClass]
    public class ExpressionTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void ConstCalculate()
        {
            LongConst a = new LongConst(3);
            LongConst b = new LongConst(5);
            DoubleConst c = new DoubleConst(7.5);
            ArithmeticExpression ae = new ArithmeticExpression(a, b);
            ArithmeticExpression ae2 = new ArithmeticExpression(ae, c, Operator.Multiply);
            Assert.IsTrue(ae2.GetResult(null).ToString() == "60");
        }

        //"IntA": 60;
        //"IntB": -5;
        //"IntC": 999990;
        //"DoubleA": 2.55;
        //"DoubleB": 7.8;
        //"DoubleC": -65.237819;
        //"True"
        //"False"

        [TestMethod]
        public void VariableAndConst()
        {
            FakeVariableLinker fvl = new FakeVariableLinker();
            DoubleVar d1 = new DoubleVar(FakeVariableLinker.DoubleA);
            LongVar i1 = new LongVar(FakeVariableLinker.IntA);
            LongConst a = new LongConst(300);
            DoubleConst b = new DoubleConst(20.7);
            ArithmeticExpression ae = new ArithmeticExpression(a, i1);
            ArithmeticExpression ae2 = new ArithmeticExpression(ae, d1, Operator.Multiply);
            ArithmeticExpression ae3 = new ArithmeticExpression(ae2, b, Operator.Minus);
            Assert.IsTrue(ae3.GetResult(fvl).ToString() == "897.3");
        }


        [TestMethod]
        public void BooleanExpression()
        {
            FakeVariableLinker fvl = new FakeVariableLinker();
            BooleanExpression be = new BooleanExpression(BooleanConst.True, BooleanConst.False);
            BooleanExpression be2 = new BooleanExpression(new BooleanVar(FakeVariableLinker.True), be, Operator.And);
            BooleanExpression be3 = new BooleanExpression(be2, null, Operator.Not);
            Assert.IsFalse(be3.GetResult(fvl).Value);
        }


        [TestMethod]
        public void CompareExpression()
        {
            FakeVariableLinker fvl = new FakeVariableLinker();

            LongConst a = new LongConst(0304);
            NumberConst b = new DoubleConst(56.8);
            ObjectConst c = new DoubleConst(690.8);
            Assert.IsFalse((a < b).Value);
            Assert.IsTrue((a < c).Value);
            CompareExpression ce = new CompareExpression(new LongConst(30), new LongConst(30));
            Assert.IsTrue(ce.GetResult(fvl).Value);
            CompareExpression ce2 = new CompareExpression(new LongConst(30), new LongVar(FakeVariableLinker.IntA));
            Assert.IsFalse(ce2.GetResult(fvl).Value);
            ArithmeticExpression ae = new ArithmeticExpression(new LongConst(30), new DoubleConst(30d));
            CompareExpression ce3 = new CompareExpression(ae, new LongVar(FakeVariableLinker.IntA));
            Assert.IsTrue(ce3.GetResult(fvl).Value);
            CompareExpression ce4 = new CompareExpression(new LongConst(40), new DoubleConst(30), Operator.GreaterThan);
            Assert.IsTrue(ce4.GetResult(fvl).Value);
            CompareExpression ce5 = new CompareExpression(new BooleanConst(true), new DoubleConst(30), Operator.GreaterThan);
            Assert.ThrowsException<ArithmeticException>(() => ce5.GetResult(fvl));
            CompareExpression ce6 = new CompareExpression(new LongConst(20), new DoubleConst(30), Operator.LessThan);
            Assert.IsTrue(ce6.GetResult(fvl).Value);
            CompareExpression ce7 = new CompareExpression(new LongConst(20), new DoubleConst(20), Operator.LessThanOrEqualTo);
            Assert.IsTrue(ce7.GetResult(fvl).Value);
            CompareExpression ce8 = new CompareExpression(new DoubleVar(FakeVariableLinker.DoubleA), new DoubleConst(2.55), Operator.NotEqualTo);
            Assert.IsFalse(ce8.GetResult(fvl).Value);
        }

        [TestMethod]
        public void StringExpression()
        {
            FakeVariableLinker fvl = new FakeVariableLinker();
            StringExpression se = new StringExpression(new StringConst('a'), new StringConst("bbbb"));
            Assert.IsTrue(se.GetResult(fvl).Value == "abbbb");
            StringExpression se2 = new StringExpression(new StringConst("add"), new StringConst("__bb"));
            Assert.IsTrue(se2.GetResult(fvl).Value == "add__bb");
            StringExpression se3 = new StringExpression(new ConvertToStringExpression(new DoubleVar(FakeVariableLinker.DoubleB)), new StringConst("bb"));
            Assert.IsTrue(se3.GetResult(fvl).Value == "7.8bb");
        }

        [TestMethod]
        public void JsonSave()
        {
            DoubleVar d1 = new DoubleVar(FakeVariableLinker.DoubleA);
            LongVar i1 = new LongVar(FakeVariableLinker.IntA);
            LongConst a = new LongConst(300);
            DoubleConst b = new DoubleConst(20.7);
            ArithmeticExpression ae = new ArithmeticExpression(a, i1);
            ArithmeticExpression ae2 = new ArithmeticExpression(ae, d1, Operator.Multiply);
            ArithmeticExpression ae3 = new ArithmeticExpression(ae2, b, Operator.Minus);
            TestContext.WriteLine(JsonConvert.SerializeObject(ae3));
        }

        [TestMethod]
        public void ExactlyDivideTest()
        {
            FakeVariableLinker fvl = new FakeVariableLinker();
            DoubleVar d1 = new DoubleVar(FakeVariableLinker.DoubleA);
            LongVar i1 = new LongVar(FakeVariableLinker.IntA);
            LongConst a = new LongConst(300);
            LongConst c = new LongConst(603);
            DoubleConst b = new DoubleConst(20.7);

            ArithmeticExpression ae = new ArithmeticExpression(a, i1, Operator.ExactlyDivide);
            Assert.IsTrue(ae.GetResult(fvl).ToString() == "5");
            ae = new ArithmeticExpression(c, a, Operator.ExactlyDivide);
            Assert.IsTrue(ae.GetResult(fvl).ToString() == "2");
            ae = new ArithmeticExpression(c, a, Operator.Divide);
            Assert.IsTrue(ae.GetResult(fvl).ToString() == "2.01");
            ae = new ArithmeticExpression(c, b, Operator.ExactlyDivide);
            Assert.IsTrue(ae.GetResult(fvl).ToString() == "29");
            ae = new ArithmeticExpression(c, a, Operator.Remainder);
            Assert.IsTrue(ae.GetResult(fvl).ToString() == "3");
            ae = new ArithmeticExpression(c, b, Operator.Remainder);
            Assert.IsTrue(ae.GetResult(fvl).ToString() == (603 % 20.7).ToString());
            TestContext.WriteLine(ae.GetResult(fvl).ToString()); // Scan
            ae = new ArithmeticExpression(c, new DoubleConst(0), Operator.Divide);
            Assert.ThrowsException<DivideByZeroException>(() => ae.GetResult(fvl));
            ae = new ArithmeticExpression(c, new DoubleConst(15), Operator.Divide);
            Assert.IsTrue(ae.GetResult(fvl).ToString() == "40.2");

            //TestContext.WriteLine(ae.GetResult(fvl).ToString());

        }

        [TestMethod]
        public void RemainderTest()
        {
            TestContext.WriteLine((6.7 % 3.1).ToString());
            TestContext.WriteLine((713 % 32).ToString());
            TestContext.WriteLine((6 % 3.1d).ToString());
            TestContext.WriteLine((603 % 20.7).ToString());

        }
    }
}
