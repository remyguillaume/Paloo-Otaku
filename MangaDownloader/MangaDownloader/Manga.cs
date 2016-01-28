using System.Collections.Generic;

namespace MangaDownloader
{
    internal class Manga
    {
        public string Name { get; private set; }
        public string DownloadDirectory { get; private set; }
        public string StartUrl { get; private set; }

        public List<string> JustDownloaded { get; private set; }

        internal Manga(string name, string downloadDirectory, string startUrl)
        {
            Name = name;
            DownloadDirectory = downloadDirectory;
            StartUrl = startUrl;
            JustDownloaded = new List<string>();
        }
    }
}