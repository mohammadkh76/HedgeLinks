adminModule.controller('InsertUserController', ['$scope', '$uibModalInstance', 'dataService', '$window', 'toaster', 'Upload', 'convertorService', function ($scope, $uibModalInstance, dataService, $window, toaster, Upload, convertorService) {
    console.log("user insert modal")
    $scope.changeUpload = function (file) {
        debugger;
        $scope.uploadLoading = true;
        convertorService.toBase64(file).then(function (res) {
            $scope.uploadPreview = res.base64;
            $scope.uploadLoading= false;
            $scope.$apply();
        });

    }
   
    $scope.upload = function (file) {
        Upload.upload({
            url: 'upload/url',
            data: { file: file, 'username': $scope.username }
        }).then(function (resp) {
            console.log('Success ' + resp.config.data.file.name + 'uploaded. Response: ' + resp.data);
        }, function (resp) {
            console.log('Error status: ' + resp.status);
        }, function (evt) {
            var progressPercentage = parseInt(100.0 * evt.loaded / evt.total);
            console.log('progress: ' + progressPercentage + '% ' + evt.config.data.file.name);
        });
    };
}])