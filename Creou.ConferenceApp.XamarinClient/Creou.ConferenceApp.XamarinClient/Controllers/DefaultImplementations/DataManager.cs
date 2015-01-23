using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Creou.ConferenceApp.XamarinClient.Models;
using Creou.ConferenceApp.XamarinClient.ViewModels;
using Creou.ConferenceApp.XamarinClient.ViewModels.DefaultImplementations;
using Newtonsoft.Json;
using PCLStorage;

namespace Creou.ConferenceApp.XamarinClient.Controllers.DefaultImplementations
{
	public class DataManager : IDataManager
	{
		public List<Session> Sessions { get; private set; }

		public List<FeedbackReport> FeedbackReports { get; private set; }

		public EventFeedbackReport EventFeedbackReport { get; set; }

		public Enums.eDataLoadState SessionDataLoadState { get; private set; }

		public bool FeedbackDataLoaded { get; private set; }

		public Guid AppInstanceId { get; private set; }

		public DropDownOptionsGroup DropDownOptions { get; private set; }

		private readonly Func<ISessionViewModel> _sessionViewModelFactory;

		private readonly Func<ITrackViewModel> _trackViewModelFactory;

		public DataManager(
			Func<ISessionViewModel> sessionViewModelFactory,
			Func<ITrackViewModel> trackViewModelFactory)
		{
			_sessionViewModelFactory = sessionViewModelFactory;
			_trackViewModelFactory = trackViewModelFactory;
		}

		public async Task InitialDataLoad()
		{
			SessionDataLoadState = Enums.eDataLoadState.NoDataLoaded;

			bool dataLoadedFromLocalCache = await LoadSessionsFromCacheAsync();

			bool dataLoadedFromServer = await LoadSessionsFromServerAsync();

			if (dataLoadedFromServer)
			{
				SessionDataLoadState = Enums.eDataLoadState.DataLoadedFromServer;
			}
			else if (dataLoadedFromLocalCache)
			{
				SessionDataLoadState = Enums.eDataLoadState.DataLoadedFromLocalCache;
			}

			FeedbackDataLoaded = await LoadFeedbackFromServerAsync();
		}

		public async Task<IEnumerable<ITrackViewModel>> GetAvailableTracksAsync()
		{
			var distinctTrackIds = Sessions.Select(session => session.Track.Id).Distinct();

			var tracks = distinctTrackIds.Select(trackId =>
			{
				var trackViewModel = _trackViewModelFactory();
				trackViewModel.Id = trackId;
				trackViewModel.Name = Sessions.Select(session => session.Track).First(track => track.Id == trackId).Name;
				return trackViewModel;
			});	
			
			return tracks.OrderBy(track => track.Name);
		}

		public async Task<IEnumerable<ISessionViewModel>> GetSessionsByTrackAsync(ITrackViewModel track)
		{
			return Sessions.Where(session =>
				session.Track.Id == track.Id).Select(ConvertSessionToViewModel).OrderBy(session =>
					session.Session.Start).ThenBy(session =>
						session.Session.Room.RoomName);
		}

		public async Task<IEnumerable<DateTime>> GetAvailableSlotTimesAsync()
		{
			var sortedTimes = Sessions.Select(session => session.Start).Distinct().OrderBy(time => time.TimeOfDay);

			return sortedTimes;
		}

		public async Task<IEnumerable<Grouping<ISessionViewModel>>> GetSessionsByTimeAsync(DateTime time)
		{
			var allTracks = Sessions.Select(session => session.Track).ToList();

			var sessionsAtThisTime = Sessions.Where(session => session.Start == time).ToList();

			var distinctTrackIds = sessionsAtThisTime.Select(session => session.Track.Id).Distinct().OrderBy(id => id).ToList();

			var groupedSessions = new ObservableCollection<Grouping<ISessionViewModel>>();

			foreach (var trackId in distinctTrackIds)
			{
				int id = trackId;

				groupedSessions.Add(new Grouping<ISessionViewModel>(
					allTracks.First(track => track.Id == id).Name,
					sessionsAtThisTime.Where(session =>
						session.Track.Id == id).Select(ConvertSessionToViewModel).ToList().OrderBy(session =>
							session.Session.Room.RoomName)));
			};

			return groupedSessions.OrderBy(sessions => sessions.Key);
		}

		public async void GenerateAndStoreAppInstanceIdIfNotExist()
		{
			var existingId = await GetStoredStringValueAsync(Constants.AppInstanceIdFilename);

			if (string.IsNullOrWhiteSpace(existingId))
			{
				AppInstanceId = Guid.NewGuid();
				SaveStringValueAsync(AppInstanceId.ToString(), Constants.AppInstanceIdFilename);
			}
			else
			{
				AppInstanceId = new Guid(existingId);
			}
		}

		public async void SaveStringValueAsync(string value, string filename)
		{
			IFolder dataFolder = FileSystem.Current.LocalStorage;
			IFile file = await dataFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
			await file.WriteAllTextAsync(value);
		}

