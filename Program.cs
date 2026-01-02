using CommandLine;
using CommandLine.Text;
using OKDPlayer.KaraokeData;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace OKDPlayer
{
    internal class Program
    {
        private static int kdspVolume = 0x75; //Default KDSP volume
        private static byte kdspHighFreq = 0x45; //Default KDSP high frequency
        private static byte kdspLowFreq = 0x45; //Default KDSP low frequency
        private static int transposeKey = 0;
        private static bool guideMelMuted = false;
        private static int syncoffsetAdpcm = 0;
        private static string inputOKDFile = string.Empty;
        private static string kdspDevPort = string.Empty;
        private static int[] midiDevIndexes = new int[] { };
        public class OKDPlayerCommandlineOptions
        {
            [Option('m', "midi-devices", Required = false, HelpText = "Set midi playback devices as number, Ex: 1 2 3 4")]
            public IEnumerable<int> midiDevIndexs { get; set; }

            [Option('k', "kdsp-devices", Required = false, HelpText = "Set Karaoke DSP (KDSP) devices as number or Serial Port. Ex: 10, COM10")]
            public string kdspDevPort { get; set; }

            [Option('i', "input-okd-file", Required = true, HelpText = "Path to OKD file to play.")]
            public string inputOKDFile { get; set; }

            [Option('g', "guide-melody-mute", Required = false, HelpText = "Mute guide melody (PTrack 1, Channel 8) on start.")]
            public bool muteGuideMelody { get; set; }

            [Option('t', "transpose", Required = false, HelpText = "Transpose key in semitones (positive or negative).")]
            public int transposeKey { get; set; }

            [Option('s', "sync-offset-adpcm", Required = false, HelpText = "Sync offset in milliseconds to apply when ADPCM chorus is present.")]
            public int syncOffsetAdpcm { get; set; } = 0;
        }
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("OKD Player - DAM OKD File Player");
            var parser = new Parser(with => with.HelpWriter = null);
            ParserResult<OKDPlayerCommandlineOptions> parseRes = parser.ParseArguments<OKDPlayerCommandlineOptions>(args);
            
            parseRes.WithParsed((o) =>
             {
                 transposeKey = o.transposeKey;
                 guideMelMuted = o.muteGuideMelody;
                 syncoffsetAdpcm = o.syncOffsetAdpcm;
                 inputOKDFile = o.inputOKDFile;
                 midiDevIndexes = new int[o.midiDevIndexs.Count()];
                 if(o.kdspDevPort != null)
                     kdspDevPort = o.kdspDevPort;
                 for (int i = 0; i < o.midiDevIndexs.Count(); i++)
                 {
                     midiDevIndexes[i] = o.midiDevIndexs.ElementAt(i);
                 }
             })
            
            .WithNotParsed((e) =>
            {
                var txt = HelpText.AutoBuild(parseRes, h => {
                    //configure HelpText
                    h.AdditionalNewLineAfterOption = false;
                    h.Heading = "";
                    h.AddEnumValuesToHelpText = true;
                    //h.Heading = "OKDPlayer - DAM OKD Player"; 
                    h.Copyright = "Copyright (c) 2025 dhlrunner(runner38)"; 
                    return h;
                }, x => x);
                Console.WriteLine(txt);
                Environment.Exit(1);
            });


    
            Console.WriteLine($"Loading OKD file: {inputOKDFile}");
            OKD okd = new OKD();

            okd.LoadFromFile(inputOKDFile, File.Exists("key.bin") ? "key.bin" : null);

            Console.WriteLine($"OKD file loaded successfully. PTrack Count :{okd.PTracks.Length}");

            //split input by space 
            //string[] inputParts = null;

            if(midiDevIndexes.Length < 1)
            {
                // Get available MIDI output devices
                string[] midiDevs = MIDIDevice.GetOutputDeviceNames().ToArray();
                if (midiDevs.Length == 0)
                {
                    Console.WriteLine("No MIDI output devices found. Please connect a MIDI device and try again.");
                    return;
                }

                //print available MIDI devices
                Console.WriteLine("Available MIDI Output Devices:");
                for (int i = 0; i < midiDevs.Length; i++)
                {
                    Console.WriteLine($"{i}: {midiDevs[i]}");
                }

                Console.WriteLine($"Select MIDI Output Device by number. This OKD file needs {okd.PTracks.Length} MIDI Ports.");
                string input = Console.ReadLine();
                string[] sp = input.Split(' ');
                if (!sp.All(part => int.TryParse(part, out _)))
                {
                    Console.WriteLine("Invalid input. Please enter valid MIDI device indices separated by spaces.");
                    return;
                }
                midiDevIndexes = new int[sp.Length];
                for (int i = 0; i < sp.Length; i++)
                {
                    midiDevIndexes[i] = int.Parse(sp[i]);
                }
            }

            if(kdspDevPort != string.Empty)
            {
                Console.WriteLine($"Setting KDSP device at port: {kdspDevPort}");
                okd.SetKDSPDevice(kdspDevPort);

                Console.WriteLine($"Setting KDSP volume to default ({kdspVolume:X2}).");
                okd.KDSPDev.SetDSPVolume((byte)kdspVolume);

                Console.WriteLine($"Setting KDSP tone to default (Hi:{kdspHighFreq:X2}, Lo:{kdspLowFreq:X2}).");
                okd.KDSPDev.SetDSPTone(kdspHighFreq, kdspLowFreq);
            }


            if (midiDevIndexes.Length < okd.PTracks.Length)
            {
                Console.WriteLine($"WARNING: There is not enough MIDI device to play the file. Playback will be wrong!");
                //return;
            }
           
            

           // inputParts = inputParts[..(okd.PTracks.Length > inputParts.Length ? inputParts.Length : okd.PTracks.Length)];
            List<IMIDIDevice> midiDevsList = new List<IMIDIDevice>();
            foreach (var part in midiDevIndexes)
            {
                if(okd.PTracks.Length <= midiDevsList.Count)
                {
                    break; //No more needed
                }
                IMIDIDevice dev = null;
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    dev = new WinMIDIDevice();
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                    dev = new LinuxMIDIDevice();
                bool res = dev.Open(part);
                if (!res)
                {
                    Console.WriteLine($"Failed to open MIDI device at index {part}. Please check the device and try again.");
                    Environment.Exit(1);
                    //return;
                }
                midiDevsList.Add(dev);
            }

            

            okd.SetMIDIDevice(midiDevsList.ToArray());


            Console.WriteLine("Resetting TG Devices...");
            okd.ResetTGDevices(true);
            

            //Masterclock setup
            MasterClock masterClock = new MasterClock();

            OKDPlayback[] playbacks = okd.GetPTrackPlaybacks(adpcmOffsetMs:syncoffsetAdpcm);

            if(guideMelMuted)
            {
                //Mute guide melody (PTrack 1, Channel 8)
                playbacks[1].MuteChannel(8);
            }

            if(transposeKey != 0)
                okd.Transpose(transposeKey);


            //playbacks[1].MuteChannel(8);
            //Mute back chorus midi if has adpcm chorus
            //if(okd.BackChoursPCM != null) 
            //{
            //    Console.WriteLine("Muting back chorus MIDI channels due to ADPCM chorus presence.");
            //    playbacks[1].MuteChannel(6);
            //    playbacks[1].MuteChannel(7);
            //}

            foreach (var playback in playbacks)
            {
                if(playback != null)
                    playback.Play(masterClock);
                //break;
            }
            masterClock.Start();

            //For spinner effect
            ConsoleSpinner spinner = new ConsoleSpinner();
            bool firstShow = false;
            int totalPlayTimeSec = (int)(okd.TotalPlayTime / 1000);
            int totalPlayTimeMin = totalPlayTimeSec / 60;
            int totalPlayTimeSecRemainder = totalPlayTimeSec % 60;
            //wait for playback to finish

            while (playbacks.Any(p => p.IsPlaying))
            {
                //check key input
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;
                    if (key == ConsoleKey.Enter)
                    {
                        //Stop all playbacks
                        Console.WriteLine($"Stopping playback...");
                        foreach (var playback in playbacks.Where(p => p.IsPlaying))
                        {
                            playback.Stop();
                            masterClock.Stop();
                            playback.MidiDevice.StopAllSound();
                            playback.MidiDevice.Device.Close();
                        }
                        break;
                    }
                    else if (key == ConsoleKey.UpArrow)
                    {
                        //speed up

                        masterClock.SpeedRatio += 0.1f; // Increase speed by 10%
                        Console.WriteLine($"Playback speed set to {masterClock.SpeedRatio:F1}");
                    }
                    else if (key == ConsoleKey.DownArrow)
                    {
                        //slow down

                        if (masterClock.SpeedRatio <= 0.1f)
                            continue; // Don't allow speed to go below 0.1
                        masterClock.SpeedRatio -= 0.1f; // Decrease speed by 10%, but not below 0.1
                        Console.WriteLine($"Playback speed set to {masterClock.SpeedRatio:F1}");
                    }
                    else if (key == ConsoleKey.LeftArrow)
                    {
                        Console.WriteLine($"Seeking backward 10 seconds.");
                        masterClock.Seek(Math.Max(okd.FirstNoteONTime - 20, masterClock.CurrentVirtualTime + -10000) - masterClock.CurrentVirtualTime);
                        foreach (var playback in playbacks.Where(p => p.IsPlaying))
                        {
                            playback.MidiDevice.StopAllSound();
                        }
                    }
                    else if (key == ConsoleKey.RightArrow)
                    {
                        Console.WriteLine($"Seeking forward 10 seconds.");
                        masterClock.Seek(10000);
                        foreach (var playback in playbacks.Where(p => p.IsPlaying))
                        {
                            playback.MidiDevice.StopAllSound();
                        }
                    }
                    else if (key == ConsoleKey.PageUp)
                    {
                        transposeKey++;
                        okd.Transpose(transposeKey);
                        Console.WriteLine($"Transposing up: {transposeKey} semitones");
                    }
                    else if (key == ConsoleKey.PageDown)
                    {
                        transposeKey--;
                        okd.Transpose(transposeKey);
                        Console.WriteLine($"Transposing down: {transposeKey} semitones");
                    }
                    else if(key == ConsoleKey.End)
                    {
                        if(okd.KDSPDev is not null)
                        {
                            kdspVolume++;
                            if (kdspVolume > 127)
                                kdspVolume = 127;
                            okd.KDSPDev.SetDSPVolume((byte)kdspVolume);
                            Console.WriteLine($"Set KDSP volume down (Volume: {kdspVolume}).");
                        }
                        
                    }
                    else if (key == ConsoleKey.Home)
                    {
                        if (okd.KDSPDev is not null)
                        {
                            kdspVolume--;
                            if (kdspVolume < 0)
                                kdspVolume = 0;
                            okd.KDSPDev.SetDSPVolume((byte)kdspVolume);
                            Console.WriteLine($"Set KDSP volume up (Volume: {kdspVolume}).");
                        }
                        
                    }
                    else if (key == ConsoleKey.F1)
                    {
                        if (okd.KDSPDev is not null)
                        {
                            kdspHighFreq--;
                            okd.KDSPDev.SetDSPTone(kdspHighFreq, kdspLowFreq);
                            Console.WriteLine($"Adjusting KDSP High Frequency up. Hi:{kdspHighFreq:X2}, LO:{kdspLowFreq:X2}");

                            
                        }
                        
                    }
                    else if (key == ConsoleKey.F2)
                    {
                        if (okd.KDSPDev is not null)
                        {
                            kdspHighFreq++;
                            okd.KDSPDev.SetDSPTone(kdspHighFreq, kdspLowFreq);
                            Console.WriteLine($"Adjusting KDSP High Frequency down. Hi:{kdspHighFreq:X2}, LO:{kdspLowFreq:X2}");
                        }
                    }
                    else if (key == ConsoleKey.F3)
                    {
                        if (okd.KDSPDev is not null)
                        {
                            kdspLowFreq--;
                            okd.KDSPDev.SetDSPTone(kdspHighFreq, kdspLowFreq);
                            Console.WriteLine($"Adjusting KDSP Low Frequency up. Hi:{kdspHighFreq:X2}, LO:{kdspLowFreq:X2}");
                        }
                    }
                    else if (key == ConsoleKey.F4)
                    {
                        if (okd.KDSPDev is not null)
                        {
                            kdspLowFreq++;
                            okd.KDSPDev.SetDSPTone(kdspHighFreq, kdspLowFreq);
                            Console.WriteLine($"Adjusting KDSP Low Frequency down. Hi:{kdspHighFreq:X2}, LO:{kdspLowFreq:X2}");
                        }
                    }

                    else if (key == ConsoleKey.V)
                    {
                        Console.WriteLine($"Adjusting TG volume.");
                        okd.AdjustTGVolume();
                    }
                    else if (key == ConsoleKey.G)
                    {
                        //Mute or unmute guide melody (PTrack 1, Channel 8)
                        var guidePlayback = playbacks[1];
                        if (guideMelMuted)
                        {
                            guidePlayback.UnmuteChannel(8);
                            Console.WriteLine("Guide melody unmuted.");
                        }
                        else
                        {
                            guidePlayback.MuteChannel(8);
                            Console.WriteLine("Guide melody muted.");
                        }
                        guideMelMuted = !guideMelMuted;
                    }
                    else if (key == ConsoleKey.P)
                    {
                        if (masterClock.IsPlaybackPaused)
                        {
                            masterClock.Resume();
                            Console.WriteLine("Playback resumed.         ");
                        }
                        else
                        {
                            masterClock.Pause();
                            foreach (var playback in playbacks.Where(p => p.IsPlaying))
                            {
                                playback.MidiDevice.StopAllSound();
                            }
                            Console.WriteLine("Playback paused.         ");
                        }

                    }
                    else if(key == ConsoleKey.D1 || key == ConsoleKey.D2 || key == ConsoleKey.D3 || key == ConsoleKey.D4 ||
                            key == ConsoleKey.D5 || key == ConsoleKey.D6 || key == ConsoleKey.D7 ||
                            key == ConsoleKey.D8 || key == ConsoleKey.D9)
                    {
                        int trackNum = (int)key - (int)ConsoleKey.D1;
                        if (trackNum < playbacks.Length)
                        {
                            var playback = playbacks[trackNum];
                            playback.ToggleMuteAll();
                            Console.WriteLine($"PTrack {trackNum} {(playback.IsMutedAll ? "muted" : "unmuted")}.         ");
                        }
                    }

                }
                
                int totalSeconds = (int)((masterClock.CurrentVirtualTime - okd.FirstNoteONTime)/ 1000);
                int minutes = totalSeconds / 60;
                int seconds = totalSeconds % 60;

                if(totalSeconds < 0)
                {
                    spinner.Turn($"Track setup in progress.. ", 0);
                }
                else
                {
                    if (!firstShow)
                    {
                        //Console.WriteLine($"Total Play Time: {totalPlayTimeMin:D2}:{totalPlayTimeSecRemainder:D2}");
                        okd.AdjustTGVolume();
                        Console.WriteLine();
                        Console.WriteLine("Starting playback... Press Enter to stop playback, Up/Down arrows to change speed, PageUp/PageDown to transpose.");
                        firstShow = true;
                    }
                    Console.WriteLine($"-> Playing.. {minutes:D2}:{seconds:D2}/{totalPlayTimeMin:D2}:{totalPlayTimeSecRemainder:D2}");
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                }
                
                
                Thread.Sleep(100);
            }

            //Stop hanging notes
            foreach (var playback in playbacks)
            {
                playback.MidiDevice.StopAllSound();
            }
        }
    }
}
