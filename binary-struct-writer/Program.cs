using System;
using System.IO;
using System.Runtime.InteropServices;

namespace cs_con_binary_writer
{
    // 構造体のマーシャリングをカスタマイズする
    // https://docs.microsoft.com/ja-jp/dotnet/standard/native-interop/customize-struct-marshalling
    struct BinaryData {
        public float item1;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string item2;
        public Int32 item3;
        public bool item4;

        // public BinaryData(float a, string b, Int32 c, bool d)
        // {
        //     this.item1 = a;
        //     this.item2 = b;
        //     this.item3 = c;
        //     this.item4 = d;

        // }
    }

    class Program
    {
        private static string fileName = "Settings-struct.dat";
        static void Main(string[] args)
        {
            // WriteBinaryData();

            var prog = new Program2(fileName);
            prog.WriteBinaryData();
        }

        private static void WriteBinaryData()
        {
            // BinaryData bd = new BinaryData {item1 = 1.250F, item2 = "c:\\Temp", item3 = 10, item4 = true};
            // BinaryData bd = new BinaryData() {item1 = 1.250F, item2 = "c:\\Temp", item3 = 10, item4 = true};
            var bd = new BinaryData{item1 = 1.250F, item2 = "c:\\Temp", item3 = 10, item4 = true};
            // BinaryData bd = new BinaryData(1.250F, "c:\\Temp", 10, true);

            int size = Marshal.SizeOf(bd);
            IntPtr ptr = Marshal.AllocHGlobal(size);
            byte[] bytes = new byte[size];
            Marshal.StructureToPtr(bd, ptr, false);
            Marshal.Copy(ptr, bytes, 0, size);

            using (var stream = File.Open(fileName, FileMode.Create))
            {
                using (var writer = new BinaryWriter(stream))
                {
                    writer.Write(bytes);
                }
            }
        }
    }

    internal class Program2
    {
        private string fileName;

        public Program2(string fileName)
        {
            this.fileName = fileName;
        }

        internal void WriteBinaryData()
        {
            var bd = new BinaryData{item1 = 1.250F, item2 = "c:\\Temp", item3 = 10, item4 = true};

            byte[] bytes = BuildStruct(bd);

            using (var stream = File.Open(fileName, FileMode.Create))
            {
                using (var writer = new BinaryWriter(stream))
                {
                    writer.Write(bytes);
                }
            }

        }

        private byte[] BuildStruct(BinaryData bd)
        {
            int size = Marshal.SizeOf(bd);
            IntPtr ptr = Marshal.AllocHGlobal(size);
            byte[] bytes = new byte[size];
            Marshal.StructureToPtr(bd, ptr, false);
            Marshal.Copy(ptr, bytes, 0, size);

            return bytes;
        }
    }
}
