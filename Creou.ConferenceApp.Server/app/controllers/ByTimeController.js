app.controller('ByTimeController', ['$scope', 'sessionService', 'localStorageFactory',
	function ($scope, sessionService, localStorageFactory) {
		$scope.loading = true;
		$scope.sessions = sessionService.query(
			function loadData(data) {
				$scope.loading = false;
				var startTimes = _.map(
					_.uniq(
						_.map(
							$scope.sessions,
							function (session) {
								return session.Start;
							})
					),
					function (time) {
						return {
							time: time,
							sessions: _.sortBy(_.where($scope.sessions, { Start: time }), 'Track.Name')
						};
					}
				);
				localStorageFactory.getVal('time').then(function (value) {
					setActiveItemByTime($scope.startTimes, value);
				}, function () {
					$scope.text = $scope.startTimes[0].time;
				});

				$scope.$watch('text', function (newVal, oldVal) {
					if (newVal !== oldVal) {
						localStorageFactory.setVal('time', $scope.text);
					}
				});

				$scope.startTimes = _.sortBy(startTimes, 'time');

				$scope.prev = function () {
					updateActiveItem($scope.startTimes, function (newi) { return --newi; });
				}

				$scope.next = function () {
					updateActiveItem($scope.startTimes, function (newi) { return ++newi; });
				}
			}
			, function (err) {
				$scope.loading = false;
				$scope.failed = true;
			}
		);

		function findActiveItem(items) {
			for (var i = 0; i < items.length; i++) {
				if (items[i].active) {
					return i;
				}
			}
			return 0;
		}

		function setActiveItemByTime(items, time) {
			for (var i = 0; i < items.length; i++) {
				if (items[i].time === time) {
					items[i].active = true;
					$scope.text = items[i].time;
				}
			}
		}

		function updateActiveItem(items, action) {
			var newi = findActiveItem(items);
			newi = action(newi);
			if (newi < 0) {
				newi = 0;
			}
			if (newi === items.length) {
				newi = items.length - 1;
			}
			items[newi].active = true;
			$scope.text = items[newi].time;

		}
	}
]);