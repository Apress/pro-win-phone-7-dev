using GalaSoft.MvvmLight;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using System;

namespace WorkingWithMediaLibrary.ViewModel
{
  /// <summary>
  /// This class contains properties that the main View can data bind to.
  /// <para>
  /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
  /// </para>
  /// <para>
  /// You can also use Blend to data bind with the tool's support.
  /// </para>
  /// <para>
  /// See http://www.galasoft.ch/mvvm/getstarted
  /// </para>
  /// </summary>
  public class MainViewModel : ViewModelBase
  {
    private MediaLibrary _mediaLibrary;

    public string ApplicationTitle
    {
      get
      {
        return "my media";
      }
    }

    /// <summary>
    /// Initializes a new instance of the MainViewModel class.
    /// </summary>
    public MainViewModel()
    {
      _mediaLibrary = new MediaLibrary();
      if (IsInDesignMode)
      {
        CreateDesignTimeDataCollections();
      }
      else
      {
        CreateDataCollections();
      }
    }

    #region Design-time data
    private void CreateDesignTimeDataCollections()
    {
      Albums = new List<Album>();
      Album album = null;
      for (int i = 0; i < _mediaLibrary.Albums.Count; i++)
      {
        album = _mediaLibrary.Albums[i];
        Albums.Add(album);
        if (i > 10)
          break;
      }

      Artists = new List<Artist>();
      Artist artist = null;
      for (int i = 0; i < _mediaLibrary.Artists.Count; i++)
      {
        artist = _mediaLibrary.Artists[i];
        Artists.Add(artist);
        if (i > 10)
          break;
      }

      Songs = new List<Song>();
      Song song = null;
      for (int i = 0; i < _mediaLibrary.Songs.Count; i++)
      {
        song = _mediaLibrary.Songs[i];
        Songs.Add(song);
        if (i > 10)
          break;
      }

      Playlists = new List<Playlist>();
      Playlist playlist = null;
      for (int i = 0; i < _mediaLibrary.Playlists.Count; i++)
      {
        playlist = _mediaLibrary.Playlists[i];
        Playlists.Add(playlist);
        if (i > 10)
          break;
      }
    }
    #endregion

    #region run-time data
    private void CreateDataCollections()
    {
      Albums = new List<Album>();
      Album album = null;
      for (int i = 0; i < _mediaLibrary.Albums.Count;i++)
      {
        album = _mediaLibrary.Albums[i];
        Albums.Add(album);
      }

      Artists = new List<Artist>();
      Artist artist = null;
      for (int i = 0; i < _mediaLibrary.Artists.Count; i++)
      {
        artist = _mediaLibrary.Artists[i];
        Artists.Add(artist);
      }

      Songs = new List<Song>();
      Song song = null;
      for (int i = 0; i < _mediaLibrary.Songs.Count; i++)
      {
        song = _mediaLibrary.Songs[i];
        Songs.Add(song);
      }

      Playlists = new List<Playlist>();
      Playlist playlist = null;
      for (int i = 0; i < _mediaLibrary.Playlists.Count; i++)
      {
        playlist = _mediaLibrary.Playlists[i];
        Playlists.Add(playlist);
      }
    }
    #endregion

    #region Albums
    public const string AlbumsPropertyName = "Albums";
    private List<Album> _randomAlbums = null;
    public List<Album> Albums
    {
      get
      {
        return _randomAlbums;
      }

      set
      {
        if (_randomAlbums == value)
        {
          return;
        }

        var oldValue = _randomAlbums;
        _randomAlbums = value;

        // Update bindings, no broadcast
        RaisePropertyChanged(AlbumsPropertyName);
      }
    }
    #endregion

    #region Artist
    public const string ArtistsPropertyName = "Artists";
    private List<Artist> _artists = null;
    public List<Artist> Artists
    {
      get
      {
        return _artists;
      }

      set
      {
        if (_artists == value)
        {
          return;
        }

        var oldValue = _artists;
        _artists = value;
        RaisePropertyChanged(ArtistsPropertyName);
      }
    }
    #endregion

    #region Songs
    public const string SongsPropertyName = "Songs";
    private List<Song> _songs = null;
    public List<Song> Songs
    {
      get
      {
        return _songs;
      }

      set
      {
        if (_songs == value)
        {
          return;
        }

        var oldValue = _songs;
        _songs = value;
        RaisePropertyChanged(SongsPropertyName);
      }
    }
    #endregion

    #region Playlist
    public const string PlaylistsPropertyName = "Playlists";
    private List<Playlist> _playlist = null;
    public List<Playlist> Playlists
    {
      get
      {
        return _playlist;
      }

      set
      {
        if (_playlist == value)
        {
          return;
        }

        var oldValue = _playlist;
        _playlist = value;
        RaisePropertyChanged(PlaylistsPropertyName);
      }
    }
    #endregion

    ////public override void Cleanup()
    ////{
    ////    // Clean up if needed

    ////    base.Cleanup();
    ////}
  }
}