app.controller('EventFeedbackController', ['$scope', 'localUserIdFactory', 'localStorageFactory', 'passDataFactory', 'eventFeedbackService', '$window', 'statiiService', 'companySizeService',
function ($scope, localUserIdFactory, localStorageFactory, passDataFactory, eventFeedbackService, $window, statiiService, companySizeService) {
	$scope.formDisable = true;
	$scope.buttonText = 'Submit';
	$scope.statii = statiiService.query(function (value) {
		var x = value;
	});
	$scope.sizes = companySizeService.query(function (value) {
		var y = value;
	});
	//localforage.clear();

	localStorageFactory.getVal('userName').then(function (username) {
		$scope.userName = username;
	});

	localUserIdFactory.getId().then(function (value) {
		$scope.userId = value;
	}).then(function () {
		eventFeedbackService.get({ userId: $scope.userId }, function (feedback) {
			$scope.feedback = feedback;
			if (!$scope.userName || $scope.userName === '') {
				$scope.userName = $scope.feedback.Attendee.Name;
			}
			$scope.formDisable = false;
		}, function() {
			$scope.formDisable = false;
		});
	});

	$scope.$watch('userName', function (newVal, oldVal) {
		if (newVal !== oldVal) {
			localStorageFactory.setVal('userName', newVal);
		}
	});



	$scope.submit = function () {
		$scope.formDisable = true;
		$scope.buttonText = 'Please wait...';
		$scope.feedback.ClientId = $scope.userId;
		$scope.feedback.UserName = $scope.userName;
		//$scope.feedback.CompanySize = $scope.size;
		//$scope.feedback.status = $scope.status;
		eventFeedbackService.save($scope.feedback, function () {
			alert('Feedback received');
			$window.history.back();
		}, function (err) {
			$scope.formDisable = false;
			$scope.buttonText = 'Error';
			$scope.errorMessage = err;
		});
	}

	$scope.back = function () {
		$window.history.back();
	}

}]);