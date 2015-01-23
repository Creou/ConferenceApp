app.controller('FeedbackController', ['$scope', 'localUserIdFactory', 'localStorageFactory', 'passDataFactory', 'feedbackGetService', 'feedbackPostService', '$window',
function ($scope, localUserIdFactory, localStorageFactory, passDataFactory, feedbackGetService, feedbackPostService, $window) {
	$scope.formDisable = true;
	$scope.buttonText = 'Submit';
	//localforage.clear();

	localStorageFactory.getVal('userName').then(function (value) {
		$scope.userName = value;
	});

	localUserIdFactory.getId().then(function (value) {
		$scope.userId = value;
	}).then(function () {
		feedbackGetService.get({ sessionId: $scope.session.Id, userId: $scope.userId }, function (feedback) {
			if (feedback) {
				$scope.ratePresentation = feedback.RatePresentation;
				$scope.rateContent = feedback.RateContent;
				$scope.rateDelivery = feedback.RateDelivery;
				$scope.rateSlides = feedback.RateSlides;
				$scope.rateDemos = feedback.RateDemos;
				$scope.rating = feedback.Rating;
				$scope.commentLiked = feedback.LikeComments;
				$scope.commentDisliked = feedback.DislikeComments;
				$scope.commentsGeneral = feedback.GeneralComments;
			}
			$scope.formDisable = false;
		}, function(err) {
			$scope.formDisable = false;
		});
	});

	$scope.$watch('userName', function (newVal, oldVal) {
		if (newVal !== oldVal) {
			localStorageFactory.setVal('userName', newVal);
		}
	});


	$scope.session = passDataFactory.get();

	if (!$scope.session) {
		$scope.formDisable = true;
		$scope.session = {
			Title: 'Navigate here via a session to add your feedback',
			Speaker: { Name: "" },
			Room: { RoomName: "" }
		}
	}

	$scope.submit = function () {
		$scope.formDisable = true;
		$scope.buttonText = 'Please wait...';
		feedbackPostService.save(
		{
			ClientId: $scope.userId,
			UserName: $scope.userName,
			SessionId: $scope.session.Id,
			RatePresentation: $scope.ratePresentation,
			RateContent: $scope.rateContent,
			RateDelivery: $scope.rateDelivery,
			RateSlides: $scope.rateSlides,
			RateDemos: $scope.rateDemos,
			LikeComments: $scope.commentLiked,
			DislikeComments: $scope.commentDisliked,
			GeneralComments: $scope.commentsGeneral
		}, function () {
			alert('Feedback received');
			$window.history.back();
		}, function () {
			$scope.formDisable = false;
			$scope.buttonText = 'Error';
		});
	}

	$scope.back = function () {
		$window.history.back();
	}

}]);