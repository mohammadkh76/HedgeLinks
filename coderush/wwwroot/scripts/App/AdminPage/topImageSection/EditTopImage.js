angModule.controller('editTopImageController',['$scope','dataService','convertorService','encDec','$window','Upload', function ($scope, dataService,convertorService, encDec,$window,Upload) {
    const urlPath = $window.location.pathname;
    var x = urlPath.split('/');
    dataService.get('/api/TopImageApi/GetEditTopImage/'+x[3]).then(function (res) {
        $scope.data=res.data.rec;
        $scope.uploadPreview=res.data.rec.ImagePath;

    }).catch(function (err) {
        console.log(err.data)
    });
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
            url: '/api/TopImageApi/EditTopImage/'+x[3],
            data: {
                Text: data.Text,
                ButtonName: data.ButtonName,
                File: data.File
            },
            successCb(res) {
                window.location.href="/TopImages/Index";
            },
            progressCb(evt) {
                $scope.progressPercentage = parseInt(100.0 * evt.loaded / evt.total);
                console.log('progress: ' + $scope.progressPercentage + '% ' + evt.config.data);
            },
            failureCb(res){
                $scope.message=res.data;

            }
        })

    };

   

}]);