angModule.controller('addSecondMiddleImageController',['$scope','dataService','convertorService','$window','Upload',function ($scope,dataService,convertorService,$window,Upload) {
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
            url: '/api/SecondMiddlePageApi/AddSecondMiddleImage/',

            data: {
                
                FirstTitle: $scope.data.FirstTitle,
                SecondTitle: $scope.data.SecondTitle,
                ThirdTitle: $scope.data.ThirdTitle,
                ForthTitle: $scope.data.ForthTitle,
                FirstDescription: $scope.data.FirstDescription,
                SecondDescription: $scope.data.SecondDescription,
                ThirdDescription: $scope.data.ThirdDescription,
                ForthDescription: $scope.data.ForthDescription,
                File: $scope.data.File
            },
            successCb(res) {
                window.location.href ="/secondmiddlepages/Index"
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