angModule.controller('addContactPageController', ['$scope', 'dataService','convertorService', 'Upload','$window', function ($scope, dataService,convertorService, Upload,$window) {
    $scope.changeUploadedPic = function () {
        if ($scope.data.File){
            $scope.showLoading=true;
            convertorService.toBase64($scope.data.File).then(function(res){
                $scope.uploadPreview=res.base64;
                $scope.showLoading=false;
                $scope.$apply();

            });

        }

    }
    $scope.progressPercentage=0;
    $scope.showLoading=0;
    $scope.submit = function (data) {
        dataService.upload({
            url: '/api/ContactApi/AddContact/',


            data: {
                WorkingHour: data.WorkingHour,
                Address: data.Address,
                Email: data.Email,
                PhoneNumber: data.Phone,
                Fax: data.Fax,
                Latitude: data.Latitude,
                Longitude: data.Longitude,
                File: data.File

            },
            successCb(res) {
                window.location.href ="/ContactUs/Index"
            },
            progressCb(evt) {
                $scope.progressPercentage = parseInt(100.0 * evt.loaded / evt.total);
                console.log('progress: ' + $scope.progressPercentage + '% ' + evt.config.data);
            }
        }).catch(function (err) {
            $scope.errorMessage = err.data;
        })
    };

}]);
