app.controller('SessionDetailController', ['$scope', '$location', 'passDataFactory', function($scope, $location, passDataFactory) {
	$scope.Attended = function() {
		passDataFactory.set($scope.session);
		$location.path('/feedback');
	};
}]);