adminModule.controller('descriptionMenupathModalController', ['$scope', '$uibModalInstance', 'dataService', '$window', function ($scope, $uibModalInstance, dataService, $window) {
    console.log("hello description Modal");
    $scope.descriptionLoading = true;
    dataService.get($scope.url).then(function (res) {
        let html = res.data.Data;
        $('.modal-body').append(html);
        $scope.descriptionLoading = false;

    })
    $scope.cancel = function () {
        $uibModalInstance.close();

    }
}])
