adminModule.controller('InsertUserController', ['$scope', '$uibModalInstance', 'dataService', '$window', 'toaster', 'Upload', function ($scope, $uibModalInstance, dataService, $window, toaster, Upload) {
    console.log("user insert modal")
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