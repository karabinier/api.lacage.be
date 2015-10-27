using Newtonsoft.Json;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace api.lacage.be.Controllers
{



    public class Artist
    {
        public Artist(string artistName)
        {
            this.ArtistName = artistName;
        }

        public string ArtistName { get; set; }
    }

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/spotifysearch")]
    public class SpotifySearchController : BaseSpotifyController
    {
        public SpotifySearchController()
        {
            _refreshToken = "AQAjwkpsIMlHCdsLkgy8Dn-_Vj-FowL-VpqjRoiP9kHgZ-0R1ugy3OF2cdx6Y31InVJfjj3DgpVx0CcFxlQokLr8q99LmMHbtWms3c4vPfwJD8mUE-a8VVIR2uhlddis8fs";
            _redirectUriUsed = "http://localhost/shoppingapi.lacage.be/api/";
        }


        [Route("GetAlbumsByArtists")]
        public HttpResponseMessage GetAlbumsByArtists(string[] artistIds)
        {
            var result = new List<SimpleAlbum>();

            var api = new SpotifyAPI.Web.SpotifyWebAPI()
            {
                AccessToken = this.AccessToken.AccessToken,
                TokenType = this.AccessToken.TokenType,
                UseAuth = true
            };

            foreach (var artistId in artistIds)
            {
                var albumPage = api.GetArtistsAlbums(artistId, SpotifyAPI.Web.Enums.AlbumType.All);

                result.AddRange(albumPage.Items);
            }

                return this.Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("GetEels")]
        public HttpResponseMessage Get()
        {
            string artistId = "3zunDAtRDg7kflREzWAhxl";

            //var result = DownloadData<FullArtist>(string.Format("https://api.spotify.com/v1/artists/" + artistId));


            var api = new SpotifyAPI.Web.SpotifyWebAPI() 
            { 
                AccessToken = this.AccessToken.AccessToken, 
                TokenType = this.AccessToken.TokenType,
                UseAuth = true 
            };
            
            var artists = new List<FullArtist>();
            var tenArtists = api.GetFollowedArtists(10);
            artists.AddRange(tenArtists.Artists.Items);
            while(artists.Count < tenArtists.Artists.Total)
            {
                var lastArtistId = artists.Last().Id;
                tenArtists = api.GetFollowedArtists(10, lastArtistId);
                artists.AddRange(tenArtists.Artists.Items);
            }
            
                        
            var artistsByGenre = new Dictionary<string, List<FullArtist>>();
            foreach (var artist in artists)
            {
                var genresForArtist = artist.Genres.Count == 0 ? 
                    new List<string> { "Unknown genre" } : 
                    artist.Genres;

                addArtistToCollection(genresForArtist, artist, artistsByGenre);                
                
            }


            return this.Request.CreateResponse(HttpStatusCode.OK, artistsByGenre);
        }

        private void addArtistToCollection(List<string> genres, FullArtist artist, Dictionary<string, List<FullArtist>> artistsByGenre)
        {
            foreach (var genre in genres)
            {
                if (artistsByGenre.Keys.Contains(genre))
                    artistsByGenre[genre].Add(artist);
                else
                    artistsByGenre.Add(genre, new List<FullArtist> { artist });
            }
        }


        
        // GET: Spotify
        //http://jaykay/shoppingapi.lacage.be/api/spotifysearch?artist=sdff
        public HttpResponseMessage Get(string codeReceived, string artistId)
        {
            if(string.IsNullOrEmpty(codeReceived) == false)
                _codeReceived = codeReceived;


            var artists = new List<FullArtist>();


            var result = DownloadData<FollowedArtists>("https://api.spotify.com/v1/me/following?type=artist");



            
            //api.GetFollowedArtists(50,)
            return this.Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // GET: Spotify
        //http://jaykay/shoppingapi.lacage.be/api/spotifysearch?artist=sdff
        //public HttpResponseMessage Get(string searchString)
        //{
        //    var artists = new List<Artist>();


        //    artists.Add(new Artist("Eels"));

        //    var result = DownloadData<SearchItem>(string.Format("https://api.spotify.com/v1/search?q={0}&type=artist", artist));

        //    return this.Request.CreateResponse(HttpStatusCode.OK, result);
        //}

        //http://jaykay/shoppingapi.lacage.be/api/spotifysearch?artistIds=3zunDAtRDg7kflREzWAhxl&artistIds=6tbjWDEIzxoDsBA1FuhfPW
        public HttpResponseMessage Get([FromUri]string[] artistIds)
        {
            var newRelatedArtists = new List<SimpleArtist>();

            var allRelatedArtists = new List<string>();
            allRelatedArtists.AddRange(artistIds);
            
            foreach(var id in artistIds)
            {
                var relatedArtists = DownloadData<SeveralArtists>("https://api.spotify.com/v1/artists/" + id + "/related-artists");
                foreach (var artist in relatedArtists.Artists)
                {
                    if (allRelatedArtists.Contains(artist.Id) == false)
                        newRelatedArtists.Add(new SimpleArtist(){ Id = artist.Id, Name = artist.Name});
                }
            }

            return this.Request.CreateResponse(HttpStatusCode.OK, newRelatedArtists);
        }
    }
}