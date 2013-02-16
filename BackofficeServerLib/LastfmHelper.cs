using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Lastfm.Services;

namespace BackofficeServerLib
{
    public class LastfmHelper
    {
        private string API_KEY = "2aa9405aa03968a301a1dcc26657c2e4";
        private string API_SECRET = "b4f40085af42eda025cafa48701e33f1";

        private Session session;

        public LastfmHelper()
        {
             session = new Session(API_KEY, API_SECRET);
        }

        public string[] getTracks(string mArtist)
        {
            
            Artist artist = new Artist(mArtist, session);

            TopTrack[] tracks = artist.GetTopTracks();

            string[] rTracks = new string[tracks.Length];

            int i = 0;
            foreach (TopTrack t in tracks)
            {
                rTracks[i] = t.Item.Title;
                i++;
            }
            return rTracks;
        }

        public string[] getAlbums(string mArtist)
        {
            
            Artist artist = new Artist(mArtist, session);

            TopAlbum[] albums = artist.GetTopAlbums();

            string[] rAlbums = new string[albums.Length];

            int i = 0;
            foreach (TopAlbum a in artist.GetTopAlbums())
            {
                rAlbums[i] = a.Item.Name;
                i++;
            }

            return rAlbums;
        }

        public string[] getSimilarArtists(string mArtist)
        {
            
            Artist artist = new Artist(mArtist, session);

            string[] mArtists = new string[10];
            int i = 0;
            ArtistSearch s = new ArtistSearch(mArtist, session);

            foreach (Artist a in s.GetPage(1))
            {
                if (i == 10) break;
                mArtists[i] = a.Name.ToString();
                i++;
            }
            return mArtists;
        }
    }
}