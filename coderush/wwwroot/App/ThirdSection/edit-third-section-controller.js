﻿adminModule.controller('editThirdSectionController', ['$scope', '$uibModalInstance', 'dataService', '$window', 'toaster', 'convertorService', 'Upload', function ($scope, $uibModalInstance, dataService, $window, toaster, convertorService, Upload) {
    console.log("in edit modal conroller")
    $scope.editDataLoading = true;

    $scope.ImagePath = '/images/upload-image.png';
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
            $scope.Title = res.data.Data.Title;
            $scope.Subtitle = res.data.Data.Subtitle;
            $scope.Keyword = res.data.Data.Keyword;
            $scope.ImagePath = res.data.Data.FilePath;
            $scope.OldImagePath = res.data.Data.FilePath;
            $scope.editDataLoading = false;

        }


    })
    $scope.confirmEdit = function () {
        $scope.editData = {
            Id: $scope.selectedId,
            Title: $scope.Title,
            Subtitle: $scope.Subtitle,
            Keyword: $scope.Keyword,
            ImagePath: $scope.ImagePath,
            OldImagePath: $scope.OldImagePath,
            File: $scope.File,
        }
        //dataService.post('/api/ThirdSection/Edit/', $scope.editData).then(function (res) {
        //    if (res.data.Status == 'success') {
        //        $scope.cancel();

        //    }

        //})
        Upload.upload({
            url: '/api/ThirdSection/Edit',
            data: {
                Id: $scope.selectedId,
                Title: $scope.Title,
                Subtitle: $scope.Subtitle,
                Keyword: $scope.Keyword,
                ImagePath: $scope.ImagePath,
                OldImagePath: $scope.OldImagePath,
                File: $scope.File,
            }
        }).then(function (resp) {
            if (resp.data.Status == "Success") {
                debugger;
                $scope.userInsertLoading = false;

                $uibModalInstance.close();

            }
            }, function (err) {
                debugger;
            toaster.pop("error", "Error", err.data.Messages);
            $scope.userInsertLoading = false;

            console.log('Error status: ' + err);
            }, function (evt) {
           

            var progressPercentage = parseInt(100.0 * evt.loaded / evt.total);
            $scope.progress = progressPercentage;
            $scope.userInsertLoading = true;

        });

    }

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

    
}])