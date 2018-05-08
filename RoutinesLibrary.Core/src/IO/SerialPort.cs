using System;
using System.Collections;
using System.IO.Ports;
using System.Threading;

namespace RoutinesLibrary.Core.IO
{
    public class SerialPortConfig
    {
        public int BaudRate = 500000;
        public int DataBits = 8;
        public StopBits StopBits = StopBits.One;
        public Parity Parity = Parity.Even;
        public Handshake Handshake = Handshake.None;
        public int WriteBufferSize = 512;
        public int ReadBufferSize = 512;
    }

    public class SerialPort
    {
        // All SerialPorts opened
        private static Hashtable _openPorts = new Hashtable();

        // SerialPort
        private System.IO.Ports.SerialPort m_serialPort;
        private string _serialPortName = "";
        private Thread _ThreadProcessDataReceived;
        private static Hashtable _IsAliveThreadProcessDataReceived = new Hashtable();


        #region Events

        public delegate void DataReceivedEventHandler();
        private DataReceivedEventHandler DataReceivedEvent;

        public event DataReceivedEventHandler DataReceived
        {
            add { DataReceivedEvent = (DataReceivedEventHandler) System.Delegate.Combine(DataReceivedEvent, value); }
            remove { DataReceivedEvent = (DataReceivedEventHandler) System.Delegate.Remove(DataReceivedEvent, value); }
        }

        #endregion


        #region Constructors / Destructors

        /// <summary>
        /// Class constructor. Start listen on port
        /// </summary>
        /// <param name="_serialPort"></param>
        /// <remarks></remarks>
        public SerialPort(System.IO.Ports.SerialPort _serialPort)
        {
            m_serialPort = _serialPort;

            //Listen received data
            _IsAliveThreadProcessDataReceived[_serialPort.PortName] = true;
            _ThreadProcessDataReceived = new Thread(new ThreadStart(Process_DataReceived));
            _ThreadProcessDataReceived.Name = "Worker_DataReceived_SerialPort";
            _ThreadProcessDataReceived.Start();
        }

        #endregion


        #region Properties

        public System.IO.Ports.SerialPort GetSerialPort
        {
            get { return m_serialPort; }
        }

        #endregion


        #region Configuration

        public void OpenPort(SerialPortConfig _serialPortConfig)
        {
            if (OpenPort(m_serialPort, _serialPortConfig))
            {
                _serialPortName = m_serialPort.PortName;
            }
        }

        public static bool OpenPort(System.IO.Ports.SerialPort _serialPort, SerialPortConfig _serialPortConfig)
        {
            bool portOpened = false;

            if (_serialPortConfig == null)
            {
                _serialPortConfig = new SerialPortConfig();
            }

            try
            {
                _serialPort.DataBits = _serialPortConfig.DataBits;
                _serialPort.StopBits = _serialPortConfig.StopBits;
                _serialPort.Parity = _serialPortConfig.Parity;
                _serialPort.Handshake = _serialPortConfig.Handshake;
                _serialPort.WriteBufferSize = _serialPortConfig.WriteBufferSize;
                _serialPort.ReadBufferSize = _serialPortConfig.ReadBufferSize;
                _serialPort.Encoding = System.Text.Encoding.GetEncoding(28591);

                //Causes an error if it is being used
                _serialPort.Open();
                _openPorts.Add(_serialPort.PortName, _serialPort);
                _serialPort.DiscardInBuffer();
                _serialPort.DiscardOutBuffer();

                portOpened = true;
            }
            catch (Exception ex)
            {
                //Console.WriteLine(System.Reflection.MethodInfo.GetCurrentMethod().Name + ". Error: " + ex.Message);
            }

            if (portOpened)
            {
                try
                {
                    //Causes error in Mono for non-standard baud rate
                    _serialPort.BaudRate = _serialPortConfig.BaudRate;
                }
                catch (Exception ex)
                {
                    //Console.WriteLine(System.Reflection.MethodInfo.GetCurrentMethod().Name + ". Error: " + ex.Message);
                    ForceSetBaudRate(_serialPort.PortName, _serialPortConfig.BaudRate);
                }
            }

            return portOpened;
        }

        public static bool IsOpen(System.IO.Ports.SerialPort _serialPort)
        {
            return _openPorts.Contains(_serialPort.PortName);
        }

