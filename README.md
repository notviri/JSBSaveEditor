# Just Shapes & Beats Save Editor

Small command line utility to quickly encode/decode .jsb savedata.

## Where are the savefiles?
On PC, they are located at `C:\Users\<user>\AppData\LocalLow\Berzerk Studio\Just Shapes _ Beats`.

## Download
If you don't want to build it yourself get the latest build [here](https://github.com/notviri/JSBSaveEditor/releases/download/v1.0/JSBSaveEditor_v1.0.zip).

## Usage

### Decoding .jsb to .json
`JSBSaveEditor.exe path/to/file.jsb` - Outputs *file-decoded.json*, formatted with 4 space indent

### Encoding .json to .jsb savedata

`JSBSaveEditor.exe path/to/file.json` - Outputs *file-re-encoded.jsb*

Very simple! ✨

## Information
It's just a JSON file with no indentation which is then shoved through a function that XORs each byte by **129**.  
If you look in the `SimpleCrypto` class of the game you can see that 
an attempt at implementing Rijndael cipher was made, it's unused though.
It was going to use the password **UNESTIDEGROSCACA** and derive the Key / IV from the bytearray 
**73, 118, 97, 110, 32, 77, 101, 100, 118, 101, 100, 101, 118**. 
Perhaps *lachhh* realized that's a little overkill for JS&B, 
although he still went through the trouble to implement a mediocre anti-tamper for the *beat points* earned in-game.  
  
An identically functioning copy to `SimpleCrypto` can be found at https://pastebin.com/HgcZZhZw

## Contact
I'm most active on discord, add me @ **viri#0001** if anything breaks 👌
