using System.IO;
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
            StringBuilder html = new StringBuilder()

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

