using Aritiafel.Artifacts.Calculator;
using Aritiafel.Artifacts.TinaValidator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TinvaValidatorTest
{
    [TestClass]
    public class MainTest
    {
        public const string SaveLoadPath = @"C:\Programs\Standard\TinaValidator\TinaValidator\TestArea\SaveLoad";
        public const string NumberFilePath = @"C:\Programs\Standard\TinaValidator\TinaValidator\TestArea\Number File";

        public TestContext TestContext { get; set; }

        [TestMethod]
        public void TestParse()
        {
            ValidateLogic VL = new ValidateLogic(new Status());
            UnitSet us = new UnitSet(CharUnits.AtoZ);
            us.Units.Add(CharUnits.atoz);
            VL.InitialStatus.Choices.Add(new Choice(us));
            us.NextNode = EndNode.Instance;

            string testString = "DJ";

            TinaValidator tv = new TinaValidator(VL);
            bool result = tv.Validate(testString.Select(m => (object)m).ToArray());
            TestContext.WriteLine(result.ToString());
            TestContext.WriteLine(tv.CreateRandomToString());
        }

        [TestMethod]
        public void FirstTest()
        {
            ValidateLogic VL = new ValidateLogic(new Status());
            DeclareVariableStatement dvs = new DeclareVariableStatement("Times", typeof(INumber));
            SetVariableStatement svs = new SetVariableStatement(new LongVar("Times"), new LongConst(0));
            Execute initialEx = new Execute();
            initialEx.Statements.Add(dvs);
            initialEx.Statements.Add(svs);
            SetVariableStatement svs2 = new SetVariableStatement(new LongVar("Times"),
                new ArithmeticExpression(new LongVar("Times"), null, Operator.PlusOne));
            Execute ex2 = new Execute(svs2);
            CompareExpression AtLeast2 = new CompareExpression(new LongVar("Times"), new LongConst(2), Operator.GreaterThanOrEqualTo);
            Area ar1 = new Area(null, new Status(), VL);
            VL.Areas.Add(ar1);
            AreaStart ap1 = new AreaStart(ar1, null, new Status());

            VL.InitialStatus.Choices.Add(new Choice(initialEx));
            initialEx.NextNode = ap1;

            CharsToIntegerPart stip = new CharsToIntegerPart();
            ar1.InitialStatus.Choices.Add(new Choice(stip));
            UnitSet us1 = new UnitSet(CharUnits.Comma);
            us1.Units.Add(CharUnits.WhiteSpace);
            stip.NextNode = us1;
            CharsToIntegerPart stip2 = new CharsToIntegerPart();
            us1.NextNode = stip2;
            UnitSet us2 = new UnitSet(CharUnits.WhiteSpace);
            stip2.NextNode = us2;
            CharsToIntegerPart stip3 = new CharsToIntegerPart();
            us2.NextNode = stip3;
            stip3.NextNode = EndNode.Instance;

            UnitSet us3 = " CH".ToUnitSet();
            us3.Units.Add(CharUnits.AtoZ);
            (ap1.NextNode as Status).Choices.Add(new Choice(us3));

            us3.NextNode = new Status();
            UnitSet CRLF = "\r\n".ToUnitSet();
            (us3.NextNode as Status).Choices.Add(new Choice(CRLF));
            (us3.NextNode as Status).Choices.Add(new Choice(EndNode.Instance, AtLeast2));
            CRLF.NextNode = ex2;
            ex2.NextNode = ap1;
            //12, 56 70 CHA
            //08, 32 45 CHR
            //98, -3 45 CHD

            TinaValidator validator = new TinaValidator(VL);
            //
            //Run Start
            //TestContext.WriteLine(VL.Save(""));
            //return;
            bool result;            
            string[] files = Directory.GetFiles(NumberFilePath);
            for (int i = 0; i < files.Length; i++)
            {
                using (FileStream fs = new FileStream(files[i], FileMode.Open))
                {
                    using StreamReader sr = new StreamReader(fs);
                    string s = sr.ReadToEnd();
                    List<object> thing = s.ToObjectList();
                    result = validator.Validate(thing);
                }
                if (i == 0)
                    Assert.IsTrue(result);
                else
                    Assert.IsFalse(result);
            }

            for (int i = 0; i < 100; i++)
            {
                List<object> list = validator.CreateRandom();
                TestContext.WriteLine(list.ForEachToString());
                result = validator.Validate(list);
                TestContext.WriteLine("");
                Assert.IsTrue(result);
            }

            VL.Save(Path.Combine(SaveLoadPath, "Main1.json"));
            return;
        }

        [TestMethod]
        public void JsonTest()
        {
            ValidateLogic VL = new ValidateLogic(new Status());            
            Area skipChars = new Area("SkipArea", new Status(), null);            
            Area objectArea = new Area("ObjectArea", new Status(), null);
            Area arrayArea = new Area("ArrayArea", new Status(), null);
            Area valueArea = new Area("ValueArea", new Status(), null);            
            Area propertiesArea =  new Area("PropertiesArea", new Status(), null);
            VL.Areas.Add(skipChars);
            VL.Areas.Add(objectArea);
            VL.Areas.Add(arrayArea);
            VL.Areas.Add(valueArea);
            VL.Areas.Add(propertiesArea);

            UnitSet us = new UnitSet(CharUnits.WhiteSpace, skipChars);
            UnitSet us2 = new UnitSet(CharUnits.CarriageReturn, skipChars);
            UnitSet us3 = new UnitSet(CharUnits.LineFeed, skipChars);
            UnitSet us4 = new UnitSet(CharUnits.HorizontalTab, skipChars);
            skipChars.InitialStatus.Choices.Add(new Choice(us));
            skipChars.InitialStatus.Choices.Add(new Choice(us2));
            skipChars.InitialStatus.Choices.Add(new Choice(us3));
            skipChars.InitialStatus.Choices.Add(new Choice(us4));
            skipChars.InitialStatus.Choices.Add(Choice.EndChoice);
            us.NextNode = us2.NextNode = us3.NextNode = us4.NextNode = skipChars.InitialStatus;

            UnitSet leftCurlBracket = new UnitSet(CharUnits.LeftCurlyBracket);
            UnitSet rightCurlBracket = new UnitSet(CharUnits.RightCurlyBracket);
            AreaStart skSt = new AreaStart(skipChars, objectArea);
            objectArea.InitialStatus.Choices.Add(new Choice(leftCurlBracket));            
            AreaStart paSt = new AreaStart(propertiesArea, objectArea);            
            leftCurlBracket.NextNode = skSt;
            skSt.NextNode = paSt;
            skSt = new AreaStart(skipChars, objectArea);
            paSt.NextNode = skSt;
            skSt.NextNode = rightCurlBracket;
            rightCurlBracket.NextNode = EndNode.Instance;

            UnitSet leftSquareBracket = new UnitSet(CharUnits.LeftSquareBracket);
            UnitSet rightSquareBracket = new UnitSet(CharUnits.RightSquareBracket);
            arrayArea.InitialStatus.Choices.Add(new Choice(leftSquareBracket));            
            skSt = new AreaStart(skipChars, arrayArea);
            leftSquareBracket.NextNode = skSt;            
            AreaStart vaSt = new AreaStart(valueArea, arrayArea);
            skSt.NextNode = vaSt;
            skSt = new AreaStart(skipChars, arrayArea);
            vaSt.NextNode = skSt;            
            Status st1 = new Status(null, arrayArea);            
            skSt.NextNode = st1;
            UnitSet us5 = new UnitSet(CharUnits.Comma, arrayArea);
            st1.Choices.Add(new Choice(us5));
            st1.Choices.Add(new Choice(rightSquareBracket));
            skSt = new AreaStart(skipChars, arrayArea);
            us5.NextNode = skSt;
            skSt.NextNode = vaSt;
            rightSquareBracket.NextNode = EndNode.Instance;
            

            CharsToBooleanPart cbp = new CharsToBooleanPart();
            cbp.Parent = valueArea;
            CharsToDoublePart cdp = new CharsToDoublePart();
            cdp.Parent = valueArea;
            CharsToIntegerPart cip = new CharsToIntegerPart();
            cip.Parent = valueArea;
            AnyStringPart asp1 = new AnyStringPart(null, valueArea, null, new List<char> { '\"' }, 0, 0);
            AnyStringPart asp2 = new AnyStringPart(null, valueArea, null, new List<char> { '\\' }, 0, 0);
            UnitSet us6 = new UnitSet(CharUnits.QuotationMark, valueArea);
            valueArea.InitialStatus.Choices.Add(new Choice("null".ToUnitSet()));
            valueArea.InitialStatus.Choices.Add(new Choice(cbp));
            valueArea.InitialStatus.Choices.Add(new Choice(cip));
            valueArea.InitialStatus.Choices.Add(new Choice(cdp));            
            Status st2 = new Status("va_st2", valueArea);
            us6.NextNode = st2;
            st2.Choices.Add(new Choice(asp2));
            st2.Choices.Add(new Choice(asp1));
            Status st3 = new Status("va_st3", valueArea);
            asp2.NextNode = st3;
            st3.Choices.Add(new Choice("\\\"".ToUnitSet()));
            UnitSet us11 = new UnitSet(CharUnits.BackSlash);
            us11.Units.Add(new CharUnit());
            st3.Choices.Add(new Choice(us11));
            st3.Choices[0].Node.NextNode = st2;
            st3.Choices[1].Node.NextNode = st2;
            asp1.NextNode = "\"".ToUnitSet();
            asp1.NextNode.NextNode = EndNode.Instance;
            AreaStart oaSt = new AreaStart(objectArea, valueArea);
            valueArea.InitialStatus.Choices.Add(new Choice(oaSt));
            AreaStart arSt = new AreaStart(arrayArea, valueArea);
            //valueArea.InitialStatus.Choices.Add(new Choice(arSt));
            for (int i = 0; i < valueArea.InitialStatus.Choices.Count; i++)
                valueArea.InitialStatus.Choices[i].Node.NextNode = EndNode.Instance;
            valueArea.InitialStatus.Choices.Add(new Choice(us6));

            //Properties Area
            UnitSet us7 = new UnitSet("us7", propertiesArea, CharUnits.QuotationMark);
            propertiesArea.InitialStatus.ID = "us7_I";
            propertiesArea.InitialStatus.Choices.Add(new Choice(us7));            
            asp1 = new AnyStringPart(null, propertiesArea, null, new List<char> { '\"' }, 0, 0);
            asp2 = new AnyStringPart(null, propertiesArea, null, new List<char> { '\\' }, 0, 0);
            st2 = new Status("pa_st2", propertiesArea);
            us7.NextNode = st2;
            st2.Choices.Add(new Choice(asp2));
            st2.Choices.Add(new Choice(asp1));
            st3 = new Status("pa_st3", propertiesArea);
            asp2.NextNode = st3;
            st3.Choices.Add(new Choice("\\\"".ToUnitSet()));
            UnitSet us10 = new UnitSet(CharUnits.BackSlash);
            us10.Units.Add(new CharUnit());
            st3.Choices.Add(new Choice(us10));
            st3.Choices[0].Node.NextNode = st2;
            st3.Choices[1].Node.NextNode = st2;
            skSt = new AreaStart(skipChars, propertiesArea);
            asp1.NextNode = "\"".ToUnitSet();
            asp1.NextNode.NextNode = skSt;
            UnitSet us8 = new UnitSet(CharUnits.Colon, propertiesArea);
            skSt.NextNode = us8;
            skSt = new AreaStart(skipChars, propertiesArea);
            us8.NextNode = skSt;
            vaSt = new AreaStart(valueArea, propertiesArea);
            skSt.NextNode = vaSt;
            Status st4 = new Status("pa_st4");
            vaSt.NextNode = st4;
            st4.Choices.Add(new Choice(EndNode.Instance));
            skSt = new AreaStart(skipChars, propertiesArea);
            st4.Choices.Add(new Choice(skSt));
            UnitSet us9 = new UnitSet(CharUnits.Comma, propertiesArea);
            skSt.NextNode = us9;
            skSt = new AreaStart(skipChars, propertiesArea);
            us9.NextNode = skSt;
            skSt.NextNode =  propertiesArea.InitialStatus;

            //Start Main
            AreaStart ap1 = new AreaStart(skipChars, VL);
            VL.InitialStatus.Choices.Add(new Choice(ap1));
            Status JsonStartStatus = new Status(null, VL);
            ap1.NextNode = JsonStartStatus;            
            AreaStart ap2 = new AreaStart(propertiesArea, null);            
            AreaStart ap3 = new AreaStart(skipChars, VL);
            ap2.NextNode = ap3;
            ap3.NextNode = EndNode.Instance;
            JsonStartStatus.Choices.Add(new Choice(ap2));

            TinaValidator validator = new TinaValidator(VL);
            VL.Save(Path.Combine(SaveLoadPath, "JSONTest.json"));
            for (int i = 0; i < 1000; i++)
            {
                List<object> ol = validator.CreateRandom();                
                if (!validator.Validate(ol))
                {
                    TestContext.WriteLine("Wrong happen: " + i);
                    TestContext.WriteLine(ol.ForEachToString());

                    TestContext.WriteLine("TotalObjectCount:" + ol.Count);
                    TestContext.WriteLine("Error Node:" + validator.ErrorNode.ID);
                    TestContext.WriteLine(validator.ErrorNode.GetType().Name);
                    TestContext.WriteLine("Error Location:" + validator.LongerErrorLocation);
                    bool second = validator.Validate(ol);
                }
            }
            TestContext.WriteLine("End");            
        }

        [TestMethod]
        public void SaveTest()
        {

        }
    }
}
