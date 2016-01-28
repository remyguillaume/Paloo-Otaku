using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MangaDownloader
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var mangas = new List<Manga>();

            // One Piece
            string dir = @"D:\Mangas\OnePiece";
            mangas.Add(new Manga("One Piece", dir, String.Format(@"http://www.mangapanda.com/one-piece/{0}", GetNextChapter(dir))));

            // Fairy Tail
            dir = @"D:\Mangas\FairyTail";
            mangas.Add(new Manga("Fairy Tail", dir, String.Format( @"http://www.mangapanda.com/fairy-tail/{0}", GetNextChapter(dir))));

            // Fairy Tail Zero
            dir = @"D:\Mangas\FairyTailZero";
            mangas.Add(new Manga("Fairy Tail Zero", dir, String.Format(@"http://www.mangapanda.com/fairy-tail-zero/{0}", GetNextChapter(dir))));

            // Bleach
            dir = @"D:\Mangas\Bleach";
            mangas.Add(new Manga("Bleach", dir, String.Format(@"http://www.mangapanda.com/bleach/{0}", GetNextChapter(dir))));

            // Death Node
            dir = @"D:\Mangas\DeathNote";
            mangas.Add(new Manga("Death Note", dir, String.Format(@"http://www.mangapanda.com/death-note/{0}", GetNextChapter(dir))));

            // Bloody Monday
            dir = @"D:\Mangas\BloodyMonday";
            mangas.Add(new Manga("Bloody Monday", dir, String.Format(@"http://www.mangapanda.com/bloody-monday/{0}", GetNextChapter(dir))));

            // Bloody Monday Saison 2
            dir = @"D:\Mangas\BloodyMondaySeason2";
            mangas.Add(new Manga("Bloody Monday Season 2", dir, String.Format(@"http://www.mangapanda.com/bloody-monday-season-2/{0}", GetNextChapter(dir))));

            // Bloody Monday Last Season
            dir = @"D:\Mangas\BloodyMondayLastSeason";
            mangas.Add(new Manga("Bloody Monday Last Season", dir, String.Format(@"http://www.mangapanda.com/bloody-monday-last-season/{0}", GetNextChapter(dir))));

            // Bloody Monday Last Season
            dir = @"D:\Mangas\GTOShonan14Days";
            mangas.Add(new Manga("GTO Shonan 14 Days", dir, String.Format(@"http://www.mangapanda.com/gto-shonan-14-days/{0}", GetNextChapter(dir))));

            // Yakitate Japan
            dir = @"D:\Mangas\YakitateJapan";
            mangas.Add(new Manga("Yakitate Japan", dir, String.Format(@"http://www.mangapanda.com/yakitate-japan/{0}", GetNextChapter(dir))));

            // One Punch man
            dir = @"D:\Mangas\OnePunchMan";
            mangas.Add(new Manga("OnePunch Man", dir, String.Format(@"http://www.mangapanda.com/onepunch-man/{0}", GetNextChapter(dir))));

            foreach (var manga in mangas)
            {
                new global::MangaDownloader.MangaDownloader().DownloadAll(manga);
            }

            foreach (var manga in mangas)
            {
                if (manga.JustDownloaded.Any())
                {
                    string chapters = manga.JustDownloaded.Aggregate<string, string>(null, (current, chapter) => current + (chapter + " - "));
                    chapters = chapters.Remove(chapters.Length-1);
                    DebugHelper.WriteLine(String.Format("New chapter(s) downloaded for {0} : {1}", manga.Name, chapters));
                }
            }
        }

        private static string GetNextChapter(string directory)
        {
            if (!Directory.Exists(directory))
                return "1";

            string[] directories = Directory.GetDirectories(directory);
            if (!directories.Any())
                return "1";

            var dirNums = directories.Select(d => Convert.ToInt32(new DirectoryInfo(d).Name)).ToList();
            return (dirNums.Max() + 1).ToString();
        }
    }
}