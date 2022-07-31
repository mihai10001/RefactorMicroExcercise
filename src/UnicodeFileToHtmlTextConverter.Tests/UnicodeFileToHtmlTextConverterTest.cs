using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using NUnit.Framework;

namespace TDDMicroExercises.UnicodeFileToHtmlTextConverter.Tests
{
    [TestFixture]
    public class UnicodeFileToHtmlTextConverterTest
    {
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        [Test]
        public void ConvertToHtml_method_should_throw_ArgumentNullException_when_file_path_is_empty()
        {
            var fullFilenameWithPath = String.Empty;
            var textConverter = new UnicodeFileToHtmlTextConverter(fullFilenameWithPath);

            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => textConverter.ConvertToHtml());
            Assert.That(ex.Message, Does.StartWith("Filename should not be empty!"));
        }

        [Test]
        public void ConvertToHtml_method_should_throw_FileNotFoundException_when_file_is_inexistent()
        {
            var randomInexistentFilename = Path.GetRandomFileName();
            var textConverter = new UnicodeFileToHtmlTextConverter(randomInexistentFilename);

            FileNotFoundException ex = Assert.Throws<FileNotFoundException>(() => textConverter.ConvertToHtml());
            Assert.That(ex.Message, Does.StartWith("Could not find file"));
        }

        [Test]
        public void ConvertToHtml_method_should_throw_DirectoryNotFoundException_when_folder_is_inexistent()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            string[] randomPaths = { allDrives.First().Name, RandomString(10), Path.GetRandomFileName() };
            string randomInexistentFolderWithFilenamePath = Path.Combine(randomPaths);
            var textConverter = new UnicodeFileToHtmlTextConverter(randomInexistentFolderWithFilenamePath);

            DirectoryNotFoundException ex = Assert.Throws<DirectoryNotFoundException>(() => textConverter.ConvertToHtml());
            Assert.That(ex.Message, Does.StartWith("Could not find a part of the path"));
        }

        [Test]
        public void ConvertToHtml_method_should_return_empty_string_when_file_content_is_empty()
        {
            string newFilenamePath = Path.Combine(new string[] { AppDomain.CurrentDomain.BaseDirectory, Path.GetRandomFileName() });
            using (FileStream fs = File.Create(newFilenamePath));

            var textConverter = new UnicodeFileToHtmlTextConverter(newFilenamePath);
            var resultedHtmlString = textConverter.ConvertToHtml();

            Assert.AreEqual(resultedHtmlString, String.Empty);
        }

        [Test]
        public void ConvertToHtml_method_should_return_converted_content_when_file_content_is_not_empty()
        {
            string fileContent = "<html>Example file content</html>";
            string newFilenamePath = Path.Combine(new string[] { AppDomain.CurrentDomain.BaseDirectory, Path.GetRandomFileName() });
            using (FileStream fs = File.Create(newFilenamePath))
            {
                var info = new UTF8Encoding(true).GetBytes(fileContent);
                fs.Write(info, 0, info.Length);
            }

            var textConverter = new UnicodeFileToHtmlTextConverter(newFilenamePath);
            var resultedHtmlString = textConverter.ConvertToHtml();

            Assert.AreEqual(resultedHtmlString, $"{HttpUtility.HtmlEncode(fileContent)}<br />");
        }

        [Test]
        public void ConvertToHtml_method_should_return_converted_content_when_file_content_is_not_empty_multiple_lines()
        {
            string[] fileContentLines =
            {
                "<html>Example file content</html>",
                "<style> .some-random-css { background-color: black } </style>"
            };
            string newFilenamePath = Path.Combine(new string[] { AppDomain.CurrentDomain.BaseDirectory, Path.GetRandomFileName() });
            File.WriteAllLines(newFilenamePath, fileContentLines);

            var textConverter = new UnicodeFileToHtmlTextConverter(newFilenamePath);
            var resultedHtmlString = textConverter.ConvertToHtml();

            Assert.AreEqual(resultedHtmlString,
                $"{HttpUtility.HtmlEncode(fileContentLines[0])}<br />" +
                $"{HttpUtility.HtmlEncode(fileContentLines[1])}<br />"
            );
        }
    }
}
