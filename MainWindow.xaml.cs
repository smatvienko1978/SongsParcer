using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SongsParcer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    class Song
    {
        public string Artist { get; set; }
        public string SongName { get; set; }
        public string Year { get; set; }
        public string Genre { get; set; }
        public string UrlPicture { get; set; }

        public Song(string artist, string songName, string year, string genre, string urlPicture)
        {
            Artist = artist;
            SongName = songName;
            Year = year;
            Genre = genre;
            UrlPicture = urlPicture;
        }
        public override string ToString()
        {
            return Artist + "\t" + SongName + "\t" + Year + "\t" + Genre;
        }
    }

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            HtmlDocument hap = new HtmlDocument();
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load("https://www.virtualdj.com/cloudlists/9111/index.html");

            var nodes = doc.DocumentNode.SelectNodes("//section[@id='cloudlist_list']//section[@class='fullgrid']");

            var list = new List<Song>();

            if (nodes != null)
            {
                foreach (HtmlNode node in nodes)
                {
                    var htmlDoc = new HtmlDocument();
                    htmlDoc.LoadHtml(node.InnerHtml);
                    var urlSrc = htmlDoc.DocumentNode.SelectSingleNode("//img[@class='getinfo_image']").Attributes["src"].Value;

                    var parseString = node.InnerText;
                    var parse = parseString.Split("\r\n");
                    var song = new Song(parse[3], parse[4], parse[7].Remove(0, 6), parse[8].Remove(0, 6), urlSrc);
                    list.Add(song);
                }
            }

            InitializeComponent();

            songList.ItemsSource = list;
        }
    }
}
