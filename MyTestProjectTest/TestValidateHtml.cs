using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyTestProject;


namespace MyTestProjectTest
{
    [TestClass]
    public class TestValidateHtml
    {

        [TestMethod]
        public void TestHtml()
        {
            
            string html = @"<ins class='ad'></ins>";
            string html1 = @"<C><B>is centred and in boldface </B></C>";
            string html2 = @"< B > This should be in boldface, but there is an extra closing tag </ B ></ C >";
            string html3 = @"<title><b> THIS FILE </b> USES CORRECTLY NESTED TAGS </title><h1><i> First <b class='c1'> header </b> text <img src = 'pic.jpg' /> </ i ></ h1 > < p id = 'par1'> Some other text </p1>";
            string html4 = @"<title> <b> THIS FILE </title> IS </b> NOT NESTED CORRECTLY.<p> <b> some text is not nested correctly </b>";

            var validateHtml = new ValidateHtml();
            validateHtml.CheckElement(html2.ToCharArray());
            validateHtml.GetExceptionReport();
            Console.ReadLine();
        }
    }
}
