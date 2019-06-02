adminModule.controller('editTopImageController', ['$scope', '$uibModalInstance', 'dataService', '$window', 'toaster', 'Upload', 'convertorService', function ($scope, $uibModalInstance, dataService, $window, toaster, Upload, convertorService) {
    console.log("in edit modal conroller")
    $scope.editDataLoading = true;
    
    $scope.changeUpload = function (file) {
        $scope.uploadLoading = true;
        convertorService.toBase64(file).then(function (res) {
            $scope.ImagePath = res.base64;
            $scope.uploadLoading = false;
            $scope.$apply();
        }).catch(function (err) {
            console.log(err);
        });
    }

    
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
        Upload.upload({
            url: '/api/TopImage/Edit',
            data: {
                Id: $scope.selectedId,
                ImageTitle: $scope.ImageTitle,
                ImageSubtitle: $scope.ImageSubtitle,
                Keyword: $scope.Keyword,
                FilePath: $scope.ImagePath,
                File: $scope.File,
            }
        }).then(function (resp) {
            if (resp.data.Status == "Success") {
                $scope.userInsertLoading = false;

                $uibModalInstance.close();

            }
        }, function (err) {
            toaster.pop("error", "Error", err.data.Messages);
            $scope.userInsertLoading = false;

            console.log('Error status: ' + err);
        }, function (evt) {

            var progressPercentage = parseInt(100.0 * evt.loaded / evt.total);
            $scope.progress = progressPercentage;
            $scope.userInsertLoading = true;

        });

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