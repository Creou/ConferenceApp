var app = angular.module('app', ['ngRoute', 'ui.bootstrap', 'ngResource', 'appControllers', 'ui.bootstrap.carousel', 'ui.bootstrap.modal']);

app.config(function ($routeProvider, $locationProvider) {
	$routeProvider.when('/home', {
		templateUrl: 'partials/home.html',
		controller: function($scope, sessionService) {
			$scope.sessions = sessionService.query(function() {});
		}
	});

	$routeProvider.when('/byTrack', {
		templateUrl: 'partials/byTrack.html',
		controller: 'ByTrackController'
	});

	$routeProvider.when('/byTime', {
		templateUrl: 'partials/byTime.html',
		controller: 'ByTimeController'
	});

	$routeProvider.when('/all', {
		templateUrl: 'partials/all.html',
		controller: 'AllController'
	});

	$routeProvider.when('/feedback', {
		templateUrl: 'partials/feedback.html',
		controller: 'FeedbackController'
	});

	$routeProvider.when('/eventFeedback', {
		templateUrl: 'partials/eventFeedback.html',
		controller: 'EventFeedbackController'
	});

	$routeProvider.otherwise({ redirectTo: '/home' });

	//$locationProvider.html5Mode(true);
});

app.filter('unique', function () {

	return function (arr, field) {
		var o = {}, i, l = arr.length, r = [];
		for (i = 0; i < l; i += 1) {
			o[arr[i][field]] = arr[i];
		}
		for (i in o) {
			r.push(o[i]);
		}
		return r;
	};
});

app.filter('timeSpanDate', function ($filter) {

	function isDate(value) {
		return toString.call(value) === '[object Date]';
	}

	function isValidDate(value) {
		// Invalid Date: getTime() returns NaN
		return value && !(value.getTime && value.getTime() !== value.getTime());
	}
	var angularDateFilter = $filter('date');

	return function (theDate, args) {
		if (theDate && isDate(theDate) && isValidDate(theDate)) {
			return angularDateFilter(theDate, args);
		}

		//convert timespan to date here then return the angular filter on that.
	};
});

app.directive('sessionDetail', function () {
	return {
		templateUrl: 'partials/sessionDetail.html',
		restrict: 'E',
		replace: true,
		scope: {
			session: '=data'
		},
		controller: 'SessionDetailController'
	};
});

app.filter("nonBlankFilter", function ($filter) {
	return function (array, params) {
		if (params) {
			return $filter('filter')(array, params);
		} else {
			return [];
		}
	}
});

app.go = function (path) {
	$location.path(path);
};

app.directive('navActiveLink', ['$location', function ($location) {
	return {
		restrict: 'A', //use as attribute 
		replace: false,
		link: function (scope, elem) {
			//after the route has changed
			scope.$on("$routeChangeSuccess", function () {
				var selectors = ['li > [href="#' + $location.path() + '"]',
								 'li > [href="/#' + $location.path() + '"]',
								 'li > [href="/' + $location.path() + '"]', //html5: false
								 'li > [href="' + $location.path() + '"]']; //html5: true
				angular.forEach(elem.find('a'), function (a) {
					a = angular.element(a);
					if (-1 !== window.location.href.indexOf(a.attr('href'))) {
						a.parent().addClass('active');
					} else {
						a.parent().removeClass('active');
					};
				});
			});
		}
	}
}]);

app.directive('elastic', [
	'$timeout',
	function ($timeout) {
		return {
			restrict: 'A',
			link: function ($scope, element) {
				var resize = function () {
					var count = 0;
					while (element[0].style.height +"px" !== element[0].scrollHeight && count < 1000) {
						element[0].style.height = "" + element[0].scrollHeight + "px";
						count ++;
					}
					return element[0].style.height = "" + element[0].scrollHeight + "px";
				};
				element.on("blur keyup change input paste cut", resize);
				$timeout(resize, 0);
			}
		};
	}
]);