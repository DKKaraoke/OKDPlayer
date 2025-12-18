# OKDPlayer

A tool for playing DAM karaoke song data.

-----

## ⚠️ Important Notice

This project is intended **strictly for the technical study of DAM karaoke machines**.

Karaoke song data is protected by law. You must comply with these laws and adhere to the legal regulations of Japan.

-----

This software can read and play DAM KaraOKe Data (OKD) files.

### Usage

```bash
OKDPlayer.exe -i <path_to_okd_file> <options>
Options:
  -i, --input-okd-file     Path to OKD file to play
  -m, --midi-devices       Set midi playback devices as number, Ex: 1 2 3 4
  -g, --guide-melody-mute  Mute guide melody (PTrack 1, Channel 8) on start
  -t, --transpose          Transpose key in semitones (positive or negative)
  -s, --sync-offset-adpcm  Sync offset in milliseconds to apply when ADPCM chorus is present.
  -h, --help			   Display help information
```

A `key.bin` file is necessary to play scrambled OKD files. I do not provide this file or any information on how to obtain it.

**Controls:**

  * **Up/Down Arrow Keys:** Adjust tempo
  * **Left/Right Arrow Keys:** Seek backward/forward by 10 seconds
  * **Page Up/Page Down Keys:** Adjust pitch (in semitones)
  * **P Key:** Pause/Resume playback
  * **G Key:** Toggle Guide melody on/off (Not working some TGMode0 OKD)
  * **Number Keys (1-9):** Mute/Unmute Specific PTrack playback

### Platform Support

  * **Windows:** Fully supported.
  * **Linux:** Support via the ALSA library is currently in progress.

### Hardware

**Recommended Hardware:**

  * **CNVX1K module**(for X8202 Board) by [Luna Tsukinashi](https://lunatsukina.si/).
  * **SPRITE!(CNVX5K) module**(for YC945 Board) by [Luna Tsukinashi](https://lunatsukina.si/).
  * Or an equivalent MIDI sound module, such as the **Yamaha MMT-TG**.
-----

## Known Bugs

  * **Incorrect TG Volume:** The volume for TG B is abnormally high. As a temporary fix, the volume is being forcibly adjusted within the playback logic. This may cause some effects, such as fade-outs, to not function correctly.
  * **Skipped Note Events:** Some note events are occasionally skipped during normal playback or when seeking.

-----

## TODO

  * Add fully support for ADPCM back chorus.
  * Full support for the Linux platform.

-----

## Contributing

All pull requests and contributions are welcome\!

-----
## Acknowledgements, Credit

This project includes code from other open source projects. For more details, please see the [ThirdPartyNotices.md](ThirdPartyNotices.md) file.

The majority of the OKD file parsing logic in this project is derived from [dam-song-tools-oss](https://github.com/DKKaraoke/dam-song-tools-oss).

Special thanks to the authors of that project:

  * KIRISHIKI Yudai
  * 東京スーパーチャンネル

**This repository may be modified or removed at any time upon the request of the original authors.**

