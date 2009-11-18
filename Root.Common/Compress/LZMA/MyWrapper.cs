using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SevenZip;

namespace Root.Classes.Compress
{
    public static class Properties
    {
        static readonly int dictionary = 1 << 23;
        static readonly Int32 posStateBits = 2;
        static readonly Int32 litContextBits = 3; // for normal files and UInt32=0 for 32-bit data
        static readonly Int32 litPosBits = 0;// for normal files and UInt32=2 for 32-bit data
        static readonly Int32 algorithm = 2;
        static readonly Int32 numFastBytes = 128;
        static readonly String matchFilter = "bt4";
        static readonly bool eos = false;

        public static CoderPropID[] propIDs = 
				{
					CoderPropID.DictionarySize,
					CoderPropID.PosStateBits,
					CoderPropID.LitContextBits,
					CoderPropID.LitPosBits,
					CoderPropID.Algorithm,
					CoderPropID.NumFastBytes,
					CoderPropID.MatchFinder,
					CoderPropID.EndMarker
				};

        // these are the default properties, keeping it simple for now:
        public static object[] properties = 
				{
					dictionary,
					posStateBits,
					litContextBits,
					litPosBits,
					algorithm,
					numFastBytes,
					matchFilter,
					eos
				};
    }

    public class Pack : SevenZip.Compression.LZMA.Encoder, SevenZip.ICodeProgress
    {
        private ProgressBar pb = null;
        private Int64 totalSize = 0;

        public Pack(ProgressBar progressBar)
        {
            pb = progressBar;
        }

        void SevenZip.ICodeProgress.SetProgress(Int64 inSize, Int64 outSize)
        {
            pb.Value = (int)(inSize / (totalSize / 100));
            Application.DoEvents();
        }

        public void PackingFile(string inFile, string outFile, bool del_inFile)
        {
            try
            {
                using (FileStream nonpackFile = new FileStream(inFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (FileStream packFile = new FileStream(outFile, FileMode.Create, FileAccess.Write, FileShare.Write))
                    {
                        byte[] npb = new byte[nonpackFile.Length];
                        nonpackFile.Read(npb, 0, npb.Length);

                        byte[] pb = Packing(npb);
                        packFile.Write(pb, 0, pb.Length);
                    }
                }
                if (del_inFile)
                {
                    File.Delete(inFile);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        public byte[] Packing(byte[] inByte)
        {
            MemoryStream inMs = new MemoryStream(inByte);
            MemoryStream outMs = new MemoryStream();

            this.SetCoderProperties(Properties.propIDs, Properties.properties);
            this.WriteCoderProperties(outMs);
            totalSize = inMs.Length;
            for (int i = 0; i < 8; i++)
                outMs.WriteByte((Byte)(inMs.Length >> (8 * i)));

            this.Code(inMs, outMs, -1, -1, (SevenZip.ICodeProgress)this);
            return outMs.ToArray();
        }
    }

    public class UnPack : SevenZip.Compression.LZMA.Decoder, SevenZip.ICodeProgress
    {
        private ProgressBar pb = null;
        private Int64 totalSize = 0;

        public UnPack(ProgressBar progressBar)
        {
            pb = progressBar;
        }

        void SevenZip.ICodeProgress.SetProgress(Int64 inSize, Int64 outSize)
        {
            pb.Value = (int)(inSize / (totalSize / 100));
            Application.DoEvents();
        }

        public void UnPackingFile(string inFile, string outFile, bool del_inFile)
        {            
            try
            {
                using (FileStream packFile = new FileStream(inFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (FileStream nonpackFile = new FileStream(outFile, FileMode.Create, FileAccess.Write, FileShare.Write))
                    {
                        byte[] pb = new byte[packFile.Length];
                        packFile.Read(pb, 0, pb.Length);

                        byte[] npb = UnPacking(pb);
                        nonpackFile.Write(npb, 0, npb.Length);
                    }
                }
                if (del_inFile)
                {
                    File.Delete(inFile);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        public byte[] UnPacking(byte[] inByte)
        {
            MemoryStream inMs = new MemoryStream(inByte);
            inMs.Seek(0, 0);
            MemoryStream outMs = new MemoryStream();

            byte[] properties = new byte[5];
            if (inMs.Read(properties, 0, 5) != 5)
                throw new Exception("input .lzma is too short");

            totalSize = inMs.Length;
            long out_size = 0;

            for (int i = 0; i < 8; i++)
            {
                int v = inMs.ReadByte();
                if (v < 0)
                    throw new Exception("LZMA: Can't Read 1");
                out_size |= ((long)(byte)v) << (8 * i);
            }

            this.SetDecoderProperties(properties);
            long compressed_size = inMs.Length - inMs.Position;

            this.Code(inMs, outMs, compressed_size, out_size, (SevenZip.ICodeProgress)this);

            return outMs.ToArray();
        }
    }
}
