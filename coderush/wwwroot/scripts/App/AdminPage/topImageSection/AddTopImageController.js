angModule.controller('addTopImageController',['$scope','dataService','convertorService','$window','Upload',function ($scope,dataService,convertorService,$window,Upload) {
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
        $scope.data=data;
        dataService.upload({
            url: '/api/TopImageApi/AddTopImage/',
            data:{  Text: $scope.data.Text,
                ButtonName: $scope.data.ButtonName,
                File: $scope.data.File}
                ,
            successCb(res) {
                $window.location.href = "/TopImages/Index";
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
        });
    };


}]);