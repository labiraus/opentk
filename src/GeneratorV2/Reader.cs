using System.IO;
using System.Net;

namespace GeneratorV2
{
    public static class Reader
    {
        public static Stream ReadSpecFromGithub()
        {
            var link = "https://raw.githubusercontent.com/frederikja163/OpenGL-Registry/otk5/xml/gl.xml";
            Logger.Info($"Beginning to download openGL spec from {link}");
            var request = WebRequest.CreateHttp(link);

            var response = request.GetResponse();
            return response.GetResponseStream();
        }
    }
}