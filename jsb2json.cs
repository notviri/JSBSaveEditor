using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace jsb2json {
    internal class Program {
        // A basic XOR cipher, pfft
        internal static string SimpleXOR(ref string data) {
            StringBuilder src = new StringBuilder(data);
            StringBuilder dest = new StringBuilder(data.Length);
            for (int i = 0; i < data.Length; ++i) {
                dest.Append((char)(src[i] ^ 129));
            }

            return dest.ToString();
        }

        internal static void Main(string[] args) {
            if (args.Length == 0) {
                Console.WriteLine(
                    "\njsb2json [https://github.com/notviri/jsb2json/]\n\n" +
                    "Usage: jsb2json <filename>\n" +
                    "If the file is a .jsb, it'll be decoded and formatted to JSON.\n" +
                    "If it's a .json, it'll be encoded.");

                return;
            } else {
                if(File.Exists(args[0])) {
                    string fileContents = File.ReadAllText(args[0]);
                    switch (Path.GetExtension(args[0])) {
                        case ".jsb":
                            string decodedData = SimpleXOR(ref fileContents);

                            try {
                                string formattedJson = JsonConvert.SerializeObject(JsonConvert.DeserializeObject(decodedData), Formatting.Indented);
                                File.WriteAllText(Path.GetFileNameWithoutExtension(args[0]) + "-decoded.json", formattedJson);
                            } 
                            
                            catch (JsonReaderException ex) {
                                Console.WriteLine("Error reading JSON data. Stacktrace: " + ex.StackTrace);
                                return;
                            }

                            break;

                        case ".json":
                            try {
                                string unformatted = JsonConvert.SerializeObject(JsonConvert.DeserializeObject(fileContents));
                                File.WriteAllText(Path.GetFileNameWithoutExtension(args[0]) + "-re-encoded.jsb", SimpleXOR(ref unformatted));
                            }

                            catch (JsonReaderException ex) {
                                Console.WriteLine("Malformed JSON data. Stacktrace: " + ex.StackTrace);
                                return;
                            }
                            
                            break;

                        default:
                            Console.WriteLine("That's not a .jsb or a .json file, silly.");
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
