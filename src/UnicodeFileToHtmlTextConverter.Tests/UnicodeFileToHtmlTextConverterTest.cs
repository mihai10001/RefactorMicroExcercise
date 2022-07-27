using System;
using NUnit.Framework;
using Moq;

namespace TDDMicroExercises.UnicodeFileToHtmlTextConverter.Tests
{
    [TestFixture]
    public class UnicodeFileToHtmlTextConverterTest
    {
        [Test]
        public void ConvertToHtml_method_should_throw_argument_null_exception_when_fullFilenameWithPath_is_empty()
        {
            var fullFilenameWithPath = String.Empty;
            var textConverter = new UnicodeFileToHtmlTextConverter(fullFilenameWithPath);

            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => textConverter.ConvertToHtml());
            Assert.That(ex.Message, Does.StartWith("Filename should not be empty!"));
        }
    }
}
