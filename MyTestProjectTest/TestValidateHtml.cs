using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyTestProject;


namespace MyTestProjectTest
{
    [TestClass]
    public class TestValidateHtml
    {
        private const string html = @"<ins class='ad'></ins>";
        private const string html1 = @"<C><B>is centred and in boldface </B></C>";
        private const string html2 = @"< B > This should be in boldface, but there is an extra closing tag </ B ></ C >";
        private const string html3 = @"<title><b> THIS FILE </b> USES CORRECTLY NESTED TAGS </title><h1><i> First <b class='c1'> header </b> text <img src = 'pic.jpg' /> </ i ></ h1 > < p id = 'par1'> Some other text </p1>";
        private const string html4 = @"<title> <b> THIS FILE </title> IS </b> NOT NESTED CORRECTLY.<p> <b> some text is not nested correctly </b>";

        ValidateHtml _validateHtml;
        public TestValidateHtml()
        {
            _validateHtml = new ValidateHtml();
        }

        [TestMethod]
        public void TestValidHtml()
        {
            var result1 = _validateHtml.CheckElement(html1);
            Assert.IsFalse(result1.Item1, result1.Item2);
            System.Diagnostics.Debug.Write(result1.Item2);
        }

        [TestMethod]
        public void TestInvalidValidHtml()
        {
            var result1 = _validateHtml.CheckElement(html4);
            Assert.IsTrue(result1.Item1, result1.Item2);
            System.Diagnostics.Debug.Write(result1.Item2);
        }
    }
}
