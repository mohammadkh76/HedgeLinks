angModule.controller('addMiddleImageController',['$scope','dataService','convertorService','$window','Upload',function ($scope,dataService,convertorService,$window,Upload) {
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
            url: '/api/MiddleImageApi/AddMiddleImage/',

            data: {
                Text: $scope.data.Text,
                File: $scope.data.File
            },
            successCb(res) {
                window.location.href ="/MiddleImages/Index"
            },
            progressCb(evt) {
                $scope.progressPercentage = parseInt(100.0 * evt.loaded / evt.total);
                console.log('progress: ' + $scope.progressPercentage + '% ' + evt.config.data);
            },
            failureCb(res){
                $scope.message=res.data;

            }   
        }).catch(function (err) {
            $scope.errorMessage = err.data;
        })
    };

}]);