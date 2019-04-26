angModule.controller('addEmailController', ['$scope', 'dataService', 'Upload','$window', function ($scope, dataService, Upload,$window) {
    $scope.submit = function (data) {
       dataService.post('/api/EmailApi/AddEmail/',data).then(function (res) {
           $window.location.href="/Emails/Index";
       })
    }
    $scope.submitExcel = function (upd) {
        $scope.disabledExcel=true;
            Upload.upload({
            url: '/api/EmailApi/UploadExcelFile/',
            data: {
                File: upd.File[0]
            },
            method: 'post',
            encType: 'multipart/form-data'
            
        }).then(function (res) {
                $scope.disabledExcel=false;
$window.location.href="/emails/index";
            }).catch(function (res) {
                $scope.excelError=res.data;
            })
        
    }

}]);
