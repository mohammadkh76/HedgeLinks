adminModule.controller('InsertMenupathController', ['$scope', '$uibModalInstance', 'dataService', '$window', function ($scope, $uibModalInstance, dataService, $window) {
    console.log('hello insert modal!')

    $scope.confirmInsert = function (insert) {
        debugger;
        dataService.post($scope.url, insert).then(function () {
            if (res.data.status == "success") {

                dataService.get('/api/Menupath/GetAll').then(function (res) {
                    $scope.data = res.data.data
                    $uibModalInstance.close();

                })

            }
        })
    }
    $scope.cancel = function () {
        $uibModalInstance.close();

    }

}]);