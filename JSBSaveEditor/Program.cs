using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace JSBSaveEditor {
    internal class Program {
        // A basic XOR cipher, pfft
        public static string SimpleXOR(string data) {
            StringBuilder src = new StringBuilder(data);
            StringBuilder dest = new StringBuilder(data.Length);
            for (int index = 0; index < data.Length; ++index)
                dest.Append((char)(src[index] ^ 129));

            return dest.ToString();
        }

        static void Main(string[] args) {
            if (args.Length == 0) {
                Console.WriteLine(
                    "\nJust Shapes & Beats Save Editor (created by viri#0001)\n\n" +
                    "Usage: JSBSaveEditor.exe <filename>\n" +
                    "If the file is a .jsb, it'll be decrypted and formatted to JSON.\n" +
                    "If it's a .json, it'll be encrypted.");

                return;
            } else {
                if(File.Exists(args[0])) {
                    string fileContents = File.ReadAllText(args[0]);
                    switch (Path.GetExtension(args[0])) {
                        case ".jsb":
                            string decodedData = SimpleXOR(fileContents);

                            try {
                                string formattedJson = JsonConvert.SerializeObject(JsonConvert.DeserializeObject(decodedData), Formatting.Indented);
                                File.WriteAllText(Path.GetFileNameWithoutExtension(args[0]).Replace("-re-encoded", "") + "-decoded.json", formattedJson);
                            } catch (JsonReaderException ex) {
                                Console.WriteLine("Error reading JSON data. Stacktrace: " + ex.StackTrace);
                                return;
                            }
                            break;

                        case ".json":
                            try {
                                string unformatted = JsonConvert.SerializeObject(JsonConvert.DeserializeObject(fileContents));
                                File.WriteAllText(Path.GetFileNameWithoutExtension(args[0]).Replace("-decoded", "") + "-re-encoded.jsb", SimpleXOR(unformatted));
                            } catch (JsonReaderException ex) {
                                Console.WriteLine("Malformed JSON data. Stacktrace: " + ex.StackTrace);
                                return;
                            }
                            
                            break;
                    }
                } else {
                    Console.WriteLine("File does not exist!");
                    return;
                }
            }
        }
    }
}
