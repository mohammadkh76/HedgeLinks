angModule.controller('editEmailController',['$scope','dataService','$uibModal','$window','Upload',function ($scope,dataService,$uibModal,$window,Upload) {
    const urlPath = $window.location.pathname;
    var x = urlPath.split('/');
    dataService.get('/api/EmailApi/GetEditEmail/'+x[3]).then(function (res) {
        $scope.data=res.data.rec;
    }).catch(function (err) {
        console.log(err.data)
    });
    $scope.submit = function (data) {

        Upload.upload({
                url: '/api/EmailApi/EditEmail/'+x[3],
                data: {
                    Name: data.Name,
                    CompanyName: data.CompanyName,
                    PhoneNumber: data.PhoneNumber,
                    EmailTo: data.EmailTo,
                },
                method:'post',
                encType:'multipart/form-data'
            }
        ).then(function (res) {
            $window.location.href="/Emails/Index";
        }).catch(function (err) {
            $scope.errorMessage=err.data;
        })

    };  
}]);