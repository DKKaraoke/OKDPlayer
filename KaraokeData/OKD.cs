using OKDPlayer;
using OKDPlayer.KaraokeData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class OKDHeader
{
    public byte[] MagicBytes { get; set; }
    public int Length { get; set; }
    public string Version { get; set; }
    public int IdKaraoke { get; set; }
    public int AdpcmOffset { get; set; }
    public int EncryptionMode { get; set; }
    public int HeaderSize { get; set; }
}
public class GenericOKDHeader : OKDHeader
{
    public byte[] OptionData { get; set; }

    public GenericOKDHeader(byte[] magicBytes, int length, string version, int idKaraoke, int adpcmOffset, int encryptionMode, byte[] optionData, int headerSize)
    {
        MagicBytes = magicBytes;
        Length = length;
        Version = version;
        IdKaraoke = idKaraoke;
        AdpcmOffset = adpcmOffset;
        EncryptionMode = encryptionMode;
        OptionData = optionData;
        HeaderSize = headerSize;
    }
}
public class MmtOKDHeader : OKDHeader
{
    public int YksChunksLength { get; set; }
    public int MmtChunksLength { get; set; }
    public int CrcYksLoader { get; set; }
    public int CrcLoader { get; set; }

    public MmtOKDHeader(byte[] magicBytes, int length, string version, int idKaraoke, int adpcmOffset, int encryptionMode, int yksChunksLength, int mmtChunksLength, int crcYksLoader, int crcLoader, int headerSize)
    {
        MagicBytes = magicBytes;
        Length = length;
        Version = version;
        IdKaraoke = idKaraoke;
        AdpcmOffset = adpcmOffset;
        EncryptionMode = encryptionMode;
        YksChunksLength = yksChunksLength;
        MmtChunksLength = mmtChunksLength;
        CrcYksLoader = crcYksLoader;
        CrcLoader = crcLoader;
        HeaderSize = headerSize;
    }
}
public class MmkOKDHeader : OKDHeader
{
    public int YksChunksLength { get; set; }
    public int MmtChunksLength { get; set; }
    public int MmkChunksLength { get; set; }
    public int CrcYksLoader { get; set; }
    public int CrcYksMmkOkd { get; set; }
    public int CrcLoader { get; set; }

    public MmkOKDHeader(byte[] magicBytes, int length, string version, int idKaraoke,
                        int adpcmOffset, int encryptionMode, int yksChunksLength,
                        int mmtChunksLength, int mmkChunksLength, int crcYksLoader,
                        int crcYksMmkOkd, int crcLoader)
    {
        MagicBytes = magicBytes;
        Length = length;
        Version = version;
        IdKaraoke = idKaraoke;
        AdpcmOffset = adpcmOffset;
        EncryptionMode = encryptionMode;
        YksChunksLength = yksChunksLength;
        MmtChunksLength = mmtChunksLength;
        MmkChunksLength = mmkChunksLength;
        CrcYksLoader = crcYksLoader;
        CrcYksMmkOkd = crcYksMmkOkd;
        CrcLoader = crcLoader;
    }
}
public class SprOKDHeader : OKDHeader
{
    // option_data
    public int yks_chunks_length { get; set; }
    public int mmt_chunks_length { get; set; }
    public int mmk_chunks_length { get; set; }
    public int spr_chunks_length { get; set; }
    public int crc_yks_loader { get; set; }
    public int crc_yks_mmt_okd { get; set; }
    public int crc_yks_mmt_mmk_okd { get; set; }
    public int crc_loader { get; set; }

    public SprOKDHeader(byte[] magic_bytes, int length, string version, int id_karaoke, int adpcm_offset, int encryption_mode, int yks_chunks_length, int mmt_chunks_length, int mmk_chunks_length, int spr_chunks_length, int crc_yks_loader, int crc_yks_mmt_okd, int crc_yks_mmt_mmk_okd, int crc_loader)
    {
        this.MagicBytes = magic_bytes;
        this.Length = length;
        this.Version = version;
        this.IdKaraoke = id_karaoke;
        this.AdpcmOffset = adpcm_offset;
        this.EncryptionMode = encryption_mode;
        this.yks_chunks_length = yks_chunks_length;
        this.mmt_chunks_length = mmt_chunks_length;
        this.mmk_chunks_length = mmk_chunks_length;
        this.spr_chunks_length = spr_chunks_length;
        this.crc_yks_loader = crc_yks_loader;
        this.crc_yks_mmt_okd = crc_yks_mmt_okd;
        this.crc_yks_mmt_mmk_okd = crc_yks_mmt_mmk_okd;
        this.crc_loader = crc_loader;
    }
}
public class DioOKDHeader : OKDHeader
{
    public int YksChunksLength { get; set; }
    public int MmtChunksLength { get; set; }
    public int MmkChunksLength { get; set; }
    public int SprChunksLength { get; set; }
    public int DioChunksLength { get; set; }
    public int CrcYksLoader { get; set; }
    public int CrcYksMmtOkd { get; set; }
    public int CrcYksMmtMmkOkd { get; set; }
    public int CrcYksMmtMmkSprOkd { get; set; }
    public int CrcLoader { get; set; }

