using System.IO;
using System.Text;

namespace RstateAPI.Helpers {
    public class ValidatorsFile {
        public static bool IsExecutable (string filePath) {
            var firstBytes = new byte[2];
            using (var fileStream = File.Open (filePath, FileMode.Open)) {
                fileStream.Read (firstBytes, 0, 2);
            }
            return Encoding.UTF8.GetString (firstBytes) == "MZ";
        }

    }
}