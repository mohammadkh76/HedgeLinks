angModule.controller('addSellRequestController',['$scope','dataService','$window','Upload','vcRecaptchaService',function ($scope,dataService,$window,Upload,vcRecaptchaService) {
    $scope.data={};
   
    $scope.setWidgetId=function(widgetId){
        $scope.widgetId=widgetId;
    }

    $scope.cbExpiration=function(){
        $scope.recaptchaResponse=null;
        
    }
    $scope.setResponse=function(response){

        $scope.recaptchaResponse=response;
        Upload.upload({
                url: '/api/SellRequestContactApi/AddSellRequestContact/',
                data:{  Address: $scope.data.Address,
                    PhoneNumber: $scope.data.PhoneNumber,
                    Email: $scope.data.Email,
                    RecaptchaRes:$scope.recaptchaResponse,
                  },
                method: 'post',
                encType: 'multipart/form-data'
            }
        ).then(function (res) {
            $window.location.href = "/Home/Index";
        }).catch(function (err) {
            $scope.errorMessage = err.data;
        })
    }
    
    $scope.submit = function (data) {
        $scope.data=data;
        vcRecaptchaService.execute($scope.widgetId);
    };

    
}]);