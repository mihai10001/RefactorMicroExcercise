using System;
using System.IO;
using System.Text;
using System.Web;

namespace TDDMicroExercises.UnicodeFileToHtmlTextConverter
{
    public class UnicodeFileToHtmlTextConverter
    {
        private readonly string _fullFilenameWithPath;

        public UnicodeFileToHtmlTextConverter(string fullFilenameWithPath)
        {
            _fullFilenameWithPath = fullFilenameWithPath;
        }

        public string ConvertToHtml()
        {
            if (String.IsNullOrEmpty(_fullFilenameWithPath))
                throw new ArgumentNullException(nameof(_fullFilenameWithPath), "Filename should not be empty!");

            StringBuilder html = new StringBuilder();

            using (TextReader unicodeFileStream = File.OpenText(_fullFilenameWithPath))
            {
                string line;
                while ((line = unicodeFileStream.ReadLine()) != null)
                {
                    html.Append($"{HttpUtility.HtmlEncode(line)}<br />");
                }
            }

            return html.ToString();
        }
    }
}
