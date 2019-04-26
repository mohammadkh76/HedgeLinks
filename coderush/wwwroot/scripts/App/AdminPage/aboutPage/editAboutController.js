angModule.controller('editAboutController',['$scope','dataService','convertorService','encDec','$window','Upload', function ($scope, dataService,convertorService, encDec,$window,Upload) {
    const urlPath = $window.location.pathname;
    var x = urlPath.split('/');
    dataService.get('/api/AboutApi/GetEditAbout/'+x[3]).then(function (res) {
        $scope.data=res.data.rec;
        $scope.uploadPreview=res.data.rec.FilePath;

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
            url: '/api/AboutApi/EditAbout/'+x[3],


            data: {
                Title: data.Title,
                Description: data.Description,
                File: data.File
            },
            successCb(res) {
                window.location.href = "/AboutUs/Index";
            },
            progressCb(evt) {
                $scope.progressPercentage = parseInt(100.0 * evt.loaded / evt.total);
                console.log('progress: ' + $scope.progressPercentage + '% ' + evt.config.data);
            }
        })

    };


  
}]);