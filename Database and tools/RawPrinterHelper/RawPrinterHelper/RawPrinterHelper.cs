using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Text;

namespace RawPrinterHelper
{
    public class cRawPrinterHelper : IDisposable
    {
        // Structure and API declarions:
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public class DOCINFOA
        {
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDocName;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pOutputFile;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDataType;
        }
        [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, IntPtr pd);

        [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartDocPrinter(IntPtr hPrinter, Int32 level, [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);

        [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "WritePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32 dwCount, out Int32 dwWritten);


        public string PrinterName { get; set; }
        private IntPtr hPrinter = new IntPtr(0);
        private const int LOTSIZE = 6;
        private int lotLabelNumber = 0;
        public cRawPrinterHelper(string pPrinterName)
        {
            PrinterName = pPrinterName;
            if (!OpenPrinter(pPrinterName.Normalize(), out hPrinter, IntPtr.Zero))
            {
                throw new Exception("Error opening printer.");
            }
        }

        // SendBytesToPrinter()
        // When the function is given a printer name and an unmanaged array
        // of bytes, the function sends those bytes to the print queue.
        // Returns true on success, false on failure.
        public bool SendBytesToPrinter(IntPtr pBytes, Int32 dwCount)
        {
            Int32 dwError = 0, dwWritten = 0;
            bool bSuccess = false; // Assume failure unless you specifically succeed.
                                   // Start a page.
            DOCINFOA di = new DOCINFOA();
            di.pDocName = "My C#.NET RAW Document";
            di.pDataType = "RAW";
            if (lotLabelNumber % LOTSIZE == 0)
            {
                StartDocPrinter(hPrinter, 1, di);
                lotLabelNumber = 0;
            }
            lotLabelNumber++;
            if (StartPagePrinter(hPrinter))
            {
                // Write your bytes.
                bSuccess = WritePrinter(hPrinter, pBytes, dwCount, out dwWritten);
                EndPagePrinter(hPrinter);
            }
            if (lotLabelNumber % LOTSIZE == 0)
            {
                EndDocPrinter(hPrinter);
            }
            
            
            // If you did not succeed, GetLastError may give more information
            // about why not.
            if (bSuccess == false)
            {
                dwError = Marshal.GetLastWin32Error();
            }
            return bSuccess;
        }

        public bool SendFileToPrinter(string szFileName)
        {
            // Open the file.
            FileStream fs = new FileStream(szFileName, FileMode.Open);
            // Create a BinaryReader on the file.
            BinaryReader br = new BinaryReader(fs);
            // Dim an array of bytes big enough to hold the file's contents.
            Byte[] bytes = new Byte[fs.Length];
            bool bSuccess = false;
            // Your unmanaged pointer.
            IntPtr pUnmanagedBytes = new IntPtr(0);
            int nLength;

            nLength = Convert.ToInt32(fs.Length);
            // Read the contents of the file into the array.
            bytes = br.ReadBytes(nLength);
            // Allocate some unmanaged memory for those bytes.
            pUnmanagedBytes = Marshal.AllocCoTaskMem(nLength);
            // Copy the managed byte array into the unmanaged array.
            Marshal.Copy(bytes, 0, pUnmanagedBytes, nLength);
            // Send the unmanaged bytes to the printer.
            bSuccess = SendBytesToPrinter(pUnmanagedBytes, nLength);
            // Free the unmanaged memory that you allocated earlier.
            Marshal.FreeCoTaskMem(pUnmanagedBytes);
            return bSuccess;
        }
        public bool SendStringToPrinter(string szString)
        {
            IntPtr pBytes;
            Int32 dwCount;
            // How many characters are in the string?
            dwCount = szString.Length;
            // Assume that the printer is expecting ANSI text, and then convert
            // the string to ANSI text.
            pBytes = Marshal.StringToCoTaskMemAnsi(szString);
            // Send the converted ANSI string to the printer.
            SendBytesToPrinter(pBytes, dwCount);
            Marshal.FreeCoTaskMem(pBytes);
            return true;
        }
        public bool SendUTF8StringToPrinter(string szString, int num = 1)
        {
            IntPtr pBytes = new IntPtr(0);
            Int32 dwCount;
            byte[] aBytes;
            var enc = new UTF8Encoding(true, true);
            aBytes = enc.GetBytes(szString);

            // How many characters are in the string?
            dwCount = aBytes.Length;
            pBytes = Marshal.AllocCoTaskMem(dwCount);
            // Assume that the printer is expecting ANSI text, and then convert
            // the string to ANSI text.
            Marshal.Copy(aBytes, 0, pBytes, dwCount);
            // Send the converted ANSI string to the printer.
            for (var i = 1; i <= num; i++)
                if (!SendBytesToPrinter(pBytes, dwCount))
                {
                    Marshal.FreeCoTaskMem(pBytes);
                    return false;
                }
            Marshal.FreeCoTaskMem(pBytes);
            return true;
        }

        public void Dispose()
        {
            EndDocPrinter(hPrinter);
            ClosePrinter(hPrinter);
        }
    }

}

