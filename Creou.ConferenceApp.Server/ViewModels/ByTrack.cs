using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Creou.ConferenceApp.Server.Models;

namespace Creou.ConferenceApp.Server.ViewModels
{
	public class ByTrack
	{
		private readonly ICollection<Track> _tracks;

		public ByTrack()
		{
			
		}

		public ByTrack(ICollection<Track> tracks, ICollection<Session> sessions, int selectedTrackIdId)
		{
			_tracks = tracks;
			SessionsForTrack = sessions;
			SelectedTrackId = selectedTrackIdId;
		}

		[Display(Name = "Select Track")]
		public int SelectedTrackId { get; set; }

		public IEnumerable<SelectListItem> Tracks
		{
			get
			{
				var tracks = _tracks.Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Name });
				return DefaultTrack.Concat(tracks);
			}
		}

		public IEnumerable<SelectListItem> DefaultTrack
		{
			get
			{
				return new[] { new SelectListItem { Value = "-1", Text = "Select track..." } };
			}
		}

		public ICollection<Session> SessionsForTrack { get; set; }
	}
}