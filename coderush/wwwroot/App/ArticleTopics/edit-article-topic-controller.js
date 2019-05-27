adminModule.controller('editArticleTopicController', ['$scope', '$uibModalInstance', 'dataService', '$window', 'toaster', function ($scope, $uibModalInstance, dataService, $window, toaster) {
    console.log("in edit modal conroller")
    $scope.editDataLoading = true;
    $scope.page = {
        Current: $scope.currentPage,
        ItemInPage: 10
    }


   
          
    dataService.get($scope.url).then(function (res) {
        if (res.data.Status == "success") {
          debugger
            $scope.Description = res.data.Data.Description;
            $scope.Title = res.data.Data.Title;
            $scope.selectedId = res.data.Data.Id;
            $scope.editDataLoading = false;

        }


    })
    $scope.confirmEdit = function () {
        $scope.editData = {
            Id:$scope.selectedId,
            Title: $scope.Title,
            Description: $scope.Description,
        }
        dataService.post('/api/ArticleTopic/Edit/', $scope.editData).then(function (res) {
            if (res.data.Status == 'success') {
                $uibModalInstance.close();
            }

        })
        $uibModalInstance.result.then(function () {
            if (!$scope.isCanceled) {
                $scope.tableLoading = true;
                $scope.getAll({
                    successFunc() {
                        $scope.tableLoading = false;
                    },
                    messages() {
                        toaster.pop('success', 'Success', 'Your Record edited successfully');

                    }
                });

            }
           
        });
        $scope.cancel = function () {
            $scope.isCanceled = true;
            $uibModalInstance.close();

        }

    }
}])