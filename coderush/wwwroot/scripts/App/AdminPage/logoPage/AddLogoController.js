angModule.controller('addLogoController', ['$scope', 'dataService','convertorService', '$window','toaster', function ($scope, dataService, convertorService,$window,toaster) {
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
                    url: '/api/LogoApi/AddLogo/',
                    data: {
                        Name: data.Name,
                        File: data.File
                    },
                    successCb(res) {
                        window.location.href = '/Logoes/index'
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


      

   
}
]);