		public async Task<string> GetStoredStringValueAsync(string filename)
		{
			IFolder dataFolder = FileSystem.Current.LocalStorage;
			var fileExists = await dataFolder.CheckExistsAsync(filename);

			if (fileExists != ExistenceCheckResult.FileExists)
			{
				return string.Empty;
			}
			else
			{
				IFile file = await dataFolder.GetFileAsync(filename);

				var dataString = await file.ReadAllTextAsync();

				return dataString;
			}
		}

		public async Task<bool> PostFeedbackAsync(FeedbackReportSubmission feedback)
		{
			using (var client = new HttpClient())
			{
				SetupHttpClient(client);

				HttpResponseMessage response = await client.PostAsJsonAsync("api/FeedbackReports", feedback);

				if (!response.IsSuccessStatusCode)
				{
					return false;
				}
				else
				{
					return true;
				}
			}
		}

		public async Task<bool> PostEventFeedbackAsync(EventFeedbackReportSubmission eventFeedback)
		{
			using (var client = new HttpClient())
			{
				SetupHttpClient(client);

				HttpResponseMessage response = await client.PostAsJsonAsync("api/EventFeedbackReports", eventFeedback);

				if (!response.IsSuccessStatusCode)
				{
					return false;
				}
				else
				{
					return true;
				}
			}
		}

		private async Task<bool> LoadSessionsFromCacheAsync()
		{
			IFolder dataFolder = FileSystem.Current.LocalStorage;
			var cacheFileExists = await dataFolder.CheckExistsAsync(Constants.SessionCacheFilename);

			if (cacheFileExists != ExistenceCheckResult.FileExists)
			{
				return false;
			}
			else
			{
				IFile file = await dataFolder.GetFileAsync(Constants.SessionCacheFilename);

				var dataString = await file.ReadAllTextAsync();

				Sessions = JsonConvert.DeserializeObject<List<Session>>(dataString);

				return true;	
			}
		}

		private async Task<bool> LoadSessionsFromServerAsync()
		{
			using (var client = new HttpClient())
			{
				SetupHttpClient(client);

				HttpResponseMessage response = await client.GetAsync("api/sessions");

				if (!response.IsSuccessStatusCode)
				{
					return false;
				}
				else
				{
					var dataString = await response.Content.ReadAsStringAsync();

					IFolder dataFolder = FileSystem.Current.LocalStorage;
					IFile file = await dataFolder.CreateFileAsync(Constants.SessionCacheFilename, CreationCollisionOption.ReplaceExisting);
					await file.WriteAllTextAsync(dataString);

					Sessions = JsonConvert.DeserializeObject<List<Session>>(dataString);
					return true;
				}
			}
		}

		private async Task<bool> LoadFeedbackFromServerAsync()
		{
			try
			{
				using (var client = new HttpClient())
				{
					SetupHttpClient(client);

					var urlExtension = "api/" + AppInstanceId.ToString() + "/FeedbackReports";
					HttpResponseMessage response = await client.GetAsync(urlExtension);
					var dataString = await response.Content.ReadAsStringAsync();
					FeedbackReports = JsonConvert.DeserializeObject<List<FeedbackReport>>(dataString) ?? new List<FeedbackReport>();

					urlExtension = "api/EventFeedbackReports/" + AppInstanceId.ToString();
					response = await client.GetAsync(urlExtension);
					dataString = await response.Content.ReadAsStringAsync();
					EventFeedbackReport = JsonConvert.DeserializeObject<EventFeedbackReport>(dataString);

					DropDownOptions = new DropDownOptionsGroup();

					urlExtension = "api/DropDownOptionValues/CompanySize";
					response = await client.GetAsync(urlExtension);
					dataString = await response.Content.ReadAsStringAsync();
					DropDownOptions.CompanySizeDropDownOptions = JsonConvert.DeserializeObject<List<DropDownOptionValue>>(dataString);

					return true;
				}
			}
			catch
			{
				return false;
			}
		}

		private void SetupHttpClient(HttpClient client)
		{
			client.BaseAddress = new Uri("http://localhost");
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		}

		private async void CacheData(string dataString, string cacheFilename)
		{
			IFolder dataFolder = FileSystem.Current.LocalStorage;
			IFile file = await dataFolder.CreateFileAsync(cacheFilename, CreationCollisionOption.ReplaceExisting);
			await file.WriteAllTextAsync(dataString);
		}

		private ISessionViewModel ConvertSessionToViewModel(Session session)
		{
			var converted = _sessionViewModelFactory();

			converted.Session = session;
			converted.SessionTitleUpper = session.Title.ToUpper();

			converted.TimesString = String.Format("{0} - {1}", timeStringDisplayFormat(session.Start),
				timeStringDisplayFormat(session.End));
	
			converted.TimesRoomAndTrackString = String.Format("{0} - {1}, {2}", timeStringDisplayFormat(session.Start),
				timeStringDisplayFormat(session.End), session.Room.RoomName);

			const int titleLength = 36;

			if (converted.SessionTitleUpper.Length > titleLength)
			{
				converted.SessionTitleShort = session.Title.Substring(0, titleLength) + "...";
			}
			else
			{
				converted.SessionTitleShort = session.Title;
			}

			return converted;
		}

		private string timeStringDisplayFormat(DateTime input)
		{
			return input.ToString("h:mm tt");
		}
	}
}