    public DioOKDHeader(byte[] magicBytes, int length, string version, int idKaraoke,
                        int adpcmOffset, int encryptionMode, int yksChunksLength,
                        int mmtChunksLength, int mmkChunksLength, int sprChunksLength,
                        int dioChunksLength, int crcYksLoader, int crcYksMmtOkd,
                        int crcYksMmtMmkOkd, int crcYksMmtMmkSprOkd, int crcLoader)
    {
        MagicBytes = magicBytes;
        Length = length;
        Version = version;
        IdKaraoke = idKaraoke;
        AdpcmOffset = adpcmOffset;
        EncryptionMode = encryptionMode;
        YksChunksLength = yksChunksLength;
        MmtChunksLength = mmtChunksLength;
        MmkChunksLength = mmkChunksLength;
        SprChunksLength = sprChunksLength;
        DioChunksLength = dioChunksLength;
        CrcYksLoader = crcYksLoader;
        CrcYksMmtOkd = crcYksMmtOkd;
        CrcYksMmtMmkOkd = crcYksMmtMmkOkd;
        CrcYksMmtMmkSprOkd = crcYksMmtMmkSprOkd;
        CrcLoader = crcLoader;
    }
}
public class OKAHeader
{
    public byte[] MagicBytes { get; set; }
    public int Length { get; set; }
    public string Version { get; set; }
    public int IdKaraoke { get; set; }
    public int DataOffset { get; set; }
    public int Reserved { get; set; }
    public int CrcLoader { get; set; }

    public OKAHeader(byte[] magicBytes, int length, string version, int idKaraoke, int dataOffset, int reserved, int crcLoader)
    {
        MagicBytes = magicBytes;
        Length = length;
        Version = version;
        IdKaraoke = idKaraoke;
        DataOffset = dataOffset;
        Reserved = reserved;
        CrcLoader = crcLoader;
    }
}
public class OKDGenericChunk
{
    public byte[] ChunkId { get; }
    public byte[] Data { get; }

    public OKDGenericChunk(byte[] chunkId, byte[] data)
    {
        ChunkId = chunkId;
        Data = data;
    }

    public void Write(Stream stream)
    {
        stream.Write(ChunkId, 0, ChunkId.Length);
        stream.Write(Data, 0, Data.Length);
    }
}

public class OKD
{
    public OKDGenericChunk[] Chunks { get; private set; }
    public OKDHeader Header { get; private set; }
    public OKDPTrackInfo PTrackInfo { get; private set; }
    public OKDPTrack[] PTracks { get; private set; }
    public OKDMIDIDevice[] MIDIDev { get; private set; }
    public OKDKDSPDevice KDSPDev { get; private set; }
    public uint FirstNoteONTime { get; private set; } = 0;
    public uint TotalPlayTime { get; private set; } = 0;
    public byte[][] BackChoursPCM { get; private set; } = null;

    private readonly byte MIDI_DEV_MAX_COUNT = 4;
    private ushort[] OKD_SCRAMBLE_PATTERN = {};

    public enum OKDFileType
    {
        OKD,
        P3,
        M3,
        DIFF,
        ONTA,
        MP3DIFF,
        ONTADIFF,
        MP3RAWDATA,
        MMTTXT,
        FILES,
    }

    public enum OKDFileReadMode
    {
        MMT,
        YKS,
        ALL,
        DATA,
    }

    private int choose_scramble_pattern_index()
    {
        Random rand = new Random();
        return rand.Next(0x00, 0xFF + 1);
    }

    private int DetectFirstScramblePatternIdx(BinaryReader okdreader, OKDFileType file_type)
    {
        //OkdHeader header = new DioOkdHeader(null,0,null,0,0,0,0,0,0,0,0,0,0,0,0,0);
        //((DioOkdHeader)header).CrcLoader = 0;
        byte[] expected_magic_bytes = null;
        if (file_type == OKDFileType.OKD ||
            file_type == OKDFileType.P3 ||
            file_type == OKDFileType.DIFF ||
            file_type == OKDFileType.MP3DIFF ||
            file_type == OKDFileType.MMTTXT)
        {
            expected_magic_bytes = Encoding.ASCII.GetBytes("YKS1");
        }
        else if (file_type == OKDFileType.M3 ||
            file_type == OKDFileType.ONTA ||
            file_type == OKDFileType.ONTADIFF)
        {
            expected_magic_bytes = Encoding.ASCII.GetBytes("YOKA");
        }
        else
        {
            throw new Exception($"Invalid file_type {file_type}");
        }


        byte[] magic_bytes_buffer = okdreader.ReadBytes(4);
        okdreader.BaseStream.Position = 0;



        if (!magic_bytes_buffer.SequenceEqual(expected_magic_bytes))
        {
            if (OKD_SCRAMBLE_PATTERN.Length < 1)
                throw new FileLoadException("OKD scramble key is not loaded.");

            //Console.WriteLine("OKD file is scrambled");
            int expected_key = BitConverter.ToInt32(magic_bytes_buffer.Reverse().ToArray(), 0) ^
                BitConverter.ToInt32(expected_magic_bytes.Reverse().ToArray(), 0);

            for (int scramble_pattern_index = 0; scramble_pattern_index < 256; scramble_pattern_index++)
            {
                int candidated_key = 0;
                if (scramble_pattern_index == 0xff)
                {
                    candidated_key = 0x87D2;
                }
                else
                {
                    candidated_key = OKD_SCRAMBLE_PATTERN[scramble_pattern_index + 1];
                }
                candidated_key |= OKD_SCRAMBLE_PATTERN[scramble_pattern_index] << 16;
                if (expected_key == candidated_key)
                {
                    //Console.WriteLine($"OKD file scramble_pattern_index detected. scramble_pattern_index={scramble_pattern_index}");
                    return scramble_pattern_index;
                }
            }
        }
        else //not scrambled
        {
            //Console.WriteLine("This OKD file is maybe not scrambled.");
            return -1;
        }

       

        throw new Exception("Failed to detect OKD file scramble_pattern_index.");

    }

