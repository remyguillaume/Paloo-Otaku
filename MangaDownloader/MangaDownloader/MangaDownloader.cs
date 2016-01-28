using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace MangaDownloader
{
    internal class MangaDownloader 
    {
        public const string SourceRoot = "http://www.mangapanda.com";
        private static readonly WebClient WebClient = new WebClient();
        private const string ChapterPattern = @"document\['chapterno'\] = (.*);";
        private const string PagePattern = "<option value=\".*\" selected=\"selected\">(.*)</option>";
        private const string ImagePattern = "<img id=\"img\" width=\".*\" height=\".*\" src=\"(.*)\" alt=\".*\" name=\"img\" />";
        private const string NextPattern = "<a href=\".*\">Previous</a></span> <span class=\"next\"><a href=\"(.*)\">Next</a>";

        public void DownloadAll(Manga manga)
        {
            var url = DownloadPage(manga.Name, manga.DownloadDirectory, manga.StartUrl, manga.JustDownloaded);
            while (url != null)
            {
                url = DownloadPage(manga.Name, manga.DownloadDirectory, url, manga.JustDownloaded);
            }
        }

        private string DownloadPage(string name, string downloadDirectory, string url, List<string> justDownloaded)
        {
            var source = WebClient.DownloadString(url);

            // Chapter
            string chapter = null;
            var regex = new Regex(ChapterPattern);
            if (regex.IsMatch(source))
            {
                var match = regex.Match(source);
                chapter = match.Groups[1].Value.PadLeft(4, '0');
            }
            if (chapter == null)
                return null;

            if (!justDownloaded.Contains(chapter))
                justDownloaded.Add(chapter);

            // Page
            string page = null;
            regex = new Regex(PagePattern);
            if (regex.IsMatch(source))
            {
                var match = regex.Match(source);
                page = match.Groups[1].Value.PadLeft(2, '0');
            }
            if (page == null)
                throw new NotImplementedException("Page number not found");

            // Picture
            string imgUrl = null;
            regex = new Regex(ImagePattern);
            if (regex.IsMatch(source))
            {
                var match = regex.Match(source);
                imgUrl = match.Groups[1].Value;
            }
            if (imgUrl == null)
                throw new NotImplementedException("Image URL not found");

            // Local path
            var dir = Path.Combine(downloadDirectory, chapter);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            var extension = Path.GetExtension(imgUrl);
            string filename = chapter + '-' + page + extension;
            string downloadPath = Path.Combine(dir, filename);

            if (File.Exists(downloadPath))
                throw new NotImplementedException("File already exists !");

            // Download
            DebugHelper.WriteLine(String.Format("Downloading {3} {0}-{1} at {2}...", chapter, page, imgUrl, name));
            WebClient.DownloadFile(imgUrl, downloadPath);

            // Find next page
            string next = null;
            regex = new Regex(NextPattern);
            if (regex.IsMatch(source))
            {
                var match = regex.Match(source);
                next = SourceRoot + match.Groups[1].Value;
            }

            return next;
        }
    }
}