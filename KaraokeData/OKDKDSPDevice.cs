using System.IO.Ports;
public class OKDKDSPDevice
{
    public IMIDIDevice MIDIDevice { get; set; }

    //TODO: Add Serial KDSP device support
    //public SerialPort SerialDevice { get; set; }

    private enum KDSP_DEV_TYPE
    {
        MIDI,
        SERIAL
    }

    private KDSP_DEV_TYPE kdspDeviceType;


    private void writeData(byte[] data)
    {
        if (kdspDeviceType == KDSP_DEV_TYPE.MIDI)
        {
            MIDIDevice.SendSysEx(data);
        }
        else if (kdspDeviceType == KDSP_DEV_TYPE.SERIAL)
        {
            //SerialDevice.Write(data, 0, data.Length);
        }
    }

    public OKDKDSPDevice(IMIDIDevice device)
    {
        this.MIDIDevice = device;
        this.kdspDeviceType = KDSP_DEV_TYPE.MIDI;
    }

    public OKDKDSPDevice(string serialPort)
    {
        //SerialPort = serialPort;
        SerialPort port = new SerialPort(serialPort, 250000, Parity.None, 8, StopBits.One);
        this.kdspDeviceType = KDSP_DEV_TYPE.SERIAL;
    }

    public void SetDSPVolume(byte volume)
    {
        //f0 43 7c 0a 10 00 01 01 70 f7
        byte[] sysexMessage = new byte[]
        {
            0xF0,
            0x43,
            0x7C,
            0x0A,
            0x10,
            0x00,
            0x01,
            0x01,
            volume,
            0xF7
        };

        this.writeData(sysexMessage);
    }

    //Not work
    public void SetDSPTone(byte high, byte low)
    {
        //F0 43 7C 0A 10 00 03 03 <HI> <LOW> 01 F7
        //0x22~0x5E

        if (high < 0x22 || high > 0x5E || low < 0x22 || low > 0x5E)
        {
            Console.WriteLine("KDSP Tone value out of range.");
        }

        byte[] sysexMessage = new byte[]
        {
            0xF0,
            0x43,
            0x7C,
            0x0A,
            0x10,
            0x00,
            0x03,
            0x03,
            high,
            low,
            0x01,
            0xF7
        };
        this.writeData(sysexMessage);
    }
}