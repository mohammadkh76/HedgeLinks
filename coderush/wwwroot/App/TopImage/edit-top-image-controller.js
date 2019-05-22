adminModule.controller('editTopImageController', ['$scope', '$uibModalInstance', 'dataService', '$window', 'toaster', function ($scope, $uibModalInstance, dataService, $window, toaster) {
    console.log("in edit modal conroller")
    $scope.editDataLoading = true;
    


    
    dataService.get($scope.url).then(function (res) {
        debugger;
        if (res.data.Status == "success") {
            $scope.ImageTitle= res.data.Data.ImageTitle;
            $scope.ImageSubtitle = res.data.Data.ImageSubtitle;
            $scope.Keyword = res.data.Data.Keyword;
            $scope.ImagePath = res.data.Data.FilePath;
            $scope.editDataLoading = false;

        }


    })
    $scope.confirmEdit = function () {
        $scope.editData = {
            Id: $scope.selectedId,
            ImageTitle: $scope.ImageTitle,
            ImageSubtitle: $scope.ImageSubtitle,
            Keyword: $scope.Keyword,
            FilePath: $scope.ImagePath,
            File: $scope.File,
        }
        dataService.post('/api/TopImage/Edit/', $scope.editData).then(function (res) {
            if (res.data.Status == 'success') {
                $scope.cancel();

            }

        })
        $uibModalInstance.result.then(function () {
            $scope.tableLoading = true;
            $scope.getAll({
                successFunc() {
                    $scope.tableLoading = false;
                },
                messages() {
                    toaster.pop('success', 'Success', 'Your Record edited successfully');

                }
            });

        });
        $scope.cancel = function () {
            $uibModalInstance.close();

        }

    }
}])