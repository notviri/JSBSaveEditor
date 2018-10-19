# jsb2json - Δ & ♫ Save Decoder/Encoder

Small command line utility to encode/decode .jsb savedata.

# Help
### Where are the savefiles?
On PC, they are located at `C:\Users\<user>\AppData\LocalLow\Berzerk Studio\Just Shapes _ Beats`.

### How do I replace the savefiles? They keep being reverted!
To prevent auto-sync *(Steam Cloud)*, tick off [this](https://i.imgur.com/JEzIfez.png) box in the game properties.

# Downloads & Info
### Releases
If you don't want to build it yourself get the latest build for Windows [here](https://github.com/notviri/jsb2json/releases/download/v1.0/jsb2json.exe).
  
If you're on any other platform, sorry? Game's only on Windows/Nintendo Switch though.

### Usage

#### Decoding .jsb to .json
`jsb2json.exe path/to/file.jsb` - Outputs *filename-decoded.json*, formatted with 4 space indent, very comfy

#### Encoding .json to .jsb savedata

`jsb2json.exe path/to/file.json` - Outputs *filename-re-encoded.jsb*

Very simple! ✨

### "Documentation" & Notes
It's just a JSON file with no indentation which is then shoved through a function that XORs each byte by **129 / 0x81**.  
If you look in the `SimpleCrypto` class of the game you can see that 
an attempt at implementing Rijndael cipher was made, it's unused though.
It was going to use the password **UNESTIDEGROSCACA** and derive the Key / IV from the bytearray 
**73, 118, 97, 110, 32, 77, 101, 100, 118, 101, 100, 101, 118**. 
Perhaps the developers realized that's a little overkill for Δ & ♫, 
although they still went through the trouble to implement a mediocre anti-tamper for the *beat points* earned in-game.
  
An identically functioning copy to `SimpleCrypto` can be found at https://pastebin.com/HgcZZhZw

# Building
This tool depends on [Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json) [(nuget)](https://www.nuget.org/packages/Newtonsoft.Json/), so link that.

# Contact
I'm most active on discord, add me @ **viri#3116** if anything breaks 👌