        /// <summary>
        /// Release resources
        /// </summary>
        public void Dispose()
        {
            try
            {
                _IsAliveThreadProcessDataReceived[m_serialPort.PortName] = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(System.Reflection.MethodInfo.GetCurrentMethod().Name + ". Error (SerialPort Dispose): " + ex.Message);
            }
            
            DataReceivedEvent = null;

            try
            {
                if (m_serialPort != null)
                {
                    string portName = m_serialPort.PortName;
                    m_serialPort.Close();
                    m_serialPort.Dispose();
                    m_serialPort = null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(System.Reflection.MethodInfo.GetCurrentMethod().Name + ". Error: " + ex.Message);
            }

            if (_openPorts.Contains(_serialPortName))
            {
                _openPorts.Remove(_serialPortName);
            }
        }

        public static void Dispose(System.IO.Ports.SerialPort _serialPort)
        {
            if (_serialPort == null)
            {
                return;
            }
            string portName = "";

            try
            {
                if (_serialPort != null)
                {
                    portName = _serialPort.PortName;
                    _serialPort.Close();
                    _serialPort.Dispose();
                    _serialPort = null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(System.Reflection.MethodInfo.GetCurrentMethod().Name + ". Error: " + ex.Message);
            }

            if (_openPorts.Contains(portName))
            {
                _openPorts.Remove(portName);
            }
        }

        #endregion


        #region Read / Write

        public int BytesToRead()
        {
            int bytesToRead = 0;
            try
            {
                bytesToRead = m_serialPort.BytesToRead;
            }
            catch (Exception ex)
            {
                Console.WriteLine(System.Reflection.MethodInfo.GetCurrentMethod().Name + ". Error: " + ex.Message);
            }
            return bytesToRead;
        }

        public byte ReadByte()
        {
            byte readByte = 0;
            try
            {
                readByte = (byte) m_serialPort.ReadByte();
            }
            catch (Exception ex)
            {
                Console.WriteLine(System.Reflection.MethodInfo.GetCurrentMethod().Name + ". Error: " + ex.Message);
            }
            return readByte;
        }

        public byte[] ReadBytesFromPort(int iMaxBuffer = 512)
        {
            return ReadBytesFromPort(m_serialPort, iMaxBuffer);
        }

        public static byte[] ReadBytesFromPort(System.IO.Ports.SerialPort _serialPort, int iMaxBuffer = 512)
        {
            byte[] dataIn = new byte[0];
            int index = 0;

            try
            {
                byte[] datosInBytes = new byte[iMaxBuffer];

                while (_serialPort.BytesToRead != 0)
                {
                    datosInBytes[index] = (byte) (_serialPort.ReadByte());
                    index++;
                    if (index >= iMaxBuffer)
                    {
                        break;
                    }
                }

                dataIn = new byte[index];
                if (index > 0)
                {
                    Array.Copy(datosInBytes, dataIn, index);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(System.Reflection.MethodInfo.GetCurrentMethod().Name + ". Error: " + ex.Message);
                dataIn = new byte[0];
            }

            return dataIn;
        }

        public void Write(byte[] _bytes)
        {
            m_serialPort.Write(_bytes, 0, _bytes.Length);
        }

        #endregion


        #region Private methods

        private void Process_DataReceived()
        {
            string portName = m_serialPort.PortName;

            while ((bool)_IsAliveThreadProcessDataReceived[portName])
            {
                try
                {
                    if (m_serialPort.IsOpen)
                    {
                        //Causes an error if it is being used
                        if (m_serialPort.BytesToRead > 0)
                        {
                            DataReceivedEvent?.Invoke();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(System.Reflection.MethodInfo.GetCurrentMethod().Name + ". Error: " + ex.Message);
                }

                Thread.Sleep(10);
            }
        }

        private static void ForceSetBaudRate(string portName, int baudRate)
        {
            //It is not mono === not linux!
            if (Type.GetType("Mono.Runtime") == null)
            {
                return;
            }

            Console.WriteLine(portName);
            string arg = string.Format("stty -F {0} speed {1}", portName, baudRate);
            var proc = new System.Diagnostics.Process();
            proc.EnableRaisingEvents = false;
            proc.StartInfo.FileName = "sudo";
            proc.StartInfo.Arguments = arg;

            proc.Start();
            proc.WaitForExit();
        }

        #endregion
    }
}