    private int? descramble(BinaryReader okdreader, MemoryStream output, int? pIndex, int? length)
    {
        long startPos = okdreader.BaseStream.Position;
        while (length == null || (length != null && (okdreader.BaseStream.Position - startPos) < length))
        {
            if (pIndex > -1)
            {
                byte[] scrambledBuff = okdreader.ReadBytes(2);
                if ((length == null) && (scrambledBuff.Length == 0))
                    break;
                if (scrambledBuff.Length != 2)
                    throw new Exception("Invalid scrambled_buffer length.");
                ushort scrambled = BitConverter.ToUInt16(scrambledBuff.Reverse(), 0);
                int scrambledPattern;
                if (pIndex == null)
                    scrambledPattern = 0x17d7;
                else scrambledPattern = OKD_SCRAMBLE_PATTERN[(int)(pIndex % 0x100)];
                ushort dec = (ushort)(scrambled ^ scrambledPattern);
                byte[] decBuff = BitConverter.GetBytes(dec).Reverse();
                output.Write(decBuff, 0, decBuff.Length);
                if (pIndex != null)
                    pIndex += 1;
            }
            else
            {
                output.Write(okdreader.ReadBytes((int)length), 0, (int)length);
            }
           
        }
        return pIndex;
    }
    private OKDHeader readOKDHeader(BinaryReader okdreader, MemoryStream decStream, int? pIndex)
    {
        //MemoryStream decStream = new MemoryStream();
        pIndex = descramble(okdreader, decStream, pIndex, 40);
        decStream.Position = 0;
        byte[] headerBuff = new byte[40];
        decStream.Read(headerBuff, 0, 40);
        if (headerBuff.Length != 40)
            throw new Exception("Invalid header_buffer length.");


        using (BinaryReader hdr = new BinaryReader(new MemoryStream(headerBuff)))
        {
            byte[] magic = hdr.ReadBytes(4);
            if (!magic.SequenceEqual(Encoding.ASCII.GetBytes("YKS1")))
                throw new Exception("Invalid magic.");

            int fileSize = Helpers.ReadInt32BE(hdr);
            string ver = Encoding.ASCII.GetString(hdr.ReadBytes(24 - 8));
            int idKaraoke = Helpers.ReadInt32BE(hdr);
            int adpcmOffset = Helpers.ReadInt32BE(hdr);
            int encMode = Helpers.ReadInt32BE(hdr);
            int optDataSize = Helpers.ReadInt32BE(hdr);

            pIndex = descramble(okdreader, decStream, pIndex, optDataSize);
            decStream.Seek(40, SeekOrigin.Begin);

            byte[] optDataBuff = new byte[optDataSize];
            decStream.Read(optDataBuff, 0, optDataSize);

            using (BinaryReader opt = new BinaryReader(new MemoryStream(optDataBuff)))
            {
                //Console.WriteLine($"Option Data size={optDataSize}");
                if (optDataSize > 0)
                {

                    if (optDataSize == 12)
                    {
                        int yksChunkSize = Helpers.ReadInt32BE(opt);
                        int mmtChunkSize = Helpers.ReadInt32BE(opt);
                        short yksLoaderCRC = Helpers.ReadInt16BE(opt);
                        short loaderCRD = Helpers.ReadInt16BE(opt);
                        return new MmtOKDHeader(magic, fileSize, ver, idKaraoke, adpcmOffset, encMode, optDataSize, yksChunkSize, yksLoaderCRC, loaderCRD, (int)hdr.BaseStream.Position + optDataSize);
                    }
                    else if (optDataSize == 20)
                    {
                        int yksChunkSize = Helpers.ReadInt32BE(opt);
                        int mmtChunkSize = Helpers.ReadInt32BE(opt);
                        int mmkChunkSize = Helpers.ReadInt32BE(opt);
                        short yksLoaderCRC = Helpers.ReadInt16BE(opt);
                        short yksMmkOkdCRC = Helpers.ReadInt16BE(opt);
                        short loaderCRC = Helpers.ReadInt16BE(opt);
                        return new MmkOKDHeader(
                                magic,
                                fileSize,
                                ver,
                                idKaraoke,
                                adpcmOffset,
                                encMode,
                                yksChunkSize,
                                mmtChunkSize,
                                mmkChunkSize,
                                yksLoaderCRC,
                                yksMmkOkdCRC,
                                loaderCRC
                            );
                    }
                    else if (optDataSize == 24)
                    {
                        int yksChunkSize = Helpers.ReadInt32BE(opt);
                        int mmtChunkSize = Helpers.ReadInt32BE(opt);
                        int mmkChunkSize = Helpers.ReadInt32BE(opt);
                        int sprChunkSize = Helpers.ReadInt32BE(opt);
                        short yksLoaderCRC = Helpers.ReadInt16BE(opt);
                        short yksMmtOkdCRC = Helpers.ReadInt16BE(opt);
                        short yksMmtMmkOkdCRC = Helpers.ReadInt16BE(opt);
                        short loaderCRC = Helpers.ReadInt16BE(opt);
                        return new SprOKDHeader(
                             magic,
                             fileSize,
                             ver,
                             idKaraoke,
                             adpcmOffset,
                             encMode,
                             yksChunkSize,
                             mmtChunkSize,
                             mmkChunkSize,
                             sprChunkSize,
                             yksLoaderCRC,
                             yksMmtOkdCRC,
                             yksMmtMmkOkdCRC,
                             loaderCRC
                        );
                    }
                    else if (optDataSize == 32)
                    {
                        int yksChunkSize = Helpers.ReadInt32BE(opt);
                        int mmtChunkSize = Helpers.ReadInt32BE(opt);
                        int mmkChunkSize = Helpers.ReadInt32BE(opt);
                        int sprChunkSize = Helpers.ReadInt32BE(opt);
                        int dioChunkSize = Helpers.ReadInt32BE(opt);
                        short yksLoaderCRC = Helpers.ReadInt16BE(opt);
                        short yksMmtOkdCRC = Helpers.ReadInt16BE(opt);
                        short yksMmtMmkOkdCRC = Helpers.ReadInt16BE(opt);
                        short yksMmtMmkSprOkdCRC = Helpers.ReadInt16BE(opt);
                        short loaderCRC = Helpers.ReadInt16BE(opt);
                        return new DioOKDHeader(
                            magic,
                            fileSize,
                            ver,
                            idKaraoke,
                            adpcmOffset,
                            encMode,
                            yksChunkSize,
                            mmtChunkSize,
                            mmkChunkSize,
                            sprChunkSize,
                            dioChunkSize,
                            yksLoaderCRC,
                            yksMmtOkdCRC,
                            yksMmtMmkOkdCRC,
                            yksMmtMmkSprOkdCRC,
                            loaderCRC
                        );
                    }
                }


                return new GenericOKDHeader(magic,
                    fileSize,
                    ver,
                    idKaraoke,
                    adpcmOffset,
                    encMode,
                    optDataBuff,
                    (int)hdr.BaseStream.Position + optDataSize
                );
            }



            //using (FileStream file = File.Create("d.bin"))
            //{
            //    decStream.Position = 0;
            //    decStream.CopyTo(file);
            //    file.Close();
            //    file.Dispose();
            //}

        }


        return null;
    }

