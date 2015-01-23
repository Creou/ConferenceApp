app.controller('ByTrackController', ['$scope', 'sessionService', 'localStorageFactory',
	function ($scope, sessionService, localStorageFactory) {
		$scope.dropdownMessage = "Please wait for sessions to load...";

		$scope.sessions = sessionService.query(function () {
			$scope.dropdownMessage = "Choose track...";
			var tracks = _.map(
				_.uniq(
					$scope.sessions,
					false,
					function (session) {
						return session.Track.Id;
					}),
				function (session) {
					return { Name: session.Track.Name, Id: session.Track.Id };
				}
			);

			var trackSessions = _.map(
				tracks,
				function (track) {
					return {
						Id: track.Id,
						Name: track.Name,
						sessions: _.sortBy(
							_.filter(
								$scope.sessions, function (session) {
									return session.Track.Id === track.Id;
								}),
								'Start'
						)
					};
				}
			);
			$scope.tracks = _.sortBy(tracks, 'Name');
			$scope.trackSessions = _.sortBy(trackSessions, 'Name');

			localStorageFactory.getVal('trackId').then(function (value) {
				$scope.chosenTrack = getTrackByName(value);
			});
			$scope.$watch('chosenTrack', function (newVal, oldVal) {
				if (newVal !== oldVal) {
					localStorageFactory.setVal('trackId', newVal.Name);
				}
			});

		}, function (error) {
			$scope.dropdownMessage = "Failed to load data from server.";
		}
	);
	function getTrackByName(name) {
		for (var i = 0; i < $scope.tracks.length; i++) {
			if ($scope.tracks[i].Name === name) {
				return $scope.tracks[i];
			}
		}

		return $scope.tracks[0];
	}
	}]);
