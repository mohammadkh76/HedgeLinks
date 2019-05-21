adminModule.controller('InsertUserController', ['$scope', '$uibModalInstance', 'dataService', '$window', 'toaster', 'Upload', 'convertorService', function ($scope, $uibModalInstance, dataService, $window, toaster, Upload, convertorService) {
    console.log("user insert modal");
    $scope.isCanceled = false;

    $scope.uploadPreview = '/images/upload-image.png';
    $scope.changeUpload = function (file) {
        $scope.uploadLoading = true;
        convertorService.toBase64(file).then(function (res) {
            $scope.uploadPreview = res.base64;
            $scope.uploadLoading = false;
            $scope.$apply();
        }).catch(function (err) {
            console.log(err);
        });
    }
    $scope.confirmInsert = function (res) {
        $scope.data = {
           
        }
        Upload.upload({
            url: '/api/User/Insert',
            data: {
                FirstName: $scope.insert.FirstName,
                LastName: $scope.insert.LastName,
                Password: $scope.insert.Password,
                ConfirmPassword: $scope.insert.ConfirmPassword,
                Email: $scope.insert.Email,
                File: $scope.File
            }
        }).then(function (resp) {
            if (resp.data.Status == "success") {
                $uibModalInstance.close();

            }
        }, function (resp) {

            console.log('Error status: ' + resp);
        }, function (evt) {

            var progressPercentage = parseInt(100.0 * evt.loaded / evt.total);
            console.log('progress: ' + progressPercentage + '% ' + evt.config.data.File.name);
        });

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

}])