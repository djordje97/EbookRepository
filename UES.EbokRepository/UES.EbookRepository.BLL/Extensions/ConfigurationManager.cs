using System;
using System.Collections.Generic;
using System.Text;

namespace UES.EbookRepository.BLL.Extensions
{
    public static class ConfigurationManager
    {
        public static string IndexDir { get; } = @"C:\Users\Djole\Git Repository\EbookStore\indexDir\";
        public static string FileDir { get; } = @"C:\Users\Djole\Git Repository\EbookStore\fileDir\";
        public static string TempDir { get; } = @"C:\Users\Djole\Git Repository\EbookStore\tempDir\";
    }
}
