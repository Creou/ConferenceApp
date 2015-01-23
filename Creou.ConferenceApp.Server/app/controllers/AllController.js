var appControllers = angular.module('appControllers', []);

appControllers.controller('AllController', ['$scope', 'sessionService', '$timeout',
	function ($scope, sessionService, $timeout) {
		//$timeout(function() {
			 $scope.loading = true;
		//});
		
		$scope.sessions = sessionService.query(function(data) {
			$scope.loading = false;
		}, function(data) {
			$scope.loading = false;
		});
	}
]);