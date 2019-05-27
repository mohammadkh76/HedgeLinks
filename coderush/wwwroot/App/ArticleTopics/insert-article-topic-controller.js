adminModule.controller('insertArticleTopicController', ['$scope', '$uibModalInstance', 'dataService', '$window', 'toaster', function ($scope, $uibModalInstance, dataService, $window, toaster) {
    $scope.page = {
        Current: $scope.currentPage,
        ItemInPage: 10
    }

    $scope.isCanceled = false;

    $scope.confirmInsert = function () {
        $scope.toSendData = {
            Title: $scope.Title,
            Description: $scope.Description,
        }
        dataService.post($scope.url, $scope.toSendData).then(function (res) {
            if (res.data.Status == "success") {

                //window.location.href = $scope.href;
                $uibModalInstance.close();



            }
        })
    }

    $uibModalInstance.result.then(function () {
        if (!$scope.isCanceled) {

            $scope.tableLoading = true;
            $scope.getAll({
                successFunc() {
                    $scope.tableLoading = false;
                },
                messages() {
                    toaster.pop('success', 'Success', 'Your Record inserted successfully');

                }
            });
        }

    });

    $scope.cancel = function () {
        $scope.isCanceled = true;
        $uibModalInstance.close();

    }

}]);