    private OKAHeader readOKAHeader(BinaryReader okdreader, MemoryStream decStream, int? pIndex)
    {
        return null;
    }

    private (OKDHeader okdHeader, OKAHeader okaHeader) readFileHeader(BinaryReader okdreader, MemoryStream decStream, OKDFileType fileType, int? pIndex)
    {
        if (fileType == OKDFileType.OKD ||
            fileType == OKDFileType.P3 ||
            fileType == OKDFileType.DIFF ||
            fileType == OKDFileType.MP3DIFF ||
            fileType == OKDFileType.MMTTXT)
        {
            return (readOKDHeader(okdreader, decStream, pIndex), null);
        }
        else if (fileType == OKDFileType.M3 ||
            fileType == OKDFileType.ONTA ||
            fileType == OKDFileType.ONTADIFF)
        {
            return (null, readOKAHeader(okdreader, decStream, pIndex));
        }
        else
        {
            throw new Exception($"Invalid file_type {fileType}");
        }
    }

    private OKDHeader descramble(BinaryReader okdreader, MemoryStream output, OKDFileType fileType)
    {
        int pIndex = DetectFirstScramblePatternIdx(okdreader, fileType);
        var header = readFileHeader(okdreader, output, fileType, pIndex);

        int dataOffset = (int)okdreader.BaseStream.Position;
        int dataLength = header.okdHeader.Length - (dataOffset - 8);

        int extDataOffset = 0;
        if (header.okdHeader != null)
            extDataOffset = header.okdHeader.AdpcmOffset;
        else if (header.okaHeader != null)
            extDataOffset = header.okaHeader.DataOffset;

        if (extDataOffset != 0)
            extDataOffset -= 40;

        int extDataLength = 0;
        if (header.okdHeader.AdpcmOffset == 0)
            extDataLength = 0;
        else
            extDataLength = dataLength - extDataOffset;

        int scrambledLength = dataLength - extDataLength;

        descramble(okdreader, output, pIndex, scrambledLength);

        if (extDataLength > 0)
        {
            okdreader.BaseStream.Seek(extDataOffset, SeekOrigin.Begin);
            output.Write(okdreader.ReadBytes(extDataLength), 0, extDataLength);
        }

        return (header.okdHeader != null) ? header.okdHeader : null;
    }

