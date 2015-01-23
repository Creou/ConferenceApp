
app.factory('sessionService', ['$resource',
	function ($resource) {
		var sessions = $resource('/api/sessions', {}, {
			query: { method: 'GET', isArray: true, cache: true }
		});
		return sessions;
	}
]);

app.factory('feedbackGetService', ['$resource',
	function ($resource) {
		var feedback = $resource('/api/Sessions/:sessionId/FeedbackReports/:userId', {}, {
			get: { method: 'GET', isArray: false, cache: false }
		});
		return feedback;
	}
]);

app.factory('feedbackPostService', ['$resource',
	function ($resource) {
		var feedback = $resource('/api/FeedbackReports');
		return feedback;
	}
]);

app.factory('eventFeedbackService', ['$resource',
	function ($resource) {
		var feedback = $resource('/api/EventFeedbackReports/:userId');
		return feedback;
	}
]);

app.factory('companySizeService', ['$resource',
	function ($resource) {
		var companies = $resource('/api/DropDownOptionValues/CompanySize', {}, {
			query: { method: 'GET', isArray: true, cache: true }
		});
		return companies;
	}
]);

app.factory('statiiService', ['$resource',
	function ($resource) {
		var statii = $resource('/api/DropDownOptionValues/Status', {}, {
			query: { method: 'GET', isArray: true, cache: true }
		});
		return statii;
	}
]);


app.factory('localStorageFactory', ['$q', '$timeout', function ($q, $timeout) {
	var inMemory = [];
	var getVal = function (name, defaultValFunc) {
		var deferred = $q.defer();

		$timeout(function () {
			if (inMemory[name]) {
				deferred.resolve(inMemory[name]);
				return;
			}
			localforage.getItem(name).then(function (value) {
				if (value) {
					inMemory[name] = value;
					deferred.resolve(value);
				} else {
					if (defaultValFunc) {
						value = defaultValFunc();
					}
					if (value) {
						inMemory[name] = value;
						localforage.setItem(name, value);
						deferred.resolve(value);
					} else {
						deferred.reject();
					}
				}
			}, function () {
				if (defaultValFunc) {
					var value = defaultValFunc();
					deferred.resolve(value);
				} else {
					deferred.reject();
				}
			});
		});

		return deferred.promise;
	}
	var setVal = function (name, value) {
		inMemory[name] = value;
		localforage.setItem(name, value);
	}

	return { getVal: getVal, setVal: setVal };
}]);


app.factory('localUserIdFactory', ['localStorageFactory', function (localStorageFactory) {
	var getId = function () {
		return localStorageFactory.getVal('userId', getNewId);
	}
	return { getId: getId };

}]);


app.factory('passDataFactory', function() {
	var savedData = null;

	function set(data) {
		savedData = data;
	}

	function get() {
		return savedData;
	}

	return { set: set, get: get };
});

function getNewId() {
	return uuid();
}