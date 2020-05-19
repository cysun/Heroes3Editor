using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Heroes3Editor.Models
{
    public class Game
    {
        private readonly string _file;

        public byte[] Bytes { get; }

        public Game(string file)
        {
            _file = file;

            using var fileStream = (new FileInfo(file)).OpenRead();
            using var gzipStream = new GZipStream(fileStream, CompressionMode.Decompress);
            using var memoryStream = new MemoryStream();
            gzipStream.CopyTo(memoryStream);
            Bytes = memoryStream.ToArray();
        }

        public void Save()
        {
            SaveAs(_file);
        }

        public void SaveAs(string file)
        {
            using var fileStream = (new FileInfo(file)).OpenWrite();
            using var gzipStream = new GZipStream(fileStream, CompressionMode.Compress);
            using var memoryStream = new MemoryStream(Bytes);
            memoryStream.CopyTo(gzipStream);
        }
    }
}