    private OKDPTrack GetPTrackByID(int trackID)
    {
        if (this.PTracks == null || this.PTracks.Length == 0)
            return null;
        foreach (var track in this.PTracks)
        {
            if (track.TrackID == trackID)
                return track;
        }
        return null;
    }
 
    private OKDPTrackInfoEntry GetPTrackInfoByID(int trackID)
    {
        if (this.PTrackInfo != null)
        {
            foreach (var entry in this.PTrackInfo.trackInfoEntry)
            {
                if (entry.TrackNum == trackID)
                    return entry;
            }
        }
        return null;
    }
    private byte GetPortByID(int trackID)
    {
        for (int i = 0; i < OKDPTrack.ports; i++)
        {
            if (this.PTracks[i].TrackID == trackID)
            {
                return (byte)i;
            }
        }
        return 0;
    }
    public void SetMIDIDevice(IMIDIDevice[] devs)
    {
        if (devs.Length > MIDI_DEV_MAX_COUNT)
        {
            throw new ArgumentException($"Too many MIDI devices. Maximum is {MIDI_DEV_MAX_COUNT}.");
        }
        OKDMIDIDevice[] midiDevice = new OKDMIDIDevice[devs.Length];
        for (byte i = 0; i < devs.Length; i++)
        {
            midiDevice[i] = new OKDMIDIDevice
            {
                Port = i,
                Device = devs[i],
            };
            //midiDevice[i].Device.Open();
        }

        this.MIDIDev = midiDevice;
    }

