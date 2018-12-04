using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EBookStore.Configuration
{
    public static class ConfigurationManager
    {
        public static string IndexDir { get; } = @"C:\Users\Djole\source\repos\EbookRepository\EBookStore\indexDir\";
        public static string FileDir { get; } = @"C:\Users\Djole\source\repos\EbookRepository\EBookStore\fileDir\";
        public static string TempDir { get; } = @"C:\Users\Djole\source\repos\EbookRepository\EBookStore\tempDir\";
    }
}
