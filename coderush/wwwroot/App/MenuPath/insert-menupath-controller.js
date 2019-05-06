adminModule.controller('InsertMenupathController', ['$scope', '$uibModalInstance', 'dataService', '$window', function ($scope, $uibModalInstance, dataService, $window) {
    console.log('hello insert modal!')
    $scope.tinymceModel = 'Initial content';
    $scope.getContent = function () {
        console.log('Editor content:', $scope.tinymceModel);
    };

    $scope.setContent = function () {
        $scope.tinymceModel = 'Time: ' + (new Date());
    };

    $scope.tinymceOptions = {
        plugins: 'link image code',
        toolbar: 'undo redo | bold italic | alignleft aligncenter alignright | code'
    };
  $scope.confirmInsert = function () {
        $scope.toSendData = {
            Name: $scope.insert.Name,
            Description: $scope.insert.Description,
            PageName: $scope.insert.PageName

        }
        dataService.post($scope.url, $scope.toSendData).then(function (res) {
            debugger;
            if (res.data.Status == "success") {
                debugger
                dataService.get('/api/Menupath/GetAll').then(function (res) {
                    $scope.data = res.data.Data
                    $uibModalInstance.close();

                })

            }
        })
    }
    $scope.cancel = function () {
        $uibModalInstance.close();

    }

}]);