    public void SetKDSPDevice(string devPort)
    {
        string normarlized = devPort.ToUpper().Trim();
        if(devPort.StartsWith("COM"))
        {
            this.KDSPDev = new OKDKDSPDevice(devPort);
        }
        else //Starts with dev index
        {
            int devIndex;
            if (!int.TryParse(normarlized, out devIndex))
                throw new ArgumentException("Invalid KDSP device port/index.");

            IMIDIDevice dev = null;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                dev = new WinMIDIDevice();
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                dev = new LinuxMIDIDevice();
            dev.Open(devIndex);

            this.KDSPDev = new OKDKDSPDevice(dev);
        }
    }
    public void LoadFromFile(string filePath, string keyfilePath = null)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"File not found: {filePath}");

        if (keyfilePath == null)
            Load(File.OpenRead(filePath), null);
        else
            Load(File.OpenRead(filePath), File.ReadAllBytes(keyfilePath));
    }

    public void Load(Stream data, byte[] keydata)
    {
        //parse key data
        if (keydata != null)
        {
            if (keydata.Length != 512)
                throw new ArgumentException("invalid key data");

            OKD_SCRAMBLE_PATTERN = new ushort[256];
            for (int i = 0; i < 512; i += 2)
            {
                ushort key = BitConverter.ToUInt16(keydata, i);
                OKD_SCRAMBLE_PATTERN[i / 2] = key;

            }
        }

        //if(OKD_SCRAMBLE_PATTERN.Length < 1)
        //    throw new FileLoadException("OKD scramble key is not loaded.");

        byte[] okdData = null;
        BinaryReader okdFileReader = new BinaryReader(data);
        if (okdFileReader.ReadBytes(4).SequenceEqual(Encoding.ASCII.GetBytes("SPRC")))
        {
            //Console.WriteLine("Skipping SPRC header");
            okdFileReader.BaseStream.Position = 16;
            okdData = okdFileReader.ReadBytes((int)(okdFileReader.BaseStream.Length - 16));
        }
        else
        {
            okdFileReader.BaseStream.Position = 0;
            okdData = okdFileReader.ReadBytes((int)okdFileReader.BaseStream.Length);
        }
        okdFileReader.Close();
        okdFileReader.Dispose();

        BinaryReader okdReader = new BinaryReader(new MemoryStream(okdData));
        MemoryStream stream = new MemoryStream();

        OKDHeader header = descramble(okdReader, stream, OKDFileType.OKD);
        this.Header = header;
        //File.WriteAllBytes("debug.bin", stream.ToArray());
        using (BinaryReader reader = new BinaryReader(stream))
        {
            //skip header
            reader.BaseStream.Seek(this.Header.HeaderSize, SeekOrigin.Begin);

            //Read byte until stream end
            List<OKDGenericChunk> chunksList = new List<OKDGenericChunk>();
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                if (header.AdpcmOffset > 0)
                {
                    if (reader.BaseStream.Position >= header.AdpcmOffset)
                    {
                        //seek until meet "YADD" chunk
                        while (true)
                        {
                            byte[] sig = reader.ReadBytes(4);
                            if (sig.SequenceEqual(Encoding.ASCII.GetBytes("YADD")))
                            {
                                //found "YADD" chunk, seek back 4 bytes
                                reader.BaseStream.Seek(-4, SeekOrigin.Current);
                                break;
                            }
                            if (reader.BaseStream.Position >= reader.BaseStream.Length)
                            {
                                // End of stream reached
                                break;
                            }
                        }
                        //reader.BaseStream.Seek(header.AdpcmOffset + 52, SeekOrigin.Begin);
                    }

                }

                byte[] chunkSig = reader.ReadBytes(4);
                if (reader.BaseStream.Position >= reader.BaseStream.Length)
                {
                    // End of stream reached
                    break;
                }
                uint chunkSize = reader.ReadUInt32BE();
                byte[] _data = reader.ReadBytes((int)chunkSize);
                OKDGenericChunk chunk = new OKDGenericChunk(chunkSig, _data);
                chunksList.Add(chunk);


            }
            this.Chunks = chunksList.ToArray();

        }
        //dispose
        stream.Close();
        stream.Dispose();
        okdReader.Close();
        okdReader.Dispose();
        data.Close();
        data.Dispose();


        //Process chunks
        List<OKDPTrack> tracks = new List<OKDPTrack>();
        byte pTrackCount = 0;
        foreach (var chunk in this.Chunks)
        {
            if (Encoding.ASCII.GetString(chunk.ChunkId).StartsWith("YPTI"))
            {
                OKDPTrackInfo trackInfo = new OKDPTrackInfo();
                trackInfo.Parse(chunk.Data);
                this.PTrackInfo = trackInfo;
            }
            if (Encoding.ASCII.GetString(chunk.ChunkId).StartsWith("YPXI"))
            {
                OKDExtendedPTrackInfo extendedTrackInfo = new OKDExtendedPTrackInfo();
                extendedTrackInfo.Parse(chunk.Data);
                this.PTrackInfo = extendedTrackInfo;
            }

            

            //PR*
            if (chunk.ChunkId.AsSpan().StartsWith(new byte[] { 0xff, (byte)'P', (byte)'R' }))
            {
                OKDPTrack track = new OKDPTrack();
                using (var reader = new BinaryReader(new MemoryStream(chunk.Data)))
                {
                    track.Parse(reader);
                }
                track.TrackID = chunk.ChunkId[3];
                tracks.Add(track);
                pTrackCount++;
            }

            //ADPCM
            if (Encoding.ASCII.GetString(chunk.ChunkId).StartsWith("YADD"))
            {
                List<byte[]> pcmData = new List<byte[]>();

                using (BinaryReader adpcmReader = new BinaryReader(new MemoryStream(chunk.Data)))
                {
                    while (adpcmReader.BaseStream.Position < adpcmReader.BaseStream.Length)
                    {
                        string headerSig = Encoding.ASCII.GetString(adpcmReader.ReadBytes(4));
                        uint adpcmSize = adpcmReader.ReadUInt32BE();
                        if (headerSig != "YAWV")
                        {
                            Console.WriteLine($"Invalid ADPCM chunk header: {headerSig}");
                            pcmData.Add(Array.Empty<byte>());
                            continue;
                        }


                        byte[] adpcm = adpcmReader.ReadBytes((int)adpcmSize);
                        if (adpcmSize > 0)
                            pcmData.Add(OKDADPCM.ConvertToPCM(adpcm));
                        else
                            pcmData.Add(Array.Empty<byte>());
                    }

                    this.BackChoursPCM = new byte[pcmData.Count][];
                    for (int i = 0; i < pcmData.Count; i++)
                    {
                        this.BackChoursPCM[i] = pcmData[i];
                    }
                }
            }
        }

        this.PTracks = tracks.ToArray();

        foreach (var track in this.PTracks)
        {
            track.ConvertToAbsoluteTimeTrack(GetPTrackInfoByID(track.TrackID));
        }

    }

    private void ResetTG(byte port, byte id, byte mode)
    {
        if (mode > 1)
        {
            throw new ArgumentException("Invalid mode.");
        }
        if (port >= MIDI_DEV_MAX_COUNT)
        {
            throw new ArgumentException($"Invalid port number. Maximum is {MIDI_DEV_MAX_COUNT - 1}.");
        }
        if(port < MIDIDev.Length)
            MIDIDev[port].ResetTG(id, mode);
    }
    public OKDPlayback[] GetPTrackPlaybacks(bool compressSysEx = true, int adpcmOffsetMs = 0)
    {
        if (this.PTracks == null || this.PTracks.Length == 0)
            return null;

        //--start sysex events compress routine

        if (compressSysEx)
        {
            //노트 시작시간 구하기
            uint sysExCompressedDiffOffset = 0;
            List<PTrackAbsoluteTimeEvent> compressedSysExEvents = new List<PTrackAbsoluteTimeEvent>();
            foreach (var track in this.PTracks)
            {
                //FirstNoteONTime이전에 있는 SysEx 마지막 시간을 구하기
                bool isSysExTrack = false;
                uint sysExEndTimeBeforeCompress = 0;
                for (int i = 0; i < track.PTrackAbsoluteEvents.Count; i++)
                {
                    var ev = track.PTrackAbsoluteEvents[i];
                    if (i == 0)
                    {
                        //제일 처음 이벤트가 SysEx 가 아닌 트랙이면 바로 종료
                        if (ev.Status != 0xF0)
                            break;
                    }
                    if (ev.Status == 0xF0)
                    {
                        isSysExTrack = true;
                        continue;
                    }
                    if (sysExEndTimeBeforeCompress < ev.AbsoluteTime)
                    {
                        sysExEndTimeBeforeCompress = track.PTrackAbsoluteEvents[i - 1].AbsoluteTime;
                        break;
                    }
                }

                if (isSysExTrack)
                {
                    var compressor = new OKDSysExCompresser();
                    uint absTime = 0;
                    for (int i = 0; i < track.PTrackAbsoluteEvents.Count; i++)
                    {
                        PTrackAbsoluteTimeEvent ev = track.PTrackAbsoluteEvents[i];
                        if (ev.Status == 0xF0 && ev.AbsoluteTime < sysExEndTimeBeforeCompress)
                        {
                            var result = compressor.CompressMidiData(ev.FullSysExData, out byte[] compressedData);
                            if (result == CompressionResult.InvalidMessage)
                            {
                                Console.WriteLine($"OKDSysExCompress: Invalid SysEx message at time {ev.AbsoluteTime} data:{BitConverter.ToString(ev.FullSysExData).Replace("-"," ")}");
                                compressedSysExEvents.Add(new PTrackAbsoluteTimeEvent(
                                    ev.Port,
                                    ev.Track,
                                    absTime,
                                    0xF0,
                                    ev.Data
                                    ));
                            }
                            if (compressedData != null)
                            {
                                compressedSysExEvents.Add(new PTrackAbsoluteTimeEvent(
                                    ev.Port,
                                    ev.Track,
                                    absTime,
                                    0xF0,
                                    compressedData[1..]
                                    ));
                                //1바이트당 0.5ms로 계산, 반올림
                                absTime = (uint)(Math.Round(compressedData.Length * 0.5) + absTime);
                            }
                            continue;
                        }

                        //마지막 남아있는 버퍼 flush(더미데이터 넣어서 완전히 종료)
                        var finalRes = compressor.CompressMidiData(new byte[] { 0x90, 0, 0 }, out byte[] fcompressedData);
                        if (fcompressedData != null)
                        {
                            compressedSysExEvents.Add(new PTrackAbsoluteTimeEvent(
                                ev.Port,
                                ev.Track,
                                absTime,
                                0xF0,
                                fcompressedData[1..]
                                ));
                            absTime = (uint)(Math.Round(fcompressedData.Length * 0.5) + absTime);
                        }
                        break;
                    }

                    //기존 sysex 이벤트 제거
                    int removedCount = track.PTrackAbsoluteEvents.RemoveAll(e => e.AbsoluteTime < sysExEndTimeBeforeCompress && e.Status == 0xF0);

                    //오프셋 계산
                    uint offset = sysExEndTimeBeforeCompress - absTime;

                    if (sysExCompressedDiffOffset < offset)
                    {
                        sysExCompressedDiffOffset = offset;
                    }

                }


            }

            foreach (var track in this.PTracks)
            {
                //기존 이벤트의 절대시간 오프셋만큼 감소
                foreach (var ev in track.PTrackAbsoluteEvents)
                {
                    //음수면 0으로
                    if (ev.AbsoluteTime < sysExCompressedDiffOffset)
                    {
                        ev.AbsoluteTime = 0;
                    }
                    else
                        ev.AbsoluteTime -= sysExCompressedDiffOffset;
                }

                //새로운 sysex 이벤트 선두에 삽입
                if (compressedSysExEvents.Count > 0)
                {
                    List<PTrackAbsoluteTimeEvent> portSysEx = compressedSysExEvents
                        .Where(e => e.Port == track.PTrackAbsoluteEvents[0].Port)
                        .ToList();

                    track.PTrackAbsoluteEvents.InsertRange(0, portSysEx);
                }



                //track.CalculateFirstNoteONTime();
                
            }
        }

        //--end of sysex events compress routine

        //calculate first note on time
        foreach (var track in this.PTracks)
        {
            track.CalculateFirstNoteONTime();
            if (this.FirstNoteONTime == 0 || this.FirstNoteONTime > track.FirstNoteOnTime)
            {
                this.FirstNoteONTime = track.FirstNoteOnTime;
            }
        }

        //calculate total play time
        foreach (var track in this.PTracks)
        {
            uint lastEventTime = track.PTrackAbsoluteEvents.LastOrDefault()?.AbsoluteTime ?? 0;
            if (this.TotalPlayTime < lastEventTime)
            {
                this.TotalPlayTime = lastEventTime;
            }
        }

        OKDPlayback[] playbacks = new OKDPlayback[this.MIDIDev.Length];
        for (byte i = 0; i < this.PTracks.Length; i++)
        {
            if(i < MIDIDev.Length)
            {
                //find if PTrack has F8(ADPCM Note ON) event
                bool hasF8 = this.PTracks[i].PTrackAbsoluteEvents.Any(e => e.Status == 0xF8);
                //Console.WriteLine($"Track {i} has F8 event: {hasF8}");
                OKDPlayback playback = new OKDPlayback(this.PTracks[i], this.MIDIDev[i], hasF8 ? this.BackChoursPCM : null , adpcmOffsetMs); //첫번째 트랙에만 adpcm 넣기
                playbacks[i] = playback;
            }

        }
        return playbacks;
    }

    public void ResetTGDevices(bool wait)
    {
        if(this.PTrackInfo is OKDExtendedPTrackInfo)
        {
            ResetTG(0, 0x51, 1);
            ResetTG(2, 0x51, 1);
        }
        else
        {
            ResetTG(0, 0x31, 0);
            ResetTG(1, 0x31, 0);
        }
       
        if(wait)
            Thread.Sleep(2000);
    }

    public void Transpose(int key) 
    { 
        if(this.PTrackInfo is OKDExtendedPTrackInfo) //TG1
        {
            MIDIDev[0].Transpose(0x51, key);
            if(MIDIDev.Length > 2)
                MIDIDev[2].Transpose(0x51, key);
        }
        else
        {
            MIDIDev[0].Transpose(0x31, key);
            if (MIDIDev.Length > 1)
                MIDIDev[1].Transpose(0x31, key);
        }
    }

    public void SetTGVolume(byte port, ushort volume)
    {
        if(MIDIDev.Length > port)
            MIDIDev[port].SetMasterVolume(volume);
    }
    public void SetTGVolumeAll(ushort vol)
    {
        for (byte i = 0; i < this.MIDIDev.Length; i++)
        {
            SetTGVolume(i, vol);
        }
    }

    public bool SetSongVolume(byte vol)
    {
        bool executed = false;

        if (vol < 0x80)
        {
            if (PTrackInfo is OKDExtendedPTrackInfo) //TGMode0
            {
                this.MIDIDev[0].SendMMSSysEx(0x03, vol);
                if (MIDIDev.Length > 2)
                    this.MIDIDev[2].SendMMSSysEx(0x03, vol);
                //this.MIDIDev[0].SetMMTTotalVolume(0x06, 0x04, vol, 0x7f);

                executed = true;
            }
            else
            {
                this.MIDIDev[0].SendMEGSysExYKS(0x06, 0x04, 0x7f, 0x7f);
                if (MIDIDev.Length > 2)
                {
                    this.MIDIDev[2].SendMEGSysExYKS(0x06, 0x04, vol, vol);
                }
                executed = true;
                
            }
        }
        return executed;
    }

    public void AdjustTGVolume()
    {
        //なぜかTGA,Bのボリュームが合わないので調整（Bの音が結構大きい）
        if (PTrackInfo is OKDExtendedPTrackInfo) //TGMode0
        {
            this.MIDIDev[0].SendMMSSysEx(0x03, 127);
            if (MIDIDev.Length > 2)
                this.MIDIDev[2].SendMMSSysEx(0x03, 65);
        }
        else
        {
            this.MIDIDev[0].SendMEGSysExYKS(0x06, 0x04, 0x7f, 0x7f);
            //this.MIDIDev[0].SetSGVolume(0x31, 127);
            //if (MIDIDev.Length > 1)
            //    this.MIDIDev[1].SetSGVolume(0x31, 80);
        }
    }

    public static byte ReadStatusByte(BinaryReader reader)
    {
        byte statusByte = reader.ReadByte();
        if ((statusByte & 0x80) == 0x80)
        {
            return statusByte;
        }
        else
        {
            throw new InvalidOperationException($"Invalid status byte detected: {statusByte:X2}");
        }
    }

    public static byte PeekStatusByte(BinaryReader reader)
    {
        byte statusByte = reader.ReadByte();
        reader.BaseStream.Seek(-1, SeekOrigin.Current); //Move back
        if ((statusByte & 0x80) == 0x80)
        {
            return statusByte;
        }
        else
        {
            throw new InvalidOperationException($"Invalid status byte detected: {statusByte:X2}");
        }
    }

    public static bool IsDataBytes(byte[] data) 
    {
        return data.All(b => b < 128);
    }